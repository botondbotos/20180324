using Trivia;

namespace CharTests
{
    internal class TestTimer : ITimer
    {
        private bool mExpired;
        bool ITimer.Timeout => !mExpired;

        public void Expire()
        {
            mExpired = true;
        }
    }
}