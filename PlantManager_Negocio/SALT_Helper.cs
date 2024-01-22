using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;



namespace PlantManager_Negocio
{
    public static class SALT_Helper
    {
        const int Iterations = 10000;

        public static byte[] GetSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            return salt;
        }

        public static string GenerateSaltyPassword(string password, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);

            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        public static bool ValidateSaltyPassword(string enteredPassword, string storedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(storedPassword);

            byte[] salt = new byte[16];

            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, Iterations);

            byte[] hash = pbkdf2.GetBytes(20);

            int ok = 1;
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    ok = 0;
            }

            if (ok == 1)
                return true;
            else
                return false;
        }

        public static void ExecutePasswordSaltyExample(string pass)
        {
            byte[] salt = GetSalt();

            string passSalted = GenerateSaltyPassword(pass, salt);

            bool res = ValidateSaltyPassword(pass, passSalted);

            Console.WriteLine("SALT encryption example");
            Console.WriteLine("=====================");
            Console.WriteLine("Salt generado (string): " + Convert.ToBase64String(salt));
            Console.WriteLine("Password real: " + pass);
            Console.WriteLine("Password encriptada: " + passSalted);

            if (res)
                Console.WriteLine("Password correcta");
            else
                Console.WriteLine("Password incorrecta");

            Console.WriteLine("=====================");
        }

    }
}

