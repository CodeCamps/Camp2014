using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace HauntedHouseCrashers
{
    public class SpriteHelper
    {
        public static Dictionary<String, Rectangle> SpriteRects = new Dictionary<string, Rectangle>(); 

        public static void ParseRects(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var nodes = doc.SelectNodes("/TextureAtlas/sprite");
            foreach (XmlNode node in nodes)
            {
                var name = node.Attributes["n"].Value;
                var x = int.Parse(node.Attributes["x"].Value);
                var y = int.Parse(node.Attributes["y"].Value);
                var w = int.Parse(node.Attributes["w"].Value);
                var h = int.Parse(node.Attributes["h"].Value);

                SpriteHelper.SpriteRects[name] = new Rectangle(x, y, w, h);
            }
        }

    }
}
