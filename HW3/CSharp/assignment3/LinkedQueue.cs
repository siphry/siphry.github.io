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

        /// <summary>
        /// Removes and returns the first element of the queue
        /// </summary>
        /// <returns>the element that was dequeued</returns>
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

        /// <summary>
        /// Returns true if queue is empty: otherwise false
        /// </summary>
        /// <returns></returns>
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
