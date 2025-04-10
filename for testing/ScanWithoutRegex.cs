using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace for_testing
{
    public class ScanWithoutRegex
    {
        Dictionary<string, Token> state = new Dictionary<string, Token>();
        public string text;
        public string[] words;
        public string error = "";
        public Dictionary<string, int> keyValuePairs = new Dictionary<string, int>()
        {
            { "struct", 1 },
            { "int", 2},
            { "string", 3},
            { "double", 4},
            { "float", 5},
            { "public", 6},
            { "private", 7},
            { "protected", 8},
            { " ", 9},
            { "{", 10},
            { "}", 11},
            { ";", 12}
        };
        public List<string> keywords = new List<string>();
        public List<string> keyword = new List<string>();
        public List<int> codes = new List<int>();

        public ScanWithoutRegex(string text)
        {
            this.text = text;
        }

        public void Lexic()
        {
            char[] chars = text.ToCharArray();
            int code = 0;
            while (true)
            {
                char[] exma = new char[256];
                int i = 0;
                string word = "";
                while (chars[i] != ' ')
                {
                    exma[i] += chars[i];
                    word += chars[i];
                    i++;
                }
                if (keyValuePairs.ContainsKey(word))
                {
                    code = keyValuePairs[word];
                    if (code != 9)
                    {
                        codes.Add(code);
                        if (code > 0 && code < 9)
                        {
                            keywords.Add("keyword");
                        }
                        else if (code == 10 || code == 11)
                        {
                            keywords.Add("razdelitel");
                        }
                        else if (code == 12)
                        {
                            keywords.Add("end operator");
                        }
                        keyword.Add(word);
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (letter(word)) {
                    code = 13;
                    codes.Add(code);
                    keywords.Add("Id");
                    keyword.Add(word);
                }
                int j = 0;
                while (chars[i]  == ' ')
                {
                    j++;
                    i++;
                }

            }
        }
        private bool letter(string word)
        {
            if (char.IsDigit(word[0]))
                return false;

            if (word.All(c => char.IsLetterOrDigit(c) || c == '_'))
            {
                return true;
            }
            return false;
        }
    }
}
