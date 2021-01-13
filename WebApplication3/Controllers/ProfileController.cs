using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class ProfileController : Controller
    {

        // GET: Profile
        public ActionResult Index(int id)
        {
            CommitDB dB = new CommitDB();
            Member neededMember = new Member();
            foreach (Member tempMember in dB.Members)
            {
                if (tempMember.id == id)
                {
                    neededMember = tempMember;
                }
            }
            return View(neededMember);
        }

        public PartialViewResult CausesList()
        {
            CommitDB dB = new CommitDB();
            return PartialView(dB.Causes.ToList());
        }

        public PartialViewResult SignedCausesList(int id)
        {
            CommitDB dB = new CommitDB();
            List<CauseModel> signedCauses = new List<CauseModel>();
            foreach (CauseModel tempCause in dB.Causes)
            {
                if (tempCause.PeopleSigned.Contains(dB.Members.Single(member => member.id == id)))
                {
                    signedCauses.Add(tempCause);
                }
            }
            return PartialView(signedCauses);
        }

        public ViewResult DeleteCause(int causeID)
        {
            CommitDB dB = new CommitDB();
            CauseModel causeToDelete = new CauseModel();
            causeToDelete = dB.Causes.SingleOrDefault(cause => cause.id == causeID);
            causeToDelete.PeopleSigned.Clear();
            dB.Causes.Remove(causeToDelete);
            dB.SaveChanges();
            return View();
        }
    }
}