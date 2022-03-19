namespace InfluencerWannaBe.Infrastructure
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static async Task CreateRole()
        {

        }
    }
}
