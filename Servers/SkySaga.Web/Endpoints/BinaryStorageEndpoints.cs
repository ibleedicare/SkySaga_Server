using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text.Json;

namespace SkySaga.Web.Endpoints;

public static class BinaryStorageEndpoints
{
    public static void MapBinaryStorageEndpoint(this WebApplication app)
    {
        // This route handle the vote on the loading screen

        app.MapPost("/api/binary-storage/photos/_whichIsCooler", async (HttpRequest request) =>
        {
            using var reader = new StreamReader(request.Body);
            var body = await reader.ReadToEndAsync();
            var data = JsonSerializer.Deserialize<JsonElement>(body);
            int size = data.GetProperty("size").GetInt32();
            // TODO Add the logic to show picture for voting
            return Results.Ok(new { });
        });

        app.MapPost("/api/binary-storage/photos/{id}/rating/up", (string id) =>
        {
            return Results.Ok();
        });

        app.MapPost("/api/binary-storage/photos/{id}/rating/down", (string id) =>
        {
            return Results.Ok();
        });

        app.MapPost("/api/binary-storage/photos/_report", () =>
        {
            return Results.Ok();
        });

        app.MapGet("/api/binary-storage/photos/{id}", (string id) =>
        {
            return Results.Ok();
        });

        app.MapPost("/api/binary-storage/photos/{id}/_upload", (string id) =>
        {
            return Results.Ok();
        });

        app.MapGet("/api/binary-storage/photos/{id}/access", (string id) =>
        {
            return Results.Ok();
        });

        app.MapGet("/api/binary-storage/photos/_search", () =>
        {
            return Results.Ok();
        });
    }
}