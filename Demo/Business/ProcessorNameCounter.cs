using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Demo.Business
{
    public class ProcessorNameCounter : IProcessor
    {
        public delegate void OnEmpty(object sender);
        //public delegate void OnComplete(object sender, IEnumerable<Result> args);
        public delegate void OnProgress(object sender, float args, Result item);
        public event OnEmpty OnDataEmptyEvent;
        //public event OnComplete OnCompleteEvent;
        public event OnProgress OnProgressEvent;

        public event Action<object, IEnumerable<Result>> OnCompleteEvent;


        public ProcessorNameCounter(ICollection<string> data)
        {
            Data = data;
        }


        public ICollection<string> Data { get; private set; }

        public void Execute()
        {
            if (Data == null || !Data.Any())
            {
                OnDataEmptyEvent?.Invoke(this);
                return;
            }
            var results = new List<Result>();
            var i = 0;
            foreach (var item in Data)
            {
                Thread.Sleep(1000);
                var result = new Result { Name = item, Length = item.Length };
                OnProgressEvent?.Invoke(this, ++i, result);
                results.Add(result);
            }

            OnCompleteEvent?.Invoke(this, results);
        }
    }
}
