using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using lab2V1.Models;

namespace lab2V1.Models
{
	public class Family
	{ 
		public Family()
		{
			Genuses = new List<Genus>();
		}

		public int Id { get; set; }

		[Required(ErrorMessage = "Поле не повинно бути порожнім")]
		[Display(Name = "Родина")]
		public string Name { get; set; }

		[Display(Name = "Інформація про родину")]
		public string Info { get; set; }
		public virtual ICollection<Genus> Genuses { get; set; }

		public int OrderID { get; set; }
	}
}
