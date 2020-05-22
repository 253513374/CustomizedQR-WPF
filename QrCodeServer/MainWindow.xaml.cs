using Hprose.Client;
using Hprose.Server;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using QrCodeEncoding.DrawQrCode.ConfigJson;
using QrCodeServer.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QrCodeServer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {


        private HproseTcpListenerServer TcpServer;
        private HproseHttpListenerServer  HttpServer;

        private readonly Queryservice Queryservice = new Queryservice();
       // private List<RequestModel> requestModels { get; set; } = new List<RequestModel>();
        public EnQrCodeService enQrCodeService = new EnQrCodeService();

        public ObservableCollection<RequestModel> requestModels  = new ObservableCollection<RequestModel>();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this.Queryservice;
        }
        private void StopTcpButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button)) return;


            button.IsEnabled = false;
            TcpServer.Stop();
            ListBoxServerMsg.Items.Insert(0, "Tcp服务关闭成功:" + DateTime.Now.ToString());
            StarTcptButton.IsEnabled = true;
        }

        private void StartTcpButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button)) return;
            try
            {
                if (Queryservice.IsUrl())
                {
                    TcpServer = new HproseTcpListenerServer(Queryservice.GetTcpUrl());
                    TcpServer.Add("Expjson", enQrCodeService);
                    TcpServer.Add("Encoder", enQrCodeService);
                    TcpServer.OnAfterInvoke += TcpServer_OnAfterInvoke;
                    TcpServer.Start();
                    //HproseTcpListenerServer = new HproseTcpListenerServer(Queryservice.GetTpcUrl());
                    //HproseTcpListenerServer.AddMissingMethod("GetLogistinInfo",this);
                    //HproseTcpListenerServer.Start();
                    ListBoxServerMsg.Items.Insert(0, "Tcp服务启动成功:" + DateTime.Now.ToString());
                   // this.Queryservice.ServerMsg = "服务启动成功";
                    button.IsEnabled = false;
                    StopTcpButton.IsEnabled = true;
                }

            }
            catch (Exception ex)
            {
                ListBoxServerMsg.Items.Insert(0, "Tcp服务启动错误:" + ex.Message);
                // MessageBox.Show(ex.Message);
            }
        }

        private void TcpServer_OnAfterInvoke(string name, ref object[] args, bool byRef, ref object result, Hprose.Common.HproseContext context)
        {
            //if(requestModels!=null)
            //{
            //    requestModels.Clear();
            //}
           
            //enQrCodeService.requestModels.ForEach(a => requestModels.Insert(0,a));
            //Task.Run(()=> {
            //    ShowListBox.ItemsSource =);
            //});

            // throw new NotImplementedException();
        }

        private void StopHttpButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button)) return;
            button.IsEnabled = false;
            HttpServer.Stop();
            ListBoxServerMsg.Items.Insert(0, "Http服务关闭成功:" + DateTime.Now.ToString());
            StartHttpButton.IsEnabled = true;
        }

        private void StartHttpButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button)) return;
            try
            {
                var httpUrl = Queryservice.GetHttpUri();
                HttpServer = new HproseHttpListenerServer(httpUrl);
                HttpServer.Add("Expjson", enQrCodeService);
                HttpServer.Add("Encoder", enQrCodeService);
                HttpServer.Start();

                HttpServer.OnAfterInvoke += TcpServer_OnAfterInvoke;

                ListBoxServerMsg.Items.Insert(0, "Http服务启动成功:" + DateTime.Now.ToString());

                button.IsEnabled = false;
                StopHttpButton.IsEnabled = true;
            }
            catch (Exception ex)
            {

                ListBoxServerMsg.Items.Insert(0, "Http服务启动错误:" + ex.Message);
            }
          

        }

        private void TCP_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                FileStream fileStream = new FileStream(@"D:\Program Files\桌面\12112\QrCodeConfig.json", FileMode.Open);
                StreamReader sw = new StreamReader(fileStream);
                var jsonvar = sw.ReadToEnd();
                sw.Close();
                sw.Dispose();
                fileStream.Close();
                fileStream.Dispose();
                QrCodeOptions qqq= JsonConvert.DeserializeObject<QrCodeOptions>(jsonvar);


                var json = JsonConvert.SerializeObject(qqq);


                string tcp = Queryservice.GetTcpUrl();
                HproseClient client = HproseClient.Create(tcp);

                var jsonvar22 = client.Invoke<string>("Expjson", new object[] { });

                //var text = @"{"qrCodeCustoms":[{"QrCodeTag":1,"QrCodeStyle":4,"ColorBrush":"0, 0, 0","QrCodeSize":0},{"QrCodeTag":2,"QrCodeStyle":1,"ColorBrush":"0, 0, 0","QrCodeSize":0},{"QrCodeTag":3,"QrCodeStyle":6,"ColorBrush":"0, 0, 0","QrCodeSize":0},{"QrCodeTag":4,"QrCodeStyle":1,"ColorBrush":"0, 0, 0","QrCodeSize":1}],"QrCodeTypeEnum":2,"LogoImgPath":"","IsTopLogoImg":false,"DpiInch":600,"QrCodeWidthMM":200,"QuietZoneModule":1,"ErrorCorrectionLevel":2,"QrText":"https://www.hao123.com","IsTypeSize":true,"IsQrCodeAutoSize":true,"QrCodePixelWidth":800}";
                byte[] bytevar = client.Invoke<byte[]>("Encoder", new object[] { json });

            }
            catch (Exception ex)
            {

                throw;
            }
          
           // byte[] bytevar = client.Invoke<byte[]>("Encoder", new object[] { jsonvar });


        }

        private void Http_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HproseHttpClient httpclient = new HproseHttpClient(Queryservice.GetHttpUri());
                httpclient.KeepAlive = true;
                httpclient.KeepAliveTimeout = 300;
                var jsonvar = httpclient.Invoke<string>("Expjson", new object[] { });
                byte[] bytevar = httpclient.Invoke<byte[]>("Encoder", new object[] { jsonvar });
            }
            catch (Exception ex)
            {

                throw;
            }
          
          //  byte[] bytevar = httpclient.Invoke<byte[]>("Encoder", new object[] { jsonvar });

        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //
           // ShowListBox.ItemsSource = requestModels;
           // requestModels.Insert(0, new RequestModel() { DateTimes = "2121212121", JsonParameter = null });
        }
    }
}
