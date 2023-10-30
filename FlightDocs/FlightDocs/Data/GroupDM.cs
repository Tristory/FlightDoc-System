using FlightDocs.Models;

namespace FlightDocs.Data
{
    public class GroupDM
    {
        private readonly ApplicationDbContext _context;

        public GroupDM(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Group> GetGroups() => _context.Groups.ToList();

        public Group GetGroup(int Id) => _context.Groups.Find(Id);

        public string AddGroup(Group group)
        {
            _context.Groups.Add(group);
            _context.SaveChanges();
            return "Add success!";
        }

        public string UpdateGroup(Group group)
        {
            _context.Groups.Update(group);
            _context.SaveChanges();
            return "Update success!";
        }
    }
}
