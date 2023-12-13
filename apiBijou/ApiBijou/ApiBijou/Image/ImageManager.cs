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
        private void Upload(List<IFormFile> files)
        {
            fileUploader.UploadFiles(files, "Test");
        }

        /// <summary>
        /// Ajoute des images pour un nouveau bijou
        /// </summary>
        /// <param name="files">Liste des fichiers à ajouter.</param>
        /// <param name="typeBijou">Type de bijou</param>
        /// <returns>Repertoire ou les images ont été ajoutées</returns>
        private string AjouterImageBijou(List<IFormFile> files, string typeBijou)
        {
            
        }

        /// <summary>
        /// Modifie les images d'un bijou
        /// </summary>
        /// <param name="files">fichier à ajouter</param>
        /// <param name="dossierBijou">Dossier du bijou.</param>
        public void ModifierImageBijou(List<IFormFile> files, string typeBijou, string dossierBijou)
        {
            
            //On supprime le répertoire.
            fileUploader.SupprimerRepertoire("PhotosDescriptif");
            fileUploader.UploadFiles(files, dossierBijou);
        }
    }
}
