using FluentFTP;
using FluentFTP.Exceptions;
using HeyRed.Mime;

namespace ApiBijou.Image
{
    /// <summary>
    /// Classe pour publier des fichiers grâce au client ftp du serveur.
    /// </summary>
    public class FtpUploader : IFileUploader
    {
        private string ftpServer = "access985092423.webspace-data.io";
        private string ftpUsername = "acc1291933552";
        private string ftpPassword = "imageAdministrationMdpFtp2342--";

        public int? NbOfElementsDirectory(string directoryName)
        {
            try
            {
                using (var ftp = new FtpClient(ftpServer, ftpUsername, ftpPassword))
                {
                    //Connexion au serveur ftp
                    ftp.Connect();
                    //Compter le nombre d'éléments du répertoire
                    int nbElement = ftp.GetListing(directoryName).Count();
                    ftp.Disconnect();
                    return nbElement;
                }
            }
            catch (FtpException ex)
            {
                throw new Exception($"Erreur lors de la récupération du nombre d'éléments dans le répertoire : {ex.Message}", ex);
            }

        }

        public void UploadFiles(List<IFormFile> files, string destinationDirectory)
        {
            try
            {
                using (var ftp = new FtpClient(ftpServer, ftpUsername, ftpPassword))
                {
                    // Connexion au serveur FTP
                    ftp.Connect();

                    
                    if (!ftp.DirectoryExists(destinationDirectory))//Le répertoire n'existe pas 
                    {
                        ftp.CreateDirectory(destinationDirectory);
                    }

                    int indexFile = 0;
                    // Parcour chaque fichier 
                    foreach (var file in files)
                    {
        
                        // Construction du chemin
                        string remoteFilePath = Path.Combine(destinationDirectory, indexFile.ToString() + "." + GetFileType(file));

                        // Lit le contenu du fichier en tableau d'octet
                        using (var memoryStream = new MemoryStream())
                        {
                            file.CopyTo(memoryStream);
                            byte[] fileBytes = memoryStream.ToArray();

                            // Téléverse le fichier vers le serveur FTP
                            ftp.UploadBytes(fileBytes, remoteFilePath, FtpRemoteExists.Overwrite);
                        }
                        indexFile++;
                    }
                    ftp.Disconnect();
                }
            }
            catch (FtpException ex)
            {
                throw new Exception($"Erreur lors du téléversement des fichiers sur le serveur FTP : {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Renvoi l'extension du fichier.
        /// </summary>
        /// <param name="file">file.</param>
        /// <returns></returns>
        public string GetFileType(IFormFile file)
        {
            return System.IO.Path.GetExtension(file.FileName);
        }

}
}
