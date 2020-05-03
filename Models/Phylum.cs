using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using lab2V1.Models;

namespace lab2V1.Models
{
	public class Phylum
	{
		public Phylum()
		{
			Classes = new List<Class>();
		}

		public int Id { get; set; }

		[Required(ErrorMessage = "Поле не повинно бути порожнім")]
		[Display(Name = "Тип")]
		public string Name { get; set; }

		[Display(Name = "Інформація про тип")]
		public string Info { get; set; }
		public virtual ICollection<Class> Classes { get; set; }
		public int KingdomID { get; set; }
	}
}
