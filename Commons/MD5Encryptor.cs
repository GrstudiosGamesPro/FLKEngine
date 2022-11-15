using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FLKEngine.Commons
{
    public class MD5Encryptor
    {
        string hash = "@@@@@@@23n2qb97b2q78b2fq2bf87b2q87fb2q87fbq287fbq2f2q87fb27qf#@3fuqbnf987bq8fvwqb7f8wqvf0w8qvfeufsdebfiugbv3iuloyfv34fo8y43vf0348v34v4698703v4004p3o8vb453bn5jkb5gl45";

        public string Desencrypt(string MSG)
        {
            byte[] data = Convert.FromBase64String(MSG);

            MD5 md5 = MD5.Create();
            TripleDES triplD = TripleDES.Create();


            triplD.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            triplD.Mode = CipherMode.ECB;

            ICryptoTransform transform = triplD.CreateDecryptor();

            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(result);
        }

        public string Encrypt(string MSG)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(MSG);

            MD5 md5 = MD5.Create();
            TripleDES triplD = TripleDES.Create();


            triplD.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            triplD.Mode = CipherMode.ECB;

            ICryptoTransform transform = triplD.CreateEncryptor();

            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }
    }
}
