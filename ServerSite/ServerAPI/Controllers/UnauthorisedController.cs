using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ServerAPI.Controllers
{
    [RoutePrefix("api/unauthorised")]
    public class UnauthorisedController : ApiController
    {

        [HttpGet]
        [ActionName("error")]
        public virtual IHttpActionResult GetAll()
        {
            try
            {
                return Ok(" Test Reject ");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}