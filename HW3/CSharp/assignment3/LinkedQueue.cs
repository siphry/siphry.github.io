/*
 * A singly linked queue utilizing the IQueueInterface
 */

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

        /* 
         * Adds and returns an element to the queue
         * @param element The element to add
         * @return the element that was enqueued
         */
        public T Enqueue(T element)
        {
            if (element == null)
            {
                throw new System.NullReferenceException();
            }

            if (IsEmpty())
            {
                Node<T> temp = new Node<T>(element, null);
                rear = front = temp;
            }
            else
            {
                Node<T> temp = new Node<T>(element, null);
                rear.Next = temp;
                rear = temp;
            }

            return element;
        }

        /* 
        * Removes and returns the first element of the queue
        * @return the element that was dequeued
        */
        public T Dequeue()
        {
            T temp = default(T);
            if (IsEmpty())
            {
                throw new QueueUnderflowException("The queue was empty when pop was invoked.");
            }
            else if (front == rear)
            {
                temp = front.Data;
                front = null;
                rear = null;
            }
            else
            {
                temp = front.Data;
                front = front.Next;
            }

            return temp;
        }

        /* 
        * Returns true if queue is empty: otherwise false
        */
        public bool IsEmpty()
        {
            if (front == null && rear == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
