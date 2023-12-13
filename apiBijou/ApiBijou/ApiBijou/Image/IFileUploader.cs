using ApiBijou.Model.Bijoux;

namespace ApiBijou.Image
{
    /// <summary>
    /// Interface to upload a File on the servor.
    /// </summary>
    public interface IFileUploader
    {
        /// <summary>
        /// Publie les photos d'un nouveau bijou
        /// </summary>
        /// <param name="files">Photos du bijou</param>
        /// <param name="bijouType">Type du bijou</param>
        /// <returns></returns>
        public string uploadBijouPhotos(List<IFormFile> files, string bijouType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="files">Photos du bijou</param>
        /// <param name="bijouTtype">Type du bijou</param>
        /// <param name="dossierPhoto">Dossier photo du bijou</param>
        /// <returns></returns>
        public void ModifieBijouPhotos(List<IFormFile> files, string bijouType, string dossierPhoto);
    }
}
