using System;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class CreateACauseController : Controller
    {
        // GET: CreateACause
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddACause(String nameTxt, String orgInvolvedTxt, String dropDownCauseType, String groupsTxt, String detailsTxt)
        {
            CommitDB dB = new CommitDB();
            CauseModel newCause = new CauseModel();
            newCause.name = nameTxt;
            newCause.orgInvolved = orgInvolvedTxt;
            newCause.causetype = dropDownCauseType;
            newCause.groups = groupsTxt;
            newCause.description = detailsTxt;
            dB.Causes.Add(newCause);
            dB.SaveChanges();
            return View("AddACause");
        }
    }
}