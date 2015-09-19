using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Linq;

namespace MindfulnessBell
{
    public class Setting
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public interface ISettingsRepository
    {
        void Insert(Setting setting);
        void Update(Setting setting);
        Setting Receive(string key);
    }

    public class XMLSettingsRepository : ISettingsRepository
    {
        private string xmlfile = "storage.xml";
        private XDocument GetDoc()
        {
            return XDocument.Load(xmlfile);
        }
        public void Insert(Setting setting)
        {
            var doc = GetDoc();
            XElement ele = new XElement("setting");
            ele.Add(new XAttribute("key", setting.Key));
            ele.Value = setting.Value;
            doc.Root.Element(XName.Get("settings")).Add(ele);
            doc.Save(xmlfile);
        }
        public void Update(Setting setting)
        {
            var doc = GetDoc();
            var ele = doc.Root.Element("settings").Elements().Where(x => x.Attribute(XName.Get("key")).Value == setting.Key).First();
            ele.Value = setting.Value;
            doc.Save(xmlfile);
        }

        public Setting Receive(string key)
        {
            var doc = GetDoc();
            var ele = doc.Root.Element("settings").Elements().Where(x => x.Attribute(XName.Get("key")).Value == key).First();
            return new Setting() { Key = key, Value = ele.Value };
        }
    }
}
