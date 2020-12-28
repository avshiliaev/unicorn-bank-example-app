namespace Sdk.Interfaces
{
    public interface IConcurrentHost
    {
        public int LastSequentialNumber { get; set; }
    }
}