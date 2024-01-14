using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projektowaniaOprogramowania.ViewModels
{
    public class MaturaAndDorobekNaukowyAndDodatkoweOsiagnieciePackage
	{
		public List<DodatkoweOsiagniecieModel> DodatkoweOsiagniecia { get; set; }
		public List<PrzelicznikOsiagniecModel> PrzelicznikiOsiagniec { get; set; }
		public List<OcenaModel> Oceny { get; set; }
		public List<DorobekNaukowyModel> DorobkiNaukowe { get; set; }
		public List<KategoriaDorobkuModel> KategorieDorobku { get; set; }
		public MaturaModel Matura { get; set; }
		public PodanieNaStudiaIIStopniaModel PodanieNaIIStopien { get; set; }
		public PodanieNaStudiaIStopniaModel PodanieNaIStopien { get; set; }

    }
}
