using FlightDocs.Models;

namespace FlightDocs.Data
{
    public class RoleDM
    {
        private readonly ApplicationDbContext _context;

        public RoleDM(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public string AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return "Add success!";
        }

        public string UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
            return "Update success!";
        }

        public string DeleteRole(Role role)
        {
            _context.Roles.Remove(role);
            _context.SaveChanges();
            return "Delete success!";
        }
    }
}
