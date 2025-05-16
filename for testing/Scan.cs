using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace for_testing
{
    public enum Token
    {
        IF,
        THEN,
        ELSE,
        GOTO,
        OPERATOR,    
        SEMICOLON,   
        ID,         
        NUMBER,
        EQUELS,
        VOSKL,
        UNKNOWN
    }
    public class Scan
    {
        Dictionary<string, Token> state = new Dictionary<string, Token>();
        public string text;
        public string[] words;
        public List<MyToken> myTokens;
        public List<int> line = new List<int>();
        public List<int> position = new List<int>();
        public Scan(string text)
        {
            this.text = text;
            myTokens = new List<MyToken>();
            line = new List<int>();
            position = new List<int>();
        }
        public List<string> keywords = new List<string>();
        public List<string> keyword = new List<string>();
        public List<int> codes = new List<int>();
        public List<Token> tokens = new List<Token>();
        public List<string> errors = new List<string>();

        public List<string> fortoken = new List<string>();
        public Dictionary<string, int> keyValuePairs = new Dictionary<string, int>()
        {
            { "IF", 1 },
            { "THEN", 2 },
            { "GOTO", 3 },
            { "ELSE", 4 },
            { "=", 5 },
            { ">", 6 },
            { "<", 7 },
            { ";", 8 },
            { "!", 9 }

        };
        public void Tokenize()
        {
            for (int i = 0; i < codes.Count; i++)
            {
                switch (codes[i])
                {
                    case 1:
                        tokens.Add(Token.IF);
                        fortoken.Add(keyword[i]);
                        break;
                    case 2:
                        tokens.Add(Token.THEN);
                        fortoken.Add(keyword[i]);
                        break;
                    case 3:
                        tokens.Add(Token.GOTO);
                        fortoken.Add(keyword[i]);
                        break;
                    case 4:
                        tokens.Add(Token.ELSE);
                        fortoken.Add(keyword[i]);
                        break;
                    case 5:
                        tokens.Add(Token.EQUELS);
                        fortoken.Add(keyword[i]);
                        break;
                    case 6:
                    case 7:
                        tokens.Add(Token.OPERATOR);
                        fortoken.Add(keyword[i]);
                        break;
                    case 8:
                        tokens.Add(Token.SEMICOLON);
                        fortoken.Add(keyword[i]);
                        break;
                    case 13:
                        tokens.Add(Token.ID);
                        fortoken.Add(keyword[i]);
                        break;
                    case 14:
                        tokens.Add(Token.NUMBER);
                        fortoken.Add(keyword[i]);
                        break;
                    case 9:
                        tokens.Add(Token.VOSKL);
                        fortoken.Add(keyword[i]);
                        break;
                    default:
                        break;
                }
            }
        }
        public void Lexic()
        {
            int i = 0;
            int prevcode = 0;
            int line = 1;
            bool expectNumber = false; // Ожидается ли число после GOTO

            while (i < text.Length)
            {
                char c = text[i];

                if (c == '\n')
                {
                    line++;
                    i++;
                    continue;
                }

                if (char.IsWhiteSpace(c))
                {
                    i++;
                    continue;
                }

                // Разрешенные одиночные символы
                if (c == '=' || c == '>' || c == '<' || c == ';' || c == '!')
                {
                    int code = keyValuePairs.ContainsKey(c.ToString()) ? keyValuePairs[c.ToString()] : 0;
                    if (code != 0)
                    {
                        codes.Add(code);
                        keyword.Add(c.ToString());
                        keywords.Add("operator or end");
                        myTokens.Add(new MyToken(i, line, c.ToString()));
                        prevcode = code;
                    }
                    else
                    {
                        errors.Add(c.ToString());
                        this.line.Add(line);
                        this.position.Add(i);
                    }
                    i++;
                    continue;
                }

                // Идентификаторы и ключевые слова
                if (char.IsLetter(c) || c == '_')
                {
                    int start = i;
                    string word = "";
                    bool hasRussian = false;

                    while (i < text.Length && !char.IsWhiteSpace(text[i]) && !IsDelimiter(text[i].ToString()))
                    {
                        char ch = text[i];

                        if (char.IsLetterOrDigit(ch) || ch == '_')
                        {
                            if ((ch >= 'А' && ch <= 'я') || ch == 'ё' || ch == 'Ё')
                            {
                                hasRussian = true;
                            }
                            else
                            {
                                word += ch;
                            }
                            i++;
                        }
                        else
                        {
                            break; 
                        }
                    }


                    if (word.Length > 0)
                    {
                        if (hasRussian)
                        {
                            errors.Add(word);
                            this.line.Add(line);
                            this.position.Add(start);
                        }
                        else
                        {
                            string wordUpper = word.ToUpper(); // игнорируем регистр
                            if (keyValuePairs.ContainsKey(wordUpper))
                            {
                                int code = keyValuePairs[wordUpper];
                                codes.Add(code);
                                keywords.Add("keyword");
                                keyword.Add(wordUpper);
                                myTokens.Add(new MyToken(start, line, wordUpper));
                                prevcode = code;

                                if (wordUpper == "GOTO")
                                    expectNumber = true;
                                else
                                    expectNumber = false;
                            }
                            else
                            {
                                codes.Add(13); // идентификатор
                                keywords.Add("Id");
                                keyword.Add(word);
                                myTokens.Add(new MyToken(start, line, word));
                                prevcode = 13;
                                expectNumber = false;
                            }
                        }
                    }
                    continue;
                }

                // Числа
                // Числа
                if (char.IsDigit(c))
                {
                    int start = i;

                    if (c == '0')
                    {
                        // Добавить 0 как отдельный токен
                        codes.Add(14);
                        keywords.Add("number");
                        keyword.Add("0");
                        myTokens.Add(new MyToken(i, line, "0"));
                        prevcode = 14;
                        i++;

                        // Проверить, есть ли ещё цифры после 0
                        if (i < text.Length && char.IsDigit(text[i]))
                        {
                            int secondStart = i;
                            string number = "";
                            while (i < text.Length && !char.IsWhiteSpace(text[i]) && !IsDelimiter(text[i].ToString()) && char.IsDigit(text[i]))
                            {
                                number += text[i];
                                i++;
                            }

                            if (number.Length > 0)
                            {
                                codes.Add(14);
                                keywords.Add("number");
                                keyword.Add(number);
                                myTokens.Add(new MyToken(secondStart, line, number));
                                prevcode = 14;
                            }
                        }
                    }
                    else
                    {
                        string number = "";
                        while (i < text.Length && !char.IsWhiteSpace(text[i]) && !IsDelimiter(text[i].ToString()) && char.IsDigit(text[i]))
                        {
                            number += text[i];
                            i++;
                        }

                        if (number.Length > 0)
                        {
                            codes.Add(14);
                            keywords.Add("number");
                            keyword.Add(number);
                            myTokens.Add(new MyToken(start, line, number));
                            prevcode = 14;
                        }
                    }

                    continue;
                }


                // Все остальные символы — ошибка
                errors.Add(c.ToString());
                this.line.Add(line);
                this.position.Add(i);
                i++;
            }
        }






        private bool IsSymbol(char c)
        {
            // Символы, которые могут входить в "грязные" идентификаторы
            return "@$.#%&*!?\"\\|:[]/+-=<>()".Contains(c);
        }

        private bool IsDelimiter(string s)
        {
            return keyValuePairs.ContainsKey(s) &&
                   (s.Length == 1 && "{};,".Contains(s));
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
