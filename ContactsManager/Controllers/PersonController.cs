using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.Controllers
{
    public class PersonController:Controller
    {

        [Route("/persons/index")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
