using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.WebApi;
using RoskildeTasks.Api.Models;
using System.Net;

namespace RoskildeTasks.Api.Controllers
{
    public class MembersController : UmbracoApiController
    {
        [RoleAuthorize]
        [HttpGet]
        public IHttpActionResult Init()
        {
            var currentUser = Members.CurrentUserName;

            IMemberService ms = Services.MemberService;

            if(ms.GetByUsername(currentUser).IsApproved)
            {
                return StatusCode(HttpStatusCode.OK);
            }
            else
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            }
        }
    }
}
