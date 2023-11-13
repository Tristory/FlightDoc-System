using FlightDocs.Controllers;
using FlightDocs.File_Handler.Code;
using FlightDocs.Models;
using Microsoft.AspNetCore.Mvc;
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

        //Read operation 
        public List<Document> GetDocuments() => _context.Documents.ToList();
        public List<Document> GetFlightDocuments(int flightId)
        {
            return _context.Documents
                .Include(e => e.Flight)
                .Where(e => e.FlightId == flightId).ToList();
        }

        public List<Document> GetOtherFlightDocuments(int flightId,  int userId)
        {
            return _context.Documents
                .Include(e => e.Flight)
                .Where(e => e.FlightId == flightId && e.CreatorId != userId).ToList();
        }

        public List<Document> GetUserFlightDocuments(int flightId, int userId)
        {
            return _context.Documents
                .Include(e => e.Flight)
                .Where(e => e.FlightId == flightId && e.CreatorId == userId).ToList();
        }

        public List<Document> GetUserDocuments(int userId)
        {
            return _context.Documents
                .Include(e => e.DocumentType)
                .Include(e => e.Flight)
                .Include(e => e.User)
                .Where(e => e.CreatorId  == userId).ToList();
        }

        public List<Document> GetUserDocumentByTime(int userId)
        {
            return _context.Documents
                .Include(e => e.User)
                .Where(e => e.CreatorId == userId)
                .OrderByDescending(e => e.Updated_date).ToList();
        }

        public List<Document> GetUserRecentActivity(int userId)
        {
            return _context.Documents
                .Include(e => e.DocumentType)
                .Include(e => e.Flight)
                .Include(e => e.User)
                .Where(e => e.CreatorId == userId)
                .OrderByDescending(e => e.Updated_date).ToList();
        }

        public Document GetDocument(int Id)
        {
            return _context.Documents
                .Include(e => e.DocumentType)
                .Include(e => e.Flight)
                .Include(e => e.User)
                .FirstOrDefault(e => e.Id == Id);
        }

        public List<OldVersion> GetOldVersions(int documentId)
        {
            return _context.OldVersions
                .Include(e => e.Document)
                .Where(e => e.DocumentId == documentId).ToList();
        }

        //Add operation
        public async Task<int> AddDocumentAsync(Document document, IFormFile file)
        {
            document.Version = "1.0";
            document.Created_date = DateTime.Now;
            document.Updated_date = DateTime.Now;

            var filePath =  await FileNameHandlerAsync(document.Name, 1.0f, file);

            document.File_path = filePath;

            _context.Documents.Add(document);
            _context.SaveChanges();

            return document.Id;
        }

        //Update operation
        public string UpdateDocument(Document document)
        {
            /*if (document.CreatorId.ToString() != IdentityController.CurrentUserId(tokenString)
                || IdentityController.UserIsAdmin(tokenString))
                return "You shall not pass!";*/

            document.Updated_date = DateTime.Now;

            _context.Documents.Update(document);
            _context.SaveChanges();

            return "Update success!";
        }

        public async Task<string> UpdateDocumentAsync(Document document, IFormFile file)
        {
            //Make sure that user is the creator
            /*if (document.CreatorId.ToString() != IdentityController.CurrentUserId(tokenString) 
                || IdentityController.UserIsAdmin(tokenString))
                return "You shall not pass!";*/

            //Create the old version record
            AddOldVersion(GetDocument(document.Id));

            //Update document version;
            float version = float.Parse(document.Version);
            version += 0.1f;

            document.Version = version.ToString();
            document.Updated_date = DateTime.Now;

            var filePath = await FileNameHandlerAsync(document.Name, version, file);

            document.File_path = filePath;

            _context.Documents.Update(document);
            _context.SaveChanges();

            return "Update success!";
        }

        public async Task<string> UpdateDocumentAsync(Document document, IFormFile file, IFormFile pic)
        {
            //Make sure that user is the creator
            /*if (document.CreatorId.ToString() != IdentityController.CurrentUserId(tokenString) 
                || IdentityController.UserIsAdmin(tokenString))
                return "You shall not pass!";*/

            //Create the old version record
            AddOldVersion(GetDocument(document.Id));

            //Update document version;
            float version = float.Parse(document.Version);
            version += 0.1f;

            document.Version = version.ToString();
            document.Updated_date = DateTime.Now;

            var filePath = await FileNameHandlerAsync(document.Name, version, file);
            var picPath = await PicNameHandlerAsync(document.Name, version, pic);

            document.File_path = filePath;
            document.Signature_Filepath = picPath;

            _context.Documents.Update(document);
            _context.SaveChanges();

            return "Update success!";
        }

        //Delete operation
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

        public bool IsCreator(Document document, string currentUserId)
        {
            if (document.CreatorId.ToString() == currentUserId)
            {
                return true;
            }

            return false;
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

        //File name handler
        public string FileNameExtractor(string filename)
        {
            int first = filename.IndexOf("(");
            int second = filename.IndexOf(')', first);
            //int third = filename.IndexOf(')', second);
            string fileName = filename.Substring(0, first);
            string fileVersion = filename.Substring(first + 1, second - first - 1);

            return fileName;
        }

        public async Task<string> FileNameHandlerAsync(string docName, float version, IFormFile file)
        {
            var newFileName =  string.Format("{0}({1}).pdf", docName, version.ToString());

            await FileHandler.UploadFile(file, newFileName);

            return newFileName;
        }

        public async Task<string> PicNameHandlerAsync(string docName, float version, IFormFile pic)
        {
            var newPicName = string.Format("{0}_Sign({1}).png", docName, version.ToString());

            await FileHandler.UploadPicture(pic, newPicName);

            return newPicName;
        }
    }
}
