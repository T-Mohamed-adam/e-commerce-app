namespace TagerProject.Helpers
{
    public class MembershipNumberHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MembershipNumberHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        // Get membership number from auth data
        public string GetMembershipNumber()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated == true)
            {
                // Assuming MembershipNumber is stored as a claim
                var membershipNumber = user.Claims.FirstOrDefault(c => c.Type == "MembershipNumber")?.Value;
                return membershipNumber ?? throw new Exception("MembershipNumber claim not found");
            }

            throw new Exception("User is not authenticated");
        }
    }
}
