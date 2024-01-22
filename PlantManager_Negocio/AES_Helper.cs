using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PlantManager_Negocio
{
    public static class AES_Helper
    {
        const string key = "b14ca5898a4e4133bbce6ea2315a1916";

        public static string EncryptString(string data)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (System.IO.MemoryStream memoStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream =
                           new CryptoStream((Stream)memoStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(data);
                        }
                        array = memoStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string data)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(data);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream =
                            new CryptoStream((Stream)memoStream,
                            decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(
                                (Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}
