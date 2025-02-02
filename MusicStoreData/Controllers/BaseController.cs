using Microsoft.AspNetCore.Mvc;
using MusicStoreData.Data;
 
namespace WebAPI.Controllers
{
    public class BaseController : Controller
    {
        public MusicStoreContext Database { get; set; }

        public BaseController(MusicStoreContext databaseContext) => Database = databaseContext;
    }
}
