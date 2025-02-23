﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// JsEncryptHelper 的摘要说明
/// </summary>
/// <summary>
/// AJax提交参数加密帮助类
/// </summary>
public class RSAHelper
{
    //私钥
    private const string defaultprivateKey = @"MIICXAIBAAKBgQCC0hrRIjb3noDWNtbDpANbjt5Iwu2NFeDwU16Ec87ToqeoIm2K
I+cOs81JP9aTDk/jkAlU97mN8wZkEMDr5utAZtMVht7GLX33Wx9XjqxUsDfsGkqN
L8dXJklWDu9Zh80Ui2Ug+340d5dZtKtd+nv09QZqGjdnSp9PTfFDBY133QIDAQAB
AoGAJBNTOITaP6LCyKVKyEdnHaKNAz0DS+V9UwjKhyAgfcAxwm3sDdd6FQCEW0TI
JA7Np7rFYrGwcR1UOoKxkNxB10ACl6JX4rE7xKS6NLZumdwxON/KgDb+2SQtWEXD
gBySZ7Znv/FhEp1RmoBDjZ05E99kILWO3ToorUM0Eq2GHQkCQQCnUMXgZa4HS0tu
INzysgB37d7ene9+CIARyJphs079qao2UWCgXqen43Ob6GJUgulz7We+4JOZFld0
TfEi1E5rAkEAyClQAVzafLO3gXgqH7tbRbPPx788+4opxT9QBo2Trzl6/3FlcC1P
IZeqbQ/Oc2wT7jmidFnpyTEnM2p7Yq3U1wJBAILTWaX4W3dAnJ5j+9+Y51zfFiEj
hRwbMWi2XmB+gAlAHOOUBeXfnWBdLQx/TEOgiUIoI7LQjxhoq8E5II+HSjkCQDlK
SdH6B7dFoTJ3eGcYsykiLEiZ3hSJGSeR1Y/qmei/ZQsUI9qVvV56EJeivI6g0puO
94ah7Z5eaT/4LFS0OIUCQDgLn586pGgeidLhQsIe/AR3y9YOCAygTFLxzmeBXOKt
M90q4516KWlTtK2u99442mNi7hNmjryBVwk62foWo8w=";
    //公钥
    private const string defaultpublicKey = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCC0hrRIjb3noDWNtbDpANbjt5Iwu2NFeDwU16Ec87ToqeoIm2KI+cOs81JP9aTDk/jkAlU97mN8wZkEMDr5utAZtMVht7GLX33Wx9XjqxUsDfsGkqNL8dXJklWDu9Zh80Ui2Ug+340d5dZtKtd+nv09QZqGjdnSp9PTfFDBY133QIDAQAB";

    private RSACryptoServiceProvider _privateKeyRsaProvider;
    private RSACryptoServiceProvider _publicKeyRsaProvider;

    public RSAHelper(string privateKey = "", string publicKey = "")
    {

        if (string.IsNullOrEmpty(privateKey))
        {
            privateKey = defaultprivateKey;
        }
        if (string.IsNullOrEmpty(publicKey))
        {
            publicKey = defaultpublicKey;
        }

        if (!string.IsNullOrEmpty(privateKey))
        {
            _privateKeyRsaProvider = CreateRsaProviderFromPrivateKey(privateKey);
        }

        if (!string.IsNullOrEmpty(publicKey))
        {
            _publicKeyRsaProvider = CreateRsaProviderFromPublicKey(publicKey);
        }
    }

    public string Decrypt(string cipherText)
    {
        if (_privateKeyRsaProvider == null)
        {
            throw new Exception("_privateKeyRsaProvider is null");
        }

        if (string.IsNullOrEmpty(cipherText))
        {
            return "";
        }


        return Encoding.UTF8.GetString(_privateKeyRsaProvider.Decrypt(Convert.FromBase64String(cipherText), false));
    }

    public string Encrypt(string text)
    {
        if (_publicKeyRsaProvider == null)
        {
            throw new Exception("_publicKeyRsaProvider is null");
        }
        return Convert.ToBase64String(_publicKeyRsaProvider.Encrypt(Encoding.UTF8.GetBytes(text), false));
    }


    private RSACryptoServiceProvider CreateRsaProviderFromPrivateKey(string privateKey)
    {
        var privateKeyBits = System.Convert.FromBase64String(privateKey);

        var RSA = new RSACryptoServiceProvider();
        var RSAparams = new RSAParameters();

        using (BinaryReader binr = new BinaryReader(new MemoryStream(privateKeyBits)))
        {
            byte bt = 0;
            ushort twobytes = 0;
            twobytes = binr.ReadUInt16();
            if (twobytes == 0x8130)
                binr.ReadByte();
            else if (twobytes == 0x8230)
                binr.ReadInt16();
            else
                throw new Exception("Unexpected value read binr.ReadUInt16()");

            twobytes = binr.ReadUInt16();
            if (twobytes != 0x0102)
                throw new Exception("Unexpected version");

            bt = binr.ReadByte();
            if (bt != 0x00)
                throw new Exception("Unexpected value read binr.ReadByte()");

            RSAparams.Modulus = binr.ReadBytes(GetIntegerSize(binr));
            RSAparams.Exponent = binr.ReadBytes(GetIntegerSize(binr));
            RSAparams.D = binr.ReadBytes(GetIntegerSize(binr));
            RSAparams.P = binr.ReadBytes(GetIntegerSize(binr));
            RSAparams.Q = binr.ReadBytes(GetIntegerSize(binr));
            RSAparams.DP = binr.ReadBytes(GetIntegerSize(binr));
            RSAparams.DQ = binr.ReadBytes(GetIntegerSize(binr));
            RSAparams.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
        }

        RSA.ImportParameters(RSAparams);
        return RSA;
    }

    private int GetIntegerSize(BinaryReader binr)
    {
        byte bt = 0;
        byte lowbyte = 0x00;
        byte highbyte = 0x00;
        int count = 0;
        bt = binr.ReadByte();
        if (bt != 0x02)
            return 0;
        bt = binr.ReadByte();

        if (bt == 0x81)
            count = binr.ReadByte();
        else
            if (bt == 0x82)
        {
            highbyte = binr.ReadByte();
            lowbyte = binr.ReadByte();
            byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
            count = BitConverter.ToInt32(modint, 0);
        }
        else
        {
            count = bt;
        }

        while (binr.ReadByte() == 0x00)
        {
            count -= 1;
        }
        binr.BaseStream.Seek(-1, SeekOrigin.Current);
        return count;
    }

    private RSACryptoServiceProvider CreateRsaProviderFromPublicKey(string publicKeyString)
    {
        // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
        byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
        byte[] x509key;
        byte[] seq = new byte[15];
        int x509size;

        x509key = Convert.FromBase64String(publicKeyString);
        x509size = x509key.Length;

        // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
        using (MemoryStream mem = new MemoryStream(x509key))
        {
            using (BinaryReader binr = new BinaryReader(mem))  //wrap Memory Stream with BinaryReader for easy reading
            {
                byte bt = 0;
                ushort twobytes = 0;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                seq = binr.ReadBytes(15);       //read the Sequence OID
                if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8203)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x00)     //expect null byte next
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                byte lowbyte = 0x00;
                byte highbyte = 0x00;

                if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                    lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                else if (twobytes == 0x8202)
                {
                    highbyte = binr.ReadByte(); //advance 2 bytes
                    lowbyte = binr.ReadByte();
                }
                else
                    return null;
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                int modsize = BitConverter.ToInt32(modint, 0);

                int firstbyte = binr.PeekChar();
                if (firstbyte == 0x00)
                {   //if first byte (highest order) of modulus is zero, don't include it
                    binr.ReadByte();    //skip this null byte
                    modsize -= 1;   //reduce modulus buffer size by 1
                }

                byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes

                if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                    return null;
                int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
                byte[] exponent = binr.ReadBytes(expbytes);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSAParameters RSAKeyInfo = new RSAParameters();
                RSAKeyInfo.Modulus = modulus;
                RSAKeyInfo.Exponent = exponent;
                RSA.ImportParameters(RSAKeyInfo);

                return RSA;
            }

        }
    }

    private bool CompareBytearrays(byte[] a, byte[] b)
    {
        if (a.Length != b.Length)
            return false;
        int i = 0;
        foreach (byte c in a)
        {
            if (c != b[i])
                return false;
            i++;
        }
        return true;
    }
}