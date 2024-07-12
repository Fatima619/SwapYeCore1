using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using SwapYeCore1.Data;
using SwapYeCore1.Models;
using SwapYeCore1.ViewModels;

namespace SwapYeCore1.Controllers
{
    public class UserController : Controller
    {
       public string SessionUserId = "_UserId";

        private readonly ILogger<UserController> _logger;
        private readonly SwapYeCoreContext _context;

        public UserController(ILogger<UserController> logger, SwapYeCoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public ActionResult Login(User _user)
        {
            _context.SaveChanges();
            //if (ModelState.IsValid)
            //{
                try
                {

                    User user = _context.Users.First(i => i.UserName == _user.UserName && i.Passwords == _user.Passwords);


                    HttpContext.Session.SetInt32(SessionUserId, user.UserID);

                    if (user.UserTypeId == 3) { return RedirectToAction("Payment", "Admin"); }

                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Home", new { errorMessage = "لا يوجد حساب بأسم المستخدم أو كلمة المرور هذه" });
                }
                return RedirectToAction("Index", "Home", new { success = "Login" });

            //}
            //return RedirectToAction("Index", "Home", new { fail = "Login" });

        }

        [HttpPost]
        public ActionResult Register(User _user)
        {
            if (_user == null)
            {
                return NotFound();
            }

            //try
            //{
                User user = new User()
                {
                    FirstName = _user.FirstName,
                    LastName = _user.LastName,
                    Email = _user.Email,
                    UserName = _user.UserName,
                    Passwords = _user.Passwords,
                    Dob = _user.Dob,
                    UserTypeId = 2,
                    Gender = _user.Gender,
                    City = _user.City,
                    street = _user.street,
                    Phone = _user.Phone,
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                HttpContext.Session.SetInt32(SessionUserId, user.UserID);

                return RedirectToAction("Index", "Home");
            //}
            //catch (Exception ex)
            //{
            //    return RedirectToAction("Index", "Home", new { errorMessage = "حدثة مشكلة اثناء محاولة فتح حساب الرجاء المحاولة لاحقاً" ,m=ex.Message });

            //}
        }


        public ActionResult LogOut()
        {
            var User = HttpContext.Session.GetInt32(SessionUserId);
            if (User != null)
            {

                HttpContext.Session.Remove(SessionUserId);
                return RedirectToAction("Index", "Home");
            }
            return NotFound();
        }


        public ActionResult Profile(int? page)

        {
            int User = (int)HttpContext.Session.GetInt32("_UserId");
            var user = _context.Users.Find(User);
            if (user == null)
            {
                return NotFound();
            }
            var items = _context.Items
                .Where(m => m.UserID == user.UserID)
                .ToPagedList(page ?? 1, 3); 

            UserItem userItem = new UserItem()
            {
                user = user,
                items = items
            };

            return View(userItem);
        }

        [HttpPost]
        public ActionResult Edit(int id, string UserName, string Passwords, string FirstName, string LastName, string City, string Gender, string Phone, DateTime Dob, string street, string Email)
        {
            try
            {
                User users = _context.Users.Find(id);
                users.UserName = UserName;
                users.Passwords = Passwords;
                users.FirstName = FirstName;
                users.LastName = LastName;
                users.City = City;
                users.Gender = Gender;
                users.Phone = Phone;
                users.Dob = Dob;
                users.street = street;
                users.Email = Email;

                _context.Entry(users).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Profile", "User");
            }
            catch (Exception)
            {

                return RedirectToAction("Profile", "User", new { errorMessage = "حدثة مشكلة اثناء محاولة تعديل الحساب الرجاء المحاولة لاحقاً" });
            }

        }


    }
}
