using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewsParser
{
    public partial class Form1 : Form
    {
        Form2 form;
        MainPagePaser main;
        NewsPageParser news;
        public Form1()
        {
            InitializeComponent();
            form = new Form2();
            main = new MainPagePaser();
            news = new NewsPageParser();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string site = SiteFromUrl.GetSite("https://nsk.rbc.ru");
            main.Site = site;
            main.GenerateList();
            foreach (TitleURLPairs pair in main.Pairs)
            {
                listBox1.Items.Add(pair.Title);
            }
            //string site = SiteFromUrl.GetSite("https://sportrbc.ru/news/631a1d639a79473f0d025383?from=newsfeed");
            //form.SetText(site);
            //form.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedIndex != -1)
                {
                    form.Text = listBox1.SelectedItem.ToString();
                    string url = main.Pairs[listBox1.SelectedIndex].URL;
                    string site = SiteFromUrl.GetSite(url);
                    //string site = SiteFromUrl.GetSite("https://www.rbc.ru/photoreport/11/09/2022/63198e3c9a7947814bd2bc6d");
                    news.GetText(site);
                    form.SetText(news.Text);
                    form.ShowDialog();
                }
            }
            catch {
                MessageBox.Show("Ошибка");
                //на некоторые ссылки request.GetResponse() выдает ошибку 404
                //не могу понять почему
            }
        }
    }
}