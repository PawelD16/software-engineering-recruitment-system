using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projektowaniaOprogramowania.ViewModels
{
	public class WszystkieInformacjePodaniaViewModel
	{
		public List<DodatkoweOsiagniecieModel> DodatkoweOsiagniecia { get; set; }
		public List<PrzelicznikOsiagniecModel> PrzelicznikiOsiagniec { get; set; }
		public List<OcenaZPrzedmiotem> Oceny { get; set; }
		public List<DorobekNaukowyModel> DorobkiNaukowe { get; set; }
		public List<KategoriaDorobkuModel> KategorieDorobku { get; set; }
		public MaturaModel Matura { get; set; }
		public PodanieNaStudiaIIStopniaModel PodanieNaIIStopien { get; set; }
		public PodanieNaStudiaIStopniaModel PodanieNaIStopien { get; set; }
		public PodanieKandydataModel PodanieKandydata { get; set;  }

	}
}
