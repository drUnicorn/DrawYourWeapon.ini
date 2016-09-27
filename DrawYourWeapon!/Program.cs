using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawYourWeapon
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string url = Console.ReadLine();
            var ini = new Ini();

            ini.Parse(url);

            Console.ReadKey();
        }
    }
}
