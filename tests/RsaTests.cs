using System;
using System.Diagnostics;
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
    [Theory]
    public void FastExp(int x, int pow, int mod, int exp)
    {
        // When
        // var s1 = new Stopwatch(); s1.Start();
        var y = Rsa.FastExp(x, pow, mod); 
        // s1.Stop(); Console.WriteLine(s1.Elapsed);

        // var s2 = new Stopwatch(); s2.Start();
        var slow = x;
        for (int i = 0; i < pow-1; i++)
        {
            slow = slow * x % mod;
        }
        // s2.Stop(); Console.WriteLine(s2.Elapsed);


        // Then
        Assert.Equal(exp, y);
    }
  
}