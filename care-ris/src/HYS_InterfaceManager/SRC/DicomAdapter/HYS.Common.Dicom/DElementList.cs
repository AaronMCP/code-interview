using Dicom;
using HYS.Common.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYS.Common.Dicom
{
    public class DElementList : XObjectCollection, IDisposable
    {
        internal DicomDataset _elementList;

        private void FillBaseCollection(DicomDataset rootEleList)
        {
            base.Clear();

            foreach(DicomItem item in _elementList)
            {
                try
                {
                    //DicomElement eleRef = _elementList.Get<DicomElement>(item.Tag);
                    DElement element = new DElement(item, rootEleList);
                    base.Add(element);
                }
                catch (Exception err)
                {
                    LogMgt.Logger.Write(err.ToString());
                }

            }
        }
        private void FillDicomCollection()
        {
            _elementList.Clear();
            _elementList = new DicomDataset();
            foreach (DElement ele in this.GetList())
            {
                _elementList.Add(ele._element);
            }
        }

        public DElementList()
            : base(typeof(DElement))
        {
            _elementList = new DicomDataset();
            FillBaseCollection(_elementList);
        }
        public DElementList(DicomDataset elementList)
            : base(typeof(DElement))
        {
            _elementList = elementList;
            FillBaseCollection(_elementList);
        }
        internal DElementList(DicomDataset elementList, DicomDataset rootEleList)
            : base(typeof(DElement))
        {
            _elementList = elementList;
            FillBaseCollection(rootEleList);
        }

        public static DElementList OpenFile(string fileName)
        {
            try
            {
                if (!File.Exists(fileName)) return null;

                var df = DicomFile.Open(fileName);

                DElementList eList = null;
                eList = new DElementList(df.Dataset);

                return eList;
            }
            catch (Exception err)
            {
                LogMgt.Logger.Write(err.ToString());
                return null;
            }
        }

        /// <summary>
        /// Please input a MElementList object here.
        /// </summary>
        /// <param name="elementList"></param>
        /// <returns></returns>
        public static DElementList CreateFromKdtElementList(object elementList)
        {
            DicomDataset list = elementList as DicomDataset;
            if (list != null) return new DElementList(list);
            return null;
        }
        public static DElementList CreateFromXml(string xmlString)
        {
            DElementList list = XObjectManager.CreateObject(xmlString, typeof(DElementList)) as DElementList;
            if (list != null) list.FillDicomCollection();
            return list;
        }
        public static DElementList CreateFromDicomXml(string dicomRootedXmlString)
        {
            return CreateFromXml(DHelper.GetElementListXmlFromDicomXml(dicomRootedXmlString));
        }

        public static DElementList OpenXmlFile(string fileName)
        {
            try
            {
                if (!File.Exists(fileName)) return null;

                string strXml = "";
                using (StreamReader sr = File.OpenText(fileName))
                {
                    strXml = sr.ReadToEnd();
                }

                DElementList list = XObjectManager.CreateObject(strXml, typeof(DElementList)) as DElementList;
                if (list != null) list.FillDicomCollection();
                return list;
            }
            catch (Exception err)
            {
                LogMgt.Logger.Write(err.ToString());
                return null;
            }
        }
        public bool SaveXmlFile(string fileName)
        {
            try
            {
                if (fileName == null || fileName.Length < 1) return false;

                const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n";
                string strXml = XMLHeader + this.ToXMLString();
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.Write(strXml);
                }

                return true;
            }
            catch (Exception err)
            {
                LogMgt.Logger.Write(err.ToString());
                return false;
            }
        }

        public DElement GetElement(int tag)
        {
            //MElementRef eleRef = _elementList.get_Element((tag_t)tag);
            //if (eleRef == null) return null;
            //return new DElement(eleRef);

            foreach (DElement ele in this)
            {
                if (ele.Tag == tag)
                {
                    return ele;
                }
            }
            return null;
        }

        [XNode(false)]
        public new int Count
        {
            get
            {
                return base.Count;
                //return _elementList.elcount;
            }
        }

        [XNode(false)]
        public new DElement this[int index]
        {
            get
            {
                if (index < 0 || index >= Count) return null;
                return base[index] as DElement;
                //if (index < 0 || index >= _elementList.elcount) return null;
                //MElement ele = _elementList.get_Element(index);
                //if (ele == null) return null;

                //MElementRef eleRef = _elementList.get_Element(ele.tag);
                //return new DElement(eleRef);
            }
        }

        public override XBase Add(XBase value)
        {
            DElement e = value as DElement;
            return Add(e);
        }
        public DElement Add(DElement element)
        {
            if (element == null) return null;
            if (element.IsRef) return null;
            _elementList.Add(element._element);
            return base.Add(element) as DElement;
        }
        public new void Clear()
        {
            _elementList.Clear();
            _elementList = new DicomDataset();
            base.Clear();
        }
        public bool Contains(DElement element)
        {
            return base.Contains(element);
        }
        public new DElementList Copy()
        {
            DElementList list = new DElementList();
            foreach (DElement ele in this)
            {
                list.Add(ele);
            }
            return list;
        }
        public new IEnumerator GetEnumerator()
        {
            return base.GetEnumerator();
        }
        public int IndexOf(DElement element)
        {
            return base.IndexOf(element);
        }
        [Obsolete("This method is not supported.", true)]
        public void Insert(int index, DElement value)
        {
        }
        public void Remove(DElement element)
        {
            if (!Contains(element)) return;

            //Tag t;
            //if (element.IsRef)
            //{
            //    t = element._elementRef.element.tag;
            //}
            //else
            //{
            //    t = element._element.tag;
            //}

            //_elementList.deleteElement(t);
            base.Remove(element);
        }

        public new string ToXMLString()
        {
            return base.ToXMLString(DHelper.XMLNameSpace);
        }

        /// <summary>
        /// Convert to DICOM rooted XML string.
        /// </summary>
        /// <returns></returns>
        public string ToDicomXMLString()
        {
            return DHelper.ConvertToDicomXml(this);
        }

        #region IDisposable Members

        public void Dispose()
        {
            _elementList.Clear();
        }

        #endregion
    }
}
