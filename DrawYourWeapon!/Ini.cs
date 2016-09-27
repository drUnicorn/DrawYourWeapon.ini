using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawYourWeapon
{
    class Ini : IEnumerable<KeyValuePair<string, Dictionary<string, object>>>
    {
        protected Dictionary<string, Dictionary<string, object>> data;


        public void Parse(string url)
        {
            data = new Dictionary<string, Dictionary<string, object>>();
            var file = new File(url);

            string section = "",
                   key     = "",
                   value   = "";

            int state = 0;
            const int NONE    = 0,
                      SECTION = 1,
                      KEY     = 2,
                      VALUE   = 3,
                      COMMENT = 4,
                      EOL     = 5;

            foreach (char ch in file.Chars)
            {
                switch (state)
                {
                    case NONE:
                        if (Char.IsWhiteSpace(ch))
                            continue;
                        else if (ch == '[')
                        {
                            state = SECTION;
                            section = "";
                        }
                        else if (ch == ';')
                            state = COMMENT;
                        else if (ch == '=')
                            state = VALUE;
                        else
                        {
                            state = KEY;
                            key += ch;
                        }
                        break;

                    case SECTION:
                        if (ch == '\n' || ch == '\r')
                            throw new Exception("Unterminated section!");
                        else if (ch == ']')
                            state = EOL;
                        else
                            section += ch;
                        break;

                    case KEY:
                        if (ch == '=')
                            state = VALUE;
                        else if (ch == '\n' || ch == '\r')
                        {
                            state = NONE;
                            data[section][key] = value;
                            key = value = "";
                        }
                        else
                            key += ch;
                        break;

                    case VALUE:
                        if (ch == '\n' || ch == '\r')
                        {
                            state = NONE;
                            data[section][key] = value;
                            key = value = "";
                        }
                        else
                            value += ch;
                        break;

                    case COMMENT:
                        if (ch == '\n' || ch == '\r')
                            state = NONE;
                        else
                            continue;
                        break;

                    case EOL:
                        if (ch == '\n' || ch == '\r')
                            state = NONE;
                        else if (Char.IsWhiteSpace(ch))
                            continue;
                        else
                            throw new Exception("End of line expected!");
                        break;
                }
            }
            
        }

        public Dictionary<string,object> this[ string key ]
        {
            get
            {
                return data[key];
            }

            set
            {
                data[key] = value;
            }
        }

        public IEnumerator<KeyValuePair<string,Dictionary<string, object>>> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
