using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS_DB_Lib;
using Basic;
using MyUI;
using SQLUI;
using DrawingClass;
using H_Pannel_lib;

namespace 智能藥庫系統
{
    public partial class Dialog_批號數量修改 : MyDialog
    {
        private string _藥碼 = "";
        private string _藥名 = "";
        private string _效期 = "";
        private string _批號 = "";
        private int _數量 = 0;

        public StockClass Value = new StockClass();
        public Dialog_批號數量修改(string 藥碼, string 藥名, string 效期, string 批號, int 數量)
        {
            InitializeComponent();

            this.Load += Dialog_批號數量修改_Load;

            this.rJ_Button_確認選擇.MouseDownEvent += RJ_Button_確認選擇_MouseDownEvent;
            _藥碼 = 藥碼;
            _藥名 = 藥名;

            _效期 = 效期;
            _批號 = 批號;
            _數量 = 數量;
        }

        private void Dialog_批號數量修改_Load(object sender, EventArgs e)
        {
            this.rJ_Lable_藥品資訊.Text = $"({_藥碼}){_藥名}";
            this.rJ_TextBox_效期.Texts = $"{_效期}";
            this.rJ_TextBox_批號.Texts = $"{_批號}";
            this.rJ_TextBox_數量.Texts = $"{_數量}";

        }

        private void RJ_Button_確認選擇_MouseDownEvent(MouseEventArgs mevent)
        {
            if (this.rJ_TextBox_數量.Texts.StringIsInt32() == false)
            {
                Dialog_AlarmForm dialog_AlarmForm = new Dialog_AlarmForm($"數量格式錯誤", 1500);
                dialog_AlarmForm.ShowDialog();
                return;
            }
            this.Invoke(new Action(delegate
            {
                Value.Code = _藥碼;
                Value.Name = _藥名;
                Value.Validity_period = this.rJ_TextBox_效期.Texts;
                Value.Lot_number = this.rJ_TextBox_批號.Texts;
                Value.Qty = this.rJ_TextBox_數量.Texts.ToString();
                this.Close();
                this.DialogResult = DialogResult.Yes;
            }));
        }
    }
}
