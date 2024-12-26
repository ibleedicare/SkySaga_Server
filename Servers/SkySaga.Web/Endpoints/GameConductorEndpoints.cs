using System;
using Microsoft.AspNetCore.Builder;

namespace SkySaga.Web.Endpoints;

public static class GameConductorEndpoints
{
    public record GameConductorReserve(int Character, Guid ImUuid);
    public record GeoNodeResponse(Guid Uuid, string Datacentre, string Ip, int Port);
    public record GameWorldResponse(int RetryInMillis, Guid World, string Ip, int Port, Guid Server);

    public static void MapGameConductorEndpoints(this WebApplication app)
    {
        app.MapGet("/api/game-conductor/geonode", GetGeoNode);
        app.MapPut("/api/game-conductor/reserve", ReserveGame);
        app.MapPost("/api/game-conductor/retrieve", RetrieveGame);
    }

    private static object GetGeoNode()
    {
        var geoNode = new GeoNodeResponse(
            Uuid: Guid.NewGuid(),
            Datacentre: "UK",
            Ip: "127.0.0.1",
            Port: 5164
        );

        return new { result = new[] { geoNode } };
    }

    private static object ReserveGame(GameConductorReserve reserve)
    {
        return new { result = new { } };
    }

    private static object RetrieveGame()
    {
        var gameWorld = new GameWorldResponse(
            RetryInMillis: 5000,
            World: Guid.NewGuid(),
            Ip: "127.0.0.1",
            Port: 42069,
            Server: Guid.NewGuid()
        );

        return new { result = gameWorld };
    }
}