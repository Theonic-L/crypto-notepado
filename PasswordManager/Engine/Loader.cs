using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PasswordManager.Cryptography;

namespace PasswordManager.Engine
{

    static public class Loader
    {
        static public string path_file;
        static private string password;
        static public bool pass_on;
        static public string text_decrypt;
        static public void Load(string path, string pass)
        {
            if (pass_on) password = pass;
            else password = "gardarianiumistrix";
            path_file = path;
            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                text_decrypt = CryptAES.DecryptString(text, password);
            }
        }
        static public void Save()
        {
            try
            {
                string text = CryptAES.EncryptString(Links.txtText.Text, password);
                File.WriteAllText(path_file, text);
            }
            catch { }
        }
    }
}
