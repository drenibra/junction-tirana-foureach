using System.Security.Claims;

namespace RFMoneyMatters.CurrentUser.ExtensionMethods
{
    public static class ClaimPrincipleExtensions
    {

        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public static string GetUserEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;
        }
        public static IEnumerable<string> GetUserRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();
        }
    }
}
