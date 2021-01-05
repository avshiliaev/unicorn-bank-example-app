namespace Sdk.Interfaces
{
    public interface IApprovable
    {
        public bool Approved { get; set; }
        public bool Pending { get; set; }
        public bool Blocked { get; set; }
    }
}