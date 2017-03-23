using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using HtmlAgilityPack;

namespace HtmlFilteringDemo
{
    public static class HtmlFilter
    {
        public static MvcHtmlString Filter(string html, string[] allowedTags)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            StringBuilder buffer = new StringBuilder();
            Process(doc.DocumentNode, buffer, allowedTags);

            return MvcHtmlString.Create(buffer.ToString());
        }

        static string[] RemoveChildrenOfTags = new string[] { "script", "style" };
        static void Process(HtmlNode node, StringBuilder buffer, string[] allowedTags)
        {
            switch (node.NodeType)
            {
                case HtmlNodeType.Text:
                    buffer.Append(HttpUtility.HtmlEncode(((HtmlTextNode)node).Text));
                    break;
                case HtmlNodeType.Element:
                case HtmlNodeType.Document:
                    bool allowedTag = allowedTags.Contains(node.Name.ToLower());
                    if (allowedTag)
                        buffer.AppendFormat("<{0}>", node.Name);
                    if (!RemoveChildrenOfTags.Contains(node.Name))
                        foreach (HtmlNode childNode in node.ChildNodes)
                            Process(childNode, buffer, allowedTags);
                    if (allowedTag)
                        buffer.AppendFormat("</{0}>", node.Name);
                    break;
            }
        }
    } 

}
