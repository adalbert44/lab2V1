using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using lab2V1.Models;

namespace lab2V1.Models
{
	public class Class
	{
		public Class()
		{
			Orders = new List<Order>();
		}

		public int Id { get; set; }

		[Required(ErrorMessage = "Поле не повинно бути порожнім")]
		[Display(Name = "Клас")]
		public string Name { get; set; }

		[Display(Name = "Інформація про клас")]
		public string Info { get; set; }
		public virtual ICollection<Order> Orders { get; set; }

		public int PhylumID { get; set; }
	}
}
