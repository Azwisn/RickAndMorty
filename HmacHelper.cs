using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;

class HmacHelper
{
    public byte[] HmacSha3_256(byte[] key, byte[] data)
    {
        var hmac = new HMac(new Sha3Digest(256));
        hmac.Init(new KeyParameter(key));
        hmac.BlockUpdate(data, 0, data.Length);
        byte[] outb = new byte[hmac.GetMacSize()];
        hmac.DoFinal(outb, 0);
        return outb;
    }
}
