using System;
using System.Collections.Generic;
using System.Text;
using GoCart_ManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoCart_ManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {


        public DbSet<UserModel> UserTable { get; set; }

        public DbSet<RoleModel> RoleTable { get; set; }

        public DbSet<TrackModel> TrackTable { get; set; }

        public DbSet<ReservationModel> ReservationTable { get; set; }

        public DbSet<PaymentModel> PaymentTable { get; set; }

        public DbSet<DiscountCouponModel> DiscountCouponTable { get; set; }

        public DbSet<RankModel> RankTable { get; set; }







        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base()
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"SERVER=kt135075-001.dbaas.ovh.net;PORT=35547;UID=root32011;PASSWORD=koniecznyPWSZ32011;DATABASE=pwsz;convert zero datetime=True";

            optionsBuilder.UseMySQL(connectionString);



        }


        public DbSet<GoCart_ManagementSystem.Models.RankModel> RankModel { get; set; }
    }
}
