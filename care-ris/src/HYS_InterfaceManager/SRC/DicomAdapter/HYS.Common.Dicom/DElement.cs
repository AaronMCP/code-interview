using Dicom;
using Dicom.IO.Buffer;
using HYS.Common.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYS.Common.Dicom
{
    public class DElement : XObject, IDisposable
    {
        internal DicomItem _element;

        private DicomItem CreateEmptyElement()
        {
            return CreateEmptyElement(new DicomTag(0, 0), DicomVR.UN);
        }
        private DicomItem CreateEmptyElement(DicomTag tag, DicomVR vr)
        {
            if (vr == DicomVR.SQ) Sequence = new DSequence(this);

            if (vr == DicomVR.AE)
            {
                return new DicomApplicationEntity(tag, EmptyBuffer.Value);
            }
            else if (vr == DicomVR.AS)
            {
                return new DicomAgeString(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.AT)
            {
                return new DicomAttributeTag(tag, EmptyBuffer.Value);
            }
            else if (vr == DicomVR.CS)
            {
                return new DicomCodeString(tag, EmptyBuffer.Value);
            }
            else if (vr == DicomVR.DA)
            {
                return new DicomDate(tag, EmptyBuffer.Value);
            }
            else if (vr == DicomVR.DS)
            {
                return new DicomDecimalString(tag, EmptyBuffer.Value);
            }
            else if (vr == DicomVR.DT)
            {
                return new DicomDateTime(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.FD)
            {
                return new DicomFloatingPointDouble(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.FL)
            {
                return new DicomFloatingPointSingle(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.IS)
            {
                return new DicomIntegerString(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.LO)
            {
                return new DicomLongString(tag, DicomEncoding.Default, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.LT)
            {
                return new DicomLongText(tag, DicomEncoding.Default, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.OB)
            {
                return new DicomOtherByte(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.OD)
            {
                return new DicomOtherDouble(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.OF)
            {
                return new DicomOtherFloat(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.OW)
            {
                return new DicomOtherWord(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.PN)
            {
                return new DicomPersonName(tag, DicomEncoding.Default, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.SH)
            {
                return new DicomShortString(tag, DicomEncoding.Default, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.SL)
            {
                return new DicomSignedLong(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.SQ)
            {
                return new DicomSequence(tag);
            }

            else if (vr == DicomVR.SS)
            {
                return new DicomSignedShort(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.ST)
            {
                return new DicomShortText(tag, DicomEncoding.Default, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.TM)
            {
                return new DicomTime(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.UC)
            {
                return new DicomUnlimitedCharacters(tag, DicomEncoding.Default, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.UI)
            {
                return new DicomUniqueIdentifier(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.UL)
            {
                return new DicomUnsignedLong(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.UN)
            {
                return new DicomUnknown(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.UR)
            {
                return new DicomUniversalResource(tag, DicomEncoding.Default, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.US)
            {
                return new DicomUnsignedShort(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.UT)
            {
                return new DicomUnlimitedText(tag, DicomEncoding.Default, EmptyBuffer.Value);
            }

            return null;
        }
        private void FillSequenceFromElementRef(DicomDataset rootEleList)
        {
            try
            {
                DicomVR vr = _element.ValueRepresentation;

                if (vr == DicomVR.SQ)
                {
                    Sequence = new DSequence(this);
                    DicomSequence sequence = _element as DicomSequence;
                    for (int i = 0; i < sequence.Items.Count; i++)
                    {
                        DicomDataset list = sequence.Items[i];

                        DElementList dlist = new DElementList(list, rootEleList);
                        Sequence._add(dlist);
                    }
                }
            }
            catch (Exception err)
            {
                DElement errEle = new DElement(_tag, _vr);
                LogMgt.Logger.Write(err.ToString());
            }
        }

        private void setVR(DVR vr)
        {
            try
            {
                DicomVR dicomVR = DHelper.ConvertToDicomVR(vr);
                if (_element != null)
                {
                    DicomTag tag = _element.Tag;
                    //string val = Value;
                    _element = CreateEmptyElement(tag, dicomVR);
                    //Value = val;
                }
            }
            catch(Exception ex)
            {
                LogMgt.Logger.Write(ex.ToString());
            }
        }


        public DVR getVR()
        {
            if (_element != null)
            {
                DicomVR vr = _element.ValueRepresentation;
                return DHelper.ConvertToDVR(vr);
            }
            return DVR.UN;
        }

        private void setTag(int tag)
        {
            DicomTag dicomTag = DicomTag.Parse(DHelper.Int2HexString(tag));
            if (_element != null)
            {
                DicomVR vr = _element.ValueRepresentation;
                //string val = Value;
                _element = CreateEmptyElement(dicomTag, vr);
                //Value = val;
            }
        }
        private int getTag()
        {
            if (_element != null) return DHelper.GE2int(_element.Tag.Group, _element.Tag.Element);
            return 0;
        }

        // --- 20080312 ---

        private bool _vrSwitched = false;
        private DicomVR _switchedVR = DicomVR.UN;
        private DicomVR SwitchVR()
        {
            if (_vrSwitched) return _switchedVR;

            uint tag = 0;
            if (_element != null) tag = DHelper.GE2uint(_element.Tag.Group, _element.Tag.Element);
            _switchedVR = PrivateTagHelper.GetPrivateTagVR(tag);
            _vrSwitched = true;

            return _switchedVR;
        }

        private string getVRString()
        {
            DicomVR vr = DicomVR.UN;
            if (_element != null) vr = _element.ValueRepresentation;

            DicomVR svr = DicomVR.UN;
            if (vr == DicomVR.UN) svr = SwitchVR();

            string str = vr.ToString();
            if (svr != DicomVR.UN) str += "(" + svr.ToString() + ")";  // this can only happen when receiving UN tag in explicit transfer syntax 20080319
            return str;
        }

        // ----------------

        public DElement()
        {
            IsRef = false;
            _element = CreateEmptyElement();
        }
        public DElement(int tag, DVR vr)
            : this(tag, vr, DValueType.Unknown)
        {
        }
        public DElement(int tag, DVR vr, DValueType type)
        {
            IsRef = false;

            DicomVR dicomVR = DHelper.ConvertToDicomVR(vr);
            DicomTag dicomTag = DHelper.Int2DicomTag(tag);

            _element = CreateEmptyElement(dicomTag, dicomVR);

            Type = type;
        }
        public DElement(int groupNumber, int elementNumber, DVR vr)
            : this(groupNumber, elementNumber, vr, DValueType.Unknown)
        {
        }
        public DElement(int groupNumber, int elementNumber, DVR vr, DValueType type)
        {
            IsRef = false;

            DicomVR dicomVR = DHelper.ConvertToDicomVR(vr);
            DicomTag dicomTag = new DicomTag((ushort)groupNumber, (ushort)elementNumber);

            _element = CreateEmptyElement(dicomTag, dicomVR);

            Type = type;
        }
        public DElement(int groupNumber, int elementNumber, DVR vr, string value)
        {
            IsRef = false;

            DicomVR dicomVR = DHelper.ConvertToDicomVR(vr);
            DicomTag dicomTag = new DicomTag((ushort)groupNumber, (ushort)elementNumber);

            _element = CreateEmptyElement(dicomTag, dicomVR);
            Value = value;
        }
        internal DElement(DicomItem elementRef, DicomDataset rootEleList)
        {
            IsRef = true;
            _element = elementRef;
            _rootEleList = rootEleList;
            FillSequenceFromElementRef(rootEleList);
        }
        internal DElement(DicomItem element)
        {
            _element = element;
        }

        internal DicomDataset _rootEleList;

        private DicomItem GetDicomItemEmpty(DicomVR vr, DicomTag tag)
        {
            if (vr == DicomVR.AE)
            {
                return new DicomApplicationEntity(tag, EmptyBuffer.Value);
            }
            else if (vr == DicomVR.AS)
            {
                return new DicomAgeString(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.AT)
            {
                return new DicomAttributeTag(tag, EmptyBuffer.Value);
            }
            else if (vr == DicomVR.CS)
            {
                return new DicomCodeString(tag, EmptyBuffer.Value);
            }
            else if (vr == DicomVR.DA)
            {
                return new DicomDate(tag, EmptyBuffer.Value);
            }
            else if (vr == DicomVR.DS)
            {
                return new DicomDecimalString(tag, EmptyBuffer.Value);
            }
            else if (vr == DicomVR.DT)
            {
                return new DicomDateTime(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.FD)
            {
                return new DicomFloatingPointDouble(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.FL)
            {
                return new DicomFloatingPointSingle(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.IS)
            {
                return new DicomIntegerString(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.LO)
            {
                return new DicomLongString(tag, DicomEncoding.Default, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.LT)
            {
                return new DicomLongText(tag, DicomEncoding.Default, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.OB)
            {
                return new DicomOtherByte(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.OD)
            {
                return new DicomOtherDouble(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.OF)
            {
                return new DicomOtherFloat(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.OW)
            {
                return new DicomOtherWord(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.PN)
            {
                return new DicomPersonName(tag, DicomEncoding.Default, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.SH)
            {
                return new DicomShortString(tag, DicomEncoding.Default, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.SL)
            {
                return new DicomSignedLong(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.SQ)
            {
                return new DicomSequence(tag);
            }

            else if (vr == DicomVR.SS)
            {
                return new DicomSignedShort(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.ST)
            {
                return new DicomShortText(tag, DicomEncoding.Default, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.TM)
            {
                return new DicomTime(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.UC)
            {
                return new DicomUnlimitedCharacters(tag, DicomEncoding.Default, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.UI)
            {
                return new DicomUniqueIdentifier(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.UL)
            {
                return new DicomUnsignedLong(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.UN)
            {
                return new DicomUnknown(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.UR)
            {
                return new DicomUniversalResource(tag, DicomEncoding.Default, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.US)
            {
                return new DicomUnsignedShort(tag, EmptyBuffer.Value);
            }

            else if (vr == DicomVR.UT)
            {
                return new DicomUnlimitedText(tag, DicomEncoding.Default, EmptyBuffer.Value);
            }

            return null;
        }

        private DicomItem GetDicomItemValue(DicomVR vr, DicomTag tag, string[] list)
        {
            if (vr == DicomVR.AE)
            {
                return new DicomApplicationEntity(tag, list.ToArray());
            }
            else if (vr == DicomVR.AS)
            {
                return new DicomAgeString(tag, list.ToArray());
            }

            //else if (vr == DicomVR.AT)
            //{
            //    return new DicomAttributeTag(tag, EmptyBuffer.Value);
            //}
            else if (vr == DicomVR.CS)
            {
                return new DicomCodeString(tag, list.ToArray());
            }
            else if (vr == DicomVR.DA)
            {
                return new DicomDate(tag, list.ToArray());
            }
            else if (vr == DicomVR.DS)
            {
                return new DicomDecimalString(tag, list.ToArray());
            }
            else if (vr == DicomVR.DT)
            {
                return new DicomDateTime(tag, list.ToArray());
            }

            else if (vr == DicomVR.FD)
            {
                List<Double> dList = new List<double>();
                foreach (string s in list)
                {
                    dList.Add(Double.Parse(s));
                }
                return new DicomFloatingPointDouble(tag, dList.ToArray());
            }

            else if (vr == DicomVR.FL)
            {
                List<float> fList = new List<float>();
                foreach (string s in list)
                {
                    fList.Add(float.Parse(s));
                }
                return new DicomFloatingPointSingle(tag, fList.ToArray());
            }

            else if (vr == DicomVR.IS)
            {
                return new DicomIntegerString(tag, list.ToArray());
            }

            else if (vr == DicomVR.LO)
            {
                return new DicomLongString(tag, DicomEncoding.Default, list.ToArray());
            }

            else if (vr == DicomVR.LT)
            {
                return new DicomLongText(tag, list.Cast<string>().First());
            }

            //else if (vr == DicomVR.OB)
            //{
            //    return new DicomOtherByte(tag, EmptyBuffer.Value);
            //}

            else if (vr == DicomVR.OD)
            {
                List<Double> dList = new List<double>();
                foreach (string s in list)
                {
                    dList.Add(Double.Parse(s));
                }
                return new DicomOtherDouble(tag, dList.ToArray());
            }

            else if (vr == DicomVR.OF)
            {
                List<float> fList = new List<float>();
                foreach (string s in list)
                {
                    fList.Add(float.Parse(s));
                }
                return new DicomOtherFloat(tag, fList.ToArray());
            }

            //else if (vr == DicomVR.OW)
            //{
            //    return new DicomOtherWord(tag, EmptyBuffer.Value);
            //}

            else if (vr == DicomVR.PN)
            {
                if (DHelper.PersonNameEncodingRule.Enable)
                {
                    DPersonName2 name = DPersonName2.FromDicomString(list.First());
                    if (name == null) return new DicomPersonName(tag, DicomEncoding.Default, EmptyBuffer.Value);
                    return new DicomPersonName(tag, DicomEncoding.Default, name.ToNamestring(new DPersonNameEncodingRule()));
                }
                else
                {
                    return new DicomPersonName(tag, DicomEncoding.Default, list.ToArray());
                }
                
            }

            else if (vr == DicomVR.SH)
            {
                return new DicomShortString(tag, DicomEncoding.Default, list.ToArray());
            }

            else if (vr == DicomVR.SL)
            {
                List<int> iList = new List<int>();
                foreach (string s in list)
                {
                    iList.Add(int.Parse(s));
                }
                return new DicomSignedLong(tag, iList.ToArray());
            }

            //else if (vr == DicomVR.SQ)
            //{
            //    return new DicomSequence(tag);
            //}

            else if (vr == DicomVR.SS)
            {
                List<short> sList = new List<short>();
                foreach (string s in list)
                {
                    sList.Add(short.Parse(s));
                }
                return new DicomSignedShort(tag, sList.ToArray());
            }

            else if (vr == DicomVR.ST)
            {
                return new DicomShortText(tag, DicomEncoding.Default, list.First());
            }

            else if (vr == DicomVR.TM)
            {
                return new DicomTime(tag, list.ToArray());
            }

            else if (vr == DicomVR.UC)
            {
                return new DicomUnlimitedCharacters(tag, list.ToArray());
            }

            else if (vr == DicomVR.UI)
            {
                return new DicomUniqueIdentifier(tag, list.ToArray());
            }

            else if (vr == DicomVR.UL)
            {
                List<uint> uList = new List<uint>();
                foreach (string s in list)
                {
                    uList.Add(uint.Parse(s));
                }
                return new DicomUnsignedLong(tag, uList.ToArray());
            }

            //else if (vr == DicomVR.UN)
            //{
            //    return new DicomUnknown(tag, EmptyBuffer.Value);
            //}

            else if (vr == DicomVR.UR)
            {
                return new DicomUniversalResource(tag, list.First());
            }

            else if (vr == DicomVR.US)
            {
                List<ushort> uList = new List<ushort>();
                foreach (string s in list)
                {
                    uList.Add(ushort.Parse(s));
                }
                return new DicomUnsignedShort(tag, uList.ToArray());
            }

            else if (vr == DicomVR.UT)
            {
                return new DicomUnlimitedText(tag, list.First());
            }

            return null;
        }

        private void setValue(string value)
        {
            DicomVR vr = _element.ValueRepresentation;
                DicomTag tag = _element.Tag;
            if (value == null || value.Length < 1)
            {
                _element = GetDicomItemEmpty(vr, tag);
            }
            else
            {
                string[] slist = value.Split(DHelper.ValueDelimiter);

                _element = GetDicomItemValue(vr, tag, slist);
            }
        }

        public override string ToString()
        {
            if (_element != null)
                return "(" + DHelper.Int2HexString(_element.Tag.Group) + ","
                    + DHelper.Int2HexString(_element.Tag.Element) + ") "
                    + getVRString() + " "
                    + getValue() + "\t\t\t"
                    + DHelper.GetTagName(DHelper.GE2uint(_element.Tag.Group, _element.Tag.Element));

            return "";
        }

        private string getValue()
        {
            try
            {
                int vm = 0;
                DVR vr = DHelper.ConvertToDVR(DicomVR.UN);
                if (_element != null)
                {
                    vr = DHelper.ConvertToDVR(_element.ValueRepresentation);
                    //vm = _element.VM;
                }
                if (vr == DVR.UN)
                {
                    if (_element != null)        // do not implement _element yet, because error handler is using Element with (0000,0000), fix this in the future 20080312
                    {
                        return "";
                    }
                }

                // ----------------

                switch (vr)
                {
                    case DVR.UN:
                    case DVR.OB:
                    case DVR.OF:
                    case DVR.OW:
                    case DVR.SQ:
                        {
                            return "";
                        }

                    //VRs of LO, LT, SH, ST, and UT plus PN for person name are subject to character set extensions.  
                    //VRs AE, AS, CS, DS, and IS always use the default character set and have further restrictions on their contents.
                    case DVR.LO:
                    case DVR.LT:
                    case DVR.SH:
                    case DVR.ST:
                    case DVR.UT:
                    default:
                        {
                            DicomMultiStringElement dicomMultiStringElement = _element as DicomMultiStringElement;

                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < dicomMultiStringElement.Count; i++) sb.Append(dicomMultiStringElement.Get<string>(i)).Append(DHelper.ValueDelimiter);
                            return sb.ToString().TrimEnd(DHelper.ValueDelimiter);
                        }
                }
                
            }
            catch (Exception err)
            {

                DElement errEle = new DElement(_tag, _vr);
                LogMgt.Logger.Write(err.ToString());
            }
            return "";
        }

        private string _value;
        public string Value
        {
            get
            {
                _value = getValue();
                return _value;
            }
            set
            {
                _value = value;
                setValue(_value);
            }
        }

        protected override object GetValueEx(string name)
        {
            if (name == "Tag")
            {
                string str = DHelper.Int2HexString(Tag);
                if (str.Length == 4) str = "0000" + str;
                else if (str.Length == 5) str = "000" + str;
                else if (str.Length == 6) str = "00" + str;
                else if (str.Length == 7) str = "0" + str;
                return str;
            }
            else if (name == "Value")
            {
                // 20110119  To avoid XML-invalid characters such as &,<,> be copied from the DICOM tag into the XML string
                //return string.Format("<![CDATA[{0}]]>", base.GetValueEx(name));
                return XMLTransformer.ConvertToXMLEscapeString(base.GetValueEx(name).ToString());
            }
            else
            {
                return base.GetValueEx(name);
            }
        }

        protected override bool SetValueEx(string name, string newvalue)
        {
            if (name == "Tag")
            {
                Tag = DHelper.HexString2Int(newvalue);
                return true;
            }
            else
            {
                return base.SetValueEx(name, newvalue);
            }
        }

        [XNode(false)]
        public readonly bool IsRef;

        [XNode(false)]
        public readonly DValueType Type = DValueType.Unknown;

        [XNode(false)]
        public string Catagory = "";

        [XNode(false)]
        public int VM
        {
            get
            {
                if (_element != null)
                {
                    if (_element.ValueRepresentation == DicomVR.AE)
                    {
                        return (_element as DicomApplicationEntity).Count;
                    }
                    else if (_element.ValueRepresentation == DicomVR.AS)
                    {
                        return (_element as DicomAgeString).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.AT)
                    {
                        return (_element as DicomAttributeTag).Count;
                    }
                    else if (_element.ValueRepresentation == DicomVR.CS)
                    {
                        return (_element as DicomCodeString).Count;
                    }
                    else if (_element.ValueRepresentation == DicomVR.DA)
                    {
                        return (_element as DicomDate).Count;
                    }
                    else if (_element.ValueRepresentation == DicomVR.DS)
                    {
                        return (_element as DicomDecimalString).Count;
                    }
                    else if (_element.ValueRepresentation == DicomVR.DT)
                    {
                        return (_element as DicomDateTime).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.FD)
                    {
                        return (_element as DicomFloatingPointDouble).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.FL)
                    {
                        return (_element as DicomFloatingPointSingle).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.IS)
                    {
                        return (_element as DicomIntegerString).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.LO)
                    {
                        return (_element as DicomLongString).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.LT)
                    {
                        return (_element as DicomLongText).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.OB)
                    {
                        return (_element as DicomOtherByte).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.OD)
                    {
                        return (_element as DicomOtherDouble).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.OF)
                    {
                        return (_element as DicomOtherFloat).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.OW)
                    {
                        return (_element as DicomOtherWord).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.PN)
                    {
                        return (_element as DicomPersonName).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.SH)
                    {
                        return (_element as DicomShortString).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.SL)
                    {
                        return (_element as DicomSignedLong).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.SQ)
                    {
                        return (_element as DicomSequence).Items.Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.SS)
                    {
                        return (_element as DicomSignedShort).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.ST)
                    {
                        return (_element as DicomShortText).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.TM)
                    {
                        return (_element as DicomTime).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.UC)
                    {
                        return (_element as DicomUnlimitedCharacters).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.UI)
                    {
                        return (_element as DicomUniqueIdentifier).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.UL)
                    {
                        return (_element as DicomUnsignedLong).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.UN)
                    {
                        return (_element as DicomUnknown).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.UR)
                    {
                        return (_element as DicomUniversalResource).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.US)
                    {
                        return (_element as DicomUnsignedShort).Count;
                    }

                    else if (_element.ValueRepresentation == DicomVR.UT)
                    {
                        return (_element as DicomUnlimitedText).Count;
                    }
                };
                return -99;
            }
        }

        private DVR _vr = DVR.UN;
        public DVR VR
        {
            get
            {
                _vr = getVR();
                return _vr;
            }
            set
            {
                _vr = value;
                setVR(_vr);
            }
        }

        private int _tag;
        public int Tag
        {
            get
            {
                _tag = getTag();
                return _tag;
            }
            set
            {
                _tag = value;
                setTag(_tag);
            }
        }

        public DicomTag DicomTag
        {
            get
            {
                return _element.Tag;
            }
        }

        //private string _value;
        //public string Value
        //{
        //    get
        //    {
        //        _value = getValue();
        //        return _value;
        //    }
        //    set
        //    {
        //        _value = value;
        //        setValue(_value);
        //    }
        //}

        private DSequence _sequence;
        [Browsable(false)]
        public DSequence Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_element != null) _element = null;
        }

        #endregion
    }
}
