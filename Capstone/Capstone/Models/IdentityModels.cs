using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Capstone.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserRole { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<MemberModel> MemberModels { get; set; }
        public DbSet<BookAPIModel> BookAPIModels { get; set; }
        public DbSet<BookEntryModel> BookEntryModels { get; set; }
        // public DbSet<GroupMembersModel> GroupMembersModels { get; set; }
        public DbSet<GroupModel> GroupModels { get; set; }
        public DbSet<CurrentlyReadingModel> CurrentlyReadingModels { get; set; }
        public DbSet<SendEmailModel> SendEmailModels { get; set; }
        public DbSet<ReadingListModel> ReadingListModels { get; set; }



        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}