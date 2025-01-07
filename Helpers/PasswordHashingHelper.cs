using Microsoft.AspNetCore.Identity;

namespace TagerProject.Helpers
{
    public class PasswordHashingHelper
    {
        private readonly PasswordHasher<object> _passwordHasher;

        public PasswordHashingHelper(PasswordHasher<object> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        // Hash a password
        public string HashPassword(string plainPassword)
        {
            return _passwordHasher.HashPassword(null, plainPassword);
        }

        // Verify a password
        public bool VerifyPassword(string hashedPassword, string plainPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, plainPassword);
            return result == PasswordVerificationResult.Success;
        }
    }

}
