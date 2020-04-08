using System;
using System.Diagnostics;

namespace WebApiDocumentUploader.Model.DTO.Developer
{
    public class BaseDeveloperResponse
    {
        private readonly Stopwatch _stopwatch;
        public double ExecutedTimeInMs { get; set; }
        
        public double ExecutedTimeInSec => ExecutedTimeInMs / 1000;
        public double ExecutedTimeInMin => ExecutedTimeInSec / 60;

        public string Message { get; set; }

        public BaseDeveloperResponse()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public BaseDeveloperResponse Stop(string message = null)
        {
            _stopwatch.Stop();
            ExecutedTimeInMs = _stopwatch.ElapsedMilliseconds;
            
            if(string.IsNullOrEmpty(Message))
                Message = message;
            else
            {
                Message += $"{Environment.NewLine}{message}";
            }
            
            return this;
        }
    }
}