using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Baidu.Aip.Ocr;
using System.IO;
using System.Net.Http;
using System.Threading;

namespace BD文字识别工具
{
    
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            // 
            InitializeComponent();
            String[] arr = new String[] { "通用识别", "营业执照识别","表格","待续" };
            for (int i = 0; i < arr.Length; i++)
            {
                comboBox1.Items.Add(arr[i]);
            }
            textBox1.Text = " ";
            textBox2.Text = " ";

            //string tokenresult = AccessToken.getAccessToken();
            //string token = ((JObject)JsonConvert.DeserializeObject(tokenresult))["access_token"].ToString();
            //Console.WriteLine(token);


        }


        
       
        public string apikey = "";
        public string secretkey = "";
        public static string type = "accurate";
        private void Form1_Load(object sender, EventArgs e)
        {
           

        }

        private void Btnopenimg_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "C:\\Users\\Administrator\\Downloads";    //打开对话框后的初始目录
            //fileDialog.Filter = "文本文件|*.txt|所有文件|*.*";
            fileDialog.RestoreDirectory = false;    //若为false，则打开对话框后为上次的目录。若为true，则为初始目录
            if (fileDialog.ShowDialog() == DialogResult.OK)
               textBox3.Text = System.IO.Path.GetFullPath(fileDialog.FileName);//将选中的文件的路径传递给TextBox “File

        }
        Ocr client = null;
        Thread t1;
         private void Btnstartocr_Click(object sender, EventArgs e)
        {
            apikey = textBox1.Text;
            secretkey = textBox2.Text;
            client = new Baidu.Aip.Ocr.Ocr(apikey, secretkey);
           this.t1 = new Thread(new ThreadStart( NewMethod));
           this.t1.Start();
        }

        private void NewMethod()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            btnstartocr.Enabled = false;
            btnstartocr.Text = "识别中请稍等";

            int index = comboBox1.SelectedIndex;
            //GeneralBasicDemo();

            
            switch (index)
            {
                case 0:
                    GeneralBasicDemo();
                    break;
                case 1:
                    BusinessLicenseDemo();
                    break;
                case 2:
                    TableRecognitionGetResultDemo();
                    break;
                default:
                    break;
            }


            btnstartocr.Enabled = true;
            btnstartocr.Text = "开始识别";
        }

        public void TableRecognitionRequestDemo()
        {
            var image = File.ReadAllBytes(textBox3.Text);
            // 调用表格文字识别，可能会抛出网络等异常，请使用try/catch捕获
            var result = client.TableRecognitionRequest(image);
            Console.WriteLine(result);
        }
        public void TableRecognitionGetResultDemo()
        {
            var image = File.ReadAllBytes(textBox3.Text);
            // 调用表格文字识别，可能会抛出网络等异常，请使用try/catch捕获
            var result = client.TableRecognitionRequest(image);
            
            Console.WriteLine(result);

            var jo = JsonConvert.DeserializeObject<Root>(result.ToString());
            
           var requestId =jo.result[0].request_id;// 字段名

            // 调用表格识别结果，可能会抛出网络等异常，请使用try/catch捕获
            result = client.TableRecognitionGetResult(requestId);
            Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>{
                                     {"result_type", "excel"}
                                       };
            // 带参数调用表格识别结果
            var result2 = client.TableRecognitionGetResult(requestId, options);
            var jo2 = JsonConvert.DeserializeObject<Root2>(result2.ToString());
            string downloadurl = jo2.result.result_data;
            richTextBox1.Clear();
            richTextBox1.Text += "文件限制链接如下：" + "\n";
            richTextBox1.Text += downloadurl;
            richTextBox1.Text += '\n';
            Console.WriteLine(downloadurl);
        }
        public void GeneralBasicDemo()
        {
            var image = File.ReadAllBytes(textBox3.Text);
            // 调用通用文字识别, 图片参数为本地图片，可能会抛出网络等异常，请使用try/catch捕获
            //var result = client.GeneralBasic(image);
            //Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>{
             {"language_type", "CHN_ENG"},
             {"detect_direction", "true"},
             {"detect_language", "true"},
             {"probability", "true"}
               };
            // 带参数调用通用文字识别, 图片参数为本地图片
            var result = client.GeneralBasic(image, options);
            var jo = JsonConvert.DeserializeObject<baiduJson.Root>(result.ToString());// 反序列化

            //Hashtable ht = new Hashtable();

            for (int i = 0; i < jo.words_result.Count; i++)
            {
                var word = jo.words_result[i].words;// 字段名

                richTextBox1.Text += word;
                richTextBox1.Text += '\n';

                Console.WriteLine(word);

            }
            //Console.WriteLine(result);
        }

        public void BusinessLicenseDemo()
        {
            var image = File.ReadAllBytes(textBox3.Text);
            // 调用通用文字识别, 图片参数为本地图片，可能会抛出网络等异常，请使用try/catch捕获
            //var result = client.GeneralBasic(image);
            //Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>{
             {"language_type", "CHN_ENG"},
             {"detect_direction", "true"},
             {"detect_language", "true"},
             {"probability", "true"}
               };
            // 带参数调用通用文字识别, 图片参数为本地图片
            var result = client.BusinessLicense(image, options);
            Console.WriteLine(result);
            richTextBox1.Text += result;
            
        }

        private void Btncopy_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.Copy();
            MessageBox.Show("复制成功");
        }

        private void Btnclear_Click(object sender, EventArgs e)
        {

            richTextBox1.SelectAll();
            richTextBox1.Clear();
            MessageBox.Show("清空成功");
        }

        private void btn_about_Click(object sender, EventArgs e)
        {
            Form f2 = new Form2();
            f2.ShowDialog();
        }
    }
    
}
