using System.Threading.Tasks;
using Moq;
using Sdk.Interfaces;
using Sdk.License.Interfaces;
using Sdk.Tests.Interfaces;

namespace Sdk.Tests.Mocks
{
    public class LicenseManagerMockFactory<TModel> : IMockFactory<ILicenseManager<TModel>>
        where TModel : class, IDataModel
    {
        public Mock<ILicenseManager<TModel>> GetInstance()
        {
            var licenseManager = new Mock<ILicenseManager<TModel>>();
            licenseManager
                .Setup(
                    p => p.EvaluatePendingAsync(
                        It.IsAny<TModel>()
                    )
                )
                .Returns(Task.FromResult(true));
            licenseManager
                .Setup(
                    p => p.EvaluateNotPendingAsync(
                        It.IsAny<TModel>()
                    )
                )
                .Returns(Task.FromResult(true));
            return licenseManager;
        }
    }
}