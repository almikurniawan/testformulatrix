using System.Timers;
using TestC.Interfaces;

namespace TestC
{
    public class FrameCalculateAndStream
    {
        private IValueReporter _reporter;
        private Queue<Frame> _receivedFrames = new Queue<Frame>();
        private System.Timers.Timer _timer;
        public FrameCalculateAndStream(FrameGrabber fg, IValueReporter vr)
        {
            fg.OnFrameUpdated += HandleFrameUpdated;
            _timer = new System.Timers.Timer(1000 / 30);
            _timer.Elapsed += OnTimerElapsed;
            _reporter = vr;
        }
        private void HandleFrameUpdated(Frame frame)
        {
            _receivedFrames.Enqueue(frame);
        }
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_receivedFrames.Count > 0)
            {
                Frame frame = _receivedFrames.Dequeue();
                byte[] raw = frame.GetRawData();
                // https://stackoverflow.com/questions/29312223/finding-the-arithmetic-mean-of-an-array-c-sharp
                int sum = 0;
                for (int i = 0; i < raw.Length; i++)
                    sum += raw[i];
                int result = sum / raw.Length; // result now has the average of those numbers.
                _reporter.Report(result);
            }
        }
        public void StartStreaming()
        {
            _timer.Enabled = true;
        }
    }
}
