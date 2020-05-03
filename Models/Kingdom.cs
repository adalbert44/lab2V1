using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using lab2V1.Models;

namespace lab2V1.Models
{
	public class Kingdom
	{
		public Kingdom()
		{
			Phylums = new List<Phylum>();
		}

		public int Id { get; set; }

		[Required(ErrorMessage = "Поле не повинно бути порожнім")]
		[Display(Name = "Царство")]
		public string Name { get; set; }

		[Display(Name = "Інформація про царство")]
		public string Info { get; set; }
		public virtual ICollection<Phylum> Phylums { get; set; }
	}
}
