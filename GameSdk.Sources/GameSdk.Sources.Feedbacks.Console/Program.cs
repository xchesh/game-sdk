using GameSdk.Sources.Feedbacks;

public class Program
{
    public static void Main()
    {
        var strategies = FeedbackStrategyCache.Strategies;

        Console.WriteLine($"Strategies count {strategies.Count}");
    }
}
