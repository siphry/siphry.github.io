namespace assignment3
{
    public interface IQueueInterface<T>
    {

        T Push(T element);

        T Pop();

        bool IsEmpty();

    }
}
