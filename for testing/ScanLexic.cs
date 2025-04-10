using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace for_testing
{
    enum Token { START, ModStruct, StructKeyword, StructName, OpenBrace, Mod, Type, IDentificator, Semicolon, CloseBrace, END, error }
    public class ScanLexic
    {
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
        public ScanLexic(string text)
        {
            this.text = text;
        }
        private List<Token> State = new List<Token>();
        public List<string> keywords = new List<string>();
        public List<string> keyword = new List<string>();
        public List<int> codes = new List<int>();
        public void Lexic()
        {
            var result = Regex.Matches(text, @"\w+|[{};/\+-?=,()]|\s+|\"".*?\""");
            int prevcode = 0;
            foreach (Match match in result)
            {
                int code = 0;
                if (match.Value.Trim().Length == 0 && prevcode != 9 && (prevcode >= 1 && prevcode < 9))
                {
                    code = keyValuePairs[match.Value];
                    codes.Add(code);
                    keywords.Add("WhiteSpace");
                    keyword.Add(match.Value);

                }
                else if (keyValuePairs.ContainsKey(match.Value))
                {
                    code = keyValuePairs[match.Value];
                    if (code != 9)
                    {
                        codes.Add(code);
                        if (code > 0 && code < 9)
                        {
                            //if(code == 1)
                            //{
                            //    State.Add(Token.StructKeyword);
                            //}
                            //else if(code > 1 && code < 6)
                            //{
                            //    State.Add(Token.Type);
                            //}
                            //else if(code>5&&code <9)
                            //{

                            //    State.Add(Token.Mod);
                            //}
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
                        keyword.Add(match.Value);
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (letter(match.Value))
                {
                    code = 13;
                    codes.Add(code);
                    keywords.Add("Id");
                    keyword.Add(match.Value);
                }
                else
                {
                    if (match.Value.Trim().Length != 0)
                    {
                        code = 0;
                        codes.Add(code);
                        keywords.Add("Error");
                        keyword.Add(match.Value);
                    }
                }
                prevcode = code;
            }
        }

        public void Syntax()
        {

        }

        private void test()
        {
            Token CurrentToken = Token.START;
            Token Prev_token = Token.START;
            while (true)
            {
                for (int i = 0; i < codes.Count; i++)
                {
                    if (CurrentToken == Token.START)
                    {
                        if (Prev_token == Token.START)
                        {
                            if (codes[i] > 5 && codes[i] < 9)
                            {
                                CurrentToken = Token.ModStruct;
                            }
                        }
                        else if (Prev_token == Token.END)
                        {

                        }

                    }
                    else if (CurrentToken == Token.ModStruct && Prev_token == Token.START)
                    {


                    }

                }
            }
        }
        private void test2()
        {
            Token token = Token.START;
            int i = 0;
            Token prev_token = Token.START;
            while (i<codes.Count)
            {
                if (codes[i] == 9)
                {
                    i++;
                }
                if (token == Token.START)
                {
                    if (codes[i] == 1)
                    {
                        prev_token = token;
                        token = Token.StructKeyword;
                        Console.WriteLine("Warning in syntax! need MOD!");
                    }
                    else if (codes[i] > 5 && codes[i] < 9)
                    {
                        prev_token = token;
                        token = Token.ModStruct;
                    }
                    else
                    {
                        prev_token = token;
                        token = Token.StructKeyword;
                        Console.WriteLine("Ошибка в синтаксисе. Ожидается private/public/protected или struct");
                    }
                }
                else if (token == Token.ModStruct)
                {
                    if (codes[i] == 1)
                    {
                        token = Token.StructKeyword;
                    }
                    else
                    {
                        token = Token.StructKeyword;
                        Console.WriteLine("Ошибка в синтаксисе. Ожидается struct");
                    }
                }
                else if (token == Token.StructKeyword)
                {
                    if (codes[i] == 13)
                    {
                        token = Token.StructName;
                    }
                    else
                    {
                        token = Token.StructName;
                        Console.WriteLine("Ошибка в синтаксисе. Ожидается struct");
                    }
                }
                else if (token == Token.StructName)
                {
                    if(codes[i] == 10)
                    {
                        token = Token.OpenBrace;
                    }
                    else
                    {
                        token= Token.OpenBrace;
                        Console.WriteLine("Error! Нужна {");
                    }
                }
                else if (token == Token.OpenBrace)
                {

                    if (codes[i] == 11)
                    {
                        token = Token.CloseBrace;
                    }
                    else if (codes[i] > 5 && codes[i] < 9)
                    {
                        token = Token.Mod;
                    }
                    else if (codes[i] > 2 && codes[i] < 6)
                    {
                        token = Token.Type;
                        Console.WriteLine("при объявлении переменной вы не указали модификатор доступа");
                    }
                    
                    
                }
                else if (token == Token.CloseBrace)
                {
                    if (i + 1 < codes.Count)
                    {
                        if (codes[i + 1] == 12)
                        {

                            token = Token.END;
                        }
                        else
                        {
                            string error = $"Ошибка в синтаксисе ожидалось ';', а на ввод было дано{keyword[i + 1]}";
                            token = Token.END;
                        }
                    }
                    else
                    {
                        string error = $"Нет опереатора для обозначения завершения программы";
                    }
                }
                else if (token == Token.Mod)
                {
                    if (codes[i] > 5 && codes[i] < 9) {
                        token = Token.Type;
                    }
                    else
                    {
                        token = Token.Type;
                        Console.WriteLine("Error MOd");
                    }
                }
                else if (token == Token.Type)
                {
                    if (codes[i] > 1 && codes[i] < 6)
                    {
                        token = Token.IDentificator;
                    }
                    else
                    {
                        token = Token.IDentificator;
                        Console.WriteLine("Error Type");
                    }
                }
                else if (token == Token.IDentificator)
                {
                    if (codes[i] == 12)
                    {
                        token = Token.Semicolon;
                    }
                    else
                    {
                        token = Token.Semicolon;
                        Console.WriteLine("Error ID");
                    }

                }
                else if (token == Token.Semicolon)
                {
                    if (codes[i] == 11)
                    {
                        token = Token.CloseBrace;
                    }
                    else if (codes[i] > 5 && codes[i] < 9)
                    {
                        token = Token.Mod;
                    }
                    else if (codes[i] > 2 && codes[i] < 6)
                    {
                        token = Token.Type;
                        Console.WriteLine("при объявлении переменной вы не указали модификатор доступа");
                    }
                    else
                    {
                        Console.WriteLine("Ожидалась '{', 'private/protected/public' или 'int/double/float/string' ");
                    }
                }
                else if (token == Token.END)
                {
                    if (codes[i] == 12&& i+1<codes.Count)
                    {
                        
                        token = Token.START;
                    }
                    else {
                        Console.WriteLine("LAst error");
                    }
                }
                i++;
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



