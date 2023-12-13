namespace ApiBijou.Image
{
    /// <summary>
    /// Interface to upload a File on the servor.
    /// </summary>
    public interface IFileUploader
    {
        /// <summary>
        /// Publie des fichiers
        /// </summary>
        /// <param name="files">Fichiers à publier</param>
        /// <param name="destinationDirectory">Dossier de destination</param>
        /// <param name="imageType">Type du bijou de l'image</param>
        /// <returns></returns>
        public void UploadFiles(List<IFormFile> files, string destinationDirectory);

        /// <summary>
        /// Renvoi le nombre d'éléments dans un Dossier
        /// </summary>
        /// <param name="directoryName">Nom du Dossier</param>
        /// <returns></returns>
        public int? NbOfElementsDirectory(string directoryName);

        /// <summary>
        /// Supprime un répertoire
        /// </summary>
        /// <param name="directoryName">Repertoire à supprimer</param>
        public void SupprimerRepertoire(string directoryName);
    }
}
