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
        mortyValue = RandomNumberGenerator.GetInt32(range);
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
