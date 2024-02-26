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
namespace 調劑台管理系統
{
    public partial class Dialog_盤點數量錯誤 : MyDialog
    {
     
        private Point location = new Point(0, 0);
        public new Point Location
        {
            get
            {
                return this.location;
            }
            set
            {

                this.location = value;
            }

        }
        public Dialog_盤點數量錯誤()
        {
            InitializeComponent();
            this.Load += Dialog_盤點數量錯誤_Load;
        }

        private void Dialog_盤點數量錯誤_Load(object sender, EventArgs e)
        {
            this.rJ_Button_是.MouseDownEventEx += RJ_Button_是_MouseDownEventEx;
            this.rJ_Button_否.MouseDownEventEx += RJ_Button_否_MouseDownEventEx;
            if (this.location.X != 0 && this.location.Y != 0)
            {
                this.StartPosition = FormStartPosition.WindowsDefaultLocation;
                base.Location = this.location;
            }

        }

        private void RJ_Button_否_MouseDownEventEx(MyUI.RJ_Button rJ_Button, MouseEventArgs mevent)
        {
            this.DialogResult = DialogResult.No;
        }
        private void RJ_Button_是_MouseDownEventEx(MyUI.RJ_Button rJ_Button, MouseEventArgs mevent)
        {
            this.DialogResult = DialogResult.Yes;
        }
    }
}
