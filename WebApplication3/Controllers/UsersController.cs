using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class UsersController : Controller
    {

        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        //Validating Signup and updating database
        [HttpPost]
        public JsonResult SignUp(Member member)
        {
            String message = "Account created successfully";
            CommitDB dB = new CommitDB();
            if (dB.Members.Any(tempmember => tempmember.email == member.email) == false)
            {
                if (member.firstName != null)
                {
                    if (member.lastName != null)
                    {
                        if (new EmailAddressAttribute().IsValid(member.email) && member.email != null)
                        {
                            if (member.password.Count() > 6)
                            {
                                if (dB.Members.Count() == 0)
                                {
                                    member.admin = true;
                                }
                                dB.Members.Add(member);
                                dB.SaveChanges();
                            }
                            else
                            {
                                message = "Your password must be at least six characters";
                            }

                        }
                        else
                        {
                            message = "Please enter a valid email address";
                        }
                    }
                    else
                    {
                        message = "Please insert your last name";
                    }
                }
                else
                {
                    message = "Please insert your first name";
                }
            }
            else
            {
                message = "There is an account already associated with this email";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }


        //Validating login details 
        [HttpPost]
        public ActionResult LogIn(String emailLogIn, String passwordLogIn)
        {
            Session["member"] = null;
            CommitDB dB = new CommitDB();
            int length = dB.Members.Count();
            String message = "email and password not matching";
            foreach (Member tempMember in dB.Members)
            {
                if (tempMember.email == emailLogIn && tempMember.password == passwordLogIn)
                {
                    Session["memberId"] = tempMember.id;
                    Session["memberName"] = tempMember.firstName + " " + tempMember.lastName;
                }
            }
            if (Session["memberId"] == null)
            {
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = "logInSuccess", url = Url.Action("Index", "Causes") });
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return View();
        }
    }
}