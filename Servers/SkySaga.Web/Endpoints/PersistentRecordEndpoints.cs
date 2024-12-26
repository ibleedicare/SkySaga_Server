using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace SkySaga.Web.Endpoints;

public static class PersistentRecordEndpoints
{
    private static Guid _characterUUID = Guid.Empty;

    public static void MapPersistentRecordEndpoints(this WebApplication app)
    {
        app.MapGet("/GetGUID", GetCharacterGuid)
           .WithName("GetCharacterGuid")
           .WithOpenApi();

        var group = app.MapGroup("/api/persistent-record");

        group.MapGet("/characters/list", GetCharactersList)
             .WithName("GetCharactersList")
             .WithOpenApi();

        group.MapPost("/characters/_create", CreateCharacter)
             .WithName("CreateCharacter")
             .WithOpenApi();
    }

    private static IResult GetCharacterGuid()
    {
        return Results.Ok(new { result = new { GUID = _characterUUID } });
    }

    private static IResult GetCharactersList()
    {
        if (_characterUUID == Guid.Empty)
        {
            return Results.Ok(new
            {
                Error = new { code = 11001, message = "", detail = "" }
            });
        }

        return Results.Ok(new
        {
            result = new
            {
                characters = new[]
                {
                    new
                    {
                        uuid = _characterUUID,
                        name = "EDITz",
                        homeBiome = "Desert",
                        positionInList = 0
                    }
                }
            }
        });
    }

    private static IResult CreateCharacter()
    {
        _characterUUID = Guid.NewGuid();
        return Results.Ok(new { result = new { characterUUID = _characterUUID } });
    }
}
