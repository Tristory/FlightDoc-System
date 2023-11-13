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

        public int AddDocumentType(DocumentType documentType)
        {
            documentType.Created_date = DateTime.Now;
            //documentType.CreatorId = currentUserId;

            _context.DocumentTypes.Add(documentType);
            _context.SaveChanges();
            return documentType.Id;
        }

        public string UpdateDocumentType(DocumentType documentType)
        {
            _context.DocumentTypes.Update(documentType);
            _context.SaveChanges();
            return "Update success!";
        }
    }
}
