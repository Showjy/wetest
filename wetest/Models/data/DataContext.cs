using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wetest.Models.models;
using wetest.Models.relatmodels;

namespace wetest.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<wetest.Models.User> Users { get; set; }
        public DbSet<wetest.Models.Item> Items { get; set; }
        public DbSet<wetest.Models.Order> Orders{ get; set; }
        public DbSet<wetest.Models.models.UserInfo> UserInfo { get; set; }
        public DbSet<wetest.Models.relatmodels.UserToItem> UserToItem { get; set; }
        public DbSet<wetest.Models.relatmodels.UserToOrder> UserToOrder { get; set; }
}
}
