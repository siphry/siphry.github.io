using System;
using System.Text;
using System.Collections.Generic;

namespace assignment3
{
    class BinaryRepresentor
    {
        static LinkedList<String> GenerateBinaryRepresentationList(int n)
        {
            LinkedQueue<StringBuilder> Q = new LinkedQueue<StringBuilder>();

            LinkedList<string> Output = new LinkedList<string>();

            if(n < 1)
            {
                return Output;
            }

            Q.Enqueue(new StringBuilder("1"));

            while(n-- > 0)
            {
                StringBuilder SB = Q.Dequeue();
                Output.AddLast(SB.ToString());

                StringBuilder SBC = new StringBuilder(SB.ToString());

                SB.Append('0');
                Q.Enqueue(SB);
                SBC.Append('1');
                Q.Enqueue(SBC);
            }

            return Output;
        }

        public static void Main(string[] args)
        {
            int N = 10;
            if(args.Length < 1)
            {
                Console.WriteLine("Please invoke with the max value to print binary up to, like this:");
                Console.WriteLine("\t HOW TO RUN PROGRAM FROM CMDLN");

                return;
            }
            try
            {
                N = int.Parse(args[0]);
            }
            catch (NotFiniteNumberException)
            {
                Console.WriteLine("I'm sorry, I can't understand the number: " + args[0]);
                return;
            }

            LinkedList<string> Output = GenerateBinaryRepresentationList(N);
            int MaxLength = Output.Last.Value.Length;
            foreach (string S in Output)
            {
                for(int i = 0; i < MaxLength - S.Length; ++i)
                {
                    Console.WriteLine(" ");
                }
                Console.WriteLine(S);
            }

        }
    }
}
