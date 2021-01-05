using Sdk.Api.Interfaces;

namespace Sdk.Api.Dto
{
    public class ProfileDto : IProfileModel
    {
        public string Id { get; set; }
        public int Version { get; set; }
    }
}