using SwapYeCore1.Models;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace SwapYeCore1.Data
{
    public class SwapYeCoreContext: DbContext
    {

        public SwapYeCoreContext(DbContextOptions<SwapYeCoreContext> options) : base(options)
        {

        }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemType> ItemTypes { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<ReportComment> ReportComments { get; set; }
        public virtual DbSet<ReportItem> ReportItems { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
    }
}
