using BookstoreWeb.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreWeb.Controllers
{
    public abstract class MainController : Controller
    {
        protected ApplicationDbContext _db;
    }
}
