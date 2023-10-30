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

        public Document GetDocument(int Id) => _context.Documents.Find(Id);

        public List<OldVersion> GetOldVersions(int Id)
        {
            return _context.OldVersions
                .Include(e => e.Document)
                .Where(e => e.DocumentId == Id).ToList();
        }

        public string AddDocument(Document document)
        {
            _context.Documents.Add(document);
            _context.SaveChanges();
            return "Add success!";
        }

        public string UpdateDocument(Document document)
        {
            _context.Documents.Update(document);
            _context.SaveChanges();
            return "Update success!";
        }

        public string AddOldVersion(Document document)
        {
            OldVersion oldVersion =  new OldVersion();
            oldVersion.Id = document.Id;
            oldVersion.Version = document.Version;
            oldVersion.File_path = document.File_path;
            oldVersion.Signature_Filepath = document.Signature_Filepath;

            oldVersion.Document = document;

            _context.OldVersions.Add(oldVersion);
            _context.SaveChanges();
            return "Add success!";
        }
    }
}
