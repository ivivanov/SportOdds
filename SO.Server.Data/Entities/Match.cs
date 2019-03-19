using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SO.Server.Data.Entities
{
    public class Match
    {
		public Match() {
			Bets = new HashSet<Bet>();
		}

        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public string MatchType { get; set; }

		public ICollection<Bet> Bets { get; set; }
	}
}