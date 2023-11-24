using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Cors;
using ApiBijou.Model.Bijoux;
using ApiBijou.Model.Mail;
using Newtonsoft.Json;
using ApiBijou.Model.formModel;

namespace API_SAE.Controllers
{
    /// <summary>
    /// Controller de l'api bijoux
    /// </summary>
    [ApiController]
    [Route("Bijoux")]
    public class BijouxController : ControllerBase
    {
        public BijouxController()
        {

        }

        /// <summary>
        /// Vérifie la validité d'un ID et renvoie un bijou correspondant s'il existe.
        /// </summary>
        /// <param name="id">L'ID du bijou à vérifier.</param>
        /// <returns>
        /// Un ActionResult contenant le bijou s'il existe, NotFound si l'ID est valide mais ne correspond à aucun bijou,
        /// ou BadRequest si aucun ID n'est spécifié.
        /// </returns>
        [HttpGet("GetBijouWithId", Name = "Check")]
        public ActionResult<Bijou> Check(int? id)
        {
            ActionResult<Bijou> result = BadRequest("No id specified");

            if (id.HasValue)
            {
                result = NotFound();

                Bijou? bijou = BijouManager.Instance.GetBijouById(id.Value);

                if (bijou != null)
                {
                    result = Ok(bijou); // Si le bijou existe, change la réponse en Ok avec le bijou
                }
            }

            return result;
        }

        /// <summary>
        /// Récupère la liste de tous les bijoux.
        /// </summary>
        /// <returns>
        /// Un ActionResult contenant la liste de tous les bijoux, 
        /// ou BadRequest si une erreur survient lors de la récupération.
        /// </returns>
        [HttpGet("GetAllBijoux")]
        public ActionResult<IEnumerable<Bijou>> GetAllBijoux()
        {
            ActionResult<IEnumerable<Bijou>> reponse = BadRequest();

            IEnumerable<Bijou> users = BijouManager.Instance.GetAllBijoux();

            if (users != null)
            {
                reponse = Ok(users); // Si la liste de bijoux est valide, change la réponse en Ok avec la liste
            }

            return reponse;
        }

        /// <summary>
        /// Envoi un formulaire sur Mesure
        /// </summary>
        /// <param name="formulaire"></param>
        /// <returns></returns>
        [HttpPost("EnvoyerFormulaireSurMesure")]
        public IActionResult EnvoyerFormulaireSurMesure([FromForm] FormulaireSurMesureData formulaire)
        {
            ActionResult result = BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    // Utilisez directement l'objet FormulaireSurMesureModel pour récupérer les données
                    SurMesureMail mailBuilder = new SurMesureMail(formulaire);

                    result = Ok("Formulaire soumis avec succès !");
                }
                catch (Exception ex)
                {
                    result = StatusCode(500, "Une erreur s'est produite lors de la tentative d'envoi du formulaire.");
                }
            }
            else
            {
                result = BadRequest(ModelState);
            }

            return result;
        }

        /// <summary>
        /// Envoi un formulaire sur Mesure
        /// </summary>
        /// <param name="formulaire"></param>
        /// <returns></returns>
        [HttpPost("EnvoyerFormulaireOuMeTrouver")]
        public IActionResult EnvoyerFormulaireOuMeTrouver([FromForm] FormulaireOuMeTrouverData formulaire)
        {
            ActionResult result = BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    // Utilisez directement l'objet FormulaireSurMesureModel pour récupérer les données
                    OuMeTrouverMail mailBuilder = new OuMeTrouverMail(formulaire);
                    result = Ok("Formulaire soumis avec succès !");
                }
                catch (Exception ex)
                {
                    result = StatusCode(500, "Une erreur s'est produite lors de la tentative d'envoi du formulaire.");
                }
            }
            else
            {
                result = BadRequest(ModelState);
            }

            return result;
        }

    }
}