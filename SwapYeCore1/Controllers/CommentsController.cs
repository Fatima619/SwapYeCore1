using Microsoft.AspNetCore.Mvc;
using SwapYeCore1.Data;
using SwapYeCore1.Models;

namespace SwapYeCore1.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ILogger<CommentsController> _logger;
        private readonly SwapYeCoreContext _context;

        public CommentsController(ILogger<CommentsController> logger, SwapYeCoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(string Content, IFormFile image1, int id)
        {
            try
            {
                Comment comment = new Comment();
                if (Content == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                if (image1 != null && image1.Length > 0)
                {

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserImg", image1.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        image1.CopyTo(stream);
                    }
                    comment.Image_1 = "~/UserImg/"+ image1.FileName;
                }

                if (Content != null)
                {
                    int User = (int)HttpContext.Session.GetInt32("_UserId");
                    comment.ItemID = id;
                    comment.UserID = User;
                    comment.Dateposted = DateTime.Now;
                    comment.Content = Content;


                    _context.Comments.Add(comment);
                    _context.SaveChanges();
                }
                return RedirectPermanent("/Items/Details/" + id);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home", new { errorMessage = "حدثة مشكلة اثناء اضافة التعليق الرجاء المحاولة لاحقاً" });

            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //try
            //{
                Comment comment = _context.Comments.Find(id);
                if (comment == null)
                {
                    return NotFound();
                }
                int ItemId = comment.ItemID;
                List<ReportComment> Reports = _context.ReportComments.ToList();
                for (int i = 0; i < Reports.Count; i++)
                {
                    if (Reports[i].CommentId == comment.CommentId)
                    {
                        _context.ReportComments.Remove(Reports[i]);
                    }
                }


                //if (report != null) 
                //    _context.ReportComment(report);


                _context.Comments.Remove(comment);

                _context.SaveChanges();
                return RedirectPermanent("/Items/Details/" + ItemId);
            //}
            //catch (Exception)
            //{
            //    return RedirectToAction("Items", "Details", new {id = id, errorMessage = "حدثة مشكلة اثناء حذف التعليق الرجاء المحاولة لاحقاً" });

            //}

        }
    }
}
