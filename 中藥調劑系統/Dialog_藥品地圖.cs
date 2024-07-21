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

namespace 中藥調劑系統
{
    public partial class Dialog_藥品地圖 : MyDialog
    {
        private List<string> list_正面左上 = new List<string>();
        private List<string> list_正面中上 = new List<string>();
        private List<string> list_正面右上 = new List<string>();

        private List<string> list_正面左下 = new List<string>();
        private List<string> list_正面中下 = new List<string>();
        private List<string> list_正面右下 = new List<string>();

        private List<string> list_背面左上 = new List<string>();
        private List<string> list_背面中上 = new List<string>();
        private List<string> list_背面右上 = new List<string>();

        private List<string> list_背面左下 = new List<string>();
        private List<string> list_背面中下 = new List<string>();
        private List<string> list_背面右下 = new List<string>();


        private List<string> list_庫存B = new List<string>();

        private List<string> list_庫存C_左 = new List<string>();
        private List<string> list_庫存C_中 = new List<string>();
        private List<string> list_庫存C_右 = new List<string>();

        private List<string> list_外用 = new List<string>();

        private List<object> devices = null;
        private medClass medClass = null;

        private MyThread myThread;

        public Dialog_藥品地圖(medClass medClass)
        {
            InitializeComponent();
            this.medClass = medClass;
            this.LoadFinishedEvent += Dialog_藥品地圖_LoadFinishedEvent;
            this.FormClosing += Dialog_藥品地圖_FormClosing;
        }

     
        private void Dialog_藥品地圖_LoadFinishedEvent(EventArgs e)
        {
            LoadingForm.ShowLoadingForm();
            devices = Main_Form.Function_從本地資料取得儲位(this.medClass.藥品碼, true); 
            rJ_Lable_藥名.Text = $"({medClass.藥品碼}){medClass.藥品名稱}";
            LoadingForm.CloseLoadingForm();
            #region 正面
            list_正面左上.Add("192.168.40.100");
            list_正面左上.Add("192.168.40.101");
            list_正面左上.Add("192.168.40.102");
            list_正面左上.Add("192.168.40.103");
            list_正面左上.Add("192.168.40.104");

            list_正面左下.Add("192.168.40.105");
            list_正面左下.Add("192.168.40.106");
            list_正面左下.Add("192.168.40.107");
            list_正面左下.Add("192.168.40.108");

            list_正面中上.Add("192.168.40.109");
            list_正面中上.Add("192.168.40.110");
            list_正面中上.Add("192.168.40.111");
            list_正面中上.Add("192.168.40.112");
            list_正面中上.Add("192.168.40.113");

            list_正面中下.Add("192.168.40.114");
            list_正面中下.Add("192.168.40.115");
            list_正面中下.Add("192.168.40.116");
            list_正面中下.Add("192.168.40.117");

            list_正面右上.Add("192.168.40.118");
            list_正面右上.Add("192.168.40.119");
            list_正面右上.Add("192.168.40.120");
            list_正面右上.Add("192.168.40.121");
            list_正面右上.Add("192.168.40.122");

            list_正面右下.Add("192.168.40.123");
            list_正面右下.Add("192.168.40.124");
            list_正面右下.Add("192.168.40.125");
            list_正面右下.Add("192.168.40.126");
            #endregion
            #region 背面
            list_背面左上.Add("192.168.40.127");
            list_背面左上.Add("192.168.40.128");
            list_背面左上.Add("192.168.40.129");
            list_背面左上.Add("192.168.40.130");
            list_背面左上.Add("192.168.40.131");

            list_背面左下.Add("192.168.40.132");
            list_背面左下.Add("192.168.40.133");
            list_背面左下.Add("192.168.40.134");

            list_背面中上.Add("192.168.40.135");
            list_背面中上.Add("192.168.40.136");
            list_背面中上.Add("192.168.40.137");
            list_背面中上.Add("192.168.40.138");
            list_背面中上.Add("192.168.40.139");

            list_背面中下.Add("192.168.40.140");
            list_背面中下.Add("192.168.40.141");
            list_背面中下.Add("192.168.40.142");

            list_背面右上.Add("192.168.40.143");
            list_背面右上.Add("192.168.40.144");
            list_背面右上.Add("192.168.40.145");
            list_背面右上.Add("192.168.40.146");
            list_背面右上.Add("192.168.40.147");

            list_背面右下.Add("192.168.40.148");
            list_背面右下.Add("192.168.40.149");
            list_背面右下.Add("192.168.40.150");
            #endregion
            #region 庫存B
            list_庫存B.Add("192.168.41.100");
            list_庫存B.Add("192.168.41.101");
            list_庫存B.Add("192.168.41.102");
            list_庫存B.Add("192.168.41.103");
            list_庫存B.Add("192.168.41.104");
            list_庫存B.Add("192.168.41.105");
            list_庫存B.Add("192.168.41.106");
            list_庫存B.Add("192.168.41.107");
            list_庫存B.Add("192.168.41.108");
            #endregion

            #region 庫存C
            list_庫存C_左.Add("192.168.41.109");
            list_庫存C_左.Add("192.168.41.110");
            list_庫存C_左.Add("192.168.41.111");
            list_庫存C_左.Add("192.168.41.112");
            list_庫存C_左.Add("192.168.41.113");
            list_庫存C_左.Add("192.168.41.114");

            list_庫存C_中.Add("192.168.41.115");
            list_庫存C_中.Add("192.168.41.116");
            list_庫存C_中.Add("192.168.41.117");
            list_庫存C_中.Add("192.168.41.118");
            list_庫存C_中.Add("192.168.41.119");
            list_庫存C_中.Add("192.168.41.120");

            list_庫存C_右.Add("192.168.41.121");
            list_庫存C_右.Add("192.168.41.122");
            list_庫存C_右.Add("192.168.41.123");
            list_庫存C_右.Add("192.168.41.124");
            list_庫存C_右.Add("192.168.41.125");
            list_庫存C_右.Add("192.168.41.126");

            #endregion
            #region 外用
            list_外用.Add("192.168.42.100");
            list_外用.Add("192.168.42.101");
            #endregion
            myThread = new MyThread();
            myThread.AutoRun(true);
            myThread.SetSleepTime(500);
            myThread.Add_Method(sub_program);
        }
        bool flag_ON_OFF_Light = false;
        private void sub_program()
        {
            this.Invoke(new Action(delegate 
            {
                if(medClass != null)
                {
                    Color color = Color.GreenYellow;
                    if(flag_ON_OFF_Light == true)
                    {
                        color = Color.Green;
                    }
                    flag_ON_OFF_Light = !flag_ON_OFF_Light;
                    for (int i = 0; i < devices.Count; i++)
                    {
                        Device device = (devices[i]) as Device;
                        if (device != null)
                        {
                            string IP = device.IP;

                            List<string> vs = new List<string>();

                            #region 正面
                            vs = (from temp in list_正面左上
                                  where temp == IP
                                  select temp).ToList();

                            if (vs.Count > 0) panel_正面左上.BackColor = color;

                            vs = (from temp in list_正面中上
                                  where temp == IP
                                  select temp).ToList();

                            if (vs.Count > 0) panel_正面中上.BackColor = color;

                            vs = (from temp in list_正面右上
                                  where temp == IP
                                  select temp).ToList();


                            vs = (from temp in list_正面左下
                                  where temp == IP
                                  select temp).ToList();

                            if (vs.Count > 0) panel_正面左下.BackColor = color;

                            vs = (from temp in list_正面中下
                                  where temp == IP
                                  select temp).ToList();

                            if (vs.Count > 0) panel_正面中下.BackColor = color;

                            vs = (from temp in list_正面右下
                                  where temp == IP
                                  select temp).ToList();


                            if (vs.Count > 0) panel_正面右上.BackColor = color;
                            #endregion
                            #region 背面
                            vs = (from temp in list_背面左上
                                  where temp == IP
                                  select temp).ToList();

                            if (vs.Count > 0) panel_背面左上.BackColor = color;

                            vs = (from temp in list_背面中上
                                  where temp == IP
                                  select temp).ToList();

                            if (vs.Count > 0) panel_背面中上.BackColor = color;

                            vs = (from temp in list_背面右上
                                  where temp == IP
                                  select temp).ToList();


                            vs = (from temp in list_背面左下
                                  where temp == IP
                                  select temp).ToList();

                            if (vs.Count > 0) panel_背面左下.BackColor = color;

                            vs = (from temp in list_背面中下
                                  where temp == IP
                                  select temp).ToList();

                            if (vs.Count > 0) panel_背面中下.BackColor = color;

                            vs = (from temp in list_背面右下
                                  where temp == IP
                                  select temp).ToList();

                            if (vs.Count > 0) panel_背面右上.BackColor = color;
                            #endregion
                            #region 庫存B
                            vs = (from temp in list_庫存B
                                  where temp == IP
                                  select temp).ToList();
                            if (vs.Count > 0) panel_庫存B.BackColor = color;
                            #endregion
                            #region 庫存C
                            vs = (from temp in list_庫存C_左
                                  where temp == IP
                                  select temp).ToList();
                            if (vs.Count > 0) panel_庫存C_左.BackColor = color;

                            vs = (from temp in list_庫存C_中
                                  where temp == IP
                                  select temp).ToList();
                            if (vs.Count > 0) panel_庫存C_中.BackColor = color;

                            vs = (from temp in list_庫存C_右
                                  where temp == IP
                                  select temp).ToList();
                            if (vs.Count > 0) panel_庫存C_右.BackColor = color;
                            #endregion
                            #region 外用
                            vs = (from temp in list_外用
                                  where temp == IP
                                  select temp).ToList();
                            if (vs.Count > 0) panel_外用.BackColor = color;
                            #endregion
                            #region 冰箱
                            if (Function_檢查IP範圍(IP, 42, 10, 84)) panel_冰箱.BackColor = color;
                            #endregion
                            #region 庫存C_下
                            if (Function_檢查IP範圍(IP, 41, 10, 57)) panel_庫存C_下.BackColor = color;
                            #endregion
                            #region 飲片
                            if (Function_檢查IP範圍(IP, 40, 10, 84))
                            {
                                panel_飲片正面_右.BackColor = color;
                                panel_飲片正面_中.BackColor = color;
                                panel_飲片正面_右.BackColor = color;

                                panel_飲片背面_右.BackColor = color;
                                panel_飲片背面_中.BackColor = color;
                                panel_飲片背面_右.BackColor = color;

                                panel_飲片吊櫃_1.BackColor = color;
                                panel_飲片吊櫃_2.BackColor = color;
                                panel_飲片吊櫃_3.BackColor = color;
                                panel_飲片吊櫃_4.BackColor = color;
                            }
                            #endregion
                        }

                    }
                }
            }));
        }
            
        private void Dialog_藥品地圖_FormClosing(object sender, FormClosingEventArgs e)
        {
            myThread.Abort();
            myThread = null;
        }
        private bool Function_檢查IP範圍(string IP, int 網段, int 起始IP, int 結束IP)
        {
            string[] IP_Array = IP.Split('.');
            if (IP_Array.Length != 4) return false;

            if(IP_Array[2].StringToInt32() == 網段)
            {
                int temp = IP_Array[3].StringToInt32();

                if (temp >= 起始IP && temp <= 結束IP)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
