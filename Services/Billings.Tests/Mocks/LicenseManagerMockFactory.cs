using System.Threading.Tasks;
using Billings.Interfaces;
using MassTransit;
using Moq;
using Sdk.Api.Interfaces;
using Sdk.Tests.Interfaces;

namespace Billings.Tests.Mocks
{
    public class LicenseManagerMockFactory: IMockFactory<ILicenseManager>
    {
        public Mock<ILicenseManager> GetInstance()
        {
            var licenseManager = new Mock<ILicenseManager>();
            licenseManager
                .Setup(
                    p => p.EvaluateByUserLicenseScope(
                        It.IsAny<ITransactionModel>()
                    )
                )
                .Returns(Task.FromResult(true));
            return licenseManager;
        }
    }
}