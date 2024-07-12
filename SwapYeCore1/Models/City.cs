namespace SwapYeCore1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class City
    {
        [Key]
        public int CityId { get; set; }
        [Required]
        public string CityName { get; set; }

        public virtual ICollection<Item> Items { get; set; }


    }
}
