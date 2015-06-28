namespace Twitter.Shared.Messages
{
    public class Results
    {
        public abstract class BaseResult
        { }

        public class SuccessResult : BaseResult
        { }

        public class FailureResult : BaseResult
        {
            public FailureResult()
                : this("Unspecified error")
            { }

            public FailureResult(string error)
            {
                Error = error;
            }

            public string Error { get; private set; }
        }
    }
}
