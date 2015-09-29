using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BigFile.DLL;

namespace BigFile.BLL
{
    public static class UserAuthentication
    {
        public static void Authentication(this HttpContextBase httpcontext, ManageUser manageuser)
        {
            ClaimsIdentity _identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            _identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, manageuser.UserID.ToString()));
            _identity.AddClaim(new Claim(ClaimTypes.Name, manageuser.UserName));
            _identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity"));

            httpcontext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            httpcontext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties() { IsPersistent = true }, _identity);
        }

        public static void LoginOut(this HttpContextBase httpcontext)
        {
            httpcontext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public static string GetUserName(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier);
            return claim == null ? null : claim.Value;
        }
    }
}
