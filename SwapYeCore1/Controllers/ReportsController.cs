using Microsoft.AspNetCore.Mvc;
using SwapYeCore1.Data;
using SwapYeCore1.Models;

namespace SwapYeCore1.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly SwapYeCoreContext _context;

        public ReportsController(ILogger<ReportsController> logger, SwapYeCoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        //public IActionResult Report_Item(int id)
        //{         
        //  var item = _context.Items.First(i => i.ItemID == id);
        //      if (item == null)
        //      {
        //          return RedirectToAction("Index", "Home");
        //      }
        //  ReportItem reportItem = new ReportItem();

        //  reportItem.Description_1 = "بلاغ على المنتج";
        //  reportItem.ItemId = item.ItemID;
        //  reportItem.UserID = (int)(Session["UserID"]);
        //  _context.ReportItems.Add(reportItem);
        //  _context.SaveChanges();

        //  return RedirectToAction("Index", "Home");
        //}

        [HttpPost]
        public IActionResult create_itemrep(int userid, string text, int itemid)
        {
            if (userid == 0) { return Content("the user is not found"); }

            var itemrep = new ReportItem()
            {
                UserID = userid,
                Description_1 = text,
                ItemId = itemid,
            };
            _context.ReportItems.Add(itemrep);
            //_context.ReportItem.Add(report);
            _context.SaveChanges();
            //return View();
            return Redirect("../Home/Index");

        }

        public IActionResult Report_Comments(int id, string text, int itemid)
        {

            var Comment = _context.Comments.First(i => i.CommentId == itemid);

            if (Comment == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ReportComment reportComment = new ReportComment();

            int User = id;
            reportComment.Description_1 = text;
            reportComment.CommentId = Comment.CommentId;
            reportComment.UserID = User;
            _context.ReportComments.Add(reportComment);
            _context.SaveChanges();

            return RedirectPermanent("/Items/Details/" + Comment.ItemID);
        }
    }
}
