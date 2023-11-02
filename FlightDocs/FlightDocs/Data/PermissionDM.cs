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

        public List<PermissionDG> GetPermissionDGs(int documentId)
        {
            return _context.PermissionDG
                .Include(e => e.Document)
                .Where(e => e.DocumentId == documentId).ToList();
        }

        public PermissionDG GetPermissionDG(int Id) => _context.PermissionDG.Find(Id);

        public List<PermissionDTG> GetPermissionDTGs(int documentTypeId) 
        {
            return _context.PermissionDTG
                .Include(e => e.DocumentType)
                .Where(e => e.DocumentTypeId == documentTypeId).ToList();
        }

        public string AddPermissionDG(PermissionDG permissionDG) 
        {
            _context.PermissionDG.Add(permissionDG);
            //Need the id of the current Document
            _context.SaveChanges();
            return "Add success!";
        }

        public string AddPermissionDTG(PermissionDTG permissionDTG, int access_level)
        {
            // O mean no access, 1 mean read only and 2 mean read and write
            // For east query, null and 0 is treated the same
            permissionDTG.Access_Level = access_level;

            _context.PermissionDTG.Add(permissionDTG);
            //Need the id of the current DocumenType
            _context.SaveChanges();
            return "Add success!";
        }

        public string DeletePermissionDG(int documentId)
        {
            _context.PermissionDG.Remove(GetPermissionDG(documentId));
            _context.SaveChanges();
            return "Delete success!";
        }
    }
}
