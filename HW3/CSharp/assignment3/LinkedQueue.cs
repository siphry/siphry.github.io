
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

        public T push(T element)
        {
            if(element == null)
            {
                throw new System.NullReferenceException();
            }

            if(isEmpty())
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


    }
}
