using Moq;

namespace Accounts.Tests.Interfaces
{
    public interface IMockFactory<T> where T : class
    {
        public Mock<T> GetInstance();
    }
}