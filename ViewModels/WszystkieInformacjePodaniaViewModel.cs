using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projektowaniaOprogramowania.ViewModels
{
	public class WszystkieInformacjePodaniaViewModel
	{
		public List<DodatkoweOsiagniecieViewModel> DodatkoweOsiagniecia { get; set; }
		public List<PrzelicznikOsiagniecViewModel> PrzelicznikiOsiagniec { get; set; }
		public List<OcenaViewModel>? Oceny { get; set; }
		public List<DorobekNaukowyViewModel> DorobkiNaukowe { get; set; }
		public List<KategoriaDorobkuViewModel> KategorieDorobku { get; set; }
		public MaturaViewModel? Matura { get; set; }
		public PodanieNaIIStopienViewModel? PodanieNaIIStopien { get; set; }
		public PodanieNaIStopienViewModel? PodanieNaIStopien { get; set; }
		public PodanieKandydataViewModel PodanieKandydata { get; set;  }

	}
}
