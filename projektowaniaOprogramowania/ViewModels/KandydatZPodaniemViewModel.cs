using projektowaniaOprogramowania.ViewModels.CollegeStructures;
using projektowaniaOprogramowania.ViewModels.Users;
using System.Collections.Generic;

namespace projektowaniaOprogramowania.ViewModels
{
    public class KandydatZPodaniemViewModel
    {
        public PodanieKandydataModel podanieKandydataViewModel { get; set; }
        public KandydatModel kandydatViewModel { get; set; }

        public IEnumerable<KierunekModel> kierunkiNaPodaniu { get; set; }
    }
}
