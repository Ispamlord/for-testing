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
            string text = "private struct iaf {private int aga; public int name; };";
            Scan lexic = new Scan(text);

            lexic.Lexic();
            for (int i = 0; i < lexic.codes.Count; i++) {
                Console.WriteLine($"{lexic.keyword[i]}: {lexic.keywords[i]}: {lexic.codes[i]}");
            }
            for (int i = 0; i < lexic.errors.Count; i++) {
                Console.WriteLine($"оnброшено {lexic.errors[i]}");
            }
            lexic.syntax();
        }
    }
}
