namespace API.Helpers.Utilities
{
    public static class RandomNumber
    {
        private static readonly Random random = new Random();

        public static string GenerateUniqueIdentifier()
        {
            // Generate a random number between 100000 and 999999
            int randomNumber = random.Next(100000, 999999);

            // Get the current timestamp in milliseconds
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            // Combine the random number and timestamp to create a unique identifier
            string uniqueId = $"{timestamp}_{randomNumber}";

            return uniqueId;
        }
    }
}