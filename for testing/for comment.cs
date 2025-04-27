using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace for_testing
{

    //public TokenReturn Rekurs(int CurrentPosition, TokenReturn token)
    //{



    //    if (CurrentPosition < tokens.Count)
    //    {
    //        TokenReturn currenttoken = token;
    //        TokenReturn nexttoken;
    //        bool err = er(CurrentPosition, token.tokens);
    //        if (err)
    //        {
    //            Token tr = GetNeedToken(token.tokens);

    //            currenttoken.error.Add($"Удалил что-то {CurrentPosition}");
    //            currenttoken = Rekurs(CurrentPosition + 1, new TokenReturn(1, currenttoken.tokens, currenttoken.errcounts));

    //            nexttoken = new TokenReturn(1, token.tokens, token.errcounts);
    //            nexttoken.tokens.Add(tr);
    //            nexttoken.error.Add($"Заменил что-то {CurrentPosition} на что то");
    //            nexttoken = Rekurs(CurrentPosition + 1, new TokenReturn(1, nexttoken.tokens, nexttoken.errcounts));
    //            if (nexttoken.errcounts.Sum() <= currenttoken.errcounts.Sum())
    //            {
    //                currenttoken = nexttoken;
    //            }
    //            nexttoken = token;
    //            nexttoken.tokens.Add(tr);
    //            nexttoken.error.Add($"Добавил перед {CurrentPosition} что-то ");
    //            nexttoken = Rekurs(CurrentPosition, nexttoken);
    //            if (nexttoken.errcounts.Sum() < currenttoken.errcounts.Sum())
    //            {
    //                currenttoken = nexttoken;
    //            }
    //        }
    //        else
    //        {

    //            currenttoken.tokens.Add(tokens[CurrentPosition]);
    //            currenttoken = Rekurs(CurrentPosition + 1, new TokenReturn(0, currenttoken.tokens, currenttoken.errcounts));
    //        }
    //        return currenttoken;
    //    }
    //    return token;
    //}
    //public int Rekurs1(int CurrentPosition, TokenReturn token)
    //{
    //    int absoluteErr = 0;
    //    TokenReturn closebracechange;
    //    TokenReturn closebraceAdd;
    //    bool err = false;
    //    if (CurrentPosition < tokens.Count)
    //    {
    //        if (err)
    //        {
    //            Token tr = GetNeedToken(token.tokens);
    //            TokenReturn tok = new TokenReturn(1, token.tokens, token.errcounts);
    //            Rekurs1(CurrentPosition + 1, tok);
    //            absoluteErr = tok.errcounts[tok.errcounts.Count-1];
    //            TokenReturn t = new TokenReturn(1, token.tokens, token.errcounts);
    //            if (tr == Token.TYPE) {
    //                closebracechange = new TokenReturn(1, token.tokens, token.errcounts);
    //                closebracechange.tokens.Add(Token.CLOSEBRACE);
    //                Rekurs1(CurrentPosition+1, closebracechange);
    //                if(absoluteErr > closebracechange.errcounts[closebracechange.errcounts.Count-1])
    //                {
    //                    absoluteErr = closebracechange.errcounts[closebracechange.errcounts.Count - 1];
    //                }
    //            }
    //            t.tokens.Add(tr);
    //            Rekurs1(CurrentPosition+1,t);
    //            if (absoluteErr > t.errcounts[t.errcounts.Count - 1])
    //            {
    //                absoluteErr = t.errcounts[t.errcounts.Count - 1];
    //            }
    //            TokenReturn token1 = new TokenReturn(1, token.tokens, token.errcounts);
    //            if (tr == Token.TYPE) { 
    //                closebraceAdd = new TokenReturn(1, token.tokens, token.errcounts);
    //                closebraceAdd.tokens.Add(Token.CLOSEBRACE);
    //                Rekurs1(CurrentPosition, closebraceAdd);
    //                if (absoluteErr > closebraceAdd.errcounts[closebraceAdd.errcounts.Count - 1])
    //                {
    //                    absoluteErr = closebraceAdd.errcounts[closebraceAdd.errcounts.Count - 1];
    //                }
    //            }
    //            token1.tokens.Add(tr);
    //            Rekurs1(CurrentPosition, token1);
    //            if (absoluteErr > token1.errcounts[token1.errcounts.Count - 1])
    //            {
    //                absoluteErr = token1.errcounts[token1.errcounts.Count - 1];
    //            }
    //        }
    //        else
    //        {
    //            token.tokens.Add(tokens[CurrentPosition]);
    //            int i = Rekurs1(CurrentPosition + 1, new TokenReturn(0, token.tokens, token.errcounts));
    //        }
    //    }
    //    return absoluteErr;
    //}
    //public void Lexic1()
    //{
    //    var result = Regex.Matches(text, @"[\p{L}0-9_@$.#%&*!?]+|[{};/ \+\-=?<>,()]|\s+|\"".*?\""");


    //    int prevcode = 0;
    //    foreach (Match match in result)
    //    {
    //        int code = 0;
    //        if (match.Value.Trim().Length == 0 && prevcode != 9 && (prevcode >= 1 && prevcode < 9))
    //        {
    //            code = keyValuePairs[match.Value];
    //            codes.Add(code);
    //            keywords.Add("WhiteSpace");
    //            keyword.Add(match.Value);

    //        }
    //        else if (keyValuePairs.ContainsKey(match.Value))
    //        {
    //            code = keyValuePairs[match.Value];
    //            if (code != 9)
    //            {
    //                codes.Add(code);
    //                if (code > 0 && code < 9)
    //                {
    //                    keywords.Add("keyword");
    //                }
    //                else if (code == 10 || code == 11)
    //                {
    //                    keywords.Add("razdelitel");
    //                }
    //                else if (code == 12)
    //                {
    //                    keywords.Add("end operator");
    //                }
    //                keyword.Add(match.Value);
    //            }
    //            else
    //            {
    //                continue;
    //            }
    //        }
    //        else if (letter(match.Value))
    //        {
    //            code = 13;
    //            codes.Add(code);
    //            keywords.Add("Id");
    //            keyword.Add(match.Value);
    //        }
    //        else
    //        {
    //            if (match.Value.Trim().Length != 0)
    //            {
    //                code = 0;
    //                codes.Add(code);
    //                keywords.Add("Error");
    //                keyword.Add(match.Value);
    //            }
    //        }
    //        prevcode = code;
    //    }
    //public void test2()
    //{
    //    Token token = Token.START;
    //    Token prev_token = Token.START;
    //    List<Token> tokens = new List<Token>();
    //    int i = 0;
    //    int err = 0;
    //    int OB = 0;
    //    int CB = 0;
    //    List<Token> types = new List<Token>();
    //    while (i < codes.Count)
    //    {

    //        switch (codes[i])
    //        {
    //            case 1:
    //                if (err == 0)
    //                {
    //                    prev_token = token;
    //                }
    //                token = Token.StructKeyword;

    //                break;
    //            case 6:
    //            case 7:
    //            case 8:
    //                if (err == 0)
    //                {
    //                    prev_token = token;
    //                }

    //                if (token == Token.START)
    //                {

    //                    token = Token.ModStruct;
    //                }
    //                else 
    //                {
    //                    token = Token.Mod;
    //                    tokens.Add(token);
    //                }


    //                break;
    //            case 2:
    //            case 3:
    //            case 4:
    //            case 5:
    //                if (err == 0)
    //                {
    //                    prev_token = token;
    //                }
    //                token = Token.Type;

    //                tokens.Add(token);
    //                break;
    //            case 9:
    //                i++;
    //                continue;
    //            case 10:
    //                if (err == 0)
    //                {
    //                    prev_token = token;
    //                }
    //                OB++;
    //                token = Token.OpenBrace;

    //                break;
    //            case 11:
    //                if (err == 0)
    //                {
    //                    prev_token = token;
    //                }
    //                CB++;
    //                token = Token.CloseBrace;

    //                break;
    //            case 12:
    //                if (err == 0)
    //                {
    //                    prev_token = token;
    //                }
    //                if (token == Token.CloseBrace)
    //                {
    //                    token = Token.END;
    //                }
    //                else
    //                {
    //                    token = Token.Semicolon;
    //                }
    //                break;
    //            case 13:
    //                if (err == 0)
    //                {
    //                    prev_token = token;
    //                }
    //                if (token == Token.StructKeyword)
    //                {
    //                    token = Token.StructName;
    //                }
    //                else
    //                {
    //                    tokens.Add(token);

    //                    token = Token.IDentificator;
    //                }
    //                break;
    //            default:
    //                Console.WriteLine("Лексическая ошибка");
    //                i++;
    //                token = Token.error;
    //                continue;

    //        }
    //        if(token == Token.END)
    //        {
    //            token = Token.START;
    //            prev_token = Token.END;
    //            tokens.Clear(); 
    //        }
    //        if (token == Token.Semicolon||token==Token.CloseBrace)
    //        {
    //            if (tokens.Count > 3)
    //            {
    //                //Console.WriteLine("Слишком большое количество аргументов");

    //            }
    //            tokens.Clear();
    //        }

    //        //private struct Mystruct {  private int  x; private string name; }; 
    //        if (token == Token.ModStruct && prev_token != Token.START)
    //        {
    //            Console.WriteLine("Врядли возможно");
    //            err = 1;
    //        }
    //        else if (token == Token.StructKeyword && prev_token != Token.START && prev_token != Token.ModStruct)
    //        {
    //            Console.WriteLine("Структура не может быть объявлена здесь");
    //            err = 1;
    //        }
    //        else if (token == Token.OpenBrace && prev_token != Token.StructName)
    //        {
    //            Console.WriteLine("expected {");
    //            err = 1;
    //        }
    //        else if (token == Token.CloseBrace && prev_token != Token.Semicolon && prev_token != Token.OpenBrace)
    //        {
    //            Console.WriteLine("Неверно объявлена скобка");
    //            err = 1;
    //        }
    //        else if (token == Token.Mod && prev_token != Token.Semicolon && prev_token != Token.OpenBrace)
    //        {
    //            Console.WriteLine("Ошибка объявленния модификатора доступа");
    //            err = 1;
    //        }
    //        else if (token == Token.Type && prev_token != Token.Semicolon && prev_token != Token.OpenBrace && prev_token != Token.Mod)
    //        {
    //            Console.WriteLine("Ошибка объявления типа");
    //            err = 1;
    //        }
    //        else if (token == Token.IDentificator && prev_token != Token.Type)
    //        {
    //            Console.WriteLine("Ошибка объявления идентификатора");
    //            err = 1;
    //        }
    //        else if (token == Token.StructName && prev_token != Token.StructKeyword)
    //        {
    //            Console.WriteLine("не объявлено имя структуры");
    //            err = 1;
    //        }
    //        else {
    //            err = 0;
    //        }
    //        if (token == Token.Semicolon) {
    //            err = 0;
    //        }
    //        if (OB > CB && (token == Token.END || i+1 ==codes.Count))
    //        {
    //            Console.WriteLine("Структура не закрыта");
    //        }
    //        if (i == codes.Count && token != Token.END) {
    //            Console.WriteLine("Структура не закончена");
    //        }
    //        i++;
    //    }
    //}
    //public void Lexic1()
    //{
    //    var result = Regex.Matches(text, @"[\p{L}0-9_@$.#%&*!?]+|[{};/ \+\-=?<>,()]|\s+|\"".*?\""");


    //    int prevcode = 0;
    //    foreach (Match match in result)
    //    {
    //        int code = 0;
    //        if (match.Value.Trim().Length == 0 && prevcode != 9 && (prevcode >= 1 && prevcode < 9))
    //        {
    //            code = keyValuePairs[match.Value];
    //            codes.Add(code);
    //            keywords.Add("WhiteSpace");
    //            keyword.Add(match.Value);

    //        }
    //        else if (keyValuePairs.ContainsKey(match.Value))
    //        {
    //            code = keyValuePairs[match.Value];
    //            if (code != 9)
    //            {
    //                codes.Add(code);
    //                if (code > 0 && code < 9)
    //                {
    //                    keywords.Add("keyword");
    //                }
    //                else if (code == 10 || code == 11)
    //                {
    //                    keywords.Add("razdelitel");
    //                }
    //                else if (code == 12)
    //                {
    //                    keywords.Add("end operator");
    //                }
    //                keyword.Add(match.Value);
    //            }
    //            else
    //            {
    //                continue;
    //            }
    //        }
    //        else if (letter(match.Value))
    //        {
    //            code = 13;
    //            codes.Add(code);
    //            keywords.Add("Id");
    //            keyword.Add(match.Value);
    //        }
    //        else
    //        {
    //            if (match.Value.Trim().Length != 0)
    //            {
    //                code = 0;
    //                codes.Add(code);
    //                keywords.Add("Error");
    //                keyword.Add(match.Value);
    //            }
    //        }
    //        prevcode = code;
    //    }
    //}


}
