using System;
using System.Threading.Tasks;
using Moq;
using Sdk.Api.Dto;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
using Sdk.Tests.Interfaces;
using Transactions.Interfaces;

namespace Transactions.Tests.Mocks
{
    public class LicenseManagerMockFactory : IMockFactory<ILicenseManager>
    {
        public Mock<ILicenseManager> GetInstance()
        {
            var licenseManager = new Mock<ILicenseManager>();
            licenseManager
                .Setup(
                    p => p.CheckAccountStateAsync(
                        It.IsAny<Guid>()
                    )
                )
                .Returns(Task.FromResult(false));
            licenseManager
                .Setup(p => p.UpdateAccountStateAsync(It.IsAny<IAccountModel>()))
                .Returns(((IAccountModel accountModel) => Task.FromResult(accountModel)));
            return licenseManager;
        }
    }
}