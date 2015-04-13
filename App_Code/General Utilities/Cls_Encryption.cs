using System;
using System.IO;
using System.Data;
using System.Text;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;

namespace Mechsoft.GeneralUtilities
{
    /// <summary>
    /// Created By: Rupali
    /// Created Date:5 july 2012
    /// Description : Decrypt and encrypt strings using following methods.
    /// </summary>
    public class Cls_Encryption
    {
        private static byte[] KEY_192 = { 42, 16, 93, 156, 78, 4, 218, 32, 15, 167, 44, 80, 26, 250, 155, 112, 2, 94, 11, 204, 119, 35, 184, 197 };
        private static byte[] IV_192 = { 55, 103, 246, 79, 36, 99, 167, 3, 42, 5, 62, 83, 184, 7, 209, 13, 145, 23, 200, 58, 173, 10, 121, 222 };

        public Cls_Encryption()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string getMd5Hash(string input)
        {
            // Create a new instance of the MD5 object.
            MD5 md5Hasher = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            int i;
            for (i = 0; i <= data.Length - 1; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
   
        public bool verifyMd5Hash(string input, string hash)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.Hash;
            // Hash the input.
            string hashOfInput = getMd5Hash(input);
            // Create a StringComparer an comare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //TRIPLE DES encryption
        public static string EncryptTripleDES(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_192, IV_192), CryptoStreamMode.Write);
                StreamWriter sw = new StreamWriter(cs);

                sw.Write(value);
                sw.Flush();
                cs.FlushFinalBlock();
                ms.Flush();

                //convert back to a string

                return Convert.ToBase64String(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length));
            }
            else
                return "";
        }
        //TRIPLE DES decryption
        public static string DecryptTripleDES(string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();

                    //convert from string to byte array
                    byte[] buffer = Convert.FromBase64String(value.Replace("%2f", "/").Replace("%3d", "=").Replace(" ", "+"));
                    MemoryStream ms = new MemoryStream(buffer);
                    CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_192, IV_192), CryptoStreamMode.Read);
                    StreamReader sr = new StreamReader(cs);

                    return sr.ReadToEnd();
                }
                else
                    return "";
            }
            catch (Exception)
            {
                return "";
            }
        }


    }
}