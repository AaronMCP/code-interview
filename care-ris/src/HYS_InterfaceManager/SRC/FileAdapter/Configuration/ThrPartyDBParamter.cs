using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using System.Text;
using System.Data.OleDb;

namespace HYS.FileAdapter.Configuration
{
    public class ThrPartyDBParamter : XObject
    {
        private int _fieldID = 0;
        public int FieldID
        {
            get { return _fieldID; }
            set { _fieldID = value; }
        }

        private OleDbType _fieldType = OleDbType.LongVarChar;
        public OleDbType FieldType
        {
            get { return _fieldType; }
            set { _fieldType = value; }
        }

        private string _fieldName = "";
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        private string _SectionName = "";
        public string SectionName
        {
            get { return _SectionName; }
            set { _SectionName = value; }
        }

        public ThrPartyDBParamter Clone()
        {
            ThrPartyDBParamter p = new ThrPartyDBParamter();
            p.FieldID = this.FieldID;
            p.FieldName = this.FieldName;
            p.FieldType = this.FieldType;
            p.SectionName = this.SectionName;
            return p;
        }

        public ThrPartyDBParamter Clone(ThrPartyDBParamter DestParam)
        {
            ThrPartyDBParamter p = new ThrPartyDBParamter();
            DestParam.FieldID = this.FieldID;
            DestParam.FieldName = this.FieldName;
            DestParam.FieldType = this.FieldType;
            DestParam.SectionName = this.SectionName;
            return DestParam;
        }
    }

    public class FileGeneralParam : XObject
    {
         #region File Name Setup
        private string _FilePrefix = "";
        public string FilePrefix
        {
            get { return _FilePrefix; }
            set { _FilePrefix = value; }
        }
        private string _FileDtFormat = "yyyyMMddHHmmss";
        public string FileDtFormat
        {
            get { return _FileDtFormat; }
            set { _FileDtFormat = value; }
        }
        private string _FileSuffix = ".TXT";
        public string FileSuffix
        {
            get { return _FileSuffix; }
            set { _FileSuffix = value; }
        }
        #endregion


        #region File Path
        private string _FilePath = "";
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }

        public FileGeneralParam Clone()
        {
            FileGeneralParam p = new FileGeneralParam();
            p.FilePrefix = this.FilePrefix;
            p.FileSuffix = this.FileSuffix;
            p.FileDtFormat = this.FileDtFormat;
            p.FilePath = this.FilePath;
            return p;
        }

        public FileGeneralParam Clone(FileGeneralParam DestParam)
        {
            DestParam.FilePrefix = this.FilePrefix;
            DestParam.FileSuffix = this.FileSuffix;
            DestParam.FileDtFormat = this.FileDtFormat;
            DestParam.FilePath = this.FilePath;
            return DestParam;
        }

        #endregion
    }

    public class ThrPartyDBParamterExOut : ThrPartyDBParamter
    {
        private bool _FileFieldFlag = false;
        public bool FileFieldFlag
        {
            get { return _FileFieldFlag; }
            set { _FileFieldFlag = value; }
        }

        FileGeneralParam _FileFieldParam = new FileGeneralParam();
        public FileGeneralParam FileFieldParam
        {
            get { return _FileFieldParam; }
            set { _FileFieldParam = value; }
        }

        /// <summary>
        /// Template for replace on adapter
        /// </summary>
        string _FileFieldTemplate = "";
        public string FileFieldTemplate
        {
            get { return _FileFieldTemplate; }
            set { _FileFieldTemplate = value; }
        }

        /// <summary>
        /// used to add appended field to framework config on configuration interface implemented,
        /// </summary>
        private XCollection<GWDataDBField> _FileFields = new XCollection<GWDataDBField>();
        public XCollection<GWDataDBField> FileFields
        {
            get { return _FileFields; }
            set { _FileFields = value; }
        }

        new public ThrPartyDBParamterExOut Clone()
        {
            ThrPartyDBParamterExOut pex = new ThrPartyDBParamterExOut();
            base.Clone(pex);

            pex.FileFieldFlag = this.FileFieldFlag;
            pex.FileFieldParam = this.FileFieldParam.Clone();

            pex.FileFieldTemplate = this.FileFieldTemplate; 
            foreach (GWDataDBField f in this.FileFields)
                pex.FileFields.Add(f);
            
            return pex;
        }

    }

    public class ThrPartyDBParamterExIn : ThrPartyDBParamter
    {
        private bool _FileFieldFlag = false;
        public bool FileFieldFlag
        {
            get { return _FileFieldFlag; }
            set { _FileFieldFlag = value; }
        }

        new public ThrPartyDBParamterExIn Clone()
        {
            ThrPartyDBParamterExIn pex = new ThrPartyDBParamterExIn();
            
            base.Clone(pex);
            pex.FileFieldFlag = this.FileFieldFlag;                    

            return pex;
        }


    }
}
