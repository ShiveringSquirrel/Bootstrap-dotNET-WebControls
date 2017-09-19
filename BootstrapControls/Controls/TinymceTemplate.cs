using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapControls.Controls
{
    [Serializable]
    public class TinymceTemplate
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }

        public override string ToString()
        {
            string ret = "{ title: \"";
            ret += Title;
            ret += "\", description: \"";
            ret += Description;
            ret += "\"";

            if (!string.IsNullOrEmpty(Url))
            {
                ret += " ,url: \"";
                ret += this.Url;
                ret += "\"";
            }
            else if (!string.IsNullOrEmpty(Content))
            {
                ret += " ,content: \"";
                ret += this.Content.Replace("\"", "\\\"");
                ret += "\"";
            }
            ret += " }";
            return ret;
        }
    }
}
