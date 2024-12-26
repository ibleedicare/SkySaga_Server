using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OpenApi;
using System;

namespace SkySaga.Web.Endpoints;

public static class AuthenticationEndpoints
{
    public record ApplicationLogin(string Name, string Password);
    public record SmilegateAuthLogin(string Token);
    public static void MapAuthenticationEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/authentication");

        group.MapPost("/applications/names/login", HandleApplicationLogin)
             .WithName("ApplicationLogin")
             .WithOpenApi();

        group.MapPost("/sgauth/_login", HandleSmilegateLogin)
             .WithName("SmilegateLogin")
             .WithOpenApi();

        group.MapPost("/credentials/usernames/autologin", HandleAutoLogin)
             .WithName("AutoLogin")
             .WithOpenApi();

        group.MapPost("/credentials/usernames/logout", () => Results.Ok())
             .WithName("Logout")
             .WithOpenApi();

        group.MapPost("/tokens/refresh", () => Results.Ok())
             .WithName("RefreshToken")
             .WithOpenApi();
    }

    private static IResult HandleApplicationLogin(ApplicationLogin login)
    {
        Console.WriteLine($"Application login attempt: {login.Name}");
        return Results.Ok(new
        {
            result = new
            {
                tokenId = "MyTokenId",
                refreshingTokenId = "refreshingTokenId",
                timeout = 999999
            }
        });
    }

    private static IResult HandleSmilegateLogin(SmilegateAuthLogin login)
    {
        return Results.Ok(new
        {
            result = new
            {
                sgUser = "",
                memberId = "1",
                username = "EDITz",
                token = new
                {
                    tokenId = "tokenId",
                    refreshingTokenId = "refreshingTokenId",
                    timeout = 999999
                }
            }
        });
    }

    private static IResult HandleAutoLogin()
    {
        return Results.Ok(new
        {
            result = new
            {
                tokenId = "tokenId",
                refreshingTokenId = "refreshingTokenId",
                timeout = 999999
            }
        });
    }
}