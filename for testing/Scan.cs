using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace for_testing
{
    public class Scan
    {
        Dictionary<string, Token> state = new Dictionary<string, Token>();
        public string text;
        public string[] words;
        
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
        public Scan(string text)
        {
            this.text = text;
        }
        public List<string> keywords = new List<string>();
        public List<string> keyword = new List<string>();
        public List<int> codes = new List<int>();
        
        public void test2()
        {
            Token token = Token.START;
            Token prev_token = Token.START;
            List<Token> tokens = new List<Token>();
            int i = 0;
            int err = 0;
            int OB = 0;
            int CB = 0;
            List<Token> types = new List<Token>();
            while (i < codes.Count)
            {

                switch (codes[i])
                {
                    case 1:
                        if (err == 0)
                        {
                            prev_token = token;
                        }
                        token = Token.StructKeyword;

                        break;
                    case 6:
                    case 7:
                    case 8:
                        if (err == 0)
                        {
                            prev_token = token;
                        }

                        if (token == Token.START)
                        {

                            token = Token.ModStruct;
                        }
                        else 
                        {
                            token = Token.Mod;
                            tokens.Add(token);
                        }


                        break;
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        if (err == 0)
                        {
                            prev_token = token;
                        }
                        token = Token.Type;

                        tokens.Add(token);
                        break;
                    case 9:
                        i++;
                        continue;
                    case 10:
                        if (err == 0)
                        {
                            prev_token = token;
                        }
                        OB++;
                        token = Token.OpenBrace;

                        break;
                    case 11:
                        if (err == 0)
                        {
                            prev_token = token;
                        }
                        CB++;
                        token = Token.CloseBrace;

                        break;
                    case 12:
                        if (err == 0)
                        {
                            prev_token = token;
                        }
                        if (token == Token.CloseBrace)
                        {
                            token = Token.END;
                        }
                        else
                        {
                            token = Token.Semicolon;
                        }
                        break;
                    case 13:
                        if (err == 0)
                        {
                            prev_token = token;
                        }
                        if (token == Token.StructKeyword)
                        {
                            token = Token.StructName;
                        }
                        else
                        {
                            tokens.Add(token);

                            token = Token.IDentificator;
                        }
                        break;
                    default:
                        Console.WriteLine("Лексическая ошибка");
                        i++;
                        token = Token.error;
                        continue;
                        
                }
                if(token == Token.END)
                {
                    token = Token.START;
                    prev_token = Token.END;
                    tokens.Clear(); 
                }
                if (token == Token.Semicolon||token==Token.CloseBrace)
                {
                    if (tokens.Count > 3)
                    {
                        //Console.WriteLine("Слишком большое количество аргументов");

                    }
                    tokens.Clear();
                }

                //private struct Mystruct {  private int  x; private string name; }; 
                if (token == Token.ModStruct && prev_token != Token.START)
                {
                    Console.WriteLine("Врядли возможно");
                    err = 1;
                }
                else if (token == Token.StructKeyword && prev_token != Token.START && prev_token != Token.ModStruct)
                {
                    Console.WriteLine("Структура не может быть объявлена здесь");
                    err = 1;
                }
                else if (token == Token.OpenBrace && prev_token != Token.StructName)
                {
                    Console.WriteLine("expected {");
                    err = 1;
                }
                else if (token == Token.CloseBrace && prev_token != Token.Semicolon && prev_token != Token.OpenBrace)
                {
                    Console.WriteLine("Неверно объявлена скобка");
                    err = 1;
                }
                else if (token == Token.Mod && prev_token != Token.Semicolon && prev_token != Token.OpenBrace)
                {
                    Console.WriteLine("Ошибка объявленния модификатора доступа");
                    err = 1;
                }
                else if (token == Token.Type && prev_token != Token.Semicolon && prev_token != Token.OpenBrace && prev_token != Token.Mod)
                {
                    Console.WriteLine("Ошибка объявления типа");
                    err = 1;
                }
                else if (token == Token.IDentificator && prev_token != Token.Type)
                {
                    Console.WriteLine("Ошибка объявления идентификатора");
                    err = 1;
                }
                else if (token == Token.StructName && prev_token != Token.StructKeyword)
                {
                    Console.WriteLine("не объявлено имя структуры");
                    err = 1;
                }
                else {
                    err = 0;
                }
                if (token == Token.Semicolon) {
                    err = 0;
                }
                if (OB > CB && (token == Token.END || i+1 ==codes.Count))
                {
                    Console.WriteLine("Структура не закрыта");
                }
                if (i == codes.Count && token != Token.END) {
                    Console.WriteLine("Структура не закончена");
                }
                i++;
            }
        }
        public void syntax()
        {
            Token token = Token.START;
            Token prev_token = Token.START;
            Token next_token = Token.ModStruct;
            int OB =0;
            int i = 0;
            int CBR = 0;
            int CB = 0;
            while (i<codes.Count) {
                switch (codes[i])
                {
                    case 1:
                        prev_token = token;
                        token = Token.StructKeyword;
                        break;
                    case 6:
                    case 7:
                    case 8:
                        prev_token = token;
                        if (token == Token.START)
                        {
                            token = Token.ModStruct;
                        }
                        else
                        {
                            token = Token.Mod;
                        }
                        break;
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        prev_token = token;
                        token = Token.Type;
                        break;
                    case 9:
                        i++;
                        continue;
                    case 10:
                        prev_token = token;
                        token = Token.OpenBrace;
                        OB ++;
                        break;
                    case 11:
                        prev_token = token;
                        token = Token.CloseBrace;
                        OB --;
                        CBR ++;
                        break;
                    case 12:
                        prev_token = token;
                        if (token == Token.CloseBrace || CB >=1)
                        {
                            token = Token.END;
                        }
                        else
                        {
                            token = Token.Semicolon;
                        }
                        break;
                    case 13:
                        prev_token = token;
                        if (token == Token.StructKeyword || next_token == Token.StructName)
                        {
                            token = Token.StructName;
                        }
                        else
                        {
                            token = Token.IDentificator;
                        }
                        break;
                    default:
                        Console.WriteLine("Лексическая ошибка");
                        prev_token = token;
                        token = Token.error;
                        break;
                }
                
                if (token == Token.error) {
                    token = Token.IDentificator;
                }
                if (token == Token.OpenBrace && OB>1) {
                    Console.WriteLine("Отбрасывается лишняя {");
                    i++;
                    continue;
                }
                if (next_token == Token.ModStruct )
                {
                    if (next_token != token)
                    {
                        if (token == Token.StructKeyword) 
                        {
                            next_token = Token.StructName;
                            Console.WriteLine("Отстутсвует MODSTRUCT");
                        }
                        else
                        {
                            Console.WriteLine($"Неправильно объявлен модификатор доступа {keyword[i]}");
                            next_token = Token.StructKeyword;
                        }
                    }
                    else
                    {
                        next_token = Token.StructKeyword;
                    }
                }
                else if (next_token == Token.StructKeyword)
                {
                    if (next_token != token)
                    {
                        next_token = Token.StructName;
                        Console.WriteLine($"Неправильно объявлена переменная ожидается 'struct'");
                    }
                    else
                    {
                        next_token = Token.StructName;
                    }
                }
                else if (next_token == Token.StructName)
                {
                    
                    if (next_token != token && token != Token.OpenBrace)
                    {
                        Console.WriteLine($"Неправильно объявлена переменная {keyword[i]}, ожидается идентификатор структуры");
                        next_token = Token.OpenBrace;
                    }
                    else
                    {
                        next_token = Token.OpenBrace;
                    }
                }
                else if (next_token == Token.OpenBrace)
                {
                    if (next_token != token)
                    {
                        next_token = Token.OpenBrace;
                        Console.WriteLine("ожидается {"+$" а была объявлена {keyword[i]}");
                        token = Token.StructName;
                    }
                    else
                    {
                        next_token = Token.Mod;
                    }
                }
                else if (next_token == Token.Semicolon)
                {
                    if (next_token != token && token != Token.CloseBrace)
                    {
                        next_token = Token.Semicolon;
                        Console.WriteLine($"ожидается ; а была объявлена {keyword[i]}");
                        token = Token.IDentificator;
                    }
                    else
                    {
                        next_token = Token.Mod;
                    }
                }
                else if (next_token == Token.Mod)
                {
                    if (token == Token.Semicolon) { 
                        Console.WriteLine("Пустая строка");
                        i++;
                        continue; 
                    }
                    if (next_token != token && token != Token.CloseBrace)
                    {
                        if (token == Token.Type)
                        {
                            next_token = Token.IDentificator;
                            Console.WriteLine(" Отсутствует MOD");
                        }
                        else
                        {
                            Console.WriteLine("MOD ERR" + $" {keyword[i]}");
                            next_token = Token.Type;
                        }
                    } 
                    else
                    {
                        next_token = Token.Type;
                    }
                }
                else if (next_token == Token.Type)
                {
                    if (token == Token.Semicolon) {
                        Console.WriteLine("TYPE ERR");
                        Console.WriteLine("ID ERR");
                        i++;
                        next_token = Token.Mod;
                        continue;
                    }
                    if (next_token != token)
                    {
                        Console.WriteLine($"TYPE ERR {keyword[i]}");
                        next_token = Token.IDentificator;
                    }
                    else
                    {
                        next_token = Token.IDentificator;
                    }
                }
                else if (next_token == Token.IDentificator)
                {
                    if (next_token != token && token != Token.Semicolon)
                    {
                        Console.WriteLine($"ID ERR {keyword[i]}");
                        next_token = Token.Semicolon;
                    }
                    else if (token == Token.Semicolon)
                    {
                        Console.WriteLine($"ID отстутсвует");
                        next_token = Token.Mod;
                    }
                    else if (token == Token.CloseBrace)
                    {
                        Console.WriteLine($"ID и ; отстутсвует");
                    }
                    else
                    {
                        next_token = Token.Semicolon;
                    }
                }
                else if (next_token == Token.END)
                {
                    if (next_token != token)
                    {
                        Console.WriteLine($"Ожидается ; а было объявлено {keyword[i]}");
                        next_token = Token.END;
                    }
                    else
                    {
                        next_token = Token.ModStruct;
                    }
                }
                if(token == Token.END)
                {
                    CB--;
                    if (i - 1 == codes.Count)
                    {
                        token = Token.START;
                    }
                }
                if (token == Token.CloseBrace)
                {
                    if (prev_token != Token.OpenBrace && prev_token != Token.Semicolon && prev_token != Token.CloseBrace)
                    {
                        Console.WriteLine("ожидается что до этого строка будет закрыта то есть ; или только начата структура {");
                    }
                    if (CB > 0)
                    {
                        Console.WriteLine("элемент } отбрасывается");
                    }
                    CB++;
                    next_token = Token.END;
                }
                else if (token == Token.Semicolon)
                {
                    if(prev_token != Token.IDentificator && next_token != Token.Mod && next_token != Token.ModStruct && OB != 0)
                    {
                        Console.WriteLine("Слишком рано завершилась");
                    }
                }
                else if (token == Token.OpenBrace) 
                {
                    if (prev_token != Token.StructName && next_token != Token.Mod) {
                        Console.WriteLine("Слишком рано ввели");
                    }
                    next_token = Token.Mod;
                }
                i++;
            }
            if (CB >= 1)
            {
                Console.WriteLine(" END ERR");
            }
            if (token != Token.END) {
                Console.WriteLine("Структура не завершена");
            }
            
        }


        public void Lexic1()
        {
            var result = Regex.Matches(text, @"[\p{L}0-9_@$.#%&*!?]+|[{};/ \+\-=?<>,()]|\s+|\"".*?\""");


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

        public List<string> errors = new List<string>();

        public void Lexic()
        {
            int i = 0;
            int prevcode = 0;

            while (i < text.Length)
            {
                char c = text[i];

                // Пробел
                if (char.IsWhiteSpace(c))
                {
                    if (prevcode != 9 && (prevcode >= 1 && prevcode < 9))
                    {
                        codes.Add(keyValuePairs[" "]);
                        keywords.Add("WhiteSpace");
                        keyword.Add(" ");
                    }
                    i++;
                    continue;
                }

                // Разделители { } ;
                if (IsDelimiter(c.ToString()))
                {
                    int code = keyValuePairs[c.ToString()];
                    codes.Add(code);

                    if (code > 0 && code < 9)
                        keywords.Add("keyword");
                    else if (code == 10 || code == 11)
                        keywords.Add("razdelitel");
                    else if (code == 12)
                        keywords.Add("end operator");

                    keyword.Add(c.ToString());
                    prevcode = code;
                    i++;
                    continue;
                }

                // Строка
                if (c == '"')
                {
                    string str = "\"";
                    i++;
                    while (i < text.Length && text[i] != '"')
                    {
                        str += text[i];
                        i++;
                    }
                    if (i < text.Length && text[i] == '"')
                    {
                        str += "\"";
                        i++;
                    }

                    codes.Add(14); // код строки
                    keywords.Add("string");
                    keyword.Add(str);
                    prevcode = 14;
                    continue;
                }

                // Идентификатор или ключевое слово
                if (char.IsLetter(c) || c == '_')
                {
                    string cleaned = "";
                    while (i < text.Length && !char.IsWhiteSpace(text[i]) && !IsDelimiter(text[i].ToString()))
                    {
                        char ch = text[i];

                        if (char.IsLetterOrDigit(ch) || ch == '_')
                        {
                            cleaned += ch;
                        }
                        else
                        {
                            errors.Add(ch.ToString()); // символ с ошибкой — в отдельный список
                        }

                        i++;
                    }

                    if (cleaned.Length > 0)
                    {
                        if (keyValuePairs.ContainsKey(cleaned))
                        {
                            int code = keyValuePairs[cleaned];
                            codes.Add(code);
                            keywords.Add("keyword");
                            keyword.Add(cleaned);
                            prevcode = code;
                        }
                        else
                        {
                            codes.Add(13);
                            keywords.Add("Id");
                            keyword.Add(cleaned);
                            prevcode = 13;
                        }
                    }

                    continue;
                }

                // Прочие символы — ошибка
                if (c != '{' && c != '}' && c != ';')
                {
                    errors.Add(c.ToString()); // ошибка только в errors
                }

                i++;
            }
        }
        private bool IsSymbol(char c)
        {
            // Символы, которые могут входить в "грязные" идентификаторы
            return "@$.#%&*!?".Contains(c);
        }

        private bool IsDelimiter(string s)
        {
            return keyValuePairs.ContainsKey(s) &&
                   (s.Length == 1 && "{};/+-=<>(),".Contains(s));
        }
       
        public void Syntax()
        {

        }


        private bool letter(string word)
        {
            if (char.IsDigit(word[0]))
                return false;

            if (word.All(c => char.IsLetterOrDigit(c) || c == '_') && !Regex.IsMatch(word, "[А-Яа-я]"))
            {
                return true;
            }
            return false;
        }
    }
}
