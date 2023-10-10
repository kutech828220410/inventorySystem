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
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using H_Pannel_lib;
namespace 勤務傳送櫃
{

    public partial class Pannel_Box : UserControl
    {
        public delegate void EPDSettingEventHandler(string EPD_IP , string Name);
        public event EPDSettingEventHandler EPDSettingEvent;

        public static int AlarmTime = 5000;
        public static int AlarmBeepTime = 5000;
        public static int InitDelay = 2000;
        public delegate void AlarmEventHandler(string WardName, string Number, string Time);
        public delegate void CloseEventHandler(string WardName, string Number, string Time);
        public event AlarmEventHandler AlarmEvent;
        public event CloseEventHandler CloseEvent;

        public bool AlarmBeep = false;

        private RFID_UI rFID_UI;

        private PLC_Device pLC_Device_sensor_input = new PLC_Device();
        private PLC_Device pLC_Device_lock_input = new PLC_Device();
        private PLC_Device pLC_Device_output = new PLC_Device();
        private PLC_Device pLC_Device_LED_State = new PLC_Device();

        private int cnt_alarm_led_blink_time = 0;
        private MyTimer myTimer_InitDelay = new MyTimer();
        private MyTimer myTimer_alarm_time = new MyTimer();
        private MyTimer myTimer_sensor_input_time = new MyTimer();
        private MyTimer myTimer_opendoor_delayTime = new MyTimer();
        private MyTimer myTimer_alarm_led_blink_time = new MyTimer();
        private MyTimer myTimer_alarm_beep_time = new MyTimer();
        private int rFID_num = -1;
        [ReadOnly(false), Browsable(false), Category("Config"), Description(""), DefaultValue("")]
        public int RFID_num
        {
            get
            {
                return rFID_num;
            }
            set
            {
                rFID_num = value;
            }
        }
        private int lock_output_num = -1;
        [ReadOnly(false), Browsable(false), Category("Config"), Description(""), DefaultValue("")]
        public int Lock_output_num
        {
            get
            {
                return lock_output_num;
            }
            set
            {
                lock_output_num = value;
            }
        }
        private int lock_input_num = -1;
        [ReadOnly(false), Browsable(false), Category("Config"), Description(""), DefaultValue("")]
        public int Lock_input_num
        {
            get
            {
                return lock_input_num;
            }
            set
            {
                lock_input_num = value;
            }
        }
        private int sensor_input_num = -1;
        [ReadOnly(false), Browsable(false), Category("Config"), Description(""), DefaultValue("")]
        public int Sensor_input_num
        {
            get
            {
                return sensor_input_num;
            }
            set
            {
                sensor_input_num = value;
            }
        }
        private int led_output_num = -1;
        [ReadOnly(false), Browsable(false), Category("Config"), Description(""), DefaultValue("")]
        public int Led_output_num
        {
            get
            {
                return led_output_num;
            }
            set
            {
                led_output_num = value;
            }
        }

        private bool sQL_Write = false;
        [ReadOnly(false), Browsable(false), Category("Config"), Description(""), DefaultValue("")]
        public bool SQL_Write
        {
            get
            {
                return this.sQL_Write;
            }
            set
            {
                if (this.sQL_Write != value)
                {
                    this.sQL_Write = value;

                }
            }
        }
        private string iP = "";
        [ReadOnly(false), Browsable(false), Category("Config"), Description(""), DefaultValue("")]
        public string IP
        {
            get
            {
                return iP;
            }
            set
            {
                iP = value;
            }
        }
        private int port = 0;
        [ReadOnly(false), Browsable(false), Category("Config"), Description(""), DefaultValue("")]
        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
            }
        }

        private string number = "";
        [ReadOnly(false), Browsable(false), Category("Config"), Description(""), DefaultValue("")]
        public string Number
        {
            get
            {
                return this.number;
            }
            set
            {
                if (this.number != value)
                {
                    this.number = value;
                    this.label_編號.Text = value;

                }
            }
        }
        private string wardName = "";
        [ReadOnly(false), Browsable(false), Category("Config"), Description(""), DefaultValue("")]
        public string WardName
        {
            get
            {
                return this.wardName;
            }
            set
            {
                if (this.wardName != value)
                {
                    this.wardName = value;
                    this.label_病房名稱.Text = value;
                }        
            }
        }
        [ReadOnly(false), Browsable(false), Category("Config"), Description(""), DefaultValue("")]
        public Font WardFont
        {
            get
            {
                return this.label_病房名稱.Font;
            }
            set
            {
                if (this.label_病房名稱.Font != value)
                {
                    this.label_病房名稱.Font = value;
                }
            }
        }

        private bool mVisible = true;
        [ReadOnly(false), Browsable(false), Category("Config"), Description(""), DefaultValue("")]
        public bool MVisible
        {
            get
            {
                return this.mVisible;
            }
            set
            {
                this.Visible = value;
            }
        }

        private string ePD_IP = "";
        [ReadOnly(false), Browsable(false), Category("Config"), Description(""), DefaultValue("")]
        public string EPD_IP
        {
            get
            {
                return ePD_IP;
            }
            set
            {
                ePD_IP = value;
            }
        }

        public enum enum_door_state
        {
            Close,
            Open,
            Alarm,
            None,
        }
        private string _Sensor_Input_Adress = "";
        public string Sensor_Input_Adress
        {
            get
            {
                return this._Sensor_Input_Adress;
            }
            set
            {
                this._Sensor_Input_Adress = value;
            }
        }

        private string _Input_Adress = "";
        public string Input_Adress
        {
            get
            {
                return this._Input_Adress;
            }
            set
            {
                this._Input_Adress = value;
            }
        }
        private string _Output_Adress = "";
        public string Output_Adress
        {
            get
            {
                return this._Output_Adress;
            }
            set
            {
                this._Output_Adress = value;
            }
        }
        private string _Alarm_Time_Adress = "";
        public string Alarm_Time_Adress
        {
            get
            {
                return this._Alarm_Time_Adress;
            }
            set
            {
                this._Alarm_Time_Adress = value;
            }
        }
        private string _LED_Adress = "";
        public string LED_Adress
        {
            get
            {
                return this._LED_Adress;
            }
            set
            {
                this._LED_Adress = value;
            }
        }
        private string _LED_State_Adress = "";
        public string LED_State_Adress
        {
            get
            {
                return this._LED_State_Adress;
            }
            set
            {
                this._LED_State_Adress = value;
            }
        }
        private enum_door_state _enum_door_state = enum_door_state.None;
        public enum_door_state enum_Door_State
        {
            get
            {
                return this._enum_door_state;
            }
            set
            {
                if (this.IsHandleCreated)
                {

                    if (value != this._enum_door_state)
                    {
                        this.Invoke(new Action(delegate
                        {
                            this._enum_door_state = value;
                            if (value == enum_door_state.Open)
                            {
                                this.label_病房名稱.BackColor = Color.Yellow;
                            }
                            else if (value == enum_door_state.Close)
                            {
                                this.label_病房名稱.BackColor = Color.Pink;
                            }
                            else if (value == enum_door_state.Alarm)
                            {
                                this.label_病房名稱.BackColor = Color.Red;
                                cnt_alarm_led_blink_time = 0;
                            }
                        }));
                    }

                }
                else
                {
                    if (value != this._enum_door_state)
                    {

                        this.FindForm().Invoke(new Action(delegate
                        {
                            this._enum_door_state = value;
                            if (value == enum_door_state.Open)
                            {
                                this.label_病房名稱.BackColor = Color.Yellow;
                            }
                            else if (value == enum_door_state.Close)
                            {
                                this.label_病房名稱.BackColor = Color.Pink;
                            }
                            else if (value == enum_door_state.Alarm)
                            {
                                this.label_病房名稱.BackColor = Color.Red;
                                cnt_alarm_led_blink_time = 0;
                            }
                        }));

                    }
                }
       
                
            }
        }

        public void Init(int number , string input_Adress , string output_Adress, string alarm_time_Adress, string LED_Adress , string LED_State_Adress ,string Sensor_Input_Adress)
        {
            this.Number = number.ToString();
            this.Input_Adress = input_Adress;
            this.Output_Adress = output_Adress;
            this.Alarm_Time_Adress = alarm_time_Adress;
            this.LED_Adress = LED_Adress;
            this.LED_State_Adress = LED_State_Adress;
            this.Sensor_Input_Adress = Sensor_Input_Adress;
            this.pLC_Device_lock_input = new PLC_Device(this.Input_Adress);
            this.pLC_Device_output = new PLC_Device(this.Output_Adress);
            this.pLC_Device_LED_State = new PLC_Device(this.LED_State_Adress);
            this.pLC_Device_sensor_input = new PLC_Device(this.Sensor_Input_Adress);
            this.myTimer_alarm_time = new MyTimer(this.Alarm_Time_Adress);
            this.myTimer_sensor_input_time = new MyTimer();
        }
        public void Init(int number ,RFID_UI rFID_UI)
        {
            this.Number = number.ToString();
            this.rFID_UI = rFID_UI;
        }
        bool flag_alarm_is_active = true;
        bool flag_Init = false;
        bool flag_LED_State = false;
        bool flag_sensor_State = false;
        bool flag_lockinput_State = false;
        public void Run()
        {
            if (this.MVisible == false)
            {
                return;
            }
            string jsonstring = this.rFID_UI.GetUDPJsonString(IP);
            if (jsonstring.StringIsEmpty())
            {
                return;
            }
            if (!flag_Init)
            {
                this.myTimer_alarm_time.StartTickTime(AlarmTime);
                this.myTimer_InitDelay.StartTickTime(InitDelay);
                if (this.myTimer_InitDelay.IsTimeOut())
                {
                    flag_Init = true;
                }
                return;
            }
         
            if (this.pLC_Device_output.Bool == true)
            {
                this.rFID_UI.Set_OutputPINTrigger(IP, Port, this.lock_output_num, true);
                this.pLC_Device_output.Bool = false;
            }

            this.pLC_Device_LED_State.Bool = this.rFID_UI.GetOutput(this.IP, this.led_output_num);
            this.pLC_Device_lock_input.Bool = this.rFID_UI.GetInput(this.IP, this.lock_input_num);
            this.pLC_Device_sensor_input.Bool = this.rFID_UI.GetInput(this.IP, this.sensor_input_num);

            if (this.pLC_Device_LED_State.Bool != this.flag_LED_State)
            {
                this.flag_LED_State = this.pLC_Device_LED_State.Bool;
             
                this.Invoke(new Action(delegate
                {
                    if (this.flag_LED_State)
                    {
                        this.label_編號.BackColor = Color.Lime;
                    }
                    else
                    {
                        this.label_編號.BackColor = Color.White;
                    }
                }));
               
            }

            if (this.pLC_Device_sensor_input.Bool != this.flag_sensor_State)
            {
                this.flag_sensor_State = this.pLC_Device_sensor_input.Bool;
                if (this.flag_sensor_State)
                {
                    bool flag = this.rFID_UI.GetOutput(this.IP, this.Led_output_num);
                    if (!flag) this.rFID_UI.Set_OutputPIN(IP, Port, this.Led_output_num, true);

                    this.Invoke(new Action(delegate
                    {
                        this.label_sensor.BackColor = Color.Lime;
                    }));
                }
                else
                {
                    this.Invoke(new Action(delegate
                    {
                        this.label_sensor.BackColor = Color.White;
                    }));
                }
            }

            if(this.pLC_Device_lock_input.Bool != flag_lockinput_State)
            {
                this.flag_lockinput_State = this.pLC_Device_lock_input.Bool;
                if (this.flag_lockinput_State)
                {
                    bool flag = this.rFID_UI.GetOutput(this.IP, this.Led_output_num);
                    if (flag) this.rFID_UI.Set_OutputPIN(IP, Port, this.Led_output_num, false);
                }
                else
                {
                    bool flag = this.rFID_UI.GetOutput(this.IP, this.Led_output_num);
                    if (!flag) this.rFID_UI.Set_OutputPIN(IP, Port, this.Led_output_num, true);

                }
            }

  


            if (!this.pLC_Device_lock_input.Bool)
            {
                if(_enum_door_state != enum_door_state.Alarm)
                {
                    this.enum_Door_State = enum_door_state.Open;
                }
                
            }
            else
            {
                if(this.enum_Door_State != enum_door_state.Close)
                {
                    if (this.CloseEvent != null && flag_Init) this.CloseEvent(this.WardName, this.Number, DateTime.Now.ToDateTimeString_6());
                }
                this.enum_Door_State = enum_door_state.Close;
            }

            if (this.enum_Door_State == enum_door_state.Open)
            {
              
                if (this.myTimer_alarm_time.IsTimeOut())
                {
                    if(flag_alarm_is_active)
                    {
                        enum_Door_State = enum_door_state.Alarm;
                        this.myTimer_alarm_beep_time.TickStop();
                        this.myTimer_alarm_beep_time.StartTickTime(AlarmBeepTime);

                        if (this.AlarmEvent != null && flag_Init) this.AlarmEvent(this.WardName, this.Number, DateTime.Now.ToDateTimeString_6());

                        flag_alarm_is_active = false;
                    }
              
                }
            }
            else if (this.enum_Door_State == enum_door_state.Close)
            {
                if (pLC_Device_sensor_input.Bool)
                {
                    if (myTimer_sensor_input_time.IsTimeOut())
                    {
                        this.myTimer_sensor_input_time.TickStop();
                        this.myTimer_sensor_input_time.StartTickTime(200);
                    }
                }
                else
                {
                    this.myTimer_sensor_input_time.TickStop();
                    this.myTimer_sensor_input_time.StartTickTime(200);
                }

                this.myTimer_alarm_time.TickStop();
                this.myTimer_alarm_time.StartTickTime(AlarmTime);
                flag_alarm_is_active = true;
            }
            else if (this.enum_Door_State == enum_door_state.Alarm)
            {
           
                if (cnt_alarm_led_blink_time == 0)
                {
                    this.rFID_UI.Set_OutputPIN(IP, Port, this.Led_output_num, true);
                    this.myTimer_alarm_led_blink_time.TickStop();
                    this.myTimer_alarm_led_blink_time.StartTickTime(500);
                    cnt_alarm_led_blink_time++;
                }
                if (cnt_alarm_led_blink_time == 1)
                {
                    if (this.myTimer_alarm_led_blink_time.IsTimeOut())
                    {
                        this.rFID_UI.Set_OutputPIN(IP, Port, this.Led_output_num, false);
                        this.myTimer_alarm_led_blink_time.TickStop();
                        this.myTimer_alarm_led_blink_time.StartTickTime(500);
                        cnt_alarm_led_blink_time++;
                    }
                }
                if (cnt_alarm_led_blink_time == 2)
                {
                    if (this.myTimer_alarm_led_blink_time.IsTimeOut())
                    {
                        cnt_alarm_led_blink_time++;
                    }
                }
                if (cnt_alarm_led_blink_time == 3)
                {
                    cnt_alarm_led_blink_time = 0;
                }
            }
            if (this.enum_Door_State == enum_door_state.Alarm && !this.myTimer_alarm_beep_time.IsTimeOut())
            {
                AlarmBeep = true;
            }
            else
            {
                AlarmBeep = false;
            }
            flag_Init = true;

        }
        public void Open()
        {
            if (myTimer_opendoor_delayTime.IsTimeOut())
            {
                this.pLC_Device_output.Bool = true;
                myTimer_opendoor_delayTime.StartTickTime(500);
            }
    
        }
        public bool IsOpen()
        {
            if (!myTimer_opendoor_delayTime.IsTimeOut()) return true;
            if (this.pLC_Device_output.Bool) return true;
            return (!pLC_Device_lock_input.Bool);
        }
        public Pannel_Box()
        {
            InitializeComponent();
        }

        private void 開門ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Open();
        }
        private void 更改病房名稱ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog_更改病房名稱 form = Dialog_更改病房名稱.GetForm(this.WardName);
            form.ShowDialog();
            if(form.Enum_Type == Dialog_更改病房名稱.enum_Type.OK)
            {
                RFIDClass rFIDClass = this.rFID_UI.SQL_GetRFIDClass(this.IP);
                if (rFIDClass == null) return;
                if (this.rFID_num >= rFIDClass.DeviceClasses.Length) return;
                rFIDClass.DeviceClasses[this.rFID_num].Name = form.修改名稱;
                this.rFID_UI.SQL_ReplaceRFIDClass(rFIDClass);
                this.WardName = form.修改名稱;
               
            }

        }
        private void 更改字體ToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            RFIDClass rFIDClass = this.rFID_UI.SQL_GetRFIDClass(this.IP);
            if (rFIDClass == null) return;
            if (this.rFID_num >= rFIDClass.DeviceClasses.Length) return;
            this.fontDialog.Font = rFIDClass.DeviceClasses[this.rFID_num].Name_font;

            if (this.fontDialog.ShowDialog() == DialogResult.OK)
            {
                rFIDClass.DeviceClasses[this.rFID_num].Name_font = this.fontDialog.Font;
                this.rFID_UI.SQL_ReplaceRFIDClass(rFIDClass);
                this.WardFont = this.fontDialog.Font;
            }
        }
        private void label_病房名稱_Click(object sender, EventArgs e)
        {
            bool flag = this.rFID_UI.GetOutput(this.IP, this.Led_output_num);
            this.rFID_UI.Set_OutputPIN(IP, Port, this.Led_output_num, !flag);
        }
        private void 電子紙設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EPDSettingEvent != null) EPDSettingEvent(EPD_IP , WardName);
        }
    }
    static class Pannel_Box_Method
    {
        static public Pannel_Box SortByOutput(this List<Pannel_Box> list_Pannel_Box, string Output_Adress)
        {
            List<Pannel_Box> list_Pannel_Box_buf = (from value in list_Pannel_Box
                                                    where value.Output_Adress == Output_Adress
                                                    select value).ToList();
            if (list_Pannel_Box_buf.Count == 0) return null;
            return list_Pannel_Box_buf[0];
        }
        static public Pannel_Box SortByRFID(this List<Pannel_Box> list_Pannel_Box, string IP, int RFID_Num)
        {
            List<Pannel_Box> list_Pannel_Box_buf = (from value in list_Pannel_Box
                                                    where value.IP == IP
                                                    where value.RFID_num == RFID_Num
                                                    select value).ToList();
            if (list_Pannel_Box_buf.Count == 0) return null;
            return list_Pannel_Box_buf[0];
        }
    }
}
