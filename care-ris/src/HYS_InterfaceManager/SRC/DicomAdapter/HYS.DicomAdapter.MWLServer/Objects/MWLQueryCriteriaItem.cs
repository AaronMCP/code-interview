using System;
using System.Collections.Generic;
using HYS.Common.Dicom;
using HYS.Common.Objects.Rule;
using HYS.DicomAdapter.Common;
using HYS.DicomAdapter.MWLServer.Dicom;

namespace HYS.DicomAdapter.MWLServer.Objects
{
    public class MWLQueryCriteriaItem : QueryCriteriaItem, IDicomMappingItem
    {
        private DPath _dpath = new DPath();
        public DPath DPath
        {
            get { return _dpath; }
            set { _dpath = value; }
        }

        public bool IsValid()
        {
            return !(DPath.VR != DVR.SQ &&
                    Translating.Type == TranslatingType.None &&
                    GWDataDBField.Table == GWDataDBTable.None);
        }
        public IDicomMappingItem Clone()
        {
            MWLQueryCriteriaItem itemB = new MWLQueryCriteriaItem();
            itemB.GWDataDBField = GWDataDBField.Clone();
            itemB.Translating = Translating.Clone();
            itemB.RedundancyFlag = RedundancyFlag;
            itemB.SourceField = SourceField;
            itemB.TargetField = TargetField;
            itemB.DPath = DPath.Clone();
            itemB.Operator = Operator;
            itemB.Type = Type;
            return itemB;
        }
        public void Refresh()
        {
            if (DHelper.IsStringLike(DPath.VR)) Operator = QueryCriteriaOperator.Like;
            SourceField = DicomMappingHelper.DPath2DataColumnName(DPath);
        }

        private void Initialize()
        {
            Type = QueryCriteriaType.And;
            if (DHelper.IsStringLike(DPath.VR)) Operator = QueryCriteriaOperator.Like;
            //if (DHelper.IsDateTime(DPath.VR)) RangeType = DRangeType.Range;
            DPath.Enable = false;
        }
        public MWLQueryCriteriaItem()
        {
            Type = QueryCriteriaType.And;
        }
        public MWLQueryCriteriaItem(DPath dcmPath)
        {
            DPath = dcmPath;
            base.SourceField = DicomMappingHelper.DPath2DataColumnName(dcmPath);
            GWDataDBField = GWDataDBField.Null;
            Translating.Type = TranslatingType.None;
            Initialize();
        }
        public MWLQueryCriteriaItem(DPath dcmPath, GWDataDBField gwField)
        {
            DPath = dcmPath;
            GWDataDBField = gwField;
            base.SourceField = DicomMappingHelper.DPath2DataColumnName(dcmPath);
            Initialize();
        }

        public static MWLQueryCriteriaItem[] GetDefault()
        {
            DPath[] path = WorklistIOD.CreateDPath(false);
            List<MWLQueryCriteriaItem> list = new List<MWLQueryCriteriaItem>();
            foreach (DPath p in path)
            {
                list.Add(new MWLQueryCriteriaItem(p));
            }
            return list.ToArray();
        }
    }
}
