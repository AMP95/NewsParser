using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsParser
{
    class NewsPageParser
    {
        public string Text { get; set; }
        string DelTrash(string text) {
            string str = text;
            string res = "";
            int index = str.IndexOf('&');
            while (index != -1) {
                res += str.Substring(0, index);
                str = str.Remove(0, index);
                index = str.IndexOf(';');
                str = str.Remove(0, index);
                index = str.IndexOf('&');
            }
            res += str;
            str = "";
            index = res.IndexOf('<');
            while (index != -1) {
                str += res.Substring(0, index);
                res = res.Remove(0, index);
                index = res.IndexOf('>');
                res = res.Remove(0, index);
                index = res.IndexOf('<');
            }
            str += res;
            str = str.Replace(';', ' ');
            str = str.Replace(">", "");
            return str;
        }
        public void GetText(string site) {
            Text = "";
            site = site.Substring(site.IndexOf("<title>") + 7);
            string buf = site.Substring(0, site.IndexOf("</title>"));
            Text += DelTrash(buf);
            Text += "\n\n";
            int index = site.IndexOf("<p>");
            while (index != -1) {
                site = site.Remove(0, index + 3);
                if (site[0] != ' ')
                {
                    buf = site.Substring(0, site.IndexOf("</p>"));
                    buf = DelTrash(buf);
                    Text += buf;
                    Text += "\n";
                }
                index = site.IndexOf("<p>");
            }
        }
    }
}
