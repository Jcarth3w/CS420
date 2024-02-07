using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using WebRazor.Middleware;


namespace Middleware.Tests;

public class MyAuthTests : IAsyncLifetime
{
    IHost? host;
    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
    public async Task InitializeAsync()
    {
        using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder => 
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                    }).Configure(app => 
                    {
                        app.UseMiddleware<MyAuth>();
                        app.Run(async context =>
                        {
                            await context.Response.WriteAsync("User details: Authorized");
                        });
                    });
            })
            .StartAsync();
        
    }

    [Fact]
    public async Task MiddlewareTest_FailWhenNotAuthenticated()
    {
        if(host != null)
        {
             var response = await host.GetTestClient().GetAsync("/");
            Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal("Not authorized", result);
        }
    }

    [Fact]
    public async Task MiddlewareTest_UserAuthenticated()
    {
        if(host != null)
        {
            var response = await host.GetTestClient().GetAsync("/?username=user1&password=password1");
            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal("User details: Authorized", result);
        }
        
    }

    [Fact]
    public async Task MiddlewareTest_NotAuthorizedOnlyUserName()
    {
        if(host != null)
        {
            var response = await host.GetTestClient().GetAsync("/?username=user1");
            Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal("Not authorized", result);
        }
        
    }

    [Fact]
    public async Task MiddlewareTest_NotAuthorizedWrongUserAndPass()
    {
        if(host != null)
        {
            var response = await host.GetTestClient().GetAsync("/?username=user5&password=password2");
            Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal("Not authorized", result);
        }
        
    }







}