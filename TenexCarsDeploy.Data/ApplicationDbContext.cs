using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TenexCarsDeploy.Data.Models;

namespace TenexCars.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Operator> Operators { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<OperatorMember> OperatorMembers { get; set; }

        public virtual DbSet<Co_SubscriberInvitee> Co_SubscriberInvitees { get; set; }
        public virtual DbSet<Subscriber> Subscribers { get; set; }
        public virtual DbSet<Subscription> Subscriptions{ get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleRequest> VehicleRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
           .HasOne(t => t.Subscriber)
           .WithMany(s => s.Transactions)
           .HasForeignKey(t => t.SubscriberId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Operator)
                .WithMany(o => o.Transactions)
                .HasForeignKey(t => t.OperatorId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Vehicle)
                .WithMany(v => v.Transactions)
                .HasForeignKey(t => t.VehicleId);

        }
    }
}