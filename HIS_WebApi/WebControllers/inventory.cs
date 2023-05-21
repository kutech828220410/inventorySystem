using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SQLUI;
using Basic;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Configuration;
using MyOffice;
using NPOI;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using MyUI;
using H_Pannel_lib;
using HIS_DB_Lib;
namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class inventoryController : Controller
    {
     

        static private string DataBaseName = ConfigurationManager.AppSettings["database"];
        static private string UserName = ConfigurationManager.AppSettings["user"];
        static private string Password = ConfigurationManager.AppSettings["password"];
        static private string IP = ConfigurationManager.AppSettings["IP"];
        static private uint Port = (uint)ConfigurationManager.AppSettings["port"].StringToInt32();
        static private MySqlSslMode SSLMode = MySqlSslMode.None;

        static private string MDC_DataBaseName = ConfigurationManager.AppSettings["MED_cloud_DB"];
        static private string MDC_IP = ConfigurationManager.AppSettings["MED_cloud_IP"];

        private SQLControl sQLControl_inventory_creat = new SQLControl(IP, DataBaseName, "inventory_creat", UserName, Password, Port, SSLMode);
        private SQLControl sQLControl_inventory_content = new SQLControl(IP, DataBaseName, "inventory_content", UserName, Password, Port, SSLMode);
        private SQLControl sQLControl_inventory_sub_content = new SQLControl(IP, DataBaseName, "inventory_sub_content", UserName, Password, Port, SSLMode);
        private SQLControl sQLControl_MED_cloud = new SQLControl(MDC_IP, MDC_DataBaseName, "medicine_page_cloud", UserName, Password, Port, SSLMode);


        //取得可建立今日最新盤點單
        [Route("new_IC_SN")]
        [HttpGet]
        public string GET_new_IC_SN()
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData returnData = new returnData();
            List<object[]> list_inventory_creat = this.sQLControl_inventory_creat.GetAllRows(null);
            List<object[]> list_inventory_creat_buf = new List<object[]>();

            list_inventory_creat_buf = list_inventory_creat.GetRowsInDate((int)enum_盤點單號.建表時間, DateTime.Now);

            List<object[]> list_medecine = this.sQLControl_MED_cloud.GetAllRows(null);
            List<object[]> list_medecine_buf = new List<object[]>();
            string 盤點單號 = "";
            int index = 0;
            while (true)
            {
                盤點單號 = $"{DateTime.Now.ToDateTinyString()}-{index}";
                index++;
                list_inventory_creat_buf = list_inventory_creat.GetRows((int)enum_盤點單號.盤點單號, 盤點單號);
                if (list_inventory_creat_buf.Count == 0) break;
            }
            returnData.Value = 盤點單號;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"成功! {myTimer.ToString()}";
            return returnData.JsonSerializationt(true);
        }

        //以建表日搜尋盤點單
        [Route("creat_get_by_CT_TIME")]
        [HttpPost]
        public string POST_creat_get_by_CT_TIME([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            inventoryClass.creat creat = inventoryClass.creat.ObjToClass(returnData.Data);
            if (creat.建表時間.Check_Date_String() == false)
            {
                returnData.Code = -5;
                returnData.Result = "輸入日期格式錯誤!";
                return returnData.JsonSerializationt();
            }

            List<object[]> list_inventory_creat = this.sQLControl_inventory_creat.GetAllRows(null);
            list_inventory_creat = list_inventory_creat.GetRowsInDate((int)enum_盤點單號.建表時間, creat.建表時間.StringToDateTime());
            returnData = Function_Get_inventory_creat(list_inventory_creat);
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"取得盤點資料成功!";

            return returnData.JsonSerializationt(true);
        }
        //以盤點單號搜尋盤點單
        [Route("creat_get_by_IC_SN")]
        [HttpPost]
        public string POST_creat_get_by_IC_SN([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            inventoryClass.creat creat = inventoryClass.creat.ObjToClass(returnData.Data);
            //if (returnData.Value.StringIsEmpty())
            //{
            //    returnData.Code = -5;
            //    returnData.Result = "輸入盤點單號不得空白!";
            //    return returnData.JsonSerializationt();
            //}
            List<object[]> list_inventory_creat = this.sQLControl_inventory_creat.GetAllRows(null);
            list_inventory_creat = list_inventory_creat.GetRows((int)enum_盤點單號.盤點單號, creat.盤點單號);
            if (list_inventory_creat.Count == 0)
            {
                returnData.Code = -5;
                returnData.Result = $"查無此單號資料[{returnData.Value}]!";
                return returnData.JsonSerializationt();
            }
            returnData = Function_Get_inventory_creat(list_inventory_creat);
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"取得盤點資料成功!";

            return returnData.JsonSerializationt();
        }
        //盤點單新增
        [Route("creat_add")]
        [HttpPost]
        public string POST_creat_add([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            inventoryClass.creat creat = inventoryClass.creat.ObjToClass(returnData.Data);
            List<object[]> list_inventory_creat = this.sQLControl_inventory_creat.GetAllRows(null);
            List<object[]> list_inventory_creat_buf = new List<object[]>();
            List<object[]> list_inventory_content = this.sQLControl_inventory_content.GetAllRows(null);
            List<object[]> list_inventory_content_buf = new List<object[]>();
            List<object[]> list_inventory_sub_content = this.sQLControl_inventory_sub_content.GetAllRows(null);
            List<object[]> list_inventory_sub_content_buf = new List<object[]>();


            List<object[]> list_medecine = this.sQLControl_MED_cloud.GetAllRows(null);
            List<object[]> list_medecine_buf = new List<object[]>();
            if (creat == null)
            {
                returnData.Code = -5;
                returnData.Result += $"Data 資料錯誤 \n";
                return returnData.JsonSerializationt();
            }

            list_inventory_creat_buf = list_inventory_creat.GetRows((int)enum_盤點單號.盤點單號, creat.盤點單號);
            if (list_inventory_creat_buf.Count > 0)
            {
                returnData.Code = -6;
                returnData.Result += $"盤點單號: {creat.盤點單號} 已存在,請刪除後再建立! \n";
                return returnData.JsonSerializationt();
            }
            creat.GUID = Guid.NewGuid().ToString();
            creat.建表時間 = DateTime.Now.ToDateTimeString();
            creat.盤點開始時間 = DateTime.MinValue.ToDateTimeString();
            creat.盤點結束時間 = DateTime.MinValue.ToDateTimeString();

            List<object[]> list_inventory_creat_add = new List<object[]>();
            List<object[]> list_inventory_content_add = new List<object[]>();
            object[] value;
            value = new object[new enum_盤點單號().GetLength()];

            value[(int)enum_盤點單號.GUID] = creat.GUID;
            value[(int)enum_盤點單號.盤點單號] = creat.盤點單號;
            value[(int)enum_盤點單號.建表人] = creat.建表人;
            value[(int)enum_盤點單號.建表時間] = creat.建表時間;
            value[(int)enum_盤點單號.盤點開始時間] = creat.盤點開始時間;
            value[(int)enum_盤點單號.盤點結束時間] = creat.盤點結束時間;
            value[(int)enum_盤點單號.盤點狀態] = "等待盤點";
            list_inventory_creat_add.Add(value);

            for (int i = 0; i < creat.Contents.Count; i++)
            {
                value = new object[new enum_盤點內容().GetLength()];
                creat.Contents[i].GUID = Guid.NewGuid().ToString();
                creat.Contents[i].新增時間 = DateTime.Now.ToDateTimeString();
                creat.Contents[i].Master_GUID = creat.GUID;
                creat.Contents[i].盤點單號 = creat.盤點單號;
                value[(int)enum_盤點內容.GUID] = creat.Contents[i].GUID;
                value[(int)enum_盤點內容.Master_GUID] = creat.Contents[i].Master_GUID;
                value[(int)enum_盤點內容.藥品碼] = creat.Contents[i].藥品碼;
                value[(int)enum_盤點內容.料號] = creat.Contents[i].料號;
                value[(int)enum_盤點內容.藥品條碼1] = creat.Contents[i].藥品條碼1;
                value[(int)enum_盤點內容.藥品條碼2] = creat.Contents[i].藥品條碼2;
                value[(int)enum_盤點內容.盤點單號] = creat.Contents[i].盤點單號;
                value[(int)enum_盤點內容.理論值] = creat.Contents[i].理論值;
                value[(int)enum_盤點內容.新增時間] = creat.Contents[i].新增時間;
                list_inventory_content_add.Add(value);
            }
            sQLControl_inventory_creat.AddRows(null, list_inventory_creat_add);
            sQLControl_inventory_content.AddRows(null, list_inventory_content_add);
            returnData.Data = creat;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"成功加入新盤點單! 共{list_inventory_content_add.Count}筆資料";
            return returnData.JsonSerializationt(true);
        }
        [Route("creat_add")]
        [HttpGet]
        public string GET_creat_add()
        {
            firstclass_deviceController firstclass_DeviceController = new firstclass_deviceController();
            returnData returnData_GET_new_IC_SN = this.GET_new_IC_SN().JsonDeserializet<returnData>();
            string str_IC_SN = returnData_GET_new_IC_SN.Value;
            List<DeviceBasic> deviceBasics = firstclass_DeviceController.Function_Get_firstclass_device();
            List<object[]> list_MED_cloud = this.sQLControl_MED_cloud.GetAllRows(null);
            List<object[]> list_MED_cloud_buf = new List<object[]>();
            returnData returnData = new returnData();
            inventoryClass.creat creat = new inventoryClass.creat();
            creat.盤點單號 = str_IC_SN;
            for (int i = 0; i < deviceBasics.Count; i++)
            {
                list_MED_cloud_buf = list_MED_cloud.GetRows((int)enum_medicine_page_cloud.藥品碼, deviceBasics[i].Code);
                if(list_MED_cloud_buf.Count > 0)
                {
                    inventoryClass.content content = new inventoryClass.content();
                    content.藥品碼 = list_MED_cloud_buf[0][(int)enum_medicine_page_cloud.藥品碼].ObjectToString();
                    content.藥品名稱 = list_MED_cloud_buf[0][(int)enum_medicine_page_cloud.藥品名稱].ObjectToString();
                    content.中文名稱 = list_MED_cloud_buf[0][(int)enum_medicine_page_cloud.中文名稱].ObjectToString();
                    content.料號 = list_MED_cloud_buf[0][(int)enum_medicine_page_cloud.料號].ObjectToString();
                    content.藥品條碼1 = list_MED_cloud_buf[0][(int)enum_medicine_page_cloud.藥品條碼1].ObjectToString();
                    content.藥品條碼2 = list_MED_cloud_buf[0][(int)enum_medicine_page_cloud.藥品條碼2].ObjectToString();
                    content.包裝單位 = list_MED_cloud_buf[0][(int)enum_medicine_page_cloud.包裝單位].ObjectToString();
                    content.理論值 = deviceBasics[i].Inventory;
                    creat.Contents.Add(content);
                }
            }
            returnData.Data = creat;

            return POST_creat_add(returnData);
        }
        //以盤點單號刪除盤點單
        [Route("creat_delete_by_IC_SN")]
        [HttpPost]
        public string POST_creat_delete_by_IC_SN([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            List<object[]> list_inventory_creat = this.sQLControl_inventory_creat.GetAllRows(null);
            List<object[]> list_inventory_creat_buf = new List<object[]>();
            List<object[]> list_inventory_content = this.sQLControl_inventory_content.GetAllRows(null);
            List<object[]> list_inventory_content_buf = new List<object[]>();
            List<object[]> list_inventory_sub_content = this.sQLControl_inventory_sub_content.GetAllRows(null);
            List<object[]> list_inventory_sub_content_buf = new List<object[]>();
            inventoryClass.creat creat = inventoryClass.creat.ObjToClass(returnData.Data);

            //if(returnData.Value.StringIsEmpty())
            //{
            //    returnData.Code = -5;
            //    returnData.Result = $"請購單號不得為空白!";
            //    return returnData.JsonSerializationt();
            //}

            list_inventory_creat_buf = list_inventory_creat.GetRows((int)enum_盤點單號.盤點單號, creat.盤點單號);
            list_inventory_content_buf = list_inventory_content.GetRows((int)enum_盤點內容.盤點單號, creat.盤點單號);
            list_inventory_sub_content_buf = list_inventory_sub_content.GetRows((int)enum_盤點明細.盤點單號, creat.盤點單號);

            this.sQLControl_inventory_creat.DeleteExtra(null, list_inventory_creat_buf);
            this.sQLControl_inventory_content.DeleteExtra(null, list_inventory_content_buf);
            this.sQLControl_inventory_sub_content.DeleteExtra(null, list_inventory_sub_content_buf);
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"已將[{ creat.盤點單號}]刪除!";
            return returnData.JsonSerializationt();


        }



        [Route("contents_delete_by_GUID")]
        [HttpPost]
        public string POST_contents_delete_by_GUID([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            if (returnData.Data == null)
            {
                returnData.Code = -5;
                returnData.Result = $"Data資料長度錯誤!";
                return returnData.JsonSerializationt();
            }
            List<inventoryClass.content> contents = inventoryClass.content.ObjToListClass(returnData.Data);
            List<object> list_GUID = new List<object>();
            for (int i = 0; i < contents.Count; i++)
            {
                list_GUID.Add(contents[i].GUID);
            }
            this.sQLControl_inventory_content.DeleteExtra(null, enum_盤點內容.GUID.GetEnumName(), list_GUID);
            this.sQLControl_inventory_sub_content.DeleteExtra(null, enum_盤點明細.Master_GUID.GetEnumName(), list_GUID);
            returnData.Data = null;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"已刪除盤點內容共<{list_GUID.Count}>筆資料!";
            return returnData.JsonSerializationt();
        }



        [Route("sub_content_get_by_content_GUID")]
        [HttpPost]
        public string POST_sub_content_get_by_content_GUID([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            inventoryClass.content content = inventoryClass.content.ObjToClass(returnData.Data);
            //if (returnData.Value.StringIsEmpty())
            //{
            //    returnData.Code = -5;
            //    returnData.Result = "輸入資料不得空白!";
            //    return returnData.JsonSerializationt();
            //}

            string GUID = content.GUID;
            returnData.Data = null;
            List<object[]> list_inventory_sub_content = this.sQLControl_inventory_sub_content.GetAllRows(null);
            List<object[]> list_inventory_sub_content_buf = new List<object[]>();
            list_inventory_sub_content_buf = list_inventory_sub_content.GetRows((int)enum_盤點明細.Master_GUID, GUID);
            List<inventoryClass.sub_content> sub_Contents = new List<inventoryClass.sub_content>();
            for (int i = 0; i < list_inventory_sub_content_buf.Count; i++)
            {
                inventoryClass.sub_content sub_Content = inventoryClass.sub_content.SQLToClass(list_inventory_sub_content_buf[i]);
                sub_Contents.Add(sub_Content);
            }
            returnData.Data = sub_Contents;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"取得盤點明細成功!";
            return returnData.JsonSerializationt();
        }
        //盤點明細新增
        [Route("sub_content_add")]
        public string POST_sub_content_add([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);

            List<object[]> list_inventory_content = this.sQLControl_inventory_content.GetAllRows(null);
            List<object[]> list_inventory_content_buf = new List<object[]>();
            List<object[]> list_add = new List<object[]>();
            inventoryClass.sub_content sub_content = inventoryClass.sub_content.ObjToClass(returnData.Data);
            string Master_GUID = sub_content.Master_GUID;
            list_inventory_content_buf = list_inventory_content.GetRows((int)enum_盤點內容.GUID, Master_GUID);
            if (list_inventory_content_buf.Count > 0)
            {
                object[] value = new object[new enum_盤點明細().GetLength()];
                value[(int)enum_盤點明細.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_盤點明細.藥品碼] = list_inventory_content_buf[0][(int)enum_盤點內容.藥品碼];
                value[(int)enum_盤點明細.料號] = list_inventory_content_buf[0][(int)enum_盤點內容.料號];
                value[(int)enum_盤點明細.盤點單號] = list_inventory_content_buf[0][(int)enum_盤點內容.盤點單號];
                value[(int)enum_盤點明細.藥品條碼1] = list_inventory_content_buf[0][(int)enum_盤點內容.藥品條碼1];
                value[(int)enum_盤點明細.藥品條碼1] = list_inventory_content_buf[0][(int)enum_盤點內容.藥品條碼2];

                value[(int)enum_盤點明細.Master_GUID] = Master_GUID;
                value[(int)enum_盤點明細.效期] = sub_content.效期;
                value[(int)enum_盤點明細.批號] = sub_content.批號;
                value[(int)enum_盤點明細.盤點量] = sub_content.盤點量;
                value[(int)enum_盤點明細.操作人] = sub_content.操作人;
                value[(int)enum_盤點明細.操作時間] = DateTime.Now.ToDateTimeString();
                value[(int)enum_盤點明細.狀態] = "未鎖定";

                list_add.Add(value);
            }
            this.sQLControl_inventory_sub_content.AddRows(null, list_add);
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"新增批效成功!";
            returnData.Data = null;
            return returnData.JsonSerializationt();
        }
        //以GUID刪除盤點明細
        [Route("sub_contents_delete_by_GUID")]
        [HttpPost]
        public string POST_sub_contents_delete_by_GUID([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            List<inventoryClass.sub_content> sub_contents = inventoryClass.sub_content.ObjToListClass(returnData.Data);
            List<object> list_GUID = new List<object>();
            for (int i = 0; i < sub_contents.Count; i++)
            {
                list_GUID.Add(sub_contents[i].GUID);
            }
            this.sQLControl_inventory_sub_content.DeleteExtra(null, enum_盤點明細.GUID.GetEnumName(), list_GUID);
            returnData = new returnData();
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"已刪除盤點明細共<{list_GUID.Count}>筆資料!";
            return returnData.JsonSerializationt();
        }

        public returnData Function_Get_inventory_creat(List<object[]> list_inventory_creat)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData returnData = new returnData();
            List<object[]> list_inventory_creat_buf = new List<object[]>();
            List<object[]> list_inventory_content = this.sQLControl_inventory_content.GetAllRows(null);
            List<object[]> list_inventory_content_buf = new List<object[]>();
            List<object[]> list_inventory_sub_content = this.sQLControl_inventory_sub_content.GetAllRows(null);
            List<object[]> list_inventory_sub_content_buf = new List<object[]>();
            List<object[]> list_sub_inventory = this.sQLControl_inventory_sub_content.GetAllRows(null);
            List<object[]> list_sub_inventory_buf = new List<object[]>();

            List<object[]> list_MED_cloud = sQLControl_MED_cloud.GetAllRows(null);
            List<object[]> list_MED_cloud_buf = new List<object[]>();
            string 藥品碼 = "";
            string 藥品名稱 = "";
            string 中文名稱 = "";
            string 包裝單位 = "";
            List<inventoryClass.creat> creats = new List<inventoryClass.creat>();
            for (int i = 0; i < list_inventory_creat.Count; i++)
            {
                inventoryClass.creat creat = inventoryClass.creat.SQLToClass(list_inventory_creat[i]);
                list_inventory_content_buf = list_inventory_content.GetRows((int)enum_盤點內容.Master_GUID, creat.GUID);
                for (int k = 0; k < list_inventory_content_buf.Count; k++)
                {
                    inventoryClass.content content = inventoryClass.content.SQLToClass(list_inventory_content_buf[k]);
                    藥品碼 = content.藥品碼;
                    藥品名稱 = "";
                    中文名稱 = "";
                    包裝單位 = "";
                    list_MED_cloud_buf = list_MED_cloud.GetRows((int)enum_雲端藥檔.藥品碼, 藥品碼);
                    if (list_MED_cloud_buf.Count > 0)
                    {
                        藥品名稱 = list_MED_cloud_buf[0][(int)enum_雲端藥檔.藥品名稱].ObjectToString();
                        中文名稱 = list_MED_cloud_buf[0][(int)enum_雲端藥檔.中文名稱].ObjectToString();
                        包裝單位 = list_MED_cloud_buf[0][(int)enum_雲端藥檔.包裝單位].ObjectToString();
                    }
                    content.藥品名稱 = 藥品名稱;
                    content.中文名稱 = 中文名稱;
                    content.包裝單位 = 包裝單位;

                    int 盤點量 = 0;
                    list_inventory_sub_content_buf = list_inventory_sub_content.GetRows((int)enum_盤點明細.Master_GUID, content.GUID);
                    for (int m = 0; m < list_inventory_sub_content_buf.Count; m++)
                    {
                        inventoryClass.sub_content sub_Content = inventoryClass.sub_content.SQLToClass(list_inventory_sub_content_buf[m]);
                        sub_Content.藥品名稱 = 藥品名稱;
                        sub_Content.中文名稱 = 中文名稱;
                        sub_Content.包裝單位 = 包裝單位;
                        if (sub_Content.盤點量.StringIsInt32())
                        {
                            盤點量 += sub_Content.盤點量.StringToInt32();
                        }
                        content.Sub_content.Add(sub_Content);
                    }
                    content.盤點量 = 盤點量.ToString();

                    creat.Contents.Add(content);
                }
                creats.Add(creat);
            }
            returnData.Data = creats;
            returnData.Code = 200;
            returnData.Result = $"成功! {myTimer.ToString()}";
            return returnData;
        }
       
    }
}
