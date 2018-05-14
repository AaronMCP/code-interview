using System;
using System.Collections.Generic;
using HYS.Common.Dicom;
using HYS.Common.Objects.Rule;
using HYS.DicomAdapter.Common;
using HYS.DicomAdapter.MWLServer.Dicom;

namespace HYS.DicomAdapter.MWLServer.Objects
{
    public class MWLQueryResultItem : QueryResultItem, IDicomMappingItem
    {
        private DPath _dpath = new DPath();
        public DPath DPath
        {
            get { return _dpath; }
            set { _dpath = value; }
        }

        public bool IsValid()
        {
            return DPath.VR != DVR.Unknown;
        }
        public IDicomMappingItem Clone()
        {
            MWLQueryResultItem itemB = new MWLQueryResultItem();
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
            TargetField = DicomMappingHelper.DPath2DataColumnName(DPath);
        }

        public MWLQueryResultItem()
        {
        }
        public MWLQueryResultItem(DPath dcmPath)
        {
            DPath = dcmPath;
            base.TargetField = DicomMappingHelper.DPath2DataColumnName(dcmPath);
            GWDataDBField = GWDataDBField.Null;
            Translating.Type = TranslatingType.FixValue;
            Translating.ConstValue = "";
        }
        public MWLQueryResultItem(DPath dcmPath, GWDataDBField gwField)
        {
            DPath = dcmPath;
            GWDataDBField = gwField;
            base.TargetField = DicomMappingHelper.DPath2DataColumnName(dcmPath);
        }

        public static MWLQueryResultItem[] GetDefault()
        {
            DPath[] path = WorklistIOD.CreateDPath(true);
            List<MWLQueryResultItem> list = new List<MWLQueryResultItem>();
            foreach (DPath p in path)
            {
                list.Add(new MWLQueryResultItem(p));
            }
            return list.ToArray();
        }
    }
}
