using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using lab2V1.Models;

namespace lab2V1.Models
{
	public class Species
	{
		public Species(){}

		public int Id { get; set; }

		[Required(ErrorMessage = "Поле не повинно бути порожнім")]
		[Display(Name = "Вид")]
		public string Name { get; set; }

		[Display(Name = "Інформація про вид")]
		public string Info { get; set; }

		[Display(Name = "Чисельність")]
		public int Count { get; set; }

		[Display(Name = "Ареал")]
		public string Area { get; set; }

		public int GenusID { get; set; }

		public virtual ICollection<Species> Specieses { get; set; }
	}
}
