namespace ApiBijou.Image
{
    /// <summary>
    /// Manager des images 
    /// </summary>
    public class ImageManager
    {
        private IFileUploader fileUploader;
        
        public ImageManager()
        {
            fileUploader = new ApiBijou.Image.FtpUploader();
        }

        /// <summary>
        /// Publie des fichiers.
        /// </summary>
        /// <param name="files">Fichiers à publier.</param>
        public void Upload(List<IFormFile> files)
        {
            fileUploader.UploadFiles(files, "Test");
        }
    }
}
