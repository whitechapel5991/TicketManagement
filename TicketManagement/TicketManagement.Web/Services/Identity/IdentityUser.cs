using Microsoft.AspNet.Identity;

namespace TicketManagement.Web.Services.Identity
{
    public class IdentityUser : IUser<int>
    {
        public IdentityUser()
        {
        }

        public IdentityUser(string userName)
            : this()
        {
            this.UserName = userName;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string SecurityStamp { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Language { get; set; }
        public string TimeZone { get; set; }
        public decimal Balance { get; set; }
    }
}