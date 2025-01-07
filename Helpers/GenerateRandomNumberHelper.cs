namespace TagerProject.Helpers
{
    public class GenerateRandomNumberHelper
    {
        public string GenerateMembershipNumber()
        {
            const string numbers = "0123456789";
            const int length = 7;

            var random = new Random();

            return new string(Enumerable.Repeat(numbers, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}
