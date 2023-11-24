using System.Security.Policy;

namespace ApiBijou.Data.Images
{
    public interface IImageDAO
    {
        /// <summary>
        /// Change les images d'un bijou.
        /// </summary>
        /// <param name="images">Nouvelles images à insérer</param>
        /// <param name="bijouType">Type du bijou</param>
        /// <param name="bijouDossier">Nom de dossier du bijou</param>
        public bool ChangerImageBijou(IFormFile images, string bijouType, string bijouDossier);

        /// <summary>
        /// Supprime le repertoire image d'un bijou.
        /// </summary>
        /// <param name="bijouType"></param>
        /// <param name="bijouDossier"></param>
        public void SupprimerImageBijou(string bijouType, string bijouDossier);

        /// <summary>
        /// Créer un dossier pour stocker les images du bijou.
        /// <param name="bijouType">Type du bijou</param>
        /// <param name="dossierName"></param>
        /// <returns></returns>
        public bool CreerDossierImage(string bijouType, string dossierName);
    }
}
