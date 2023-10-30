using FlightDocs.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocs.Data
{
    public class PermissionDM
    {
        private readonly ApplicationDbContext _context;

        public PermissionDM(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<PermissionDG> GetPermissionDG(int Id)
        {
            return _context.PermissionDG
                .Include(e => e.Document)
                .Where(e => e.DocumentId == Id).ToList();
        }

        public List<PermissionDTG> GetPermissionDTG(int Id) 
        {
            return _context.PermissionDTG
                .Include(e => e.DocumentType)
                .Where(e => e.DocumentTypeId == Id).ToList();
        }

        public string AddPermissionDG(PermissionDG permissionDG) 
        {
            _context.PermissionDG.Add(permissionDG);
            //Need the id of the current Document
            _context.SaveChanges();
            return "Add success!";
        }

        public string AddPermissionDTG(PermissionDTG permissionDTG)
        {
            _context.PermissionDTG.Add(permissionDTG);
            //Need the id of the current DocumenType
            _context.SaveChanges();
            return "Add success!";
        }
    }
}
