using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace UITest.XmlMapper
{
    public class XListViewControler : TListViewControler
    {
        private string _baseDirectory;
        public string BaseDirectory
        {
            get { return _baseDirectory; }
            set { _baseDirectory = value; }
        }

        internal Dictionary<string, XmlSchemaInclude> _includes;
        internal Dictionary<string, XmlSchemaComplexType> _complexTypes;
        internal Dictionary<string, XmlSchemaAttributeGroup> _attributeGroups;
        internal Dictionary<string, XmlSchemaAttribute> _attributes;
        internal Dictionary<string, XmlSchemaElement> _elements;
        internal Dictionary<string, XmlSchemaGroup> _groups;

        internal XmlSchemaComplexType GetElementComplexType(XmlSchemaElement ele)
        {
            XmlSchemaComplexType ctt = null;
            if (ele.SchemaType != null)
            {
                ctt = ele.SchemaType as XmlSchemaComplexType;
            }
            else if (ele.SchemaTypeName != null &&
                    _complexTypes.ContainsKey(ele.SchemaTypeName.Name))
            {
                ctt = _complexTypes[ele.SchemaTypeName.Name];
            }
            return ctt;
        }

        internal override string Dump()
        {
            StringBuilder sb = new StringBuilder();

            DumpDictionary<XmlSchemaInclude>("Includes", sb, _includes);
            DumpDictionary<XmlSchemaComplexType>("ComplexTypes", sb, _complexTypes);
            DumpDictionary<XmlSchemaAttributeGroup>("AttributeGroups", sb, _attributeGroups);
            DumpDictionary<XmlSchemaAttribute>("Attributes", sb, _attributes);
            DumpDictionary<XmlSchemaElement>("Elements", sb, _elements);
            DumpDictionary<XmlSchemaGroup>("Groups", sb, _groups);

            sb.Append(base.Dump());
            return sb.ToString();
        }
        private void DumpDictionary<T>(string caption, StringBuilder sb, Dictionary<string, T> dic)
        {
            sb.Append(caption).Append(" (").Append(dic.Count.ToString()).AppendLine(")");
            sb.AppendLine("-----------------------");
            foreach (KeyValuePair<string, T> p in dic)
            {
                sb.Append(p.Key).AppendLine(" " + p.Value);
            }
            sb.AppendLine("-----------------------").AppendLine();
        }

        public override void Clear()
        {
            _baseDirectory = "";
            _includes.Clear();
            _complexTypes.Clear();
            _attributeGroups.Clear();
            _attributes.Clear();
            _elements.Clear();
            _groups.Clear();
            base.Clear();
        }

        public XListViewControler(ListView lv)
            : base(lv)
        {
            _baseDirectory = "";
            _includes = new Dictionary<string, XmlSchemaInclude>();
            _complexTypes = new Dictionary<string, XmlSchemaComplexType>();
            _attributeGroups = new Dictionary<string, XmlSchemaAttributeGroup>();
            _attributes = new Dictionary<string, XmlSchemaAttribute>();
            _elements = new Dictionary<string, XmlSchemaElement>();
            _groups = new Dictionary<string, XmlSchemaGroup>();

            lv.ItemCheck += new ItemCheckEventHandler(lv_ItemCheck);
        }

        private void lv_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ListView lv = sender as ListView;
            if (lv == null) return;

            XListViewItem item = lv.Items[e.Index] as XListViewItem;
            if (item == null) return;

            switch (e.NewValue)
            {
                case CheckState.Checked:
                    {
                        XListViewItem pitem = item._parent as XListViewItem;
                        if (pitem == null) return;
                        pitem.Checked = true;
                        break;
                    }
                case CheckState.Unchecked:
                    {
                        foreach (TListViewItem i in item._children)
                        {
                            XListViewItem citem = i as XListViewItem;
                            if (citem == null) continue;
                            citem.Checked = false;
                        }
                        break;
                    }
            }
        }
        
        public void LoadXmlSchema(string filename)
        {
            _baseDirectory = Path.GetDirectoryName(filename);
            if (_baseDirectory.Length < 1) _baseDirectory = Application.StartupPath;

            if (!Path.IsPathRooted(filename) && _baseDirectory.Length > 0)
                filename = _baseDirectory + "\\" + filename;

            if (!File.Exists(filename)) return;

            XmlTextReader reader = new XmlTextReader(filename);
            LoadXmlSchema(reader);
        }
        public void LoadXmlSchema(XmlReader reader)
        {
            if (reader == null) return;

            _includes.Clear();
            _complexTypes.Clear();

            XmlSchema schema = XmlSchema.Read(reader, ValidationCallback);
            if (schema == null) return;

            LoadXmlSchemaInclude(schema);
            foreach (XmlSchemaObject obj in schema.Items)
            {
                XmlSchemaElement ele = obj as XmlSchemaElement;
                if (ele != null)
                {
                    XListViewItem lvItem;
                    XmlSchemaComplexType cType = GetElementComplexType(ele);
                    if (cType == null)
                    {
                        lvItem = new XListViewItem(ele.Name);
                    }
                    else
                    {
                        lvItem = new XListViewItem(ele.Name, cType);
                    }
                    AddRoot(lvItem);
                }
            }
        }

        private void LoadXmlSchemaInclude(XmlSchema schema)
        {
            foreach (XmlSchemaObject obj in schema.Includes)
            {
                XmlSchemaInclude inc = obj as XmlSchemaInclude;
                if (inc == null) continue;

                string location = inc.SchemaLocation;
                if (!Path.IsPathRooted(location) && _baseDirectory.Length > 0)
                    location = _baseDirectory + "\\" + location;

                if (!_includes.ContainsKey(location))
                {
                    if (File.Exists(location))
                    {
                        XmlTextReader reader = new XmlTextReader(location);
                        XmlSchema incSchema = XmlSchema.Read(reader, ValidationCallback);
                        LoadXmlSchemaInclude(incSchema);
                        _includes.Add(location, inc);
                    }
                }
            }
            LoadXmlSchemaDictionaries(schema);
        }
        private void LoadXmlSchemaDictionaries(XmlSchema schema)
        {
            foreach (XmlSchemaObject obj in schema.Items)
            {
                if (LoadXmlSchemaGroup(obj as XmlSchemaGroup)) continue;
                else if (LoadXmlSchemaElement(obj as XmlSchemaElement)) continue;
                else if (LoadXmlSchemaAttribute(obj as XmlSchemaAttribute)) continue;
                else if (LoadXmlSchemaComplexType(obj as XmlSchemaComplexType)) continue;
                else if (LoadXmlSchemaAttributeGroup(obj as XmlSchemaAttributeGroup)) continue;
            }
        }

        private bool LoadXmlSchemaGroup(XmlSchemaGroup group)
        {
            if (group == null) return false;
            _groups.Add(group.Name, group);
            return true;
        }
        private bool LoadXmlSchemaElement(XmlSchemaElement ele)
        {
            if (ele == null) return false;
            if (ele.RefName == null || ele.RefName.Name.Length < 1)
            {
                _elements.Add(ele.Name, ele);
            }
            return true;
        }
        private bool LoadXmlSchemaAttribute(XmlSchemaAttribute att)
        {
            if (att == null) return false;
            if (att.RefName == null || att.RefName.Name.Length < 1)
            {
                _attributes.Add(att.Name, att);
            }
            return true;
        }
        private bool LoadXmlSchemaComplexType(XmlSchemaComplexType ctype)
        {
            if (ctype == null) return false;
            _complexTypes.Add(ctype.Name, ctype);
            return true;
        }
        private bool LoadXmlSchemaAttributeGroup(XmlSchemaAttributeGroup agroup)
        {
            if (agroup == null) return false;
            _attributeGroups.Add(agroup.Name, agroup);
            return true;
        }

        public event XListViewControlerErrorHanlder SchemaError;
        private void ValidationCallback(object sender, ValidationEventArgs args)
        {
            if (SchemaError == null) return;
            SchemaError(args.Severity == XmlSeverityType.Error, args.Message, args.Exception);
        }
    }

    public delegate void XListViewControlerErrorHanlder(bool isError, string message, Exception exception);
}
