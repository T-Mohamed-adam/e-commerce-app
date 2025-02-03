namespace TagerProject.Exceptions
{
    public class InsufficientQuantityException : Exception
    {
        public InsufficientQuantityException()
        {
        }

        public InsufficientQuantityException(int productId)
        : base($"Insufficient quantity for product with ID: {productId}") { }
    }
}
