/*
 * A custom unchecked exception to represent situations where
 * an illegal operation was performed on an empty queue.
 */

using System;

namespace assignment3
{
    class QueueUnderflowException : Exception
    {
        public QueueUnderflowException()
        {
        }

        public QueueUnderflowException(string message) : base(message)
        {
        }
    }
}
