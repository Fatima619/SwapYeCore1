using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using SwapYeCore1.Data;
using SwapYeCore1.Models;
using SwapYeCore1.ViewModels;

namespace SwapYeCore1.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly SwapYeCoreContext _context;

        public AdminController(ILogger<AdminController> logger, SwapYeCoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public IActionResult Index(int Pid, int Iid)
        {
            return Content($"pay id: {Pid} item id {Iid} ");
        }

        public IActionResult Payment()
        {
            var pa = _context.Payments.Where(p => p.ApprovalState != "true")
                .Include(m => m.Item)
                .ToList();
            return View(pa);
        }

        // GET: Friends/Delete/5
        [HttpGet, ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var friend = await _context.Items
                .FirstOrDefaultAsync(m => m.ItemID == id);
            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
        }
        //not workig
        [HttpPost, ActionName("delete")]
        [ValidateAntiForgeryToken]
        public IActionResult delete(int Iid)
        {
            var n = _context.Items.Find(Iid);
            
            var comment = _context.Comments.Where(p => p.ItemID == Iid).ToList();
            for (int i = 0; i < comment.Count(); i++)
            {
                _context.Comments.Remove(comment[i]);
            }
            //var payment = _context.Payments.Where(m => m.ItemID == n.ItemID);

            //_context.Payments.RemoveRange(payment);
            _context.Items.Remove(n);

            _context.SaveChanges();
            return RedirectToAction("page2");
        }

        public IActionResult Accept(int pid)
        {
            var p = _context.Payments.Find(pid);

            p.ApprovalState = "true";

            _context.SaveChanges();
            return Redirect("../Admin/Payment");
        }


        public IActionResult Reports()
        {
            var ri = _context.ReportItems.ToList();
            var rc = _context.ReportComments.Include(m=> m.Comment)
                .ToList();

            Report vm = new Report();
            vm.reitem = ri;
            vm.recomment = rc;
            return View(vm);
        }

        [HttpGet]
        public IActionResult reitem(int Iid, int m)
        {

            if (m == 0)
            {
                var rep = _context.ReportItems.Find(Iid);
                _context.ReportItems.Remove(rep);
                _context.SaveChanges();
                return Redirect("../Admin/Reports");

            }
            else
            {
                var item = _context.Items.Find(Iid);
                var comment = _context.Comments.Where(i => i.ItemID == item.ItemID);
                _context.Comments.RemoveRange(comment);
                _context.Items.Remove(item);
                _context.SaveChanges();
                return Redirect("../Admin/Reports");
            }
        }

        public IActionResult recom(int c_id, int m)
        {
            if (m == 0)
            {
                var rep = _context.ReportComments.Find(c_id);
                _context.ReportComments.Remove(rep);
                _context.SaveChanges();
                return Redirect("../Admin/Reports");

            }
            else
            {
                var item = _context.Comments.Find(c_id);
                var report_comments = _context.ReportComments.Where(m => m.CommentId == item.CommentId);
                _context.ReportComments.RemoveRange(report_comments);
                _context.Comments.Remove(item);
                _context.SaveChanges();
                return Redirect("../Admin/Reports");

            }
        }

        //public IActionResult remsg(int m_id, int m)
        //{
        //    if (m == 0)
        //    {
        //        var rep = _context.ReportChats.Find(m_id);
        //        _context.ReportChats.Remove(rep);
        //        _context.SaveChanges();
        //        return Redirect("../Admin/Reports");

        //    }
        //    else
        //    {
        //        var chat = _context.Chats.Find(m_id);
        //        var messages = _context.Messages.Where(i => i.ChatId == chat.ChatId);
        //        _context.Messages.RemoveRange(messages);
        //        _context.Chats.Remove(chat);
        //        _context.SaveChanges();
        //        return Redirect("../Admin/Reports");
        //    }
        //}

        public IActionResult Add_Nptice(int m_id, int m)
        {
            return Content("hi");
        }





        [HttpGet]
        public IActionResult Search1(string search, int? page)
        {
            var items = _context.Items
                .Where(x => x.Description_1.StartsWith(search) || x.Item_State.StartsWith(search))
                .Include(m => m.ItemType)
                .Include(m => m.City)
                .ToPagedList(page ?? 1, 2);
            return View(items);
        }

        public IActionResult page2(int? page, string? search)
        {
            var item = _context.Items.ToList();
            var items = _context.Items.Where(x => x.Description_1.StartsWith(search) || x.Item_State.StartsWith(search)).Include(m => m.City)
                .Include(m => m.ItemType)
                .ToPagedList(page ?? 1, 2);
            if (search == null)
            {
                 items = _context.Items.Include(m => m.City)
                .Include(m => m.ItemType)
                .ToPagedList(page ?? 1, 2);

            }
                

            



            if (items == null)
            {
                return NotFound();
            }

            var num = item.Count();
            ViewBag.Num = num;

            return View(items);


        }

        public IActionResult pagepay(int? page)
        {
            var pay = _context.Payments.ToList();
            var items = _context.Payments.Include(m => m.Item)
               
               .ToPagedList(page ?? 1, 2);

            if (items == null)
            {
                return NotFound();
            }
            var num = pay.Count();
            ViewBag.Num = num;

            return View(items);


        }
    }
}
