namespace BeelineMicroService
{
    public class AggregateResult
    {
        public long Version { get; }
        public bool Success { get; }
        public string Message { get; }

        public AggregateResult(long version, bool success, string message = null)
        {
            Version = version;
            Success = success;
            Message = message;
        }

        public static AggregateResult Ok(long version, string message = null)
        {
            return new(version, true, message);
        }

        public static AggregateResult Fail(long version, string message = null)
        {
            return new(version, false, message);
        }
    }
}
