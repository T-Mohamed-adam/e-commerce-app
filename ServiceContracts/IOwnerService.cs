using TagerProject.Models.Dtos.Owner;

namespace TagerProject.ServiceContracts
{
    public interface IOwnerService
    {
        public Task<List<OwnerResponse>> GetAllOwners();

        public Task<OwnerResponse?> GetOwnerById(int id);

        public Task<OwnerResponse?> AddOwner(OwnerAddRequest ownerAddRequest);

        public Task<OwnerResponse?> UpdateOwner(int id, OwnerUpdateRequest ownerUpdateRequest);

        public Task<OwnerResponse?> DeleteOwner(int id);

    }
}
