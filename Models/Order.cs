using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using lab2V1.Models;

namespace lab2V1.Models
{
	public class Order
	{
		public Order()
		{
			Families = new List<Family>();
		}

		public int Id { get; set; }

		[Required(ErrorMessage = "Поле не повинно бути порожнім")]
		[Display(Name = "Ряд")]
		public string Name { get; set; }

		[Display(Name = "Інформація про ряд")]
		public string Info { get; set; }
		public virtual ICollection<Family> Families { get; set; }

		public int ClassID { get; set; }
	}
}
