using System.Diagnostics;
using System.Reflection;
using Microsoft.Win32;

namespace Cod4Keygen
{
    internal class Program
    {
        private static string serial = "";
        private static string basekey = "";
        static string charset = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static void Main(string[] args)
        {
            Console.Title = "COD4 Keygen & Activation";
            Console.WriteLine(" ▄         ▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄       ▄▄  ▄            ▄▄▄▄▄▄▄▄▄▄▄  ▄▄        ▄ \n▐░▌       ▐░▌▐░░░░░░░░░░░▌▐░░▌     ▐░░▌▐░▌          ▐░░░░░░░░░░░▌▐░░▌      ▐░▌\n▐░▌       ▐░▌▐░█▀▀▀▀▀▀▀█░▌▐░▌░▌   ▐░▐░▌▐░▌          ▐░█▀▀▀▀▀▀▀█░▌▐░▌░▌     ▐░▌\n▐░▌       ▐░▌▐░▌       ▐░▌▐░▌▐░▌ ▐░▌▐░▌▐░▌          ▐░▌       ▐░▌▐░▌▐░▌    ▐░▌\n▐░█▄▄▄▄▄▄▄█░▌▐░█▄▄▄▄▄▄▄█░▌▐░▌ ▐░▐░▌ ▐░▌▐░▌          ▐░█▄▄▄▄▄▄▄█░▌▐░▌ ▐░▌   ▐░▌\n▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░▌  ▐░▌  ▐░▌▐░▌          ▐░░░░░░░░░░░▌▐░▌  ▐░▌  ▐░▌\n▐░█▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀█░▌▐░▌   ▀   ▐░▌▐░▌          ▐░█▀▀▀▀▀▀▀█░▌▐░▌   ▐░▌ ▐░▌\n▐░▌       ▐░▌▐░▌       ▐░▌▐░▌       ▐░▌▐░▌          ▐░▌       ▐░▌▐░▌    ▐░▌▐░▌\n▐░▌       ▐░▌▐░▌       ▐░▌▐░▌       ▐░▌▐░█▄▄▄▄▄▄▄▄▄ ▐░▌       ▐░▌▐░▌     ▐░▐░▌\n▐░▌       ▐░▌▐░▌       ▐░▌▐░▌       ▐░▌▐░░░░░░░░░░░▌▐░▌       ▐░▌▐░▌      ▐░░▌\n ▀         ▀  ▀         ▀  ▀         ▀  ▀▀▀▀▀▀▀▀▀▀▀  ▀         ▀  ▀        ▀▀ \n");
            while (serial.Length < 20)
            {
                for (int i = 0; i < 4; i++)
                {
                    char keychar = randCharGen();
                    serial += keychar;
                    basekey += keychar;
                }

                if (serial.Length < 20)
                {
                    serial += '-';
                }
            }
            int checksum = calculate_checksum(basekey);
            Console.Write(serial);
            Console.Write("{0:X}\n", checksum);
            string finalKey = basekey + string.Format("{0:X}",checksum);
            Debug.WriteLine(finalKey);
            if (setRegistry(finalKey))
            {
                Console.WriteLine("Key successfully set in registry!");
            }
            else
            {
                Console.WriteLine("ERROR: Unable to set key in registry! You'll need to manually enter it into the game!");
            }
            Console.WriteLine("Press ENTER to quit.");
            Console.ReadLine();
        }

        static bool setRegistry(string regkey)
        {
            try
            {
                Registry.CurrentUser.CreateSubKey(@"Software\Activision\Call of Duty 4").SetValue("codkey", regkey);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        static char randCharGen()
        {
            Random rnd = new Random();
            int num = (rnd.Next() % (charset.Length - 1));
            return charset[num];
        }

        static int calculate_checksum(string key)
        {
            int v2 = 0;
            int v3 = 0;
            while (v3 < 16)
            {
                v2 ^= key[v3];
                int v4 = 8;
                while (v4 != 0)
                {
                    if (v2 % 2 != 0)
                    {
                        v2 ^= 81922;
                    }
                    v2 >>= 1;
                    v4--;
                }
                v3++;
            }

            return v2;
        }

    }
}