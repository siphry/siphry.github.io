
namespace assignment3
{
    class LinkedQueue<T> : IQueueInterface<T>
    {
        private Node<T> front;
        private Node<T> rear;

        public LinkedQueue()
        {
            front = null;
            rear = null;
        }

        public T Enqueue(T element)
        {
            if (element == null)
            {
                throw new System.NullReferenceException();
            }

            if (IsEmpty())
            {
                Node<T> Temp = new Node<T>(element, null);
                rear = front = Temp;
            }
            else
            {
                Node<T> Temp = new Node<T>(element, null);
                rear.Next = Temp;
                rear = Temp;
            }

            return element;
        }

        public T Dequeue()
        {
            T Temp = default(T);
            if (IsEmpty())
            {
                throw new QueueUnderflowException("The queue was empty when pop was invoked.");
            }
            else if (front == rear)
            {
                Temp = front.Data;
                front = null;
                rear = null;
            }
            else
            {
                Temp = front.Data;
                front = front.Next;
            }

            return Temp;
        }

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
