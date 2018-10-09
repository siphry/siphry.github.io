namespace assignment3
{
    class Node<T>
    {
        public T Data;
        public Node<T> Next;

        public Node (T data, Node<T> next)
        {
            this.Data = data;
            this.Next = next;
        }

        static void Main(string[] args)
        {
        }
    }
}
