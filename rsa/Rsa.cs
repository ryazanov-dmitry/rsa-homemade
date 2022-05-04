namespace rsa;
public class Rsa
{
    public static long SqMod(long x, long mod)
    {
        return x * x % mod;
    }

    public static long SqMulMod(long input, long x, long mod)
    {
        return (SqMod(input, mod) * x) % mod;
    }

    public static long FastExp(long x, long pow, long mod)
    {
        var binString = Convert.ToString(pow, 2);

        var a = x % mod;

        foreach (var curr in binString.Substring(1))
        {
            a = curr == '0' ? Rsa.SqMod(a, mod) : Rsa.SqMulMod(a, x, mod);
        }

        return a;
    }

    public static bool IsPrime(long largePrime, int sec = 10)
    {
        if (largePrime % 2 == 0 || largePrime % 5 == 0 || largePrime % 10 == 0)
            return false;

        for (int i = 0; i < sec; i++)
        {
            var a = new Random().Next(2, 100000);
            if (Rsa.FastExp(a, largePrime - 1, largePrime) != 1)
                return false;
        }

        return true;
    }

    public static long GeneratePrime()
    {
        var count = 0;
        long prime = new Random().NextInt64(1000000000);

        while (!Rsa.IsPrime(prime))
        {
            count++;
            prime -= 1;
            Console.WriteLine(count);
        }

        return prime;
    }
}
