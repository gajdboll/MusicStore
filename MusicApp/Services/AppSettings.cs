 

namespace MusicApp.Services
{
    public static class AppSettings
    {
        public static string BaseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5115" : "http://localhost:5115";
    }
}
