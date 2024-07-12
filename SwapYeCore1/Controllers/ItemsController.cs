using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwapYeCore1.Data;
using SwapYeCore1.Models;
using SwapYeCore1.ViewModels;

namespace SwapYeCore1.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ILogger<ItemsController> _logger;
        private readonly SwapYeCoreContext _context;

        public ItemsController(ILogger<ItemsController> logger, SwapYeCoreContext context)
        {
            _logger = logger;
            _context = context;
        }
        // GET: Items/Details/5
        public IActionResult Details(int? id)
        {
            var item = _context.Items.FirstOrDefault(i => i.ItemID == id);
            var comments = _context.Comments.Where(i => i.ItemID == item.ItemID).Include(m=>m.User).ToList();
            ItemCommNotif itemCommNotif = new ItemCommNotif()
            {
                Comment = new Comment(),
                Comments = comments,
                item = item,
                reportComment = new ReportComment(),
            };
            if (item == null)
            {
                return NotFound();
            }
            return View(itemCommNotif);
        }

        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Item item = _context.Items.Find(id);
            var comment = _context.Comments.Where(p => p.ItemID == item.ItemID).ToList();

            if (item == null)
            {
                return NotFound();
            }
            _context.Comments.RemoveRange(comment);
            _context.Items.Remove(item);

            _context.SaveChanges();

            return RedirectPermanent("/User/Profile"); ;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string ItemName, string Description_1, string Price, string Transaction_type, string Item_State, int CityId, int TypeId, IFormFile ProductImg1, IFormFile ProductImg2, IFormFile ProductImg3, IFormFile Recipt)
        {
            try
            {
                Item item1 = new Item();
                Payment payment = new Payment();


                if (ProductImg1 != null && ProductImg1.Length > 0)
                {

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserImg", ProductImg1.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ProductImg1.CopyTo(stream);
                    }
                    item1.Image_1 = "~/UserImg/" + ProductImg1.FileName;
                }
                if (ProductImg2 != null && ProductImg1.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserImg", ProductImg2.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ProductImg2.CopyTo(stream);
                    }
                    item1.Image_2 = "~/UserImg/" + ProductImg2.FileName;
                }
                if (ProductImg3 != null && ProductImg1.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserImg", ProductImg3.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ProductImg3.CopyTo(stream);
                    }
                    item1.Image_3 = "~/UserImg/" + ProductImg3.FileName;
                }
                if (Recipt != null && ProductImg1.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserImg", Recipt.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Recipt.CopyTo(stream);
                    }
                    payment.File = "~/UserImg/" + Recipt.FileName;
                }
                int User = (int)HttpContext.Session.GetInt32("_UserId");
                item1.ItemName = ItemName;
                item1.Description_1 = Description_1;
                item1.Price = decimal.Parse(Price);
                item1.Transaction_type = Transaction_type;
                item1.Item_State = Item_State;
                item1.CityId = 2;// CityId;
                item1.ItemTypeId = 2; //TypeId;
                item1.UserID = User;



                _context.Items.Add(item1);
                _context.SaveChanges();

                Item item = _context.Items.ToList().Last(); 

                payment.ApprovalState = "false";
                payment.ItemID = item.ItemID;

                _context.Payments.Add(payment);
                _context.SaveChanges();
                return RedirectToAction("Profile", "User");
        }
            catch (Exception)
            {
                return RedirectToAction("Create", "Home", new { errorMessage = "حدثة مشكلة اثناء اضافة المنتج الرجاء المحاولة لاحقاً" });
            }


        }

        // GET: Items/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Item item = _context.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(int id, string ItemName, string Description_1, string Price, string Transaction_type, string Item_State, int CityId, int TypeId, IFormFile ProductImg1, IFormFile ProductImg2, IFormFile ProductImg3)
        {
            try
            {


                Item item1 = _context.Items.Find(id);

                if (ProductImg1 != null && ProductImg1.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserImg", ProductImg1.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ProductImg1.CopyTo(stream);
                    }
                    item1.Image_1 = "~/UserImg/" + ProductImg1.FileName;
                }
                if (ProductImg2 != null && ProductImg2.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserImg", ProductImg2.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ProductImg2.CopyTo(stream);
                    }
                    item1.Image_2 = "~/UserImg/" + ProductImg2.FileName;
                }
                if (ProductImg3 != null && ProductImg3.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserImg", ProductImg3.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ProductImg3.CopyTo(stream);
                    }
                    item1.Image_3 = "~/UserImg/" + ProductImg3.FileName;
                }


                int User = (int)HttpContext.Session.GetInt32("_UserId");


                item1.ItemName = ItemName;
                item1.Description_1 = Description_1;
                item1.Price = decimal.Parse(Price);
                item1.Transaction_type = Transaction_type;
                item1.Item_State = Item_State;
                item1.CityId = CityId;
                item1.ItemTypeId = TypeId;
                item1.UserID = User;

                _context.Entry(item1).State = EntityState.Modified;

                _context.SaveChanges();
                return RedirectToAction("Profile", "User");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home", new { errorMessage = "حدثة مشكلة اثناء تعديل المنتج الرجاء المحاولة لاحقاً" });
            }
        }
    }
}
