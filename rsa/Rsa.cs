namespace rsa;
public class Rsa
{
    public static int SqMod(int x, int mod)
    {
        return x * x % mod;
    }

    public static int SqMulMod(int input, int x, int mod)
    {
        return (SqMod(input, mod) * x) % mod;
    }

    public static int FastExp(int x, int pow, int mod)
    {
        var binString = Convert.ToString(pow, 2);

        var a = x % mod;

        foreach (var curr in binString.Substring(1))
        {
            a = curr == '0' ? Rsa.SqMod(a, mod) : Rsa.SqMulMod(a, x, mod);
        }

        return a;
    }
}
