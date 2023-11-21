using ApiBijou.Model.Bijoux;
using ApiBijou.Model.formModel;
using Microsoft.AspNetCore.Mvc;

namespace ApiBijou.Controllers
{
    [ApiController]
    [Route("Administration")]
    public class AdministrationController : ControllerBase
    {
        /// <summary>
        /// Modifie un bijou
        /// </summary>
        /// <param name="formulaire"></param>
        /// <returns></returns>
        [HttpPost("ModifierBijou")]
        public IActionResult ModifierBijou([FromForm] FormulaireBijouModified formulaire)
        {
            ActionResult result = BadRequest("Erreur lors de la modification");
            try
            {
                bool bijModifier = BijouManager.Instance.modifierBijou(formulaire);
                if (bijModifier)
                {
                    result = Ok();
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(ex);
            }
            return result;
        }
    }
}
