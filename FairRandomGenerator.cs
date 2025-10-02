using System.Numerics;
using System.Security.Cryptography;

class FairRandomGenerator
{
    private byte[] key = Array.Empty<byte>();
    private BigInteger mortyValue = 0;
    private string currentHmacHex = "";
    private readonly HmacHelper hmacHelper = new HmacHelper();

    public string StartGeneration(int range)
    {
        key = new byte[32];
        RandomNumberGenerator.Fill(key);
        mortyValue = RandomInt(range);
        byte[] mvBytes = mortyValue.ToByteArray();
        var h = hmacHelper.HmacSha3_256(key, mvBytes);
        currentHmacHex = ToHex(h);
        return currentHmacHex;
    }

    public (BigInteger mortyVal, BigInteger final) RevealAfterRick(int r, int range)
    {
        var final = (mortyValue + r) % range;
        return (mortyValue, final);
    }

    private BigInteger RandomInt(int range)
    {
        if (range <= 0) return 0;
        int bits = (int)Math.Ceiling(BigInteger.Log(range, 2));
        int bytes = Math.Max(1, (bits + 7) / 8);
        while (true)
        {
            byte[] b = new byte[bytes];
            RandomNumberGenerator.Fill(b);
            var val = new BigInteger(b.Concat(new byte[] { 0 }).ToArray());
            if (val < 0) val = BigInteger.Abs(val);
            if (val < range) return val;
        }
    }

    public string RevealKeyHex()
    {
        return ToHex(key);
    }

    public static string ToHex(byte[] b)
    {
        var sb = new System.Text.StringBuilder(b.Length * 2);
        foreach (var x in b) sb.Append(x.ToString("X2"));
        return sb.ToString();
    }

    public string CurrentHmacHex => currentHmacHex;
}
