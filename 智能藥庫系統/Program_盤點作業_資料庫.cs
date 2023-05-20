using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using SQLUI;
using MyUI;
using Basic;
using H_Pannel_lib;
namespace 智能藥庫系統
{

    public partial class Form1 : Form
    {
        private void sub_Program_盤點作業_資料庫_Init()
        {
            this.sqL_DataGridView_盤點單號.Init();
            if (this.sqL_DataGridView_盤點單號.SQL_IsTableCreat() == false)
            {
                this.sqL_DataGridView_盤點單號.SQL_CreateTable();
            }
            else
            {
                this.sqL_DataGridView_盤點單號.SQL_CheckAllColumnName(true);
            }
            this.sqL_DataGridView_盤點內容.Init();
            if (this.sqL_DataGridView_盤點內容.SQL_IsTableCreat() == false)
            {
                this.sqL_DataGridView_盤點內容.SQL_CreateTable();
            }
            else
            {
                this.sqL_DataGridView_盤點內容.SQL_CheckAllColumnName(true);
            }
            this.sqL_DataGridView_盤點明細.Init();
            if (this.sqL_DataGridView_盤點明細.SQL_IsTableCreat() == false)
            {
                this.sqL_DataGridView_盤點明細.SQL_CreateTable();
            }
            else
            {
                this.sqL_DataGridView_盤點明細.SQL_CheckAllColumnName(true);
            }

            this.sqL_DataGridView_盤點單號.MouseDown += SqL_DataGridView_盤點單號_MouseDown;
            this.sqL_DataGridView_盤點內容.MouseDown += SqL_DataGridView_盤點內容_MouseDown;
            this.sqL_DataGridView_盤點明細.MouseDown += SqL_DataGridView_盤點明細_MouseDown;

        }

        private void SqL_DataGridView_盤點內容_MouseDown(object sender, MouseEventArgs e)
        {
            this.sqL_DataGridView_盤點內容.SQL_GetAllRows(true);
        }

        private void SqL_DataGridView_盤點單號_MouseDown(object sender, MouseEventArgs e)
        {
            this.sqL_DataGridView_盤點單號.SQL_GetAllRows(true);
        }
        private void SqL_DataGridView_盤點明細_MouseDown(object sender, MouseEventArgs e)
        {
            this.sqL_DataGridView_盤點明細.SQL_GetAllRows(true);
        }
    }
}
