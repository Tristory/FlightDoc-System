namespace FlightDocs.File_Handler.Code
{
    public class FileHandler
    {
        public static async Task<string> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "File Handler", "Main Folder", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "Success";
        }
        
        public static IFormFile ShowFile(string fileName)
        {
            if (fileName == null)
                return null;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "File Handler", "Main Folder", fileName);

            var stream = new FileStream(path, FileMode.Open);

            var file = new FormFile(stream, 0, stream.Length, null, fileName);

            return file;

            /*var memory = new MemoryStream();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return (IFormFile) File(memory, "application/pdf");*/
        }
    }
}
