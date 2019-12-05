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
using System.Web;
using Newtonsoft.Json;

namespace RoskildeTasks.Api.Controllers
{
    public class MemberController : UmbracoApiController
    {
        [RoleAuthorize]
        [HttpGet]
        public IHttpActionResult Init()
        {
            var user = Members.CurrentUserName;

            IMemberService ms = Services.MemberService;

            var currentUser = ms.GetByUsername(user);

            if (currentUser.IsApproved)
            {
                MemberItem member = new MemberItem();
                member.Name = currentUser.GetValue<string>("memberName");
                member.Company = currentUser.GetValue<string>("memberCompany");
                member.Email = currentUser.Email;
                member.Status = "Active";
                member.AccessGroups = Roles.GetRolesForUser(user);

                return Content(HttpStatusCode.OK, member);
            }
            else
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            }
        }

        [RoleAuthorize]
        [HttpPost]
        public IHttpActionResult ChangePassword()
        {
            var user = Members.CurrentUserName;

            IMemberService ms = Services.MemberService;

            var currentUser = ms.GetByUsername(user);

            if (currentUser.IsApproved)
            {
                string Json = Functions.GetJsonFromStream(HttpContext.Current.Request.InputStream);

                Models.DTO.Password password = new Models.DTO.Password();

                try
                {
                    password = JsonConvert.DeserializeObject<Models.DTO.Password>(Json);
                }
                catch
                {
                    return BadRequest("Json parser error:" + Json);
                }

                var memberShipHelper = new Umbraco.Web.Security.MembershipHelper(Umbraco.UmbracoContext);

                if (memberShipHelper.Login(currentUser.Username, password.OldPassword))
                {
                    ms.SavePassword(currentUser, password.NewPassword);
                    return Ok();
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Old password is not correct");
                }

            }
            else
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            }
        }
        [RoleAuthorize]
        [HttpPost]
        public IHttpActionResult ChangeEmail()
        {
            var user = Members.CurrentUserName;

            IMemberService ms = Services.MemberService;

            var currentUser = ms.GetByUsername(user);

            if (currentUser.IsApproved)
            {
                string Json = Functions.GetJsonFromStream(HttpContext.Current.Request.InputStream);

                Models.DTO.Email email = new Models.DTO.Email();

                try
                {
                    email = JsonConvert.DeserializeObject<Models.DTO.Email>(Json);
                }
                catch
                {
                    return BadRequest("Json parser error:" + Json);
                }

                currentUser.Email = email.NewEmail;
                currentUser.Username = email.NewEmail;
                ms.Save(currentUser);
                return Ok();

            }
            else
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            }
        }
    }
}
