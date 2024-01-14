
using System.Threading.Tasks;
using projektowaniaOprogramowania.ViewModels;

namespace projektowaniaOprogramowania.Services.Recrutation
{
    public interface IRecruitmentValidationService
    {
        Task<(bool, string)> IsRecruitmentValid(RekrutacjaModel recrutationViewModel);
    }
}