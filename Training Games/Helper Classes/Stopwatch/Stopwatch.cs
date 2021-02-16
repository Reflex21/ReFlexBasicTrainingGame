using System;
using System.Diagnostics;

namespace Utilities
{
	public class StopwatchWrapper
	{
		public double total_time;
		public double calls;
		private Stopwatch sw;

		public StopwatchWrapper()
		{
			sw = new Stopwatch();
		}

		public void reset()
		{
			total_time = 0;
			calls = 0;
			sw.Reset();
		}

		public void start()
		{
			sw.Start();
			calls++;
		}

		public void stop()
		{
			sw.Stop();
			total_time += (double)sw.Elapsed.TotalMilliseconds;
			sw.Reset();
		}

		public double latency()
		{
			return total_time;
		}

		public double avg_latency()
		{
			return (total_time / calls);
		}
	}
}

