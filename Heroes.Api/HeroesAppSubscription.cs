using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Resolvers;
using GraphQL.Types;
using Heroes.Contracts.Grains.Stats;
using System.Reactive.Linq;
using GraphQL.Subscription;
using Heroes.Api.GraphQLCore.Types;

namespace Heroes.Api
{
    public class HeroesAppSubscription : ObjectGraphType<object>
    {
        private readonly Random _rng;
        private int _games;
        private string[] _heroes;

        public HeroesAppSubscription()
        {
            _rng = new Random();
            _heroes = new[] { "rengar", "kha-zix", "singed" };
            _games = 0;

            AddField(new EventStreamFieldType
            {
                Name = "heroStatUpdates",
                Type = typeof(HeroStatsType),
                Resolver = new FuncFieldResolver<HeroStats>(ResolveHeroStats),
                Subscriber = new EventStreamResolver<HeroStats>(StatGenerator)
            });
        }

        private HeroStats ResolveHeroStats(ResolveFieldContext context)
        {
            var heroStats = context.Source as HeroStats;
            return heroStats;
        }

	    private IObservable<HeroStats> StatGenerator(ResolveEventStreamContext context)
	    {
	        return Observable
                .Interval(TimeSpan.FromSeconds(2))
                .Select(_ => new HeroStats
	        {
	            BanRate = 100,
                HeroId = _heroes[_rng.Next(_heroes.Length)],
                TotalGames = _games++,
                WinRate = (decimal)_rng.NextDouble()
	        });
	    }
    }
}
