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

namespace 調劑台管理系統
{
    public partial class Dialog_調劑作業_調出 : MyDialog
    {


        public Dialog_調劑作業_調出()
        {
            form.Invoke(new Action(delegate 
            {
                InitializeComponent();

                this.Load += Dialog_調劑作業_調出_Load;
                this.LoadFinishedEvent += Dialog_調劑作業_調出_LoadFinishedEvent;
                this.rJ_Button_確認.MouseDownEvent += RJ_Button_確認_MouseDownEvent;
                this.rJ_Button_取消.MouseDownEvent += RJ_Button_取消_MouseDownEvent;
            }));
                    
        }

        private void Dialog_調劑作業_調出_LoadFinishedEvent(EventArgs e)
        {
            comboBox_目的調劑台名稱.SelectedIndex = 0;
            comboBox_庫儲藥品_搜尋條件.SelectedIndex = 0;
            comboBox_目的調劑台名稱.Refresh();


        }

        private void Dialog_調劑作業_調出_Load(object sender, EventArgs e)
        {
            rJ_Lable_來源調劑台名稱.Text = Main_Form.ServerName;
            List<ServerSettingClass> serverSettingClasses = ServerSettingClass.get_serversetting_by_type(Main_Form.API_Server, "調劑台");
            List<string> serverNames = (from temp in serverSettingClasses
                                        select temp.設備名稱).Distinct().ToList();
            serverNames.Remove(Main_Form.ServerName);

            comboBox_目的調劑台名稱.DataSource = serverNames;
        }

        private void RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            DialogResult = DialogResult.Yes;
            this.Close();
        }
        private void RJ_Button_取消_MouseDownEvent(MouseEventArgs mevent)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
