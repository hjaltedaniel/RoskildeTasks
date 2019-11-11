using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Security;

namespace RoskildeTasks.Api
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            bool basicValidated = false;
            var req = actionContext.Request;
            var auth = req.Headers.Authorization.ToString();
            if (!string.IsNullOrEmpty(auth))
            {
                var cred = System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
                var userName = cred[0];
                var pass = cred[1];
                basicValidated = Membership.ValidateUser(userName, pass);
                if (!basicValidated)
                {
                    base.OnAuthorization(actionContext);
                }
                else
                {
                    var roles = System.Web.Security.Roles.GetRolesForUser(userName);
                    IPrincipal principal = new GenericPrincipal(
                        new GenericIdentity(userName), roles);
                    Thread.CurrentPrincipal = principal;
                    System.Web.HttpContext.Current.User = principal;
                }
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
        }
    }
}
