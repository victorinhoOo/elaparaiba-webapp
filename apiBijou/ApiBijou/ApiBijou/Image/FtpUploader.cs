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

        /// <summary>
        /// Renvoi l'extension du fichier.
        /// </summary>
        /// <param name="file">file.</param>
        /// <returns></returns>
        public string GetFileType(IFormFile file)
        {
            return System.IO.Path.GetExtension(file.FileName).ToLower();
        }

        public string uploadBijouPhotos(List<IFormFile> files, string bijouType)
        {
            try
            {
                using (var ftp = new FtpClient(ftpServer, ftpUsername, ftpPassword))
                {
                    // Connexion au serveur FTP
                    ftp.Connect();
                    //Compter le nombre de dossier
                    var Listing = ftp.GetListing("Photosdescriptif" + bijouType);
                    int nbElement = Listing.Count();
                    //Création du répertoire
                    string destinationDirectory = bijouType + Convert.ToString((nbElement + 100));
                    ftp.CreateDirectory(Path.Combine("Photosdescriptif" + bijouType, destinationDirectory)); 
                    //Ajout des fichiers
                    int indexFile = 1;
                    // Parcour chaque fichier 
                    foreach (var file in files)
                    {

                        // Construction du chemin
                        string remoteFilePath = Path.Combine("Photosdescriptif" + bijouType , destinationDirectory, indexFile.ToString() + GetFileType(file));

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
                    return destinationDirectory;               
                }
            }
            catch (FtpException ex)
            {
                throw new Exception($"Erreur lors du téléversement des fichiers sur le serveur FTP : {ex.Message}", ex);
            }
        }

        public void ModifieBijouPhotos(List<IFormFile> files, string bijouType, string dossierPhoto)
        {
            try
            {
                using (var ftp = new FtpClient(ftpServer, ftpUsername, ftpPassword))
                {
                    // Connexion au serveur FTP
                    ftp.Connect();
                    //Path du répertoire
                    string directoryPath = Path.Combine("Photosdescriptif" + bijouType, dossierPhoto);
                    // Obtenez la liste des fichiers dans le répertoire
                    var filesToDelete = ftp.GetListing(directoryPath);

                    // Supprimez chaque fichier dans le répertoire
                    foreach (var fileToDelete in filesToDelete)
                    {
                        ftp.DeleteFile(fileToDelete.FullName);
                    }

                    //Ajout des fichiers
                    int indexFile = 1;
                    // Parcour chaque fichier 
                    foreach (var file in files)
                    {
                        // Construction du chemin
                        string remoteFilePath = Path.Combine(directoryPath, indexFile.ToString() + GetFileType(file));

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
    }
}
