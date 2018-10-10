using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace assignment3
{
    class BinaryRepresentor
    {
        static LinkedList<String> GenerateBinaryRepresentationList(int n)
        {
            LinkedQueue<StringBuilder> q = new LinkedQueue<StringBuilder>();

            LinkedList<string> output = new LinkedList<string>();

            if (n < 1)
            {
                return output;
            }

            q.Enqueue(new StringBuilder("1"));

            while (n-- > 0)
            {
                StringBuilder sb = q.Dequeue();
                output.AddLast(sb.ToString());

                StringBuilder sbc = new StringBuilder(sb.ToString());

                sb.Append('0');
                q.Enqueue(sb);
                sbc.Append('1');
                q.Enqueue(sbc);
            }

            return output;
        }

        public static void Main(string[] args)
        {
            int n = 10;
            if (args.Length < 1)
            {
                Console.WriteLine("Please invoke with the max value to print binary up to, like this:");
                Console.WriteLine("\t assignment3 12");

                return;
            }
            try
            {
                n = int.Parse(args[0]);
            }
            catch (NotFiniteNumberException)
            {
                Console.WriteLine("I'm sorry, I can't understand the number: " + args[0]);
                return;
            }

            LinkedList<string> output = GenerateBinaryRepresentationList(n);
            int maxLength = output.Count();
            foreach (string S in output)
            {
                for(int i = 0; i < maxLength - S.Length; ++i)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(S);
            }
        }
    }
}
