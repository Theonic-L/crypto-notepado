using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace PasswordManager.Cryptography
{
    public static class CryptAES
    {
        public static void EncryptFile(string inputFile, string outputFile, string skey)
        {
            RijndaelManaged aes = new RijndaelManaged();
            //aes.Mode = CipherMode.CTS;
            try
            {
                PasswordDeriveBytes derivedPassword = new PasswordDeriveBytes(skey, Encoding.ASCII.GetBytes(Data.salt), "SHA1", 1);
                byte[] keyBytes = derivedPassword.GetBytes(256 / 8);
                byte[] initialVectorBytes = Encoding.ASCII.GetBytes(Data.vector);
                byte[] key = Encoding.ASCII.GetBytes(skey); //ASCIIEncoding.UTF8.GetBytes(skey);
                using (FileStream fsCrypt = new FileStream(outputFile, FileMode.Create))
                {
                    using (CryptoStream cs = new CryptoStream(fsCrypt, aes.CreateEncryptor(keyBytes, initialVectorBytes), CryptoStreamMode.Write))
                    {
                        using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
                        {
                            int data;
                            while ((data = fsIn.ReadByte()) != -1)
                            {
                                cs.WriteByte((byte)data);
                            }
                            aes.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.TargetSite);
                aes.Clear();
            }
        }
        public static void DecryptFile(string inputFile, string outputFile, string skey)
        {
            RijndaelManaged aes = new RijndaelManaged();
            try
            {
                PasswordDeriveBytes derivedPassword = new PasswordDeriveBytes(skey, Encoding.ASCII.GetBytes(Data.salt), "SHA1", 1);
                byte[] keyBytes = derivedPassword.GetBytes(256 / 8);
                byte[] initialVectorBytes = Encoding.ASCII.GetBytes(Data.vector);
                byte[] key = Encoding.ASCII.GetBytes(skey);
                using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
                {
                    using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))
                    {
                        using (CryptoStream cs = new CryptoStream(fsCrypt, aes.CreateDecryptor(keyBytes, initialVectorBytes), CryptoStreamMode.Read))
                        {
                            int data;
                            while ((data = cs.ReadByte()) != -1)
                            {
                                fsOut.WriteByte((byte)data);
                            }
                            aes.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                aes.Clear();
            }
        }
        public static string DecryptString(string cipherText, string password, int keySize = 256)
        {
            string salt = Data.salt;
            string hashAlgorithm = "SHA512";
            string initialVector = Data.vector;
            try
            {
                if (string.IsNullOrEmpty(cipherText))
                    return "";

                byte[] initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

                PasswordDeriveBytes derivedPassword = new PasswordDeriveBytes
                (password, saltValueBytes, hashAlgorithm, 1);
                byte[] keyBytes = derivedPassword.GetBytes(keySize / 8);

                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;

                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int byteCount = 0;

                using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor
                         (keyBytes, initialVectorBytes))
                {
                    using (MemoryStream memStream = new MemoryStream(cipherTextBytes))
                    {
                        using (CryptoStream cryptoStream
                        = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read))
                        {
                            byteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                            memStream.Close();
                            cryptoStream.Close();
                        }
                    }
                }

                symmetricKey.Clear();
                return Encoding.UTF8.GetString(plainTextBytes, 0, byteCount);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Не удалось выполнить процедуру расшифровки! \n" + ex.Message);
                return cipherText;
            }
        }
        public static string EncryptString(string plainText, string password, int keySize = 256)
        {
            string salt = Data.salt;
            string hashAlgorithm = "SHA512";
            string initialVector = Data.vector;
            int passwordIterations = 1;
            try
            {
                if (string.IsNullOrEmpty(plainText))
                    return "";
                //

                byte[] initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                PasswordDeriveBytes derivedPassword = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIterations);

                byte[] keyBytes = derivedPassword.GetBytes(keySize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;

                byte[] cipherTextBytes = null;

                using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor
                (keyBytes, initialVectorBytes))
                {
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream
                                 (memStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                            cryptoStream.FlushFinalBlock();
                            cipherTextBytes = memStream.ToArray();
                            memStream.Close();
                            cryptoStream.Close();
                        }
                    }
                }

                symmetricKey.Clear();
                return Convert.ToBase64String(cipherTextBytes);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Ошибка! Не удалось выполнить процедуру шифровки! \n" + ex.Message + ex.InnerException);
                return plainText;
            }
        }
    }
}
