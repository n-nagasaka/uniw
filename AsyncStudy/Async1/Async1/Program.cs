// See https://aka.ms/new-console-template for more information
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.WriteLine("Hello, World!");
var main = new Main();

Console.WriteLine("Call AsyncTask");
await main.AsyncTask();
Console.WriteLine("AsyncTask returned");

// Console.WriteLine("Call AsyncVoid");
// error CS4008: void' を待機することができません
// await main.AsyncVoid();
// Console.WriteLine("AsyncVoid returned");

class Main
{
    public async Task AsyncTask()
    {
        Console.WriteLine("AsyncTask started");
        await Task.Delay(1000);  // Wait one second
        Console.WriteLine("AsyncTask completed");
    }

    public async void AsyncVoid() 
    {
        Console.WriteLine("AsyncVoid started");
        await Task.Delay(1000);
        Console.WriteLine("AsyncVoic completed");
    }
}