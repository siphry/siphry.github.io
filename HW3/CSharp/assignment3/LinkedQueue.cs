
namespace assignment3
{
    class LinkedQueue<T> : IQueueInterface<T>
    {
        private Node<T> Front;
        private Node<T> Rear;

        public LinkedQueue()
        {
            Front = null;
            Rear = null;
        }

        public T Enqueue(T element)
        {
            if(element == null)
            {
                throw new System.NullReferenceException();
            }

            if(IsEmpty())
            {
                Node<T> Temp = new Node<T>(element, null);
                Rear = Front = Temp;
            } else {
                Node<T> Temp = new Node<T>(element, null);
                Rear.Next = Temp;
                Rear = Temp;
            }

            return element;
        }

        public T Dequeue()
        {
            T Temp = default(T);
            if(IsEmpty())
            {
                throw new QueueUnderflowException("The queue was empty when pop was invoked.");
            }
            else if (Front == Rear)
            {
                Temp = Front.Data;
                Front = null;
                Rear = null;
            } else
            {
                Temp = Front.Data;
                Front = Front.Next;
            }

            return Temp;
        }

        public bool IsEmpty()
        {
            if(Front == null && Rear == null)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
