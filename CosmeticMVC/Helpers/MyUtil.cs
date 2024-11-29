using System.Text;

namespace CosmeticMVC.Helpers
{
    public class MyUtil
    {
        public static string GenerateRandomKey(int length = 5)
        {
            var pattern = @"qazwsxedcrfvtgbyhnujmiklopQAZWSXEDCRFVTGBYHNUJMIKOLP!";
            var sb = new StringBuilder();
            var rd = new Random();
            for (int i = 0; i < length; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }

            return sb.ToString();
        }

        public static string uploadHinh(IFormFile Hinh, string folder)
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "HinhAnh", folder, Hinh.FileName);
                using (var myfile = new FileStream(fullPath, FileMode.CreateNew))
                {
                    Hinh.CopyTo(myfile);
                }
                return Hinh.FileName;
            }
            catch (Exception ex)
            {
                return String.Empty;
            }

        }

        public static string SaveImageFromBase64(string base64String, string folderName)
        {
            try
            {
                // Define the file path
                string imagePath = Path.Combine("wwwroot", "Images", folderName);
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                // Create unique file name
                string fileName = Guid.NewGuid().ToString() + ".png";
                string fullPath = Path.Combine(imagePath, fileName);

                // Remove prefix from base64 if it exists (e.g., data:image/png;base64,)
                var base64Data = base64String.Contains(",") ? base64String.Split(',')[1] : base64String;

                // Convert base64 to byte array
                byte[] imageBytes = Convert.FromBase64String(base64Data);

                // Write bytes to the file
                File.WriteAllBytes(fullPath, imageBytes);

                // Return the relative path to save in the database
                return Path.Combine("/Images", folderName, fileName).Replace("\\", "/");
            }
            catch
            {
                return null; // Handle exception accordingly
            }
        }

        public static string GenerateSlug(string phrase)
        {
            string str = phrase.ToLower();
            str = System.Text.RegularExpressions.Regex.Replace(str, @"[^a-z0-9\s]", "");
            str = System.Text.RegularExpressions.Regex.Replace(str, @"\s+", " ").Trim();
            str = str.Replace(" ", "");
            return str;
        }

        public static string GenerateRandomId(int length = 10)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] randomChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                randomChars[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(randomChars);
        }



    }
}
