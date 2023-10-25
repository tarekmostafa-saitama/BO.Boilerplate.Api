using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace Infrastructure.IntegrationTests;

public class BaseWebHostFixture<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    public HttpClient Client { get; }

    public BaseWebHostFixture()
    {
        var applicationFactory = new WebApplicationFactory<TStartup>();
        Client = applicationFactory.CreateClient();
        Client.BaseAddress = new Uri("http://localhost:7227");
    }
}