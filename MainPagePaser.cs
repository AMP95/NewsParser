using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsParser
{
    class MainPagePaser
    {
        public List<TitleURLPairs> Pairs { get; protected set; }
        public string Site { get; set; }
        public string Date { get; set; }
        public MainPagePaser()
        {
            Pairs = new List<TitleURLPairs>();
        }
        bool isSame(string url) {
            foreach (TitleURLPairs p in Pairs) {
                if (url.IndexOf(p.URL) != -1) {
                    return true;
                }
            }
            return false;
        }
        bool isTitleUrl(string url) {
            int index = url.IndexOf(Date);
            return index != 0 && index!=-1;
        }
        public void GenerateList() {
            /* <a href="https://climate.rbc.ru/?utm_source=rbc&amp;utm_medium=main&amp;utm_campaign=clim22f-r-karbonenhet-m&amp;from=newsfeed"
            id="id_newsfeed_6315adda9a794729aa897be2"
            data-modif="1662794784"
                   class="news-feed__item js-visited js-news-feed-item js-yandex-counter"
            data-yandex-name="from_news_feed">
            <div class="news-feed__item__grid">
                            <div class="news-feed__item__grid__content">
                    <span class="news-feed__item__title">
                        В России появился внутренний рынок углеродных единиц
                    </span>*/

            Pairs.Clear();
            string site = Site.Substring(0, Site.IndexOf("l-col-container"));
            site = site.Substring(site.IndexOf("js-news-feed-list") + 19);
            int index = site.IndexOf("<a href=\"");
            while (index != -1)
            {
                site = site.Remove(0, index + 9);
                index = site.IndexOf("\"");
                string url;
                if (site.IndexOf("id") != -1)
                {
                    url = site.Substring(0, index);
                    index = site.IndexOf("news-feed__item__title");
                    //"news-feed__item__title news-feed__item_in-main")
                    site = site.Remove(0, index + 23);
                    if(site[0] != '>')
                        site = site.Remove(0, 24);
                    string title = site.Substring(21, site.IndexOf("</span>") - 21);
                    Pairs.Add(new TitleURLPairs() { Title = title, URL = url });
                }
                index = site.IndexOf("<a href=\"");
            }
            site = Site.Substring(Site.IndexOf("Топ новости"));
            index = site.IndexOf("data-vr-contentbox-url=");
            while (index != -1)
            {
                site = site.Remove(0, index + 23);
                string url = site.Substring(1, site.IndexOf("\">") - 1);
                index = site.IndexOf("-->");
                site = site.Remove(0, index + 3);
                string title = site.Substring(0, site.IndexOf("<!"));
                Pairs.Add(new TitleURLPairs() { Title = title, URL = url });
                index = site.IndexOf("data-vr-contentbox-url=\"");
            }
        }
    }
}
