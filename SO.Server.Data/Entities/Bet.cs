using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SO.Server.Data.Entities
{
    [Table("Bets")]
    public class Bet : IEntity
    {
        public Bet()
        {
            Odds = new HashSet<Odd>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [ForeignKey("Match")]
        public int MatchId { get; set; }

        public Match Match { get; set; }

        public string Name { get; set; }

        public bool IsLive { get; set; }

        public virtual ICollection<Odd> Odds { get; set; }
    }
}
