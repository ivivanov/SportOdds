using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SO.Server.Data.Entities
{
    [Table("Odds")]
    public class Odd : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [ForeignKey("Bet")]
        public int BetId { get; set; }

        public Bet Bet { get; set; }

        public string Name { get; set; }

        public double Value { get; set; }
    }
}
