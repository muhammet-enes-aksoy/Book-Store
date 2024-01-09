namespace BookStore.Services;

public interface ILoggerService
{
    public void Write(string message)
    {
        Console.WriteLine("[DBLogger] - " + message);
    }
}