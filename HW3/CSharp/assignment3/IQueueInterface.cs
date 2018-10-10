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
