## Homework 3
This assignment was designed in order for us to get comfortable with writing in C# -- the different naming conventions, interfaction implementation, exception handling, and more. We were given a simple Java program that prints out all the binary representations of a number from 1 to n and we converted that into C# using the Visual Studio IDE. 

### Links
[Home](https://siphry.github.io)  
[Assignment Details](http://www.wou.edu/~morses/classes/cs46x/assignments/HW3_1819.html)  
[Code Repository](https://github.com/siphry/siphry.github.io/tree/master/HW3)  

### Step 1 & 2 [Setup]
First we downloaded and set up the Visual Studio IDE (Community 2017) and installed the necessary modifiers for programming in C#. This was a simple process, beyond taking over an hour to install each on my Surface and on my home PC. I then downloaded the Java files, compiled, and test the program. Then I set up my .gitignore file to exclude everything but the .cs files in my HW3 subfolders.

### Step 3 & 4 [Planning & Design, Content/Coding]
After Visual Studio IDE was finished installing, I started a new console app project and had my blank .cs files and their corresponding Java files open with a split screen so I could look at the Java files while I wrote my C# versions. I also started a new branch on my git called "hw3" from the beginning (with my .gitignore file and testing that before converting the code) and used that branch for the entire project. 

### Step 5 & 6 [Content/Coding]
I started translating the Java code to C# in the order suggested by the assignment --> Node.cs, IQueueInterface.cs, QueueUnderflowException.cs, LinkedQueue.cs, and Runner.cs  

```csharp
/// <summary>
/// A singly linked node class. 
/// </summary>

namespace assignment3
{
    class Node<T>
    {
        public T Data;
        public Node<T> Next;

        public Node(T data, Node<T> next)
        {
            this.Data = data;
            this.Next = next;
        }
    }
}
```

Node.cs was the most simple one to translate -- all I had to do basically was change the naming conventions since Visual Studio IDE set up the empty class with the namespace, etc already there. Public fields in C# use UpperCamelCase, so I changed the field names from lower case to upper case.  

```csharp
/// <summary>
/// Interface for a FIFO queue in a singly linked list
/// </summary>

namespace assignment3
{
    public interface IQueueInterface<T>
    {

        /// <summary>
        /// Adds an element to the rear of the queue
        /// @return the element that was enqueued
        /// </summary>

        T Enqueue(T element);

        /// <summary>
        /// Removes and returns the front element.
        /// </summary>
        /// <returns>
        /// The front element
        /// </returns>
        T Dequeue();


        /// <summary>
        /// Tests if the queue is empty
        /// </summary>
        /// <returns>
        /// return true if queue is empty: otherwise false
        /// </returns>
        bool IsEmpty();

    }
}
```
IQueueInterface was another simple translation -- I looked up the C# Queue data structure online and decided to rename push and pop to Enqueue and Dequeue. Beyond that the interface is basically the same syntax with different naming conventions (methods in UpperCamelCase), and the interface must be start with the letter "I".

```csharp
/// <summary>
/// A custom unchecked exception to represent situations where
/// an illegal operation was performed on an empty queue.
/// </summary>

using System;

namespace assignment3
{
    class QueueUnderflowException : Exception
    {
        public QueueUnderflowException()
        {
        }

        public QueueUnderflowException(string message) : base(message)
        {
        }
    }
}
```

Again, another easy switch: The main differences being naming conventions, the exception implementation/class signature (: rather than extends), and `base` in the signature rather than `super` inside the method.

```csharp
/// <summary>
/// A singly linked queue utilizing the IQueueInterface
/// </summary>

namespace assignment3
{
    //C# interface implementation
    class LinkedQueue<T> : IQueueInterface<T>
    {
        private Node<T> front;
        private Node<T> rear;

        /* Default constructor */
        public LinkedQueue()
        {
            front = null;
            rear = null;
        }

        /// <summary>
        /// Adds and returns an element to the queue
        /// </summary>
        /// <param name="element">The element to add</param>
        /// <returns>the element that was enqueued</returns>
        public T Enqueue(T element)
        {
            if (element == null)
            {
                throw new System.NullReferenceException();
            }

            //...
```

LinkedQueue.cs and Runner.cs required the most work to change to C#. There were lots of naming conventions to check (though some stayed the same) -- private fields, parameters, and local variables in lowerCamelCase, with public fields, methods, classes, in UpperCamelCase. At first I didn't realize that local variabls were lowerCamelCase, so I had written them all in caps (Q, SB, SBC), but after talking with fellow classmates I switched them to the proper convention. 

While I wrote IQueueInterface, LinkedQueue, and Runner, I would google things I was not sure about -- like how to write exception handling in C#, the equivalent exception for NullPointerException (NullReferenceException) -- beyond that and naming conventions, the Java code translated to C# fairly closely. I wrote out the entire code exactly the same, because I could not remember how to resolve the "null error" we discussed in class. Thankfully, Visual Studio caught my error and gave me the solution: `T temp = default(T);`

```csharp
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
```

My process for writing the Runner (Main) class was so much easier than I originally thought it would be. I googled how to write a "Main" class in C# and found it was fairly similar to Java except that it could not be called 'Main'. The only things I double checked with google was `StringBuilder` and how to include the LinkedList structure in my code (`using System.Collections.Generic;`), and Visual Studio prompted the catch error (`NotFiniteNumberException`) so I didn't even have to look that up since it automatically populated into my code. Originally I set maxLength to output.Last.Value.Length, but that did not do the spacing correctly in the for loop so I removed the loop without realizing it removed the right justified spacing. I asked Nick about it and he explained `Count()` and `using System.Linq;` so I changed my code to that to include the proper spacing in the output. 

### Step 7 [Test]
![cmdln](https://siphry.github.io/HW3/images/cmdln.PNG)
It works! I ran this after fixing all my errors and adding my comments, etc. I was really surprised by how simple it was to translate from Java to C#. I really like what I've seen so far of C# and look forward to learning more about it -- anyting is better than C++ for me!