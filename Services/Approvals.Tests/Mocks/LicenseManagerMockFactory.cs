using System.Threading.Tasks;
using Approvals.Interfaces;
using Moq;
using Sdk.Api.Interfaces;
using Sdk.Tests.Interfaces;

namespace Approvals.Tests.Mocks
{
    public class LicenseManagerMockFactory : IMockFactory<ILicenseManager>
    {
        public Mock<ILicenseManager> GetInstance()
        {
            var licenseManager = new Mock<ILicenseManager>();
            licenseManager
                .Setup(
                    p => p.EvaluateByUserLicenseScope(
                        It.IsAny<IAccountModel>()
                    )
                )
                .Returns(Task.FromResult(true));
            return licenseManager;
        }
    }
}