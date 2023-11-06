using Microsoft.AspNetCore.Mvc;
using API_SAE.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Cors;
using ApiBijou.Model.formModel;
using ApiBijou.Model.SurMesure;
using Stripe;
using Stripe.Checkout;
using MySqlX.XDevAPI;
using Stripe.BillingPortal;

namespace API_SAE.Controllers
{
    /// <summary>
    /// Controller de l'api
    /// </summary>
    [ApiController]
    [Route("Bijoux")]
    public class BijouxController : ControllerBase
    {
        public BijouxController()
        {

        }

        [HttpGet("GetBijouWithId", Name = "Check")]
        public ActionResult<Bijou> Check(int? id)
        {
            ActionResult<Bijou> result = BadRequest("No id specified");

            if (id.HasValue)
            {
                result = NotFound();
                Bijou? user = BijouManager.Instance.GetBijouById(id.Value);
                if (user != null)
                {
                    result = Ok(user);
                }

            }

            return result;
        }

        [HttpGet("GetAllBijoux")]
        public ActionResult<IEnumerable<Bijou>> GetAllBijoux()
        {
            ActionResult<IEnumerable<Bijou>> reponse = BadRequest();
            IEnumerable<Bijou> users = BijouManager.Instance.GetAllBijoux();
            if (users != null)
            {
                reponse = Ok(users);
            }
            return reponse;
        }

        [HttpPost("EnvoyerFormulaireSurMesure")]
        public IActionResult EnvoyerFormulaireSurMesure([FromBody] FormulaireSurMesureModel formulaire)
        {
            ActionResult result = BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    MailBuilder mailBuilder = new MailBuilder(formulaire);
                    // Utilisez mailBuilder pour générer et envoyer le mail ici

                    return Ok("Formulaire soumis avec succès !");
                }
                catch (Exception ex)
                {
                    // Gérez l'exception ici, par exemple, en enregistrant les détails de l'erreur dans un journal.
                    return StatusCode(500, "Une erreur s'est produite lors de la tentative d'envoi de l'e-mail.");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}