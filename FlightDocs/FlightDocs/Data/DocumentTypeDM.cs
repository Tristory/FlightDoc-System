using FlightDocs.Models;

namespace FlightDocs.Data
{
    public class DocumentTypeDM
    {
        private readonly ApplicationDbContext _context;

        public DocumentTypeDM(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<DocumentType> GetDocumentTypes() => _context.DocumentTypes.ToList();

        public DocumentType GetDocumentType(int Id) => _context.DocumentTypes.Find(Id);

        public string AddDocumentType(DocumentType documentType)
        {
            _context.DocumentTypes.Add(documentType);
            _context.SaveChanges();
            return "Add success!";
        }

        public string UpdateDocumentType(DocumentType documentType)
        {
            _context.DocumentTypes.Update(documentType);
            _context.SaveChanges();
            return "Update success!";
        }
    }
}
