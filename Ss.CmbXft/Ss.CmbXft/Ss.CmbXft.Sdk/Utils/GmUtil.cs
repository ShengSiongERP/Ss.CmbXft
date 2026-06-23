using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.X509;

namespace Ss.CmbXft.Sdk.Utils;

public class GmUtil
{
    public static string Sm3WithSm2Signature(string privateKeyHex, string msg)
    {
        byte[] msgBytes = Encoding.UTF8.GetBytes(msg);
        byte[] userId = Encoding.UTF8.GetBytes("1234567812345678"); // 默认用户ID
        
        BigInteger d = new BigInteger(privateKeyHex, 16);
        ECPrivateKeyParameters privateKey = GetPrivatekeyFromD(d);
        
        byte[]? signDer = SignSm3WithSm2Asn1Rs(msgBytes, userId, privateKey);
        if (signDer == null) return string.Empty;
        
        byte[] signPlain = RsAsn1ToPlainByteArray(signDer);
        return Hex.ToHexString(signPlain);
    }

    private static readonly X9ECParameters x9ECParameters = GMNamedCurves.GetByName("sm2p256v1");
    private static readonly ECDomainParameters ecDomainParameters = new ECDomainParameters(x9ECParameters.Curve, x9ECParameters.G, x9ECParameters.N, x9ECParameters.H);

    /**
      *
      * @param msg
      * @param userId
      * @param privateKey
      * @return sign result in asn1 format
      */
    public static byte[]? SignSm3WithSm2Asn1Rs(byte[] msg, byte[] userId, AsymmetricKeyParameter privateKey)
    {
        try
        {
            ISigner signer = SignerUtilities.GetSigner("SM3withSM2");
            signer.Init(true, new ParametersWithID(privateKey, userId));
            signer.BlockUpdate(msg, 0, msg.Length);
            byte[] sig = signer.GenerateSignature();
            return sig;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static bool VerifySm3WithSm2(byte[] msg, byte[] userId, byte[] rs, AsymmetricKeyParameter publicKey)
    {
        if (rs == null || msg == null || userId == null) return false;
        if (rs.Length != RS_LEN * 2) return false;
        byte[]? asn1Rs = RsPlainByteArrayToAsn1(rs);
        if (asn1Rs == null) return false;
        return VerifySm3WithSm2Asn1Rs(msg, userId, asn1Rs, publicKey);
    }

    public static bool VerifySm3WithSm2Asn1Rs(byte[] msg, byte[] userId, byte[] sign, AsymmetricKeyParameter publicKey)
    {
        try
        {
            ISigner signer = SignerUtilities.GetSigner("SM3withSM2");
            signer.Init(false, new ParametersWithID(publicKey, userId));
            signer.BlockUpdate(msg, 0, msg.Length);
            return signer.VerifySignature(sign);
        }
        catch
        {
            return false;
        }
    }

    private static byte[] ChangeC1C2C3ToC1C3C2(byte[] c1c2c3)
    {
        int c1Len = (x9ECParameters.Curve.FieldSize + 7) / 8 * 2 + 1;
        const int c3Len = 32;
        byte[] result = new byte[c1c2c3.Length];
        Buffer.BlockCopy(c1c2c3, 0, result, 0, c1Len);
        Buffer.BlockCopy(c1c2c3, c1c2c3.Length - c3Len, result, c1Len, c3Len);
        Buffer.BlockCopy(c1c2c3, c1Len, result, c1Len + c3Len, c1c2c3.Length - c1Len - c3Len);
        return result;
    }

    private static byte[] ChangeC1C3C2ToC1C2C3(byte[] c1c3c2)
    {
        int c1Len = (x9ECParameters.Curve.FieldSize + 7) / 8 * 2 + 1;
        const int c3Len = 32;
        byte[] result = new byte[c1c3c2.Length];
        Buffer.BlockCopy(c1c3c2, 0, result, 0, c1Len);
        Buffer.BlockCopy(c1c3c2, c1Len + c3Len, result, c1Len, c1c3c2.Length - c1Len - c3Len);
        Buffer.BlockCopy(c1c3c2, c1Len, result, c1c3c2.Length - c3Len, c3Len);
        return result;
    }

    public static byte[]? Sm2Decrypt(byte[] data, AsymmetricKeyParameter key)
    {
        return Sm2DecryptOld(ChangeC1C3C2ToC1C2C3(data), key);
    }

    public static byte[]? Sm2Encrypt(byte[] data, AsymmetricKeyParameter key)
    {
        byte[]? encrypted = Sm2EncryptOld(data, key);
        if (encrypted == null) return null;
        return ChangeC1C2C3ToC1C3C2(encrypted);
    }

    public static byte[]? Sm2EncryptOld(byte[] data, AsymmetricKeyParameter pubkey)
    {
        try
        {
            SM2Engine sm2Engine = new SM2Engine();
            sm2Engine.Init(true, new ParametersWithRandom(pubkey, new SecureRandom()));
            return sm2Engine.ProcessBlock(data, 0, data.Length);
        }
        catch
        {
            return null;
        }
    }

    public static byte[]? Sm2DecryptOld(byte[] data, AsymmetricKeyParameter key)
    {
        try
        {
            SM2Engine sm2Engine = new SM2Engine();
            sm2Engine.Init(false, key);
            return sm2Engine.ProcessBlock(data, 0, data.Length);
        }
        catch
        {
            return null;
        }
    }

    public static byte[]? Sm3(byte[] bytes)
    {
        try
        {
            SM3Digest digest = new SM3Digest();
            digest.BlockUpdate(bytes, 0, bytes.Length);
            byte[] result = DigestUtilities.DoFinal(digest);
            return result;
        }
        catch
        {
            return null;
        }
    }

    private const int RS_LEN = 32;

    private static byte[] BigIntToFixexLengthBytes(BigInteger rOrS)
    {
        byte[] rs = rOrS.ToByteArray();
        if (rs.Length == RS_LEN) return rs;
        else if (rs.Length == RS_LEN + 1 && rs[0] == 0) return Arrays.CopyOfRange(rs, 1, RS_LEN + 1);
        else if (rs.Length < RS_LEN)
        {
            byte[] result = new byte[RS_LEN];
            Arrays.Fill(result, (byte)0);
            Buffer.BlockCopy(rs, 0, result, RS_LEN - rs.Length, rs.Length);
            return result;
        }
        else
        {
            throw new ArgumentException("err rs: " + Hex.ToHexString(rs));
        }
    }

    private static byte[] RsAsn1ToPlainByteArray(byte[] rsDer)
    {
        Asn1Sequence seq = Asn1Sequence.GetInstance(rsDer);
        byte[] r = BigIntToFixexLengthBytes(DerInteger.GetInstance(seq[0]).Value);
        byte[] s = BigIntToFixexLengthBytes(DerInteger.GetInstance(seq[1]).Value);
        byte[] result = new byte[RS_LEN * 2];
        Buffer.BlockCopy(r, 0, result, 0, r.Length);
        Buffer.BlockCopy(s, 0, result, RS_LEN, s.Length);
        return result;
    }

    private static byte[]? RsPlainByteArrayToAsn1(byte[] sign)
    {
        if (sign.Length != RS_LEN * 2) throw new ArgumentException("err rs. ");
        BigInteger r = new BigInteger(1, Arrays.CopyOfRange(sign, 0, RS_LEN));
        BigInteger s = new BigInteger(1, Arrays.CopyOfRange(sign, RS_LEN, RS_LEN * 2));
        Asn1EncodableVector v = new Asn1EncodableVector();
        v.Add(new DerInteger(r));
        v.Add(new DerInteger(s));
        try
        {
            return new DerSequence(v).GetEncoded("DER");
        }
        catch
        {
            return null;
        }
    }

    public static AsymmetricCipherKeyPair? GenerateKeyPair()
    {
        try
        {
            ECKeyPairGenerator kpGen = new ECKeyPairGenerator();
            kpGen.Init(new ECKeyGenerationParameters(ecDomainParameters, new SecureRandom()));
            return kpGen.GenerateKeyPair();
        }
        catch
        {
            return null;
        }
    }

    public static ECPrivateKeyParameters GetPrivatekeyFromD(BigInteger d)
    {
        return new ECPrivateKeyParameters(d, ecDomainParameters);
    }

    public static ECPublicKeyParameters GetPublickeyFromXY(BigInteger x, BigInteger y)
    {
        return new ECPublicKeyParameters(x9ECParameters.Curve.CreatePoint(x, y), ecDomainParameters);
    }

    public static AsymmetricKeyParameter? GetPublickeyFromX509File(FileInfo file)
    {
        FileStream? fileStream = null;
        try
        {
            fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
            X509Certificate certificate = new X509CertificateParser().ReadCertificate(fileStream);
            return certificate.GetPublicKey();
        }
        catch
        {
        }
        finally
        {
            if (fileStream != null)
                fileStream.Close();
        }
        return null;
    }

    public class Sm2Cert
    {
        public AsymmetricKeyParameter? privateKey;
        public AsymmetricKeyParameter? publicKey;
        public String? certId;
    }
}
