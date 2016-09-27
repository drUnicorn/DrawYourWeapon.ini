using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Encoding = System.Text.Encoding;
using System.Threading.Tasks;

namespace DrawYourWeapon
{
    class File
    {
        public string Url;


        public File(string url)
        {
            Url = url;
        }


        private IEnumerable<string> EnumLines()
        {
            using (var reader = new StreamReader(Url,true))
            {
                while (true)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                        yield return line;
                    else
                        yield break;
                }
            }
        }
        public IEnumerable<string> Lines
        {
            get { return EnumLines(); }
        }


        private IEnumerable<char> EnumChars()
        {
            using (var reader = new StreamReader(Url,true))
            {
                var ch = new char[1];
                while(!reader.EndOfStream)
                {
                    ch[0] = '\0';
                    reader.Read(ch, 0, 1);
                    yield return ch[0];
                }
                yield break;
            }
        }
        public IEnumerable<char> Chars
        {
            get { return EnumChars(); }
        }
    }
}