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
using HIS_DB_Lib;
namespace HIS_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class inspectionController : Controller
    {
        static private string DataBaseName = ConfigurationManager.AppSettings["database"];
        static private string UserName = ConfigurationManager.AppSettings["user"];
        static private string Password = ConfigurationManager.AppSettings["password"];
        static private string IP = ConfigurationManager.AppSettings["IP"];
        static private uint Port = (uint)ConfigurationManager.AppSettings["port"].StringToInt32();
        static private MySqlSslMode SSLMode = MySqlSslMode.None;


        static private string MDC_DataBaseName = ConfigurationManager.AppSettings["MED_cloud_DB"];
        static private string MDC_IP = ConfigurationManager.AppSettings["MED_cloud_IP"];

        private SQLControl sQLControl_inspection_creat = new SQLControl(IP, DataBaseName, "inspection_creat", UserName, Password, Port, SSLMode);
        private SQLControl sQLControl_inspection_content = new SQLControl(IP, DataBaseName, "inspection_content", UserName, Password, Port, SSLMode);
        private SQLControl sQLControl_inspection_sub_content = new SQLControl(IP, DataBaseName, "inspection_sub_content", UserName, Password, Port, SSLMode);
        private SQLControl sQLControl_MED_cloud = new SQLControl(MDC_IP, MDC_DataBaseName, "medicine_page_cloud", UserName, Password, Port, SSLMode);

        //取得所有驗收資料
        [HttpGet]
        public string Get()
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData returnData = new returnData();
            List<object[]> list_inspection_creat = this.sQLControl_inspection_creat.GetAllRows(null);
            returnData = Function_Get_inspection_creat(list_inspection_creat);
            return returnData.JsonSerializationt(false);
        }

        //取得可建立今日最新驗收單
        [Route("new_ACPT_SN")]
        [HttpGet]
        public string GET_new_ACPT_SN()
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData returnData = new returnData();
            List<object[]> list_inspection_creat = this.sQLControl_inspection_creat.GetAllRows(null);
            List<object[]> list_inspection_creat_buf = new List<object[]>();

            list_inspection_creat_buf = list_inspection_creat.GetRowsInDate((int)enum_驗收單號.建表時間, DateTime.Now);

            List<object[]> list_medecine = this.sQLControl_MED_cloud.GetAllRows(null);
            List<object[]> list_medecine_buf = new List<object[]>();
            string 驗收單號 = "";
            int index = 0;
            while(true)
            {
                驗收單號 = $"{DateTime.Now.ToDateTinyString()}-{index}";
                index++;
                list_inspection_creat_buf = list_inspection_creat.GetRows((int)enum_驗收單號.驗收單號, 驗收單號);
                if (list_inspection_creat_buf.Count == 0) break;
            }
            returnData.Value = 驗收單號;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"成功! {myTimer.ToString()}";
            return returnData.JsonSerializationt(true);
        }
   
        //creat
        //以建表日搜尋驗收單
        [Route("creat_get_by_CT_TIME")]
        [HttpPost]
        public string POST_creat_get_by_CT_TIME([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            inspectionClass.creat creat = inspectionClass.creat.ObjToClass(returnData.Data);
            if (creat.建表時間.Check_Date_String() == false)
            {
                returnData.Code = -5;
                returnData.Result = "輸入日期格式錯誤!";
                return returnData.JsonSerializationt();
            }

            List<object[]> list_inspection_creat = this.sQLControl_inspection_creat.GetAllRows(null);
            list_inspection_creat = list_inspection_creat.GetRowsInDate((int)enum_驗收單號.建表時間, creat.建表時間.StringToDateTime());
            returnData = Function_Get_inspection_creat(list_inspection_creat);
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"取得驗收資料成功!";

            return returnData.JsonSerializationt(true);
        }
        //以驗收單號搜尋驗收單
        [Route("creat_get_by_ACPT_SN")]
        [HttpPost]
        public string POST_creat_get_by_ACPT_SN([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            inspectionClass.creat creat = inspectionClass.creat.ObjToClass(returnData.Data);
            //if (returnData.Value.StringIsEmpty())
            //{
            //    returnData.Code = -5;
            //    returnData.Result = "輸入驗收單號不得空白!";
            //    return returnData.JsonSerializationt();
            //}
            List<object[]> list_inspection_creat = this.sQLControl_inspection_creat.GetAllRows(null);
            list_inspection_creat = list_inspection_creat.GetRows((int)enum_驗收單號.驗收單號, creat.驗收單號);
            if(list_inspection_creat.Count == 0)
            {
                returnData.Code = -5;
                returnData.Result = $"查無此單號資料[{returnData.Value}]!";
                return returnData.JsonSerializationt();
            }
            returnData = Function_Get_inspection_creat(list_inspection_creat);
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"取得驗收資料成功!";

            return returnData.JsonSerializationt();
        }
        //驗收單新增
        [Route("creat_add")]
        [HttpPost]
        public string POST_creat([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            inspectionClass.creat creat = inspectionClass.creat.ObjToClass(returnData.Data);
            List<object[]> list_inspection_creat = this.sQLControl_inspection_creat.GetAllRows(null);
            List<object[]> list_inspection_creat_buf = new List<object[]>();
            List<object[]> list_inspection_content = this.sQLControl_inspection_content.GetAllRows(null);
            List<object[]> list_inspection_content_buf = new List<object[]>();
            List<object[]> list_inspection_sub_content = this.sQLControl_inspection_sub_content.GetAllRows(null);
            List<object[]> list_inspection_sub_content_buf = new List<object[]>();
   

            List<object[]> list_medecine = this.sQLControl_MED_cloud.GetAllRows(null);
            List<object[]> list_medecine_buf = new List<object[]>();
            if (creat == null)
            {
                returnData.Code = -5;
                returnData.Result += $"Data 資料錯誤 \n";
                return returnData.JsonSerializationt();
            }
        
            list_inspection_creat_buf = list_inspection_creat.GetRows((int)enum_驗收單號.請購單號, creat.請購單號);
            if (list_inspection_creat_buf.Count > 0)
            {
                returnData.Code = -6;
                returnData.Result += $"請購單號: {creat.請購單號} 已存在,請刪除後再建立! \n";
                return returnData.JsonSerializationt();
            }
            creat.GUID = Guid.NewGuid().ToString();
            creat.建表時間 = DateTime.Now.ToDateTimeString();
            creat.驗收開始時間 = DateTime.MinValue.ToDateTimeString();
            creat.驗收結束時間 = DateTime.MinValue.ToDateTimeString();

            List<object[]> list_inspection_creat_add = new List<object[]>();
            List<object[]> list_inspection_content_add = new List<object[]>();
            object[] value;
            value = new object[new enum_驗收單號().GetLength()];
           
            value[(int)enum_驗收單號.GUID] = creat.GUID;
            value[(int)enum_驗收單號.驗收單號] = creat.驗收單號;
            value[(int)enum_驗收單號.請購單號] = creat.請購單號;
            value[(int)enum_驗收單號.建表人] = creat.建表人;
            value[(int)enum_驗收單號.建表時間] = creat.建表時間;
            value[(int)enum_驗收單號.驗收開始時間] = creat.驗收開始時間;
            value[(int)enum_驗收單號.驗收結束時間] = creat.驗收結束時間;
            value[(int)enum_驗收單號.驗收狀態] = "等待驗收";
            list_inspection_creat_add.Add(value);

            for (int i = 0; i < creat.Contents.Count; i++)
            {
                value = new object[new enum_盤點內容().GetLength()];
                creat.Contents[i].GUID = Guid.NewGuid().ToString();
                creat.Contents[i].新增時間 = DateTime.Now.ToDateTimeString();
                creat.Contents[i].Master_GUID = creat.GUID;
                value[(int)enum_驗收內容.GUID] = creat.Contents[i].GUID;
                value[(int)enum_驗收內容.Master_GUID] = creat.Contents[i].Master_GUID;
                value[(int)enum_驗收內容.藥品碼] = creat.Contents[i].藥品碼;
                value[(int)enum_驗收內容.料號] = creat.Contents[i].料號;
                value[(int)enum_驗收內容.藥品條碼1] = creat.Contents[i].藥品條碼1;
                value[(int)enum_驗收內容.藥品條碼2] = creat.Contents[i].藥品條碼2;
                value[(int)enum_驗收內容.驗收單號] = creat.Contents[i].驗收單號;
                value[(int)enum_驗收內容.請購單號] = creat.Contents[i].請購單號;
                value[(int)enum_驗收內容.應收數量] = creat.Contents[i].應收數量;
                value[(int)enum_驗收內容.新增時間] = creat.Contents[i].新增時間;
                list_inspection_content_add.Add(value);
            }
            sQLControl_inspection_creat.AddRows(null, list_inspection_creat_add);
            sQLControl_inspection_content.AddRows(null, list_inspection_content_add);
            returnData.Data = creat;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"成功加入新驗收單! 共{list_inspection_content_add.Count}筆資料";
            return returnData.JsonSerializationt(true);
        }
        //以驗收單號刪除驗收單
        [Route("creat_delete_by_ACPT_SN")]
        [HttpPost]
        public string POST_creat_delete_by_ACPT_SN([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            List<object[]> list_inspection_creat = this.sQLControl_inspection_creat.GetAllRows(null);
            List<object[]> list_inspection_creat_buf = new List<object[]>();
            List<object[]> list_inspection_content = this.sQLControl_inspection_content.GetAllRows(null);
            List<object[]> list_inspection_content_buf = new List<object[]>();
            List<object[]> list_inspection_sub_content = this.sQLControl_inspection_sub_content.GetAllRows(null);
            List<object[]> list_inspection_sub_content_buf = new List<object[]>();
            inspectionClass.creat creat = inspectionClass.creat.ObjToClass(returnData.Data);

            //if(returnData.Value.StringIsEmpty())
            //{
            //    returnData.Code = -5;
            //    returnData.Result = $"請購單號不得為空白!";
            //    return returnData.JsonSerializationt();
            //}

            list_inspection_creat_buf = list_inspection_creat.GetRows((int)enum_驗收單號.驗收單號, creat.驗收單號);
            list_inspection_content_buf = list_inspection_content.GetRows((int)enum_驗收內容.驗收單號, creat.驗收單號);
            list_inspection_sub_content_buf = list_inspection_sub_content.GetRows((int)enum_驗收明細.驗收單號, creat.驗收單號);

            this.sQLControl_inspection_creat.DeleteExtra(null, list_inspection_creat_buf);
            this.sQLControl_inspection_content.DeleteExtra(null, list_inspection_content_buf);
            this.sQLControl_inspection_sub_content.DeleteExtra(null, list_inspection_sub_content_buf);
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"已將[{ creat.驗收單號}]刪除!";
            return returnData.JsonSerializationt();


        }

        //content
        //以GUID刪除驗收內容
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
            List<inspectionClass.content> contents = inspectionClass.content.ObjToListClass(returnData.Data);
            List<object> list_GUID = new List<object>();
            for (int i = 0; i < contents.Count; i++)
            {
                list_GUID.Add(contents[i].GUID);
            }
            this.sQLControl_inspection_content.DeleteExtra(null, enum_驗收內容.GUID.GetEnumName(), list_GUID);
            this.sQLControl_inspection_sub_content.DeleteExtra(null, enum_驗收明細.Master_GUID.GetEnumName(), list_GUID);
            returnData.Data = null;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"已刪除驗收內容共<{list_GUID.Count}>筆資料!";
            return returnData.JsonSerializationt();
        }

        //subcontent
        //以驗收內容GUID搜尋驗收明細
        [Route("sub_content_get_by_content_GUID")]
        [HttpPost]
        public string POST_sub_content_get_by_content_GUID([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            inspectionClass.content content = inspectionClass.content.ObjToClass(returnData.Data);
            //if (returnData.Value.StringIsEmpty())
            //{
            //    returnData.Code = -5;
            //    returnData.Result = "輸入資料不得空白!";
            //    return returnData.JsonSerializationt();
            //}

            string GUID = content.GUID;
            returnData.Data = null;
            List<object[]> list_inspection_sub_content = this.sQLControl_inspection_sub_content.GetAllRows(null);
            List<object[]> list_inspection_sub_content_buf = new List<object[]>();
            list_inspection_sub_content_buf = list_inspection_sub_content.GetRows((int)enum_驗收明細.Master_GUID, GUID);
            List<inspectionClass.sub_content> sub_Contents = new List<inspectionClass.sub_content>();
            for (int i = 0; i < list_inspection_sub_content_buf.Count; i++)
            {
                inspectionClass.sub_content sub_Content = inspectionClass.sub_content.SQLToClass(list_inspection_sub_content_buf[i]);
                sub_Contents.Add(sub_Content);
            }
            returnData.Data = sub_Contents;
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"取得驗收明細成功!";
            return returnData.JsonSerializationt();
        }
        //驗收明細新增
        [Route("sub_content_add")]
        public string POST_sub_content_add([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
  
            List<object[]> list_inspection_content = this.sQLControl_inspection_content.GetAllRows(null);
            List<object[]> list_inspection_content_buf = new List<object[]>();
            List<object[]> list_add = new List<object[]>();
            inspectionClass.sub_content sub_content = inspectionClass.sub_content.ObjToClass(returnData.Data);
            string Master_GUID = sub_content.Master_GUID;
            list_inspection_content_buf = list_inspection_content.GetRows((int)enum_驗收內容.GUID, Master_GUID);
            if (list_inspection_content_buf.Count > 0)
            {
                object[] value = new object[new enum_驗收明細().GetLength()];
                value[(int)enum_驗收明細.GUID] = Guid.NewGuid().ToString();
                value[(int)enum_驗收明細.藥品碼] = list_inspection_content_buf[0][(int)enum_驗收內容.藥品碼];
                value[(int)enum_驗收明細.料號] = list_inspection_content_buf[0][(int)enum_驗收內容.料號];
                value[(int)enum_驗收明細.請購單號] = list_inspection_content_buf[0][(int)enum_驗收內容.請購單號];
                value[(int)enum_驗收明細.驗收單號] = list_inspection_content_buf[0][(int)enum_驗收內容.驗收單號];
                value[(int)enum_驗收明細.藥品條碼1] = list_inspection_content_buf[0][(int)enum_驗收內容.藥品條碼1];
                value[(int)enum_驗收明細.藥品條碼1] = list_inspection_content_buf[0][(int)enum_驗收內容.藥品條碼2];

                value[(int)enum_驗收明細.Master_GUID] = Master_GUID;
                value[(int)enum_驗收明細.效期] = sub_content.效期;
                value[(int)enum_驗收明細.批號] = sub_content.批號;
                value[(int)enum_驗收明細.實收數量] = sub_content.實收數量;
                value[(int)enum_驗收明細.驗收時間] = DateTime.Now.ToDateTimeString();
                value[(int)enum_驗收明細.狀態] = "未鎖定";

                list_add.Add(value);
            }
            this.sQLControl_inspection_sub_content.AddRows(null, list_add);
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"新增批效成功!";
            returnData.Data = null;
            return returnData.JsonSerializationt();
        }
        //以GUID刪除驗收明細
        [Route("sub_contents_delete_by_GUID")]
        [HttpPost]
        public string POST_sub_contents_delete_by_GUID([FromBody] returnData returnData)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            List<inspectionClass.sub_content> sub_contents = inspectionClass.sub_content.ObjToListClass(returnData.Data);
            List<object> list_GUID = new List<object>();
            for (int i = 0; i < sub_contents.Count; i++)
            {
                list_GUID.Add(sub_contents[i].GUID);
            }
            this.sQLControl_inspection_sub_content.DeleteExtra(null, enum_驗收明細.GUID.GetEnumName(), list_GUID);
            returnData = new returnData();
            returnData.Code = 200;
            returnData.TimeTaken = myTimer.ToString();
            returnData.Result = $"已刪除驗收明細共<{list_GUID.Count}>筆資料!";
            return returnData.JsonSerializationt();
        }


        public returnData Function_Get_inspection_creat(List<object[]> list_inspection_creat)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            returnData returnData = new returnData();
            List<object[]> list_inspection_creat_buf = new List<object[]>();
            List<object[]> list_inspection_content = this.sQLControl_inspection_content.GetAllRows(null);
            List<object[]> list_inspection_content_buf = new List<object[]>();
            List<object[]> list_inspection_sub_content = this.sQLControl_inspection_sub_content.GetAllRows(null);
            List<object[]> list_inspection_sub_content_buf = new List<object[]>();
            List<object[]> list_sub_inspection = this.sQLControl_inspection_sub_content.GetAllRows(null);
            List<object[]> list_sub_inspection_buf = new List<object[]>();

            List<object[]> list_MED_cloud = sQLControl_MED_cloud.GetAllRows(null);
            List<object[]> list_MED_cloud_buf = new List<object[]>();
            string 藥品碼 = "";
            string 藥品名稱 = "";
            string 中文名稱 = "";
            string 包裝單位 = "";
            List<inspectionClass.creat> creats = new List<inspectionClass.creat>();
            for (int i = 0; i < list_inspection_creat.Count; i++)
            {
                inspectionClass.creat creat = inspectionClass.creat.SQLToClass(list_inspection_creat[i]);
                list_inspection_content_buf = list_inspection_content.GetRows((int)enum_驗收內容.Master_GUID, creat.GUID);
                for (int k = 0; k < list_inspection_content_buf.Count; k++)
                {
                    inspectionClass.content content = inspectionClass.content.SQLToClass(list_inspection_content_buf[k]);
                    藥品碼 = content.藥品碼;
                    藥品名稱 = "";
                    中文名稱 = "";
                    包裝單位 = "";
                    list_MED_cloud_buf = list_MED_cloud.GetRows((int)enum_雲端藥檔.藥品碼, 藥品碼);
                    if(list_MED_cloud_buf.Count > 0)
                    {
                        藥品名稱 = list_MED_cloud_buf[0][(int)enum_雲端藥檔.藥品名稱].ObjectToString();
                        中文名稱 = list_MED_cloud_buf[0][(int)enum_雲端藥檔.中文名稱].ObjectToString();
                        包裝單位 = list_MED_cloud_buf[0][(int)enum_雲端藥檔.包裝單位].ObjectToString();
                    }
                    content.藥品名稱 = 藥品名稱;
                    content.中文名稱 = 中文名稱;
                    content.包裝單位 = 包裝單位;

                    int 實收數量 = 0;
                    list_inspection_sub_content_buf = list_inspection_sub_content.GetRows((int)enum_驗收明細.Master_GUID, content.GUID);
                    for (int m = 0; m < list_inspection_sub_content_buf.Count; m++)
                    {
                        inspectionClass.sub_content sub_Content = inspectionClass.sub_content.SQLToClass(list_inspection_sub_content_buf[m]);
                        sub_Content.藥品名稱 = 藥品名稱;
                        sub_Content.中文名稱 = 中文名稱;
                        sub_Content.包裝單位 = 包裝單位;
                        if (sub_Content.實收數量.StringIsInt32())
                        {
                            實收數量 += sub_Content.實收數量.StringToInt32();
                        }
                        content.Sub_content.Add(sub_Content);
                    }
                    content.實收數量 = 實收數量.ToString();

                    creat.Contents.Add(content);
                }
                creats.Add(creat);
            }
            returnData.Data = creats;
            returnData.Code = 200;
            returnData.Result = $"成功! {myTimer.ToString()}";
            return returnData;
        }
        //[Route("update")]
        //[HttpPost]
        //public string Post_update([FromBody] returnData data)
        //{
        //    MyTimer myTimer = new MyTimer();
        //    myTimer.StartTickTime(50000);
        //    List<object[]> list_inspection_replace = new List<object[]>();
        //    List<object[]> list_inspection_add = new List<object[]>();

        //    List<object[]> list_sub_inspection_replace = new List<object[]>();
        //    List<object[]> list_sub_inspection_add = new List<object[]>();
        //    List<object[]> list_sub_inspection_delete = new List<object[]>();

        //    List<object[]> list_inspection = this.sQLControl_inspection_content.GetAllRows(null);
        //    List<object[]> list_inspection_buf = new List<object[]>();
        //    List<object[]> list_sub_inspection = this.sQLControl_inspection_sub_content.GetAllRows(null);
        //    List<object[]> list_sub_inspection_buf = new List<object[]>();

        //    for (int i = 0; i < data.Data.Count; i++)
        //    {
        //        //取得母資料
        //        class_output_inspection_data class_Output_Inspection_Data = new class_output_inspection_data();
        //        class_Output_Inspection_Data = class_Output_Inspection_Data.ObjToData(data.Data[i]);
        //        list_inspection_buf = list_inspection.GetRows((int)enum_驗收入庫資料.GUID, class_Output_Inspection_Data.GUID);
        //        if (list_inspection_buf.Count == 0) continue;

        //        object[] value = list_inspection_buf[0];
        //        string Mater_GUID = value[(int)enum_驗收入庫資料.GUID].ObjectToString();
        //        string 藥品碼 = value[(int)enum_驗收入庫資料.藥品碼].ObjectToString();
        //        string 料號 = value[(int)enum_驗收入庫資料.料號].ObjectToString();
        //        string 請購單號 = value[(int)enum_驗收入庫資料.請購單號].ObjectToString();


        //        //刪除相關子資料
        //        list_sub_inspection_buf = list_sub_inspection.GetRows((int)enum_驗收入庫效期批號.Master_GUID, Mater_GUID);
        //        list_sub_inspection_delete.LockAdd(list_sub_inspection_buf);

        //        //重新新增子資料
        //        int 實收數量 = 0;
        //        for (int k = 0; k < class_Output_Inspection_Data.Lot_date_datas.Count; k++)
        //        {
        //            class_Output_Inspection_Data.Lot_date_datas[k].GUID = Guid.NewGuid().ToString();

        //            if (class_Output_Inspection_Data.Lot_date_datas[k].更新 == "True")
        //            {
        //                class_Output_Inspection_Data.Lot_date_datas[k].驗收時間 = DateTime.Now.ToDateTimeString();
        //            }
        //            object[] value_sub_inspection = class_output_inspection_data.Get_SQL_DATA(class_Output_Inspection_Data.Lot_date_datas[k]);
        //            value_sub_inspection[(int)enum_驗收入庫效期批號.Master_GUID] = Mater_GUID;
        //            value_sub_inspection[(int)enum_驗收入庫效期批號.藥品碼] = 藥品碼;
        //            value_sub_inspection[(int)enum_驗收入庫效期批號.料號] = 料號;
        //            value_sub_inspection[(int)enum_驗收入庫效期批號.請購單號] = 請購單號;
        //            list_sub_inspection_add.Add(value_sub_inspection);
        //            if (class_Output_Inspection_Data.Lot_date_datas[k].數量.StringIsInt32()) 實收數量 += class_Output_Inspection_Data.Lot_date_datas[k].數量.StringToInt32();
        //        }

        //        class_Output_Inspection_Data.實收數量 = 實收數量.ToString();

        //        if (class_Output_Inspection_Data.實收數量 != value[(int)enum_驗收入庫資料.實收數量].ObjectToString())
        //        {
        //            value[(int)enum_驗收入庫資料.實收數量] = class_Output_Inspection_Data.實收數量;
        //            list_sub_inspection_replace.Add(value);
        //        }
        //        data.Data[i] = class_Output_Inspection_Data;
        //    }
        //    if (list_sub_inspection_replace.Count > 0) this.sQLControl_inspection_content.UpdateByDefulteExtra(null, list_sub_inspection_replace);
        //    if (list_sub_inspection_delete.Count > 0) this.sQLControl_inspection_sub_content.DeleteExtra(null, list_sub_inspection_delete);
        //    if (list_sub_inspection_add.Count > 0) this.sQLControl_inspection_sub_content.AddRows(null, list_sub_inspection_add);
        //    data.Result = $"Inspection data update 成功! {myTimer.ToString()}";
        //    return data.JsonSerializationt();
        //}

        //[Route("download_excel")]
        //[HttpPost]
        //public async Task<ActionResult> Post_download_excel([FromBody] returnData data)
        //{
        //    string jsonstr = Post_OD_Date(data);
        //    string 請購單號 = "";
        //    string 驗收時間 = "";
        //    int NumOfRow = 0;
        //    for (int i = 0; i < data.Data.Count; i++)
        //    {
        //        class_output_inspection_date class_Output_Inspection_Date = new class_output_inspection_date();
        //        class_Output_Inspection_Date = class_Output_Inspection_Date.ObjToData(data.Data[i]);

        //        驗收時間 += class_Output_Inspection_Date.驗收時間;
        //        請購單號 += class_Output_Inspection_Date.請購單號;
        //        if (i != data.Data.Count - 1)
        //        {
        //            驗收時間 += ",";
        //            請購單號 += ",";
        //        }
        //    }


        //    returnData returnData = jsonstr.JsonDeserializet<returnData>();
        //    returnData.Code = 200;
        //    List<SheetClass> sheetClasses = new List<SheetClass>();
        //    string loadText = Basic.MyFileStream.LoadFileAllText(@"C:\excel.txt", "utf-8");
        //    SheetClass sheetClass = loadText.JsonDeserializet<SheetClass>();
        //    sheetClass.ReplaceCell(1, 2, $"{驗收時間}");
        //    sheetClass.ReplaceCell(1, 6, $"{請購單號}");
        //    NumOfRow = 0;
        //    for (int i = 0; i < returnData.Data.Count; i++)
        //    {
        //        class_output_inspection_data class_Output_Inspection_Data = new class_output_inspection_data();
        //        class_Output_Inspection_Data = class_Output_Inspection_Data.ObjToData(returnData.Data[i]);
        //        sheetClass.AddNewCell_Webapi(NumOfRow + 3, 0, $"{class_Output_Inspection_Data.請購單號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
        //        sheetClass.AddNewCell_Webapi(NumOfRow + 3, 1, $"{class_Output_Inspection_Data.藥品碼}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
        //        sheetClass.AddNewCell_Webapi(NumOfRow + 3, 2, $"{class_Output_Inspection_Data.料號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
        //        sheetClass.AddNewCell_Webapi(NumOfRow + 3, NumOfRow + 3, 3, 4, $"{class_Output_Inspection_Data.藥品名稱}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
        //        //sheetClass.AddNewCell_Webapi(NumOfRow + 3, 5, $"{class_Output_Inspection_Data.效期}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
        //        //sheetClass.AddNewCell_Webapi(NumOfRow + 3, 6, $"{class_Output_Inspection_Data.批號}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
        //        sheetClass.AddNewCell_Webapi(NumOfRow + 3, 7, $"{class_Output_Inspection_Data.應收數量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);
        //        sheetClass.AddNewCell_Webapi(NumOfRow + 3, 8, $"{class_Output_Inspection_Data.實收數量}", "微軟正黑體", 14, false, NPOI_Color.BLACK, 430, NPOI.SS.UserModel.HorizontalAlignment.Left, NPOI.SS.UserModel.VerticalAlignment.Bottom, NPOI.SS.UserModel.BorderStyle.Thin);

        //        NumOfRow++;
        //    }


        //    byte[] excelData = sheetClass.NPOI_GetBytes();
        //    Stream stream = new MemoryStream(excelData);
        //    return await Task.FromResult(File(stream, "application/vnd.ms-excel", $"{DateTime.Now.ToDateString("-")}_驗收入庫清單.xls"));
        //}

    }
}
