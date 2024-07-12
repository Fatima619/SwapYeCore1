namespace SwapYeCore1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Item
    {
        [Key]
        public int ItemID { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public string Description_1 { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Transaction_type { get; set; }
        [Required]
        public string Item_State { get; set; }
        public int CityId { get; set; }
        [Required]
        public string Image_1 { get; set; }
        [Required]
        public string Image_2 { get; set; }
        [Required]
        public string Image_3 { get; set; }
        public int ItemTypeId { get; set; }
        public int UserID { get; set; }


        public virtual City City { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ReportItem> ReportItems { get; set; }
        public virtual ItemType ItemType { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
