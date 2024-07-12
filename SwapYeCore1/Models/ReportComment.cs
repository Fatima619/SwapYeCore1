namespace SwapYeCore1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ReportComment
    {
        [Key]
        public int ReportCommentId { get; set; }
        public int UserID { get; set; }
        [Required]
        public string Description_1 { get; set; }
        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual User User { get; set; }
    }
}
