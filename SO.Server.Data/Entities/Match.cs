using System;
using System.ComponentModel.DataAnnotations;

namespace SO.Server.Data.Entities
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        [Required]
        public int MatchTypeId { get; set; }

        public virtual MatchType MatchType { get; set; }

    }
}