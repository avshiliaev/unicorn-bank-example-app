using System.Threading.Tasks;
using Moq;
using Sdk.Interfaces;
using Sdk.License.Interfaces;
using Sdk.Tests.Interfaces;

namespace Sdk.Tests.Mocks
{
    public class LicenseManagerMockFactory<TModel> : IMockFactory<ILicenseManager<TModel>> 
        where TModel: class, IDataModel
    {
        public Mock<ILicenseManager<TModel>> GetInstance()
        {
            var licenseManager = new Mock<ILicenseManager<TModel>>();
            licenseManager
                .Setup(
                    p => p.EvaluateNewEntityAsync(
                        It.IsAny<TModel>()
                    )
                )
                .Returns(Task.FromResult(true));
            licenseManager
                .Setup(
                    p => p.EvaluateStateEntityAsync(
                        It.IsAny<TModel>()
                    )
                )
                .Returns(Task.FromResult(true));
            return licenseManager;
        }
    }
}