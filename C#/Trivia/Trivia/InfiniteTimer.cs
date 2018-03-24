namespace Trivia
{
    public class InfiniteTimer : ITimer
    {
        bool ITimer.Timeout => false;
    }
}