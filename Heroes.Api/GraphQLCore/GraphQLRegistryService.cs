using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Http;
using GraphQL.Server.Transports.WebSockets;
using GraphQL.Transports.AspNetCore;
using GraphQL.Types;
using Heroes.Api.GraphQLCore.Core;
using Heroes.Api.GraphQLCore.Queries;
using Heroes.Api.GraphQLCore.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Heroes.Api.GraphQLCore
{
	public static class GraphQLRegistryService
	{
		public static void AddHeroesAppGraphQL(this IServiceCollection services)
		{
			//services.AddSingleton<IDocumentExecuter>(new DocumentExecuter());
			//services.AddSingleton<IDocumentWriter>(new DocumentWriter(true));

			services.AddSingleton<HeroesAppGraphQuery>();
            services.AddSingleton<HeroesAppSubscription>();

			services.AddSingleton<HeroRoleEnum>();
			services.AddSingleton<HeroType>();
			services.AddSingleton<HeroStatsType>();

		    services.AddSingleton<ISchema, HeroesAppSchema>(_ => (HeroesAppSchema)services.BuildServiceProvider().GetService(typeof(HeroesAppSchema)));
			
			services.AddSingleton<HeroesAppSchema, HeroesAppSchema>();

		    services.AddGraphQLHttpTransport<HeroesAppSchema>();
		    services.AddGraphQLWebSocketsTransport<HeroesAppSchema>();
		    services.AddGraphQL();
		}

		public static void SetGraphQLMiddleWare(this IApplicationBuilder app)
		{
		    app.UseWebSockets();

		    //app.Use(async (context, next) =>
		    //{
		    //    if (context.Request.Path == "/ws")
		    //    {
		    //        if (context.WebSockets.IsWebSocketRequest)
		    //        {
		    //            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
		    //            await Echo(context, webSocket);
		    //        }
		    //        else
		    //        {
		    //            context.Response.StatusCode = 400;
		    //        }
		    //    }
		    //    else
		    //    {
		    //        await next();
		    //    }

		    //});

            //app.UseMiddleware<GraphQLMiddleware>(new GraphQLSettings
            //{
            //    BuildUserContext = ctx => new GraphQLUserContext
            //    {
            //        User = ctx.User
            //    }
            //});

            app.UseGraphQLEndPoint<HeroesAppSchema>("/graphql");
        }

	    private static async Task Echo(HttpContext context, WebSocket webSocket)
	    {
	        var buffer = new byte[1024 * 4];
	        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
	        while (!result.CloseStatus.HasValue)
	        {
	            await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

	            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
	        }
	        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
	    }
    }
}
