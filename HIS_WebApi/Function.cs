using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Threading;
using HIS_DB_Lib;
using Basic;
namespace HIS_WebApi
{

    public static class Method
    {
        static public (string Server, string DB, string UserName, string Password, uint Port) GetServerInfo(string Name, string Type, string Content)
        {
            List<ServerSettingClass> serverSetting = ServerSettingController.GetAllServerSetting();
            ServerSettingClass serverSettingClass = serverSetting.MyFind(Name, Type, Content).FirstOrDefault();
            if (serverSettingClass == null)
            {
                throw new Exception("找無Server資料");
            }
            return (serverSettingClass.Server, serverSettingClass.DBName, serverSettingClass.User, serverSettingClass.Password, (uint)serverSettingClass.Port.StringToInt32());
        }

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
