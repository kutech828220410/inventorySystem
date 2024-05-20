using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
namespace E_UpdateVersion
{
    public partial class Dialog_SetApiServer : Form
    {
        public static Form form;
        public DialogResult ShowDialog()
        {
            if (form == null)
            {
                base.ShowDialog();
            }
            else
            {
                form.Invoke(new Action(delegate
                {
                    base.ShowDialog();
                }));
            }

            return this.DialogResult;
        }
        public string Value
        {
            get
            {
                return this.rJ_TextBox_API_URL.Texts;
            }
        }
        public Dialog_SetApiServer()
        {
            InitializeComponent();
            this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
        }

        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            string json = Net.WEBApiGet($"{Value}/api/test");
            if(json.StringIsEmpty() == false)
            {
                MyMessageBox.ShowDialog("連線成功");
                this.Invoke(new Action(delegate
                {
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }));
            }
            else
            {
                MyMessageBox.ShowDialog("連線失敗\n※網路異常,請檢查網路與院內連線是否正常");
                return;
            }
        }
    }
}
