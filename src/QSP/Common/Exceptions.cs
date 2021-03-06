using System;
using System.Runtime.Serialization;

namespace QSP.Common
{
    [Serializable]
    public class InvalidUserInputException : ApplicationException
    {
        public InvalidUserInputException() { }

        public InvalidUserInputException(string message) : base(message)
        { }

        public InvalidUserInputException(string message, Exception inner)
            : base(message, inner)
        { }
    }

    [Serializable]
    public class RunwayTooShortException : Exception
    {
        public RunwayTooShortException() { }

        public RunwayTooShortException(string message) : base(message)
        { }

        public RunwayTooShortException(string message, Exception inner)
            : base(message, inner)
        { }
    }

    [Serializable]
    public class PoorClimbPerformanceException : Exception
    {

        public PoorClimbPerformanceException() { }

        public PoorClimbPerformanceException(string message) : base(message)
        { }

        public PoorClimbPerformanceException(string message, Exception inner)
            : base(message, inner)
        { }
    }

    [Serializable]
    public class NoFileNameAvailException : Exception
    {
        public NoFileNameAvailException() { }
        public NoFileNameAvailException(string message) : base(message) { }
        public NoFileNameAvailException(string message, Exception inner)
            : base(message, inner)
        { }
        protected NoFileNameAvailException(
          SerializationInfo info,
          StreamingContext context)
            : base(info, context)
        { }
    }

    [Serializable]
    public class UnexpectedExecutionStateException : Exception
    {
        public UnexpectedExecutionStateException()
            : this("Something unexpected happened due to a bug in the program.")
        { }

        public UnexpectedExecutionStateException(string message)
            : base(message) { }

        public UnexpectedExecutionStateException(string message, Exception inner)
            : base(message, inner) { }

        protected UnexpectedExecutionStateException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
