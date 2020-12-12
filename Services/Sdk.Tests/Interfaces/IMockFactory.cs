using Moq;

namespace Sdk.Tests.Interfaces
{
    public interface IMockFactory<T> where T : class
    {
        public Mock<T> GetInstance();
    }
}