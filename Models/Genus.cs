using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using lab2V1.Models;

namespace lab2V1.Models
{
	public class Genus
	{
		public Genus()
		{
			Specieses = new List<Species>();
		}

		public int Id { get; set; }

		[Required(ErrorMessage = "Поле не повинно бути порожнім")]
		[Display(Name = "Рід")]
		public string Name { get; set; }

		[Display(Name = "Інформація про рід")]
		public string Info { get; set; }
		public virtual ICollection<Species> Specieses { get; set; }

		public int FamilyID { get; set; }
	}
}
