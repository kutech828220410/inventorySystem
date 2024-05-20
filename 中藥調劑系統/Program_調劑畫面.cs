using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
using Basic;
using SQLUI;
using ExcelScaleLib;
using HIS_DB_Lib;
namespace 中藥調劑系統
{
    public partial class Main_Form : Form
    {
        public enum enum_處方內容
        {
            [Description("GUID,VARCHAR,15,NONE")]
            GUID,
            [Description("藥名,VARCHAR,15,NONE")]
            藥名,
            [Description("應調,VARCHAR,15,NONE")]
            應調,
            [Description("實調,VARCHAR,15,NONE")]
            實調,
            [Description("天,VARCHAR,15,NONE")]
            天,
            [Description("單位,VARCHAR,15,NONE")]
            單位,
            [Description("服用方法,VARCHAR,15,NONE")]
            服用方法,
        }
        public enum enum_病患資訊
        {
            [Description("領藥號,VARCHAR,15,NONE")]
            領藥號,
            [Description("姓名,VARCHAR,15,NONE")]
            姓名,
        }
        public static sessionClass sessionClass = new sessionClass();
        private void Program_調劑畫面_Init()
        {
            Table table_處方內容 = new Table(new enum_處方內容());
            this.sqL_DataGridView_處方內容.Init(table_處方內容);
            this.sqL_DataGridView_處方內容.Set_ColumnVisible(false, new enum_處方內容().GetEnumNames());
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(400, DataGridViewContentAlignment.MiddleLeft, "藥名");
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleRight, "應調");
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleRight, "實調");
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleCenter, "天");
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(80, DataGridViewContentAlignment.MiddleRight, "單位");
            this.sqL_DataGridView_處方內容.Set_ColumnWidth(100, DataGridViewContentAlignment.MiddleLeft, "服用方法");

            Table table_病患資訊 = new Table(new enum_病患資訊());
            this.sqL_DataGridView_病患資訊.Init(table_病患資訊);
            this.sqL_DataGridView_病患資訊.Set_ColumnVisible(false, new enum_病患資訊().GetEnumNames());
            this.sqL_DataGridView_病患資訊.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, "領藥號");
            this.sqL_DataGridView_病患資訊.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, "姓名");

            this.plC_RJ_Button_登入.MouseDownEvent += PlC_RJ_Button_登入_MouseDownEvent;
            Function_重置處方();
            plC_UI_Init.Add_Method(Program_調劑畫面);
        }
        private void Program_調劑畫面()
        {
            
        }
        #region Function
        public void Function_重置處方()
        {
            this.Invoke(new Action(delegate
            {
                rJ_Lable_處方藥品.Text = $"-------------------";
                rJ_Lable_領藥號.Text = $"----";
                rJ_Lable_處方資訊_姓名_性別_病歷號.Text = $"-------(-) -----------";
                rJ_Lable_處方資訊_生日.Text = $"----/--/--";
                rJ_Lable_處方資訊_處方日期.Text = $"----/--/--";
                rJ_Lable_處方資訊_年齡.Text = $"--歲";
                rJ_Lable_處方資訊_單筆包數.Text = $"-包";
                rJ_Lable_處方資訊_單筆處方天數.Text = $"--天";
                rJ_Lable_處方資訊_單包重.Text = $"--.-- 克/包";
                rJ_Lable_處方資訊_單筆總重.Text = $"總重:---克";
                rJ_Lable_總包數.Text = $"--包";
                rJ_Lable_應調.Text = $"-.--";
            }));
        }
        private void Function_登入(sessionClass _sessionClass)
        {
            sessionClass = _sessionClass;
            this.Invoke(new Action(delegate 
            {
                this.plC_RJ_Button_登入.Texts = "登出";
                PLC_Device_已登入.Bool = true;
                rJ_Lable_調劑人員.Text = $"調劑人員 : {sessionClass.Name}";
                Function_重置處方();
            }));
        }
        private void Function_登出()
        {
            sessionClass = null;
            this.Invoke(new Action(delegate
            {    
                PLC_Device_已登入.Bool = false;
                this.plC_RJ_Button_登入.Texts = "登入";
                rJ_Lable_調劑人員.Text = $"調劑人員 : -------------";
                Function_重置處方();
            }));
        }
        #endregion
        #region Event
        private void PlC_RJ_Button_登入_MouseDownEvent(MouseEventArgs mevent)
        {
            if (this.plC_RJ_Button_登入.Texts == "登入")
            {
                Dialog_系統登入 dialog_系統登入 = new Dialog_系統登入();
                if (dialog_系統登入.ShowDialog() != DialogResult.Yes) return;
                this.Function_登入(dialog_系統登入.Value);            
                return;
            }
            if (this.plC_RJ_Button_登入.Texts == "登出")
            {
                if (MyMessageBox.ShowDialog("是否登出?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
                this.Function_登出();
                return;
            }
        }
        #endregion
    }
}
