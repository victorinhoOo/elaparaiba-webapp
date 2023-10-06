using Microsoft.AspNetCore.Mvc;
using API_SAE.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Cors;

namespace API_SAE.Controllers
{
    [ApiController]
    [Route("Bijoux")]
    public class BijouxController : ControllerBase
    {
        public BijouxController() 
        {

        }

        [HttpGet("GetBijouWithId",Name ="Check")]
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

        [HttpPost("AddBijou")]

        public ActionResult AddUser([FromBody] Bijou? bijou)
        {
            ActionResult result = BadRequest();

            if (BijouManager.Instance.AddBijou(bijou))
            {
                result = Ok();
            }
            return result;
        }

        [HttpGet("DeleteBijouById")]

        public IActionResult DeleteUserById(int? id)
        {
            IActionResult result = BadRequest("No id specified");

            if (id.HasValue)
            {
                result = NotFound();
                if (BijouManager.Instance.DeleteBijouById(id.Value))
                {
                    result = Ok();
                }
            }
            return result;
        }

    }
}