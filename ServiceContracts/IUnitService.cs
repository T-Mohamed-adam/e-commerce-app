using TagerProject.Models.Dtos.Unit;

namespace TagerProject.ServiceContracts
{
    public interface IUnitService
    {
        public Task<List<UnitResponse>> GetAllUnites();
        public Task<UnitResponse?> GetUnitById(int id);
        public Task<UnitResponse?> AddUnit(UnitAddRequest unitAddRequest);
        public Task<UnitResponse?> UpdateUnit(int id, UnitUpdateRequest unitUpdateRequest);
        public Task<UnitResponse?> DeleteUnit(int id);
    }
}
