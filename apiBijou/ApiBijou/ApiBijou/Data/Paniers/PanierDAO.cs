using ApiBijou.Model.Bijoux;
using ApiBijou.Model.Panier;
using Newtonsoft.Json; // Assurez-vous d'avoir ajouté la référence à la bibliothèque Newtonsoft.Json

namespace ApiBijou.Data.Paniers
{
    public class PanierDAO : IPanierDAO
    {

        public void AjouterBijouAuPanier(int idPanier, Bijou bijou)
        {
            string contenuFichier = File.ReadAllText(ObtenirCheminJson(idPanier));
            Panier panier = JsonConvert.DeserializeObject<Panier>(contenuFichier);
            panier.AddBijoux(bijou);
            UpdatePanier(idPanier, panier);
        }

        public List<PanierItem> ObtenirPanier(int idPanier)
        {
            string contenuFichier = File.ReadAllText(ObtenirCheminJson(idPanier));
            Panier panier = JsonConvert.DeserializeObject<Panier>(contenuFichier);
            return panier.GetBijoux();
        }

        public void SupprimerBijouDuPanier(int idPanier, Bijou bijou)
        {
            string contenuFichier = File.ReadAllText(ObtenirCheminJson(idPanier));
            Panier panier = JsonConvert.DeserializeObject<Panier>(contenuFichier);
            panier.DelBijoux(bijou);
            UpdatePanier(idPanier, panier);
        }

        public void CreerPanier(int idPanier)
        {
            //Création du panier 
            Panier panierBijoux = new Panier();
            //Ecriture du bijou dans le panier
            File.WriteAllText(ObtenirCheminJson(idPanier), JsonConvert.SerializeObject(panierBijoux));
        }

        /// <summary>
        /// Met à jour le panier
        /// </summary>
        /// <param name="idPanier">Id du panier à mettre à jour</param>
        /// <param name="panierBijoux">Nouveau panier à hydrater</param>
        private void UpdatePanier(int idPanier, Panier panierBijoux)
        {
            File.WriteAllText(ObtenirCheminJson(idPanier), JsonConvert.SerializeObject(panierBijoux));
        }

        /// <summary>
        /// Créer le fichier json
        /// </summary>
        /// <param name="idPanier"></param>
        public void CreerPanierJson(int idPanier)
        {
            string cheminAbsolu = ObtenirCheminJson(idPanier);

            try
            {
                // Vérifie si le répertoire existe, sinon, le crée
                string dossierParent = Path.GetDirectoryName(cheminAbsolu);
                if (!Directory.Exists(dossierParent))
                {
                    Directory.CreateDirectory(dossierParent);
                }

                // Création du fichier
                File.Create(cheminAbsolu).Close();
                Console.WriteLine("Le fichier a été créé avec succès !");
            }
            catch (Exception e)
            {
                Console.WriteLine("Une erreur s'est produite : " + e.Message);
            }
        }
        /// <summary>
        /// Renoi chemin du fichier json
        /// </summary>
        /// <param name="idPanier">id du panier sérialisé</param>
        /// <returns></returns>
        public string ObtenirCheminJson(int idPanier)
        {
            string cheminRelatif = Path.Combine("..", "panierData", Convert.ToString(idPanier) + ".json");
            return Path.GetFullPath(cheminRelatif);
        }
    }
}

