using System;
using System.Collections.Generic;
using HYS.Common.Dicom;
using HYS.Common.Objects.Rule;
using HYS.DicomAdapter.Common;
using HYS.DicomAdapter.StorageServer.Dicom;

namespace HYS.DicomAdapter.StorageServer.Objects
{
    public class StorageItem : QueryResultItem, IDicomMappingItem
    {
        private DPath _dpath = new DPath();
        public DPath DPath
        {
            get { return _dpath; }
            set { _dpath = value; }
        }

        public bool IsValid()
        {
            return GWDataDBField.Table != GWDataDBTable.None;
        }
        public IDicomMappingItem Clone()
        {
            StorageItem itemB = new StorageItem();
            itemB.GWDataDBField = GWDataDBField.Clone();
            itemB.Translating = Translating.Clone();
            itemB.RedundancyFlag = RedundancyFlag;
            itemB.SourceField = SourceField;
            itemB.TargetField = TargetField;
            itemB.DPath = DPath.Clone();
            return itemB;
        }
        public void Refresh()
        {
            SourceField = DicomMappingHelper.DPath2DataColumnName(DPath);
        }

        public StorageItem()
        {
        }
        public StorageItem(DPath dcmPath)
        {
            DPath = dcmPath;
            SourceField = DicomMappingHelper.DPath2DataColumnName(DPath);// DPath.Seperator + dcmPath.Path;
            GWDataDBField = GWDataDBField.Null;
            Translating.Type = TranslatingType.None;
            Translating.ConstValue = "";
        }
        public StorageItem(DPath dcmPath, GWDataDBField gwField)
        {
            DPath = dcmPath;
            GWDataDBField = gwField;
            SourceField = DicomMappingHelper.DPath2DataColumnName(DPath); // DPath.Seperator + dcmPath.Path;
        }

        public static StorageItem[] GetDefault()
        {
            DPath[] path = StorageIOD.StorageDPath();
            List<StorageItem> list = new List<StorageItem>();
            foreach (DPath p in path)
            {
                list.Add(new StorageItem(p));
            }
            return list.ToArray();
        }
    }
}
