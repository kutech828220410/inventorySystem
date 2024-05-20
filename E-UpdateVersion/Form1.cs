﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Ionic.Zip;
using System.Text.Json.Serialization;
using Basic;
using IWshRuntimeLibrary;
using HIS_DB_Lib;
[assembly: AssemblyVersion("1.0.14.0")]
[assembly: AssemblyFileVersion("1.0.14.0")]
namespace E_UpdateVersion
{
    public partial class Form1 : Form
    {
        public computerConfigClass computerConfigClass = new computerConfigClass();
        public static string DeviceName = "";
        public static string ApiServer
        {
            get
            {
               return myConfigClass.Api_server;
            }
        }
        static string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        #region MyConfigClass
        private const string MyConfigFileName = @"config.txt";
        public static  MyConfigClass myConfigClass = new MyConfigClass();
        public class MyConfigClass
        {
            private string default_program = "";
            public string Default_program { get => default_program; set => default_program = value; }

            private string api_server = "";
            public string Api_server { get => api_server; set => api_server = value; }
  
        }
        public static bool LoadMyConfig()
        {
            string jsonstr = MyFileStream.LoadFileAllText($".//{MyConfigFileName}");
            myConfigClass = Basic.Net.JsonDeserializet<MyConfigClass>(jsonstr);
            if(myConfigClass == null)
            {
                myConfigClass = new MyConfigClass();
            }
            string json = Net.WEBApiGet($"{ApiServer}/api/test");
            if (json.StringIsEmpty() == true)
            {
                Dialog_SetApiServer dialog_SetApiServer = new Dialog_SetApiServer();
                if(dialog_SetApiServer.ShowDialog() != DialogResult.Yes)
                {
                    Application.Exit();
                    return false;
                }
                myConfigClass.Api_server = dialog_SetApiServer.Value;
                return true;
                //MyFileStream.SaveFile($"{MyConfigFileName}", myConfigClass.JsonSerializationt(true));
            }
            else
            {
                return true;
            }
        }
        public static void SaveConfig()
        {
            string jsonstr = Basic.Net.JsonSerializationt<MyConfigClass>(myConfigClass, true);
            List<string> list_jsonstring = new List<string>();
            list_jsonstring.Add(jsonstr);
            if (!MyFileStream.SaveFile($".//{MyConfigFileName}", list_jsonstring))
            {
                MyMessageBox.ShowDialog($"建立{MyConfigFileName}檔案失敗!");
                return;
            }
        }
        #endregion
    
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MyMessageBox.form = this.FindForm();
            MyMessageBox.音效 = false;
            Dialog_login.form = this.FindForm();
            Dialog_SetApiServer.form = this.FindForm();
            Dialog_ConfigSetting.form = this.FindForm();
            if (LoadMyConfig() == false)
            {
                this.Close();
                return;
            }
            string update_version = GetVersion("update");
            if (update_version.StringIsEmpty() == false && update_version != this.ProductVersion)
            {
                Download("update", "update", "", false);
                MyFileStream.RunFile($@"{currentDirectory}", $@"{currentDirectory}\update", $@"{currentDirectory}\temp", @"E-UpdateVersion.exe", SearchOption.TopDirectoryOnly, "config.txt");
                this.Close();
                return;
            }
        
            this.label_version.Text = $"Ver {this.ProductVersion}";
            this.label_info.Text = Basic.LicenseLib.GetComputerInfo();
            DeviceName = this.label_info.Text;
            
            computerConfigClass = computerConfigClass.DownloadConfig(ApiServer, DeviceName);
            if(computerConfigClass.Parameters.Count == 0)
            {
                Dialog_ConfigSetting dialog_ConfigSetting = new Dialog_ConfigSetting(ApiServer, DeviceName);
                dialog_ConfigSetting.ShowDialog();
            }

            this.SetUI();
            this.rJ_Button_離開.MouseDownEvent += RJ_Button_離開_MouseDownEvent;
            this.rJ_Button_智慧調劑台系統.MouseDownEvent += RJ_Button_智慧調劑台系統_MouseDownEvent;
            this.rJ_Button_智能藥庫系統.MouseDownEvent += RJ_Button_智能藥庫系統_MouseDownEvent;
            this.rJ_Button_中心叫號系統.MouseDownEvent += RJ_Button_中心叫號系統_MouseDownEvent;
            this.rJ_Button_勤務傳送系統.MouseDownEvent += RJ_Button_勤務傳送系統_MouseDownEvent;
            this.rJ_Button_癌症備藥機.MouseDownEvent += RJ_Button_癌症備藥機_MouseDownEvent;
            this.rJ_Button_中藥調劑系統.MouseDownEvent += RJ_Button_中藥調劑系統_MouseDownEvent;


            
        }

        #region Event
        private void 後台設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog_login dialog_Login = new Dialog_login();
            if (dialog_Login.ShowDialog() != DialogResult.Yes) return;
            Dialog_ConfigSetting dialog_ConfigSetting = new Dialog_ConfigSetting(ApiServer, DeviceName);
            dialog_ConfigSetting.ShowDialog();
            computerConfigClass = computerConfigClass.DownloadConfig(ApiServer, DeviceName);
            this.SetUI();
        }
        private void RJ_Button_智慧調劑台系統_MouseDownEvent(MouseEventArgs mevent)
        {
            string 調劑台名稱 = computerConfigClass.GetValue("調劑台管理系統", "系統名稱");
            string 控制中心 = computerConfigClass.GetValue("調劑台管理系統", "控制中心");
            if (調劑台名稱.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("指定調劑台名稱空白,請聯繫管理員至後台設定!");
            }
            string arguments = $"{ApiServer} {調劑台名稱} {控制中心}";
            if (Download("調劑台", "調劑台管理系統", arguments) == false)
            {
                MyMessageBox.ShowDialog("取得更新資訊失敗!");
            }
        }
        private void RJ_Button_智能藥庫系統_MouseDownEvent(MouseEventArgs mevent)
        {
            string 調劑台名稱 = computerConfigClass.GetValue("智能藥庫系統", "系統名稱");
            string 控制中心 = computerConfigClass.GetValue("智能藥庫系統", "控制中心");
            if (調劑台名稱.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("指定藥庫名稱空白,請聯繫管理員至後台設定!");
            }
            string arguments = $"{ApiServer} {調劑台名稱} {控制中心}";
            if (Download("藥庫", "智能藥庫系統", arguments) == false)
            {
                MyMessageBox.ShowDialog("取得更新資訊失敗!");
            }
        }
        private void RJ_Button_中心叫號系統_MouseDownEvent(MouseEventArgs mevent)
        {
            string 名稱 = computerConfigClass.GetValue("中心叫號系統", "系統名稱");
            string 控制中心 = computerConfigClass.GetValue("中心叫號系統", "控制中心");
            if (名稱.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("指定中心叫號系統名稱空白,請聯繫管理員至後台設定!");
            }
            string arguments = $"{ApiServer} {名稱} {控制中心}";
            if (Download("中心叫號系統", "Hospital_Call_Light_System", arguments) == false)
            {
                MyMessageBox.ShowDialog("取得更新資訊失敗!");
            }
        }
        private void RJ_Button_勤務傳送系統_MouseDownEvent(MouseEventArgs mevent)
        {
            string 調劑台名稱 = computerConfigClass.GetValue("勤務傳送系統", "系統名稱");
            string 控制中心 = computerConfigClass.GetValue("勤務傳送系統", "控制中心");
            if (調劑台名稱.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("指定勤務傳送名稱空白,請聯繫管理員至後台設定!");
            }
            string arguments = $"{ApiServer} {調劑台名稱} {控制中心}";
            if (Download("傳送櫃", "勤務傳送系統", arguments) == false)
            {
                MyMessageBox.ShowDialog("取得更新資訊失敗!");
            }
        }
        private void RJ_Button_癌症備藥機_MouseDownEvent(MouseEventArgs mevent)
        {
            string 系統名稱 = computerConfigClass.GetValue("癌症備藥機", "系統名稱");
            string 控制中心 = computerConfigClass.GetValue("癌症備藥機", "控制中心");
            if (系統名稱.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("指定[癌症備藥機]名稱空白,請聯繫管理員至後台設定!");
            }
            string arguments = $"{ApiServer} {系統名稱} {控制中心}";
            if (Download("癌症備藥機", "癌症備藥機", arguments) == false)
            {
                MyMessageBox.ShowDialog("取得更新資訊失敗!");
            }
        }
        private void RJ_Button_中藥調劑系統_MouseDownEvent(MouseEventArgs mevent)
        {
            string 系統名稱 = computerConfigClass.GetValue("中藥調劑系統", "系統名稱");
            string 控制中心 = computerConfigClass.GetValue("中藥調劑系統", "控制中心");
            if (系統名稱.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("指定[中藥調劑系統]名稱空白,請聯繫管理員至後台設定!");
            }
            string arguments = $"{ApiServer} {系統名稱} {控制中心}";
            if (Download("中藥調劑系統", "中藥調劑系統", arguments) == false)
            {
                MyMessageBox.ShowDialog("取得更新資訊失敗!");
            }
        }
        private void RJ_Button_離開_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.Close();
            }));
        }
        #endregion
        #region Function
        private void SetUI()
        {
            List<ServerSettingClass> serverSettingClasses = ServerSettingClassMethod.WebApiGet($"{ApiServer}/api/serversetting");
            #region 調劑台管理系統
            ServerSettingClass serverSettingClass_調劑台 = serverSettingClasses.myFind("", "調劑台", "智慧藥局整合平台不顯示");
            if (serverSettingClass_調劑台 != null) rJ_Button_智慧調劑台系統.Visible = !(serverSettingClass_調劑台.Value == true.ToString());
            else rJ_Button_智慧調劑台系統.Visible = true;
            if ((computerConfigClass.GetValue("調劑台管理系統", "程式致能") == true.ToString()))
            {
                rJ_Button_智慧調劑台系統.Enabled = true;
                if (myConfigClass.Default_program == "調劑台管理系統")
                {
                    RJ_Button_智慧調劑台系統_MouseDownEvent(null);
                }
            }
            else
            {
                rJ_Button_智慧調劑台系統.Enabled = false;

            }
            #endregion
            #region 智能藥庫系統
            ServerSettingClass serverSettingClass_藥庫 = serverSettingClasses.myFind("", "藥庫", "智慧藥局整合平台不顯示");
            if (serverSettingClass_藥庫 != null) rJ_Button_智能藥庫系統.Visible = !(serverSettingClass_藥庫.Value == true.ToString());
            else rJ_Button_智能藥庫系統.Visible = true;
            if ((computerConfigClass.GetValue("智能藥庫系統", "程式致能") == true.ToString()))
            {
                rJ_Button_智能藥庫系統.Enabled = true;
            }
            else
            {
                rJ_Button_智能藥庫系統.Enabled = false;
            }
            #endregion
            #region 中心叫號系統

            ServerSettingClass serverSettingClass_中心叫號系統 = serverSettingClasses.myFind("", "中心叫號系統", "智慧藥局整合平台不顯示");
            if (serverSettingClass_中心叫號系統 != null) rJ_Button_中心叫號系統.Visible = !(serverSettingClass_中心叫號系統.Value == true.ToString());
            else rJ_Button_中心叫號系統.Visible = true;

            if ((computerConfigClass.GetValue("中心叫號系統", "程式致能") == true.ToString()))
            {
                rJ_Button_中心叫號系統.Enabled = true;
            }
            else
            {
                rJ_Button_中心叫號系統.Enabled = false;
            }
            #endregion
            #region 勤務傳送系統

            ServerSettingClass serverSettingClass_傳送櫃 = serverSettingClasses.myFind("", "傳送櫃", "智慧藥局整合平台不顯示");
            if (serverSettingClass_傳送櫃 != null) rJ_Button_勤務傳送系統.Visible = !(serverSettingClass_傳送櫃.Value == true.ToString());
            else rJ_Button_勤務傳送系統.Visible = true;

            if ((computerConfigClass.GetValue("勤務傳送系統", "程式致能") == true.ToString()))
            {
                rJ_Button_勤務傳送系統.Enabled = true;
                if (myConfigClass.Default_program == "勤務傳送系統")
                {
                    RJ_Button_勤務傳送系統_MouseDownEvent(null);
                }
            }
            else
            {
                rJ_Button_勤務傳送系統.Enabled = false;
            }
            #endregion
            #region 癌症備藥機

            ServerSettingClass serverSettingClass_癌症備藥機 = serverSettingClasses.myFind("", "癌症備藥機", "智慧藥局整合平台不顯示");
            if (serverSettingClass_癌症備藥機 != null) rJ_Button_癌症備藥機.Visible = !(serverSettingClass_癌症備藥機.Value == true.ToString());
            else rJ_Button_癌症備藥機.Visible = true;

            if ((computerConfigClass.GetValue("癌症備藥機", "程式致能") == true.ToString()))
            {
                rJ_Button_癌症備藥機.Enabled = true;
                if (myConfigClass.Default_program == "癌症備藥機")
                {
                    RJ_Button_癌症備藥機_MouseDownEvent(null);
                }
            }
            else
            {
                rJ_Button_癌症備藥機.Enabled = false;
            }
            #endregion
            #region 中藥調劑系統
            ServerSettingClass serverSettingClass_中藥調劑系統 = serverSettingClasses.myFind("", "中藥調劑系統", "智慧藥局整合平台不顯示");
            if (serverSettingClass_中藥調劑系統 != null) rJ_Button_中藥調劑系統.Visible = !(serverSettingClass_中藥調劑系統.Value == true.ToString());
            else rJ_Button_中藥調劑系統.Visible = true;
            if ((computerConfigClass.GetValue("中藥調劑系統", "程式致能") == true.ToString()))
            {
                rJ_Button_中藥調劑系統.Enabled = true;
                if (myConfigClass.Default_program == "中藥調劑系統")
                {
                    RJ_Button_中藥調劑系統_MouseDownEvent(null);
                }
            }
            else
            {
                rJ_Button_中藥調劑系統.Enabled = false;
            }
            #endregion
        }
        private string GetVersion(string program_name)
        {
            return Basic.Net.WEBApiGet($"{ApiServer}/api/update/version/{program_name}");
        }
        private bool Download(string program_name, string startupName, string arguments, bool autoStart = true)
        {
            Dialog_Prcessbar dialog_Prcessbar = null;
            string SaveFileName = $@"{currentDirectory}\download.zip";
            string local_folderPath = Path.GetDirectoryName(SaveFileName);
            local_folderPath = $@"{local_folderPath}\{startupName}";
            string local_fileName = Path.Combine(local_folderPath, $"{startupName}.exe");
            try
            {
                bool flag = false;

                string version = this.GetVersion(program_name);
                string local_version = "None";
                try
                {
                    Assembly assembly = Assembly.LoadFrom(local_fileName);
                    local_version = assembly.GetName().Version.ToString();
                }
                catch
                {

                }
                if (version == local_version)
                {
                    return true;
                }
                string extension = Basic.Net.WEBApiGet($"{ApiServer}/api/update/extension/{program_name}");
                if (extension == "")
                {
                    return false;
                }
                dialog_Prcessbar = new Dialog_Prcessbar(100);
                dialog_Prcessbar.ShowMaximun = true;
                dialog_Prcessbar.State = $"Ver{version} 下載中...";
                flag = Basic.Net.DownloadFile($"{ApiServer}/api/update/download/{program_name}", $@"{SaveFileName}");
                dialog_Prcessbar.State = $"Ver{version} 下載完成";
                if (flag)
                {

                    if (!Directory.Exists(local_folderPath))
                    {
                        Directory.CreateDirectory(local_folderPath);
                    }

                    var options = new ReadOptions
                    {
                        StatusMessageWriter = System.Console.Out,
                        Encoding = Encoding.GetEncoding("big5"),
                    };
                    dialog_Prcessbar.State = $"Ver{version} 解壓縮...";
                    using (ZipFile zip = ZipFile.Read(SaveFileName, options))
                    {
                        int totalCount = zip.Entries.Count; // 总文件数
                        int currentCount = 0; // 当前解压缩的文件数
                        dialog_Prcessbar.MaxValue = totalCount + 1;
                        zip.ExtractProgress += (sender, e) =>
                        {
                            if (e.EventType == ZipProgressEventType.Extracting_EntryBytesWritten)
                            {
                                dialog_Prcessbar.Value = currentCount;
                                currentCount++;

                            }
                        };
                        try
                        {
                            zip.ExtractAll(local_folderPath, ExtractExistingFileAction.OverwriteSilently);
                        }
                        catch
                        {

                        }

                    }
                    dialog_Prcessbar.State = $"Ver{version} 解壓縮完成!";
                    System.IO.File.Delete(SaveFileName);





                }
            }
            catch (Exception e)
            {
                MyMessageBox.ShowDialog($"{e.Message}/n{e.StackTrace}");
            }
            finally
            {
                if (dialog_Prcessbar != null)
                {
                    dialog_Prcessbar.Close();
                    dialog_Prcessbar.Dispose();
                }

                try
                {
                    // CreateShortcutToDesktop(local_fileName, $"{startupName}.exe", arguments);
                    if (autoStart)
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = local_fileName,
                            Arguments = arguments
                        };
                        Process.Start(startInfo);
                        Application.Exit();
                    }

                }
                catch (Exception e)
                {
                    MyMessageBox.ShowDialog($"{e.Message}/n{e.StackTrace}");
                }




            }

            return true;
        }
        public static void CreateShortcutToDesktop(string targetExePath, string shortcutName, string arguments)
        {
            string desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string shortcutPath = Path.Combine(desktopFolderPath, $"{shortcutName}.lnk");

            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            shortcut.TargetPath = targetExePath;
            shortcut.WorkingDirectory = Path.GetDirectoryName(targetExePath);
            shortcut.Arguments = arguments;
            shortcut.Save();
        }
        private void RequireAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);

            // 检查当前用户是否具有管理员角色
            bool isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);

            if (!isAdmin)
            {
                // 以管理员权限重新启动应用程序
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.FileName = Process.GetCurrentProcess().MainModule.FileName;
                startInfo.Verb = "runas"; // 使用 "runas" 启动进程以管理员身份
                try
                {
                    Process.Start(startInfo);
                }
                catch (System.ComponentModel.Win32Exception)
                {

                    // 用户取消了提升权限提示或其他错误
                    // 处理异常情况
                }

                // 退出当前应用程序
                Environment.Exit(0);
            }
        }
        #endregion



    }
}
