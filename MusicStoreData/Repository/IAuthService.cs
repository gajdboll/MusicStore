public interface IAuthService
{
    Task<bool> ValidateUserAsync(string username, string password);
}

public class AuthService : IAuthService
{
    // Symulowana weryfikacja użytkownika
    public async Task<bool> ValidateUserAsync(string username, string password)
    {
        // Tutaj możesz sprawdzić dane w bazie danych lub innym źródle
        await Task.Delay(500); // Symulacja czasu oczekiwania

        // Prosta weryfikacja, zastąp ją prawdziwą logiką
        return username == "test@example.com" && password == "password";
    }
}
