using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class CausesController : Controller
    {
        // GET: Causes
        public ActionResult Index()
        {
            CommitDB db = new CommitDB();
            List<CauseModel> causes = db.Causes.ToList();
            return View(causes);
        }
        //Function to display the details of the cause from the database
        public ActionResult CauseDetailsPage(int id)
        {
            CommitDB dB = new CommitDB();
            List<CauseModel> causes = dB.Causes.ToList();
            CauseModel matching = new CauseModel();
            foreach (CauseModel causeTemp in causes)
            {
                if (causeTemp.id == id)
                {
                    matching = causeTemp;
                    break;
                }
            }
            return View(matching);
        }
        //Using Ajax to update the signature count
        public JsonResult realTimeUpdate()
        {
            CommitDB dB = new CommitDB();
            List<CauseModel> causesList = new List<CauseModel>();
            List<Member> membersList = new List<Member>();
            foreach (CauseModel tempCause in dB.Causes.ToList())
            {
                membersList.Clear();
                foreach (Member tempMember in tempCause.PeopleSigned)
                {
                    tempMember.email = "";
                    tempMember.password = "";
                    tempMember.CausesCommitedTo.Clear();
                    membersList.Add(tempMember);
                }
                tempCause.PeopleSigned.Clear();
                tempCause.PeopleSigned = membersList.ToList();
                
                causesList.Add(tempCause);
            }
            return Json(causesList, JsonRequestBehavior.AllowGet);
        }

        //Using Ajax to update the database with the new signatures
        [HttpPost]
        public JsonResult addSignature(String idString, String idMember)
        {
            String message = "Signature Added";
            CommitDB dB = new CommitDB();
            CauseModel selectedCause = dB.Causes.Single(cause => cause.id.ToString() == idString);
            Member activeMember = dB.Members.Single(member => member.id.ToString() == idMember);
            if (selectedCause.PeopleSigned.Contains(activeMember))
            {
                message = "You have already signed this cause";
            }
            else
            {
                selectedCause.PeopleSigned.Add(activeMember);
                dB.SaveChanges();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public bool checkIfAdminById(int id)
        {
            bool admin = false;
            CommitDB db = new CommitDB();
            foreach (Member tempMember in db.Members)
            {
                if (tempMember.id == id)
                {
                    admin = tempMember.admin;
                }
            }
            return admin;
        }

    }
}