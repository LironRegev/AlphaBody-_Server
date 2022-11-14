using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaBodyAPI.Controllers
{
    public class AlphaBodyControllerBase : ControllerBase
    {
        public ActionResult OkOrBadRequest(bool success)
        {
            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }

        public ActionResult<T> OkOrBadRequest<T>(T result)
        {
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        public ActionResult OkOrUnauthorized(bool success)
        {
            if (!success)
            {
                return Unauthorized();
            }

            return Ok();
        }

        public ActionResult<T> OkOrUnauthorized<T>(T result)
        {
            if (result == null)
            {
                return Unauthorized();
            }

            return Ok(result);
        }
    }
}
