using Microsoft.AspNet.Identity;

namespace TicketManagement.Web.Services.Identity
{
    public class IdentityRole : IRole<int>
    {
        public IdentityRole()
        {
        }

        public IdentityRole(string name)
            : this()
        {
            this.Name = name;
        }

        public IdentityRole(string name, int id)
        {
            this.Name = name;
            this.Id = id;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}