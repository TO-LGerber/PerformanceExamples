using System.Threading;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/noCancellationToken", async () =>
{
    try
    {
        await Task.Run(async () =>
        {
            await Task.Delay(1_00);
            Console.WriteLine("First Task");
        });
        return "OK";
    }
    catch (Exception e)
    {
        return e.Message;
    }
});

app.MapGet("/cancellationToken", async (CancellationToken cancellationToken) =>
{
    try
    {
        await Task.Run(async () =>
        {
            await Task.Delay(5_000, cancellationToken);
            Console.WriteLine("First Task");
        });

        await Task.Run(async () =>
        {
            await Task.Delay(1_000, cancellationToken);
            Console.WriteLine("Second Task");
        }, cancellationToken);
        return "OK";
    }
    catch (Exception e)
    {
        return e.Message;
    }
});

app.Run();
