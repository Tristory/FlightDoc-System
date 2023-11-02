using FlightDocs.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocs.Data
{
    public class DocumentDM
    {
        private readonly ApplicationDbContext _context;

        public DocumentDM(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Document> GetDocuments() => _context.Documents.ToList();
        public List<Document> GetFlightDocuments(int flightId)
        {
            return _context.Documents
                .Include(e => e.Flight)
                .Where(e => e.FlightId == flightId).ToList();
        }

        public List<Document> GetUserDocuments(int userId)
        {
            return _context.Documents
                .Include (e => e.User)
                .Where(e => e.CreatorId  == userId).ToList();
        }

        public List<Document> GetUserDocumentByTime(int userId)
        {
            return _context.Documents
                .Include(e => e.User)
                .Where(e => e.CreatorId == userId)
                .OrderByDescending(e => e.Updated_date).ToList();
        }

        public Document GetDocument(int Id) => _context.Documents.Find(Id);

        public List<OldVersion> GetOldVersions(int documentId)
        {
            return _context.OldVersions
                .Include(e => e.Document)
                .Where(e => e.DocumentId == documentId).ToList();
        }

        public string AddDocument(Document document)
        {
            document.Version = "1.0";
            document.Created_date = DateTime.Now;
            document.Updated_date = DateTime.Now;

            _context.Documents.Add(document);
            _context.SaveChanges();
            return "Add success!";
        }

        public string UpdateDocumentContext(Document document)
        {
            document.Updated_date = DateTime.Now;

            _context.Documents.Update(document);
            _context.SaveChanges();
            return "Update success!";
        }

        public string UpdateDocumentVersion(Document document)
        {
            //Create the old version record
            AddOldVersion(GetDocument(document.Id));

            //Update document version;
            float version = float.Parse(document.Version);
            version += 0.1f;

            document.Version = version.ToString();
            document.Updated_date = DateTime.Now;

            _context.Documents.Update(document);
            _context.SaveChanges();
            return "Update success!";
        }

        public string DeleteDocument(Document document, int userId)
        {
            if(document.CreatorId == userId)
            {
                _context.Documents.Remove(document);
                _context.SaveChanges();
                return "Delete Success!";
            }
            else
            {
                return "Delete fail!";
            }

            
        }

        public string AddOldVersion(Document document)
        {
            OldVersion oldVersion =  new OldVersion();
            oldVersion.Version = document.Version;
            oldVersion.File_path = document.File_path;
            oldVersion.Signature_Filepath = document.Signature_Filepath;

            oldVersion.Document = document;                     

            _context.OldVersions.Add(oldVersion);
            //_context.SaveChanges();
            return "Add success!";
        }
    }
}
