using Microsoft.AspNetCore.Mvc;
namespace ContactsManager.Controllers
{
    public class HomeController:Controller
    {

        [Route("/")]

        public IActionResult Index()
        {
            return View(viewName:"Index");
        }
    }
}
