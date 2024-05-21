using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace NETFX.AsyncAwait.Winforms
{
    public partial class Form1 : Form
    {
        private const string VISUALSTUDIO_WIKI_URL = "https://en.wikipedia.org/wiki/Visual_Studio";
        private System.Timers.Timer timerSync = new System.Timers.Timer();

        public Form1()
        {
            InitializeComponent();
            timerSync.Interval = 500;
            timerSync.Elapsed += TimerSync_Elapsed;
            timerSync.Start();
        }

        private void btnGetWikiSync_Click(object sender, EventArgs e)
        {
            Thread.Sleep(2000);
            using (HttpClient client = new HttpClient())
            {
                string wikipediaContent = "";
                for (int i = 0; i < 10; i++)
                {
                    wikipediaContent = client.GetStringAsync(VISUALSTUDIO_WIKI_URL).Result;
                    Thread.Sleep(200);
                }
                textBox1.Text = wikipediaContent;
            }
        }

        private void TimerSync_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (lblTimerSync.InvokeRequired)
            {
                lblTimerSync.Invoke(new MethodInvoker(delegate
                {
                    lblTimerSync.Text = DateTime.Now.ToString("HH:mm:ss");
                }));
            }
            else
            {
                lblTimerSync.Text = DateTime.Now.ToString("HH:mm:ss");
            }
        }

        private async void btnGetWikiAsync_Click(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            string wikipediaContent = "";
            wikipediaContent = await GetWikiVisualStudioAsync();
            textBox2.Text = wikipediaContent;
        }

        private async Task<string> GetWikiVisualStudioAsync()
        {
            string wikipediaContent = "";
            using (HttpClient client = new HttpClient())
            {
                for (int i = 0; i < 10; i++)
                {
                    wikipediaContent = await client.GetStringAsync(VISUALSTUDIO_WIKI_URL);
                    await Task.Delay(200);
                }
            }
            return wikipediaContent;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
