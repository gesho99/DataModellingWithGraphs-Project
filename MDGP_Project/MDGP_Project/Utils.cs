namespace MDGP_Project
{
    public static class Utils
    {
        public static List<int> CalculatePrimeFactors(int number)
        {
            var factors = new List<int>();

            for (int divisor = 2; divisor <= number / 2; divisor++)
            {
                if (number % divisor == 0)
                {
                    if (IsPrime(divisor))
                    {
                        factors.Add(divisor);
                    }
                }
            }

            return factors;
        }

        // Is this function ok, I saw in the net that the for loop starts from 3
        public static bool IsPrime(int number)
        {
            if (number == 1)
            {
                return false;
            }

            if (number == 2)
            {
                return true;
            }

            var limit = Math.Ceiling(Math.Sqrt(number)); //hoisting the loop limit

            for (int i = 2; i <= limit; ++i)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
