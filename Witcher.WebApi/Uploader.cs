namespace Witcher.WebApi
{
    public class Uploader
    {
        private readonly IWebHostEnvironment _env;
        
        public Uploader(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string? UploadFile(IFormFile? file, string? folderName = "")
        {
            if (file == null) return null;

            var uniqueFileName = GetUniqueFileName(file.FileName);
            var filePath = GetFullPathUploadFile(uniqueFileName, folderName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"/uploads/{folderName + "/"}" + uniqueFileName;
        }

        public void DeleteFile(string? fileName, string? folderName)
        {
            if (fileName == null) return;

            fileName = fileName.TrimStart('/').Replace("\\", "/");
            var filePath = Path.Combine(_env.WebRootPath, folderName + "/" + fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        private string GetFullPathUploadFile(string fileName, string? folderName)
        {
            if (string.IsNullOrEmpty(_env.WebRootPath))
                _env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            var folderPath = Path.Combine(_env.WebRootPath, folderName ?? "");
            var filePath = Path.Combine(folderPath, fileName);

            return filePath;
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);

            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}
