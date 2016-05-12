using Microsoft.AspNet.Identity.EntityFramework;

namespace Coman3.Models.Database
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }
    }
}