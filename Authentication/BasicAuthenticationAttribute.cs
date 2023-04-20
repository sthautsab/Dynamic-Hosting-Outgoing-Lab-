using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Basic_Authentication.Authentication
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Provide Username and Password");
            }
            else
            {
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedAuthenticationToken = Encoding.UTF8.GetString(
                    Convert.FromBase64String(authenticationToken));
                string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
                string username = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];
                var user = Validate.validateUser(username, password);

                if (user != null)
                {
                    string rolesString = user.Role;
                    string[] roles = rolesString.Split(',')
                                .Select(r => r.Trim())
                                .Where(r => !string.IsNullOrWhiteSpace(r))
                                .ToArray();
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), roles);
                    HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(username), roles);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid Username or Password");
                }
            }
        }
    }
}