using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PasswordManager.Cryptography
{
    public static class Generator
    {
        public static string PasswordCompile(string pass)
        {
            string result;
            pass = "mizera" + pass + "morguliq";
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] message = UE.GetBytes(pass);
            SHA512Managed hashString = new SHA512Managed();
            string hexNumber = "";
            byte[] hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hexNumber += String.Format("{0:x2}", x);
            }
            string hashData = hexNumber;
            result = Data.saltPass + hashData;
            return result;
        }
    }
}
