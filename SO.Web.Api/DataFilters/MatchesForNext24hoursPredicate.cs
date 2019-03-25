using SO.Data.Entities;
using SO.Server.FeedConsumer.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SO.Web.Api.DataFilters
{
    public class MatchesForNext24hoursPredicate
    {
        public static Expression<Func<Match, bool>> Predicate
        {
            get {
                var now = DateTime.Now;
                var after24h = now.AddHours(24);
                return x => x.StartDate >= now && x.StartDate <= after24h && x.Bets.Count > 0;
            }
        }

        public static Expression<Func<MatchModel, bool>> ModelPredicate
        {
            get {
                var now = DateTime.Now;
                var after24h = now.AddHours(24);
                return x => x.StartDate >= now && x.StartDate <= after24h && x.Bets.Count() > 0;
            }
        }
    }
}
