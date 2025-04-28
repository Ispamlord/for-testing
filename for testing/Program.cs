using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace for_testing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Console.OutputEncoding = Encoding.UTF8;
            string text = "IF X== 0 THEN GOTO 100 ELSe GOTO id;";
            Scan lexic = new Scan(text);

            lexic.Lexic();
            for (int i = 0; i < lexic.codes.Count; i++) {
                Console.WriteLine($"{lexic.keyword[i]}: {lexic.keywords[i]}: {lexic.codes[i]}");
            }
            for (int i = 0; i < lexic.errors.Count; i++) {
                Console.WriteLine($"отброшено {lexic.errors[i]}");
            }
            lexic.Tokenize(); // Заполняет список tokens
            for (int i = 0; i < lexic.tokens.Count; i++)
            {
                Console.WriteLine($" {lexic.tokens[i]}");
            }
            Parse parse = new Parse(lexic.tokens, lexic.fortoken);
            parse.myTokens = lexic.myTokens;
            TokenReturn token = new TokenReturn();
            int k = parse.Parser(ERRORTYPE.START, 0,ref token);

            for (int i = 0; i < token.error.Count; i++) {
                Console.WriteLine($"{token.error[i].Line} строка:  {token.error[i].Position} позиция:  {token.error[i].mess}");
            }



        }
    }
}
