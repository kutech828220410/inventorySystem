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
            List<sys_serverSettingClass> serverSetting = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClass sys_serverSettingClass = serverSetting.MyFind(Name, Type, Content).FirstOrDefault();
            if (sys_serverSettingClass == null)
            {
                throw new Exception("找無Server資料");
            }
            return (sys_serverSettingClass.Server, sys_serverSettingClass.DBName, sys_serverSettingClass.User, sys_serverSettingClass.Password, (uint)sys_serverSettingClass.Port.StringToInt32());
        }
        static public string GetServerAPI(string Name, string Type, string Content)
        {
            List<sys_serverSettingClass> serverSetting = ServerSettingController.GetAllServerSetting();
            sys_serverSettingClass sys_serverSettingClass = serverSetting.MyFind(Name, Type, Content).FirstOrDefault();
            if (sys_serverSettingClass == null)
            {
                return null;
                throw new Exception("找無Server資料");
            }
            return sys_serverSettingClass.Server;
        }
        /// <summary>
        /// 取得目前請求的相對路徑（可選是否包含查詢字串）。若非 HTTP 呼叫則回傳 "[InternalCall]"。
        /// </summary>
        public static string GetRequestPath(HttpContext? context, bool includeQuery = false)
        {
            if (context?.Request == null)
                return "[InternalCall]";

            if (includeQuery)
                return context.Request.Path + context.Request.QueryString;

            return context.Request.Path;
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
