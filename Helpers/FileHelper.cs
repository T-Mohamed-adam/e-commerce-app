namespace TagerProject.Helpers
{
    public class FileHelper
    {
        public FileHelper()
        {
        }

        public async Task<string> SaveImageAsync(IFormFile image)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}_{image.FileName}";
            var fullPath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return Path.Combine("images", fileName); // Return the realtive path
        }
    }

}
