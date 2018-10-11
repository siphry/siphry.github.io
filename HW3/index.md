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
/*
 * A singly linked node class. 
 */

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
/*
 * Interface for a FIFO queue in a singly linked list
 */

namespace assignment3
{
    public interface IQueueInterface<T>
    {

        /*
         * Adds an element to the rear of the queue
         * @return the element that was enqueued
         */
        T Enqueue(T element);

        /*
         * Removes and returns the front element.
         */
        T Dequeue();

        /*
         * Tests if the queue is empty
         * @return true if queue is empty: otherwise false
         */
        bool IsEmpty();

    }
}
```
IQueueInterface was another simple translation -- I looked up the C# Queue data structure online and decided to rename push and pop to Enqueue and Dequeue. Beyond that the interface is basically the same syntax with different naming conventions (methods in UpperCamelCase), and the interface must be start with the letter "I".

```csharp
/*
 * A custom unchecked exception to represent situations where
 * an illegal operation was performed on an empty queue.
 */

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

LinkedQueue.cs and Runner.cs required the most work to change to C#. There were lots of naming conventions to check (though some stayed the same) -- private fields, parameters, and local variables in lowerCamelCase, with public fields, methods, classes, in UpperCamelCase. At first I didn't realize that local variabls were lowerCamelCase, so I had written them all in caps (Q, SB, SBC), but after talking with fellow classmates I switched them to the proper convention. 

While I wrote IQueueInterface, LinkedQueue, and Runner, I would google things I was not sure about -- like how to write exception handling in C#, the equivalent exception for NullPointerException (NullReferenceException) -- beyond that and naming conventions, the Java code translated to C# fairly closely. I wrote out the entire code exactly the same, because I could not remember how to resolve the "null error" we discussed in class. Thankfully, Visual Studio caught my error and gave me the solution: `T temp = default(T);`

My process for writing the Runner (Main) class was so much easier than I originally thought it would be. I googled how to write a "Main" class in C# and found it was fairly similar to Java except that it could not be called 'Main'. The only things I double checked with google was `StringBuilder` and how to include the LinkedList structure in my code (`using System.Collections.Generic;`), and Visual Studio prompted th catch error (`NotFiniteNumberException`) so I didn't even have to look that up since it automatically populated into my code. Originally I set maxLength to output.Last.value.Length, but that did not do the spacing correctly in the for loop so I removed the loop without realizing it removed the right justified spacing. I asked Nick about it and he explained `Count()` and `using System.Linq;` so I changed my code to that to include the proper spacing in the output. 

### Step 7 [Test]
![cmdln](https://siphry.github.io/HW3/images/cmdln.PNG)
It works! I ran this after fixing all my errors and adding my comments, etc. I was really surprised by how simple it was to translate from Java to C#. I really like what I've seen so far of C# and look forward to learning more about it -- anyting is better than C++ for me!