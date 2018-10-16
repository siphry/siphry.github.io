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
