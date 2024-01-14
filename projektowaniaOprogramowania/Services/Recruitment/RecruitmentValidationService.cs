using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projektowaniaOprogramowania.Models;
using projektowaniaOprogramowania.ViewModels;

namespace projektowaniaOprogramowania.Services.Recrutation
{
    public class RecruitmentValidationService: IRecruitmentValidationService
    {
        private MyDbContext _dbContext;
        public RecruitmentValidationService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<(bool, string)> IsRecruitmentValid(RekrutacjaModel recruitment)
        {
            if (!AreDatesCorrect(recruitment))
            {
                return (false, "Niepoprawne daty");
            }

            if (!await IsNewStateValid(recruitment))
            {
                return (false, "Niepoprawny status");
            }

            return (true, "");
        }

        private async Task<bool> IsNewStateValid(RekrutacjaModel recruitment)
        {
            var dbObj = await _dbContext.Rekrutacje.FindAsync(recruitment.Id);
            if (dbObj==null)
            {
                return true;
            }
            return (recruitment.StatusRekrutacji >= dbObj.StatusRekrutacji);

        }

        private bool AreDatesCorrect(RekrutacjaModel recruitment)
        {
            if (recruitment.DataOtwarciaRekrutacji > recruitment.DataZamknieciaPrzyjec)
            {
                return false;
            }

            if (recruitment.DataZamknieciaPrzyjec > recruitment.DataZamknieciaRekrutacji)
            {
                return false;
            }

            return true;
        }
    }
}