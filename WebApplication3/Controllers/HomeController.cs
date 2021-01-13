using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CommitDB dB = new CommitDB();
            List<CauseModel> causes = dB.Causes.ToList();

            return View(causes);
        }
    }
}