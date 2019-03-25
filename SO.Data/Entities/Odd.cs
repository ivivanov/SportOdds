using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SO.Data.Entities
{
    [Table("Odds")]
    public class Odd : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int BetId { get; set; }

        [ForeignKey("BetId")]
        public Bet Bet { get; set; }

        public string Name { get; set; }

        public double Value { get; set; }
    }
}
