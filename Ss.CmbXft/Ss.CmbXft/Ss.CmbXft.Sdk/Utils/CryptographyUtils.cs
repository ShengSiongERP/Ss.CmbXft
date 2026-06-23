using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Linq;
using System.Text;

namespace Ss.CmbXft.Sdk.Utils;

/// <summary>
/// 加密解密工具类（SM4）
/// </summary>
public class CryptographyUtils
{
    /// <summary>
    /// 计算 SM3 哈希
    /// </summary>
    public static string ComputeSm3Hash(string input)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        SM3Digest digest = new SM3Digest();
        digest.BlockUpdate(inputBytes, 0, inputBytes.Length);
        byte[] outputBytes = new byte[digest.GetDigestSize()];
        digest.DoFinal(outputBytes, 0);
        return ByteArrayToHexString(outputBytes).ToLower();
    }
    /// <summary>
    /// SM4 ECB 加密
    /// 明文 string + 十六进制密钥 → 十六进制密文
    /// </summary>
    public static string EncryptWithSm4Ecb(string plaintext, string key)
    {        // 密钥：十六进制字符串 → byte[]
        byte[] keyBytes = HexStringToByteArray(key);
        // 明文：string → byte[]
        byte[] inputBytes = Encoding.UTF8.GetBytes(plaintext);

        // 使用BouncyCastle实现SM4/ECB/PKCS5Padding加密
        SM4Engine sm4Engine = new SM4Engine();
        PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(sm4Engine, new Pkcs7Padding());
        KeyParameter keyParam = new KeyParameter(keyBytes);
        cipher.Init(true, keyParam); // true表示加密

        byte[] outputBytes = new byte[cipher.GetOutputSize(inputBytes.Length)];
        int length = cipher.ProcessBytes(inputBytes, 0, inputBytes.Length, outputBytes, 0);
        cipher.DoFinal(outputBytes, length);

        return BitConverter.ToString(outputBytes).Replace("-", "");// 将字节数组转换为十六进制字符串
    }

    /// <summary>
    /// SM4 ECB 解密
    /// 十六进制密文 + 十六进制密钥 → 明文
    /// </summary>
    public static string DecryptWithSm4Ecb(string cipherText, string key)
    {        // 密钥：十六进制字符串 → byte[]
        byte[] keyBytes = HexStringToByteArray(key);
        // 密文：十六进制字符串 → byte[]
        byte[] inputBytes = HexStringToByteArray(cipherText);

        // 使用BouncyCastle实现SM4/ECB/PKCS5Padding解密
        SM4Engine sm4Engine = new SM4Engine();
        PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(sm4Engine, new Pkcs7Padding());
        KeyParameter keyParam = new KeyParameter(keyBytes);
        cipher.Init(false, keyParam); // false表示解密

        byte[] outputBytes = new byte[cipher.GetOutputSize(inputBytes.Length)];
        int length = cipher.ProcessBytes(inputBytes, 0, inputBytes.Length, outputBytes, 0);
        length += cipher.DoFinal(outputBytes, length);

        // byte[] → string
        return Encoding.UTF8.GetString(outputBytes, 0, length);
    }

    public static byte[] HexStringToByteArray(string hex)
    {        return Enumerable.Range(0, hex.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                         .ToArray();
    }

    /// <summary>
    /// 字节数组转换为十六进制字符串
    /// </summary>
    public static string ByteArrayToHexString(byte[] bytes)
    {        if (bytes == null || bytes.Length == 0)
            return string.Empty;

        return BitConverter.ToString(bytes).Replace("-", "");
    }
}
