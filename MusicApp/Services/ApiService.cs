using MusicApp.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;



namespace MusicApp.Services;

public static class ApiService
{
    // Registration
    public static async Task<bool> RegisterUser(string firstName, string lastName, string email, string phone, string password,
                                                    string street, string postal, string city, string state, string country)
    {
        var register = new Register()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            Password = password,
            Street = street,
            Postal = postal,
            City = city,
            State = state,
            Country = country
        };
        var httpClient = new HttpClient();
        var json = JsonConvert.SerializeObject(register);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(AppSettings.BaseUrl + "/register", content);
        if (!response.IsSuccessStatusCode) return false;
        return true;
    }
    // Login
    public static async Task<bool> LoginUser(string email, string password)
    {
        var login = new Login()
        {
            Email = email,
            Password = password
        };

        var httpClient = new HttpClient();
        var json = JsonConvert.SerializeObject(login);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(AppSettings.BaseUrl + "/login", content);

        if (!response.IsSuccessStatusCode) return false;

        var jsonResult = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Token>(jsonResult);

        Preferences.Set("accesstoken", result.AccessToken);

        return true;
    }


    public static async Task<string> GetUserProfile()
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
        var response = await httpClient.GetAsync(AppSettings.BaseUrl + "/identity/profile");
        if (!response.IsSuccessStatusCode) return null;
        var jsonResult = await response.Content.ReadAsStringAsync();

        var userProfile = JsonConvert.DeserializeObject<UserProfile>(jsonResult);
        string userId = userProfile?.UserId;
        Preferences.Set("id", userProfile.id);
        Preferences.Set("username", userProfile.UserName);
        return userId;
    }
    // get low products
    public static async Task<List<Product>> GetLowProductsAsync()
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(AppSettings.BaseUrl);
        var accessToken = Preferences.Get("accesstoken", string.Empty);
        if (string.IsNullOrEmpty(accessToken))
        {
            await Shell.Current.GoToAsync("login-page");
            return null;
        }
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await httpClient.GetAsync("/api/product/lowlevel");
        var responseString = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            if (responseString.Contains("message"))
            {
                return new List<Product>(); 
            }

            return JsonConvert.DeserializeObject<List<Product>>(responseString);
        }
        else
        {
            throw new Exception("Failed to fetch products");
        }
    }

    //1) get all items and allows to list that on the main sceen - on first load
    public static async Task<List<Category>> GetShowProductSubCategories()
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));

        var response = await httpClient.GetAsync(AppSettings.BaseUrl + "/categories");

        var jsonResult = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<Category>>(jsonResult);

    }
    // that endpoint is run when first category like Electric etc is selected - List of Categories returned from db

    public static async Task<List<SubCategory>> GetShowProductSubCategories(int categoryId)
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));

        var response = await httpClient.GetAsync(AppSettings.BaseUrl + $"/categories/{categoryId}/subcategories");
        if (response.IsSuccessStatusCode)
        {

            var jsonResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<SubCategory>>(jsonResult);
        }
        else
        {
            throw new Exception("Couldn't load sub-category");
        }

    }
    // 3 that endpoint is run when first category is selected from list of categories - return list of prod types
    public static async Task<SubCategory> GetShowProductsFromSubCategory(int subCategoryId)
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));

        var response = await httpClient.GetAsync(AppSettings.BaseUrl + $"/subcategories/{subCategoryId}/producttype");

        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();

            try
            {
                return JsonConvert.DeserializeObject<SubCategory>(jsonResult);
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"Deserialization error: {ex.Message}");
                throw;
            }

        }
        else
        {
            throw new Exception("Couldn't load types");
        }
    }
    //4 that endpoint return single product type selected by specific id type from the list
    public static async Task<ProductType> GetShowProduct(int type)
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));

        var response = await httpClient.GetAsync(AppSettings.BaseUrl + $"/producttype/{type}");

        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();

            try
            {
                return JsonConvert.DeserializeObject<ProductType>(jsonResult);
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"Deserialization error: {ex.Message}");
                throw;
            }
        }
        else
        {
            throw new Exception("Couldn't load that type");
        }
    }
    public static async Task<bool> AddItemsInCart(ShoppingCartRequest shoppingCart)
    {
        var httpClient = new HttpClient();

        var json = JsonConvert.SerializeObject(shoppingCart);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));

        var response = await httpClient.PostAsync(AppSettings.BaseUrl + "/api/addToCart", content);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        return true;
    }
    public static async Task<ShoppingCartResponse> GetShoppingCartItems(string userId)
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accesstoken", string.Empty));

        var response = await httpClient.GetAsync(AppSettings.BaseUrl + "/api/summaryCart/");

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await Shell.Current.GoToAsync("login-page");

                return null;
            }

            throw new Exception($"Request failed with status {response.StatusCode}: {errorContent}");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ShoppingCartResponse>(jsonResponse);
    }


    public static async Task<List<AdminPolicies>> GetAdminPoliciesAsync()
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(AppSettings.BaseUrl + "/profile/policies/");
        var jsonResponse = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<List<AdminPolicies>>(jsonResponse);
    }


    public static async Task<bool> PlaceOrder(Order order)
    {
        var httpClient = new HttpClient();

        var json = JsonConvert.SerializeObject(order);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accesstoken", string.Empty));
        var response = await httpClient.PostAsync(AppSettings.BaseUrl + "/api/placeOrder", content);
        if (!response.IsSuccessStatusCode) return false;

        return true;
    }

    public static async Task<List<Order>> GetOrderByUser(string userId)
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(AppSettings.BaseUrl + "/api/getOrders/" + userId);
        var jsonResponse = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<List<Order>>(jsonResponse);
    }
    public static async Task<bool> ClearCartForUser(string userId)
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("accesstoken"));
        var response = await httpClient.PostAsync($"{AppSettings.BaseUrl}/api/clearCart", null);
        return response.IsSuccessStatusCode;
    }


    public static async Task<List<OrderDetail>> GetOrderDetails(int orderId)
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
        var response = await httpClient.GetStringAsync(AppSettings.BaseUrl + "/api/getOrderItems/" + orderId);
        return JsonConvert.DeserializeObject<List<OrderDetail>>(response);
    }


    public static async Task<List<Concert>> GetConcerts()
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
        var response = await httpClient.GetAsync(AppSettings.BaseUrl + "/concerts");
        var jsonResult = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<Concert>>(jsonResult);

    }
    public static async Task<Concert> GetConcerts(int id)
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
        var response = await httpClient.GetAsync(AppSettings.BaseUrl + "/concerts/" + id);
        var jsonResult = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Concert>(jsonResult);

    }
    public static async Task UpdateConcert(Concert concert)
    {
        var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
        var json = JsonConvert.SerializeObject(concert);
        var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync(AppSettings.BaseUrl + "/editconcerts/" + concert.Id, jsonContent);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to update the concert. Error: {errorContent}");
        }
    }
    public static async Task<bool> DeleteConcert(int concertId)
    {
        try
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization =
               new AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.DeleteAsync(AppSettings.BaseUrl + "/deleteconcert/" + concertId);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to delete the concert: {errorMessage}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while trying to delete the concert: " + ex.Message);
        }

    }
    public static async Task<bool> UpdateCartQuantity(int productId, string modification)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(AppSettings.BaseUrl);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accesstoken", string.Empty));
        var response = await httpClient.PutAsync("/api/shoppingcartitems?productId=" + productId + "&modification=" + modification, null);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        return true;
    }
    // address store contact endpoints
    public static async Task<MusicStoreAddress> GetStoreAddress()
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(AppSettings.BaseUrl);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accesstoken", string.Empty));
        var response = await httpClient.GetStringAsync("/storeAddress");
        return JsonConvert.DeserializeObject<MusicStoreAddress>(response);
    }

    public static async Task<List<OpeningHour>> GetOpeningHours()
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(AppSettings.BaseUrl);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accesstoken", string.Empty));
        var response = await httpClient.GetStringAsync("/openingHours");
        return JsonConvert.DeserializeObject<List<OpeningHour>>(response);
    }
    // bookmark heplers
    public static async Task<List<BookmarkedProduct>> GetAllBookmarks()
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(AppSettings.BaseUrl);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accesstoken", string.Empty));
        var response = await httpClient.GetStringAsync("/api/bookmarks/list");
        return JsonConvert.DeserializeObject<List<BookmarkedProduct>>(response);
    }

    public static async Task<bool> IsBookmarkedAsync(int productId)
    {
        // Create an instance of HttpClient
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(AppSettings.BaseUrl);
        // Set the authorization header with the bearer token
        var accessToken = Preferences.Get("accessToken", string.Empty);
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new Exception("Access token not found.");
        }
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        // Send a GET request to check if the product is bookmarked
        var response = await httpClient.GetStringAsync($"/api/bookmarks/isbookmarked/{productId}");

        // Deserialize the response to get the isBookmarked status
        var result = JsonConvert.DeserializeObject<dynamic>(response);
        // Return the boolean value of isBookmarked
        return result.isBookmarked;
    }
    public static async Task<bool> RemoveBookMarked(int productId)
    {
        // Create an instance of HttpClient
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(AppSettings.BaseUrl);

        // Set the authorization header with the bearer token
        var accessToken = Preferences.Get("accessToken", string.Empty);
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new Exception("Access token not found.");
        }
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        // Send a GET request to check if the product is bookmarked
        var response = await httpClient.DeleteAsync($"/api/bookmarks/remove/" + productId);
        return response.StatusCode == HttpStatusCode.OK;
    }
    public static async Task<BookmarkedProduct> AddBookmarkAsync(BookmarkedProduct book)
    {
        var httpClient = new HttpClient();
        // Retrieve the authorization token and set it in the request header
        var accessToken = Preferences.Get("accessToken", string.Empty);
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new Exception("Access token not found.");
        }
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        // Serialize the bookmark object to JSON format
        var json = JsonConvert.SerializeObject(book);
        var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
        // Send the POST request to add a new bookmark
        var response = await httpClient.PostAsync(AppSettings.BaseUrl + "/api/bookmarks/add", jsonContent);
        // Check if the operation was successful
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to create the bookmark. Error: {errorContent}");
        }
        // Deserialize the response content to get the added bookmark
        var responseContent = await response.Content.ReadAsStringAsync();
        var addedBookmark = JsonConvert.DeserializeObject<BookmarkedProduct>(responseContent);
        return addedBookmark;
    }

    public static async Task<Product> GetProductByColor(int colorId)
    {
        using (var client = new HttpClient())
        {
            var response = await client.GetAsync($"{AppSettings.BaseUrl}/api/product/bycolor/{colorId}");

            if (response.IsSuccessStatusCode)
            {
                var productJson = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(productJson);
                return product;
            }
            return null;
        }
    }

}

