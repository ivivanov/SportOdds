using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SO.Data.Entities
{
    [Table("Matches")]
    public class Match : IEntity
    {
        public Match()
        {
            Bets = new HashSet<Bet>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int EventId { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public string MatchType { get; set; }

        public ICollection<Bet> Bets { get; set; }
    }
}