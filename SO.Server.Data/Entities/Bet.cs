using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SO.Server.Data.Entities {
	public class Bet
	{
		public Bet() {
			Odds = new HashSet<Odd>();
		}

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }

		public string Name { get; set; }

		public bool IsLive { get; set; }

		public ICollection<Odd> Odds { get; set; }
	}
}
