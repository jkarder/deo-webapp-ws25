using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace Deo.WebApp.Tests;

public class CounterPageTests : PageTest
{
    const string DEO_FRONTEND_URL_ENV_VAR_NAME = "DEO_FRONTEND_URL";

    private string? frontendUrl;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        PlaywrightHelpers.InstallDeps();
        PlaywrightHelpers.Install();

        frontendUrl = Environment.GetEnvironmentVariable("DEO_FRONTEND_URL")
            ?? throw new InvalidOperationException($"Environment variable \"{DEO_FRONTEND_URL_ENV_VAR_NAME}\" not set.");
    }

    [SetUp]
    public void Setup()
    {
        // not needed for now
    }

    public override BrowserNewContextOptions ContextOptions()
    {
        return new()
        {
            BaseURL = frontendUrl
        };
    }

    [Test, Order(0)]
    public async Task CounterStartsAtZero()
    {
        await Page.GotoAsync("/");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Service Counter" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync("Current count: 0");
    }

    [Test, Order(1)]
    public async Task CounterIncrements()
    {
        await Page.GotoAsync("/");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Service Counter" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync("Current count: 0");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Increment" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync("Current count: 1");
    }

    [TearDown]
    public void TearDown()
    {
        // not needed for now
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        // not needed for now
    }
}
