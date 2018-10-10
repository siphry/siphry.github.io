/*
 * A FIFO queue interface. This ADT is suitable for a singly
 * linked queue
 */

namespace assignment3
{
    public interface IQueueInterface<T>
    {

        /*
         * Add an element to the rear of the queue
         * @return the element that was enqueued
         */
        T Push(T element);

        /*
         * Remove and return the front element.
         */
        T Pop();

        /*
         * Test if the queue is empty
         * @return true if queue is empty: otherwise false
         */
        bool IsEmpty();

    }
}
