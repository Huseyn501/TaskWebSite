namespace PreSchool.Helper
{
    public static class FileCreateExtention
    {
        public static string CreateFile(this IFormFile file, string folder,string root)
        {
            string fileName = "";
            if(file.FileName.Length > 64)
            {
                fileName = file.FileName.Substring(file.FileName.Length - 64);
            }
            else
            {
                fileName = Guid.NewGuid() + file.FileName;
            }
            string path = Path.Combine(root, folder, fileName);
            using(FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
    }
}
