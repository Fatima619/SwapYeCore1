namespace SwapYeCore1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int UserID { get; set; }
        [Required]
        public string Content { get; set; }
        public System.DateTime Dateposted { get; set; }
        [Required]
        public string Image_1 { get; set; }
        public int ItemID { get; set; }

        public virtual ICollection<ReportComment> ReportComments { get; set; }
        public virtual Item Item { get; set; }
        public virtual User User { get; set; }

    }
}
