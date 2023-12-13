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
        /// <param name="bijouType">Type du bijou.</param>
        public string UploadPhotoBijou(List<IFormFile> files, string bijouType)
        {
            return fileUploader.uploadBijouPhotos(files, bijouType);
        }

        /// <summary>
        /// Modifie les photos d'un bijou
        /// </summary>
        /// <param name="files">Fichiers à publier.</param>
        /// <param name="bijouType">Type du bijou.</param>
        /// <param name="dossierPhoto">Dossier contenant les anciennes photos.</param>
        public void ModifiePhotoBijou(List<IFormFile> files, string bijouType, string dossierPhoto)
        {
            fileUploader.ModifieBijouPhotos(files, bijouType, dossierPhoto);
        }
    }
}
