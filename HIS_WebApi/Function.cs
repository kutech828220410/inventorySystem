using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Threading;
namespace HIS_WebApi
{

    static class Method
    {


    }

    public class QueueManager
    {
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> UrlLocks = new ConcurrentDictionary<string, SemaphoreSlim>();

        public static void ProcessRequest(string url, Action action)
        {
            SemaphoreSlim urlLock = UrlLocks.GetOrAdd(url, _ => new SemaphoreSlim(1, 1));

            urlLock.Wait(); // 请求锁定

            try
            {
                action.Invoke();
            }
            finally
            {
                urlLock.Release(); // 释放锁定
            }
        }
    }
}
