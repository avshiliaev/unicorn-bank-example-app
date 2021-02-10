using System.Threading.Tasks;
using Moq;
using Sdk.Interfaces;
using Sdk.Tests.Interfaces;

namespace Sdk.Tests.Mocks
{
    public class LicenseServiceMockFactory<TModel> : IMockFactory<ILicenseService<TModel>>
        where TModel : class, IEntityState
    {
        public Mock<ILicenseService<TModel>> GetInstance()
        {
            var eMock = new Mock<ILicenseService<TModel>>();
            eMock
                .Setup(
                    p => p.EvaluatePendingAsync(
                        It.IsAny<TModel>()
                    )
                )
                .Returns(Task.FromResult(true));
            eMock
                .Setup(
                    p => p.EvaluateNotPendingAsync(
                        It.IsAny<TModel>()
                    )
                )
                .Returns(Task.FromResult(true));
            return eMock;
        }
    }
}