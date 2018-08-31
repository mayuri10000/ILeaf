/*  PasswordEncrypter.cs  用户密码加密类
 *  本文件属于北京邮电大学创新项目作品ILeaf
 *  作者：刘同 2017212783
 *  最后修改日期：2018年7月8日
 */

using System;
using System.IO;
using System.Text;
using ILeaf.Core.Config;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace ILeaf.Core.Utilities
{
    public static class PasswordEncrypter
    {
        // 用户密码加密使用RSA-512算法(只保存公钥)，加密时会对原密码加盐
        private static RsaKeyParameters publicKey = null;
        private static IBufferedCipher cipher = CipherUtilities.GetCipher("RSA/NONE/NoPadding"); // 无填充，防止因填充随机数导致每次加密结果不同
        private static IBufferedCipher dec_cipher = CipherUtilities.GetCipher("RSA/NONE/PKCS1Padding");

        static PasswordEncrypter()
        {
            if(publicKey == null)
            {
                using(var sr = new StringReader(SiteConfig.EncryptKey))
                {
                    var pemReader = new Org.BouncyCastle.OpenSsl.PemReader(sr);
                    publicKey = (RsaKeyParameters)pemReader.ReadObject();
                }
     
                cipher.Init(true, publicKey);
            }
        }

        public static string EncryptPasswordForStorage(string password, string salt)
        {
            byte[] toBeEncrypt = Encoding.ASCII.GetBytes(salt + password);
            return Convert.ToBase64String(cipher.DoFinal(toBeEncrypt));
        }

        public static bool CheckPassword(string password, string salt,string encryptedPassword)
        {
            byte[] encrypted = cipher.DoFinal(Encoding.ASCII.GetBytes(salt + password));
            return isArrayEqual(encrypted, Convert.FromBase64String(encryptedPassword));
        }

        public static string DecryptPasswordFromClient(string encryptedPassword, RsaKeyParameters privateKey)
        {
            if (!privateKey.IsPrivate)
                throw new Exception("Decryption requires a private key, not a public one!");
            dec_cipher.Init(false, privateKey);
            return Encoding.ASCII.GetString(dec_cipher.DoFinal(Convert.FromBase64String(encryptedPassword)));
        }

        private static bool isArrayEqual(byte[] a,byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            for(int i=0;i!=a.Length;i++)
            {
                if (a[i] != b[i])
                    return false;
            }

            return true;
        }
    }
}
