using System;
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
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
namespace E_UpdateVersion
{
    public partial class Form1 : Form
    {
        private string ApiServer
        {
            get
            {
               return myConfigClass.Api_server;
            }
        }
        static string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        static string DPS_filename = $@"{currentDirectory}\智慧調劑台系統.zip";
        #region MyConfigClass
        private const string MyConfigFileName = @"config.txt";
        public MyConfigClass myConfigClass = new MyConfigClass();
        public class MyConfigClass
        {
            private string api_server = "";

            public string Api_server { get => api_server; set => api_server = value; }
        }
        private void LoadMyConfig()
        {
            string jsonstr = MyFileStream.LoadFileAllText($".//{MyConfigFileName}");
            if (jsonstr.StringIsEmpty())
            {
                jsonstr = Basic.Net.JsonSerializationt<MyConfigClass>(new MyConfigClass(), true);
                List<string> list_jsonstring = new List<string>();
                list_jsonstring.Add(jsonstr);
                if (!MyFileStream.SaveFile($".//{MyConfigFileName}", list_jsonstring))
                {
                    MyMessageBox.ShowDialog($"建立{MyConfigFileName}檔案失敗!");
                }
            }
            else
            {
                myConfigClass = Basic.Net.JsonDeserializet<MyConfigClass>(jsonstr);

                jsonstr = Basic.Net.JsonSerializationt<MyConfigClass>(myConfigClass, true);
                List<string> list_jsonstring = new List<string>();
                list_jsonstring.Add(jsonstr);
                if (!MyFileStream.SaveFile($".//{MyConfigFileName}", list_jsonstring))
                {
                    MyMessageBox.ShowDialog($"建立{MyConfigFileName}檔案失敗!");
                }

            }


        }
        private void SaveConfig()
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
           
            this.label_version.Text = $"Ver {this.ProductVersion}";
            this.LoadMyConfig();
            this.rJ_Button_離開.MouseDownEvent += RJ_Button_離開_MouseDownEvent;
            this.rJ_Button_智慧調劑台系統.MouseDownEvent += RJ_Button_智慧調劑台系統_MouseDownEvent;
        }

        private void RJ_Button_智慧調劑台系統_MouseDownEvent(MouseEventArgs mevent)
        {
            if(Download("調劑台" , "調劑台管理系統") == false)
            {
                MyMessageBox.ShowDialog("取得更新資訊失敗!");
            }
        }
        private bool Download(string program_name , string startupName)
        {
        
            try
            {
                string SaveFileName = $@"{currentDirectory}\download.zip";
                string extension = Basic.Net.WEBApiGet($"{ApiServer}/api/update/extension/{program_name}");
                if(extension == "")
                {
                    return false;
                }

                bool flag = Basic.Net.DownloadFile($"{ApiServer}/api/update/download/{program_name}", $@"{SaveFileName}");
                if(flag)
                {
                    string folderPath = Path.GetDirectoryName(SaveFileName);
                    folderPath = $@"{folderPath}\{startupName}";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                        Console.WriteLine("文件夹已创建");
                    }

                    var options = new ReadOptions
                    {
                        StatusMessageWriter = System.Console.Out,
                        Encoding = Encoding.GetEncoding("big5"),
                    };
                    using (ZipFile zip = ZipFile.Read(SaveFileName, options))
                    {
                        zip.ExtractAll(folderPath, ExtractExistingFileAction.OverwriteSilently);
                    }
                    File.Delete(SaveFileName);
                    Process.Start(string.Format($@"{folderPath}\{startupName}.exe"));
                    Application.Exit();

                }
            }
            catch(Exception e)
            {
                MyMessageBox.ShowDialog(e.Message);
            }
           
            return true;
        }
        private void RJ_Button_離開_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                this.Close();
            }));
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
    }
}
