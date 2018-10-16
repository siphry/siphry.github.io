using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace assignment3
{
    class BinaryRepresentor
    {
        /// <summary>
        /// Prints the binary representation of all the numbers from 1 up to and incuding n.
        /// Uses a FIFO queue
        /// </summary>
        /// <param name="n"></param>
        /// <returns>
        /// LinkedList<String>
        /// </returns>
      
        static LinkedList<String> GenerateBinaryRepresentationList(int n)
        {
            
            //Create an empty queue for the traversal
            LinkedQueue<StringBuilder> q = new LinkedQueue<StringBuilder>();
      
            //A list to contain the binary values
            LinkedList<string> output = new LinkedList<string>();

            if (n < 1)
            {
                //negative numbers is not supported
                //returns the empty list
                return output;
            }

            //Enqueues the first binary number with a dynamic string
            q.Enqueue(new StringBuilder("1"));

            while (n-- > 0)
            {
                //print front of queue
                StringBuilder sb = q.Dequeue();
                output.AddLast(sb.ToString());

                //make a copy
                StringBuilder sbc = new StringBuilder(sb.ToString());

                //left child append
                sb.Append('0');
                q.Enqueue(sb);
                //right child append
                sbc.Append('1');
                q.Enqueue(sbc);
            }

            return output;
        }
        /// <summary>
        /// driver fucntion to test above
        /// </summary>
       
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
            //prints binary numbers right justified with the longest string last
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
