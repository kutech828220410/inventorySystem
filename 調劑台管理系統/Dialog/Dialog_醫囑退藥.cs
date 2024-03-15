﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
using MyUI;
using HIS_DB_Lib;
using SQLUI;
namespace 調劑台管理系統
{
    public partial class Dialog_醫令退藥 : MyDialog
    {
        private List<object[]> list_醫令資料_buf = new List<object[]>();
        private SQL_DataGridView _sqL_DataGridView_醫令資料;
  
        private object[] value;
        public object[] Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }
        public Dialog_醫令退藥(List<object[]> list_醫令資料, SQL_DataGridView sqL_DataGridView_醫令資料)
        {
            if (form == null)
            {
                InitializeComponent();
            }
            else
            {
                form.Invoke(new Action(delegate
                {
                    InitializeComponent();
                }));
            }
            this.list_醫令資料_buf = list_醫令資料;
            this._sqL_DataGridView_醫令資料 = sqL_DataGridView_醫令資料;
            //InitializeComponent();
        }

        private void Dialog_醫令退藥_Load(object sender, EventArgs e)
        {
            this.rJ_Button_退出.MouseDownEvent += RJ_Button_退出_MouseDownEvent;
            this.rJ_Button_刷新.MouseDownEvent += RJ_Button_刷新_MouseDownEvent;
            this.sqL_DataGridView_醫令資料.Init(_sqL_DataGridView_醫令資料);
            this.sqL_DataGridView_醫令資料.RowDoubleClickEvent += SqL_DataGridView_醫令資料_RowDoubleClickEvent;
            this.sqL_DataGridView_醫令資料.DataGridRefreshEvent += SqL_DataGridView_醫令資料_DataGridRefreshEvent;
            this.sqL_DataGridView_醫令資料.RefreshGrid(this.list_醫令資料_buf);
        }

        private void SqL_DataGridView_醫令資料_DataGridRefreshEvent()
        {

        }

        private void RJ_Button_刷新_MouseDownEvent(MouseEventArgs mevent)
        {
            this.sqL_DataGridView_醫令資料.RefreshGrid(this.list_醫令資料_buf);
        }


        private void SqL_DataGridView_醫令資料_RowDoubleClickEvent(object[] RowValue)
        {
            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
            int num = dialog_NumPannel.Value;
            if (num == 0)
            {
                if (MyMessageBox.ShowDialog("退藥數量為（0）,確認進行作業?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;
            }
            RowValue[(int)enum_醫囑資料.交易量] = num;
            this.value = RowValue;
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void RJ_Button_退出_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }));
        }


    }
}
