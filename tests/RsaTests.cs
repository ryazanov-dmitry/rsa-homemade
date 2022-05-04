using System;
using System.Diagnostics;
using Open.Numeric.Primes;
using rsa;
using Xunit;

namespace tests;

public class RsaTests
{
    [InlineData(9,17,13)]
    [InlineData(17,99,91)]
    [Theory]
    public void SquareMod(int x, int mod, int exp)
    {
        // When
        var result = Rsa.SqMod(x, mod);

        // Then
        Assert.Equal(exp, result);
    }

    [InlineData(5, 7, 11, 10)]
    [InlineData(17, 17, 99, 62)]
    [Theory]
    public void SquareMultMod(int input, int x, int mod, int exp)
    {
        // When
        var result = Rsa.SqMulMod(input, x, mod);

        // Then
        Assert.Equal(exp, result);
    }

    [InlineData(7, 15, 13, 5)]
    [InlineData(7, 1, 10, 7)]
    [InlineData(7, 4, 11, 3)]
    [InlineData(7, 3, 11, 2)]
    [InlineData(7, 5, 11, 10)]
    [InlineData(7, 1234567, 11, 6)]
    [InlineData(7, 87654349, 87654349, 7)]
    [InlineData(3, 1111111111111, 7, 3)]
    [Theory]
    public void FastExp(long x, long pow, long mod, long exp)
    {
        // When
        // var s1 = new Stopwatch(); s1.Start();
        var y = Rsa.FastExp(x, pow, mod); 
        // s1.Stop(); Console.WriteLine(s1.Elapsed);

        // var s2 = new Stopwatch(); s2.Start();
        // var slow = x;
        // for (int i = 0; i < pow-1; i++)
        // {
        //     slow = slow * x % mod;
        // }
        // s2.Stop(); Console.WriteLine(s2.Elapsed);


        // Then
        Assert.Equal(exp, y);
    }
    [InlineData(87654349, true)]
    [InlineData(87654348, false)]
    [InlineData(872364823764382, false)]
    [Theory]
    public void PrimeCheck_Positive(long prime, bool exp)
    {
        // When
        bool isPrime = Rsa.IsPrime(prime);

        // Then
        Assert.Equal(exp, isPrime);
    }


    [Fact]
    public void GenerateLargePrime()
    {
        // When
        long largeP=0;

        for (int i = 0; i < 100; i++)
        {
            largeP = Rsa.GeneratePrime();
        }

        // Then
        Assert.True(Number.IsPrime(largeP));
    }
}