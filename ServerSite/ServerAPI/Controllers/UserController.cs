using Newtonsoft.Json;
using ServerAPI.ActionFilters;
using Ss.Core;
using Ss.Data.Models;
using Ss.Data.ModelViews;
using Ss.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ServerAPI.Controllers
{
    [RoutePrefix("api/user")]
    [Authorize]
    public class UserController : BaseController<User, UserView>
    {
        public UserController(IUserService userService) : base(userService)
        {

        }

        [NonAction]
        public static async Task<dynamic> HttpClientPostAsync(string baseAddress, string requestUri, HttpContent postData, string tokenKey = null)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(tokenKey))
                    client.DefaultRequestHeaders.Add("Authorization", tokenKey);

                client.BaseAddress = new Uri(baseAddress);
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    var response = await client.PostAsync(requestUri, postData);
                    return
                        new { HasError = false, Result = await response.Content.ReadAsStringAsync(), ResponseUri = (response.Headers == null || response.Headers.Location == null) ? null : response.Headers.Location.AbsoluteUri };
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        [HttpPost]
        [ActionName("login")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Login([FromUri]string username, [FromUri] string password)
        {
            try
            {
                var user = _service.GetSingle(o => o.UserName.Equals(username) && o.Password.Equals(password));
                if (user == null)
                {

                }
                var resultInfo = await HttpClientPostAsync(ControllerContext.Request.RequestUri.GetLeftPart(UriPartial.Authority), "token", new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("login_type", "auth"),
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("password", password),
                        new KeyValuePair<string, string>("grant_type", "password")
                    }));

                if (resultInfo.HasError) throw new Exception();
                var responseObject = JsonConvert.DeserializeObject<dynamic>(resultInfo.Result);
                return Ok(responseObject);
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        [RbacAuthorize]
        public override IHttpActionResult GetAll()
        {
            LogHelper.LogDebug("Request GetAll user");

            return base.GetAll();
        }

    }
}