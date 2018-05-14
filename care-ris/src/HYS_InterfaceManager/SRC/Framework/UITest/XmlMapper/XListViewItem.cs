using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Collections.Generic;
using System.Text;

namespace UITest.XmlMapper
{
    public class XListViewItem : TListViewItem
    {
        private string _nodeName;
        public string NodeName
        {
            get
            {
                return _nodeName;
            }
            set
            {
                _nodeName = value;
                Refresh();
            }
        }

        public string GetXPath()
        {
            StringBuilder sb = new StringBuilder();
            XListViewItem parent = _parent as XListViewItem;
            if (parent != null) sb.Append(parent.GetXPath());
            sb.Append('/').Append(NodeName);
            return sb.ToString();
        }

        private XmlSchemaComplexType _complexType;
        public XmlSchemaComplexType ComplexType
        {
            get { return _complexType; }
            set { _complexType = value; }
        }

        private XListViewControler xcontoler;
        public XListViewItem(string nodeName)
        {
            NodeName = nodeName;
        }
        public XListViewItem(string nodeName, XmlSchemaComplexType complexType)
        {
            NodeName = nodeName;
            ComplexType = complexType;
        }

        public override void Refresh()
        {
            if (SubItems.Count < 2)
            {
                SubItems.Add(_nodeName);
            }
            else
            {
                SubItems[1].Text = _nodeName;
            }

            base.Refresh();

            if (_visible)
            {
                LoadChild();
            }
        }

        private void LoadChild()
        {
            if (HasChildren()) return;

            xcontoler = _controler as XListViewControler;
            if (xcontoler == null) return;

            LoadXmlSchemaComplexType(_complexType);
        }
        private void LoadXmlSchemaObject(XmlSchemaObject obj)
        {
            if (obj == null) return;
            if (LoadXmlSchemaAttribute(obj as XmlSchemaAttribute)) return;
            else if (LoadXmlSchemaAttributeGroupRef(obj as XmlSchemaAttributeGroupRef)) return;
            else if (LoadXmlSchemaElement(obj as XmlSchemaElement)) return;
            else if (LoadXmlSchemaGroupRef(obj as XmlSchemaGroupRef)) return;
            else if (LoadXmlSchemaGroupBase(obj as XmlSchemaGroupBase)) return;  ////XmlSchemaSequence, XmlSchemaChoice
        }
        private void LoadXmlSchemaObjectCollection(XmlSchemaObjectCollection collection)
        {
            if (collection == null) return;
            foreach (XmlSchemaObject obj in collection)
            {
                LoadXmlSchemaObject(obj);
            }
        }
        private void LoadXmlSchemaComplexType(XmlSchemaComplexType ctype)
        {
            if (ctype == null) return;
            LoadXmlSchemaObjectCollection(ctype.Attributes);
            XmlSchemaComplexContent content = ctype.ContentModel as XmlSchemaComplexContent;
            if (content != null)
            {
                XmlSchemaComplexContentExtension cext = content.Content as XmlSchemaComplexContentExtension;
                if (cext != null )
                {
                    LoadXmlSchemaObjectCollection(cext.Attributes);
                    if (cext.BaseTypeName != null)
                    {
                        string typeName = cext.BaseTypeName.Name;
                        if (xcontoler._complexTypes.ContainsKey(typeName))
                        {
                            LoadXmlSchemaComplexType(xcontoler._complexTypes[typeName]);
                        }
                    }
                    LoadXmlSchemaObject(cext.Particle);
                }
            }
            else
            {
                XmlSchemaSimpleContent scontent = ctype.ContentModel as XmlSchemaSimpleContent;
                if (scontent != null)
                {
                    XmlSchemaSimpleContentExtension sext = scontent.Content as XmlSchemaSimpleContentExtension;
                    if (sext != null)
                    {
                        LoadXmlSchemaObjectCollection(sext.Attributes);
                    }
                }
                else
                {
                    LoadXmlSchemaObject(ctype.Particle);
                }
            }
        }

        private bool LoadXmlSchemaAttributeGroupRef(XmlSchemaAttributeGroupRef attGroupRef)
        {
            if (attGroupRef == null) return false;
            if (attGroupRef.RefName != null)
            {
                string groupName = attGroupRef.RefName.Name;
                if (xcontoler._attributeGroups.ContainsKey(groupName))
                {
                    XmlSchemaAttributeGroup attGroup = xcontoler._attributeGroups[groupName];
                    LoadXmlSchemaObjectCollection(attGroup.Attributes);
                }
            }
            return true;
        }
        private bool LoadXmlSchemaGroupBase(XmlSchemaGroupBase groupBase)
        {
            if (groupBase == null) return false;
            LoadXmlSchemaObjectCollection(groupBase.Items);
            return true;
        }
        private bool LoadXmlSchemaGroupRef(XmlSchemaGroupRef groupRef)
        {
            if (groupRef == null) return false;
            if (groupRef.RefName != null)
            {
                XmlSchemaParticle part = null;
                string groupName = groupRef.RefName.Name;
                if (xcontoler._complexTypes.ContainsKey(groupName))
                {
                    XmlSchemaComplexType cct = xcontoler._complexTypes[groupName];
                    part = cct.Particle;
                }
                else if (xcontoler._groups.ContainsKey(groupName))
                {
                    XmlSchemaGroup group = xcontoler._groups[groupName];
                    part = group.Particle;
                }
                LoadXmlSchemaObject(part);
            }
            return true;
        }
        private bool LoadXmlSchemaAttribute(XmlSchemaAttribute att)
        {
            if (att == null) return false;
            if (att.RefName != null &&
                xcontoler._attributes.ContainsKey(att.RefName.Name))
            {
                XmlSchemaAttribute refAtt = xcontoler._attributes[att.RefName.Name];
                LoadXmlSchemaAttribute(refAtt);
            }
            else
            {
                _controler.AddChild(this, new XListViewItem("@" + att.Name));
            }
            return true;
        }
        private bool LoadXmlSchemaElement(XmlSchemaElement ele)
        {
            if (ele == null) return false;
            if (ele.RefName != null &&
                xcontoler._elements.ContainsKey(ele.RefName.Name))
            {
                XmlSchemaElement refEle = xcontoler._elements[ele.RefName.Name];
                LoadXmlSchemaElement(refEle);
            }
            else
            {
                XmlSchemaComplexType ctt = xcontoler.GetElementComplexType(ele);
                if (ctt != null)
                {
                    _controler.AddChild(this, new XListViewItem(ele.Name, ctt));
                }
                else
                {
                    _controler.AddChild(this, new XListViewItem(ele.Name));
                }
            }
            return true;
        }
    }
}
