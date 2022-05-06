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
        if (largePrime < 4) return true;

        if (largePrime % 2 == 0 || largePrime % 5 == 0 || largePrime % 10 == 0)
            return false;

        for (int i = 0; i < sec; i++)
        {
            var a = new Random().NextInt64(2, largePrime - 2);
            if (Rsa.FastExp(a, largePrime - 1, largePrime) != 1)
                return false;
        }

        return true;
    }

    public static long GeneratePrime(long lessThan = 10000)
    {
        long prime = new Random().NextInt64(1, lessThan);

        while (!Rsa.IsPrime(prime))
        {
            prime -= 1;
        }

        return prime;
    }

    public static long Inverse(long r1, long r0)
    {
        var i = 1;
        var r = new List<long>() { r0, r1 };
        var q = new List<long>() { 1 };
        var s = new List<long>() { 1, 0 };
        var t = new List<long>() { 0, 1 };

        do
        {
            i++;
            r.Add(r[i - 2] % r[i - 1]);
            q.Add((r[i - 2] - r[i]) / r[i - 1]);
            s.Add(s[i - 2] - (q[i - 1] * s[i - 1]));
            t.Add(t[i - 2] - (q[i - 1] * t[i - 1]));

        } while (r[i] != 0);

        var inverse = t[i - 1];
        if(inverse < 0)
            return inverse + r0;

        return t[i - 1];
    }
}
