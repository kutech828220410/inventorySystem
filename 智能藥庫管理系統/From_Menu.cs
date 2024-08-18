using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using SQLUI;
using MyUI;
using Basic;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using H_Pannel_lib;
using HIS_DB_Lib;
namespace 智能藥庫系統
{
    public partial class Main_Form : Form
    {
        private void Form_Menu_Init()
        {

            this.timer_盤點單管理.Tick += Timer_盤點單管理_Tick;
            this.rJ_Button_下拉選單_盤點單管理.MouseDownEvent += RJ_Button_下拉選單_盤點單管理_MouseDownEvent;
            this.rJ_Button_下拉選單_盤點單管理_State.MouseDownEvent += RJ_Button_下拉選單_盤點單管理_MouseDownEvent;
        }

        bool menuExpand_盤點單管理 = false;
        private void RJ_Button_下拉選單_盤點單管理_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate { this.timer_盤點單管理.Start(); }));
        }    
        private void Timer_盤點單管理_Tick(object sender, EventArgs e)
        {
            if (menuExpand_盤點單管理 == false)
            {
                flowLayoutPanel_盤點單管理.Height += 10;
                if (flowLayoutPanel_盤點單管理.Height == 210)
                {
                    timer_盤點單管理.Stop();
                    menuExpand_盤點單管理 = true;
                    rJ_Button_下拉選單_盤點單管理_State.BackgroundImage = global::智能藥庫系統.Properties.Resources._211690_up_arrow_icon;
                }

            }
            else
            {
                flowLayoutPanel_盤點單管理.Height -= 10;
                if (flowLayoutPanel_盤點單管理.Height <= 60)
                {
                    timer_盤點單管理.Stop();
                    menuExpand_盤點單管理 = false;
                    rJ_Button_下拉選單_盤點單管理_State.BackgroundImage = global::智能藥庫系統.Properties.Resources._211687_down_arrow_icon;

                }
            }
        }
    }
}
