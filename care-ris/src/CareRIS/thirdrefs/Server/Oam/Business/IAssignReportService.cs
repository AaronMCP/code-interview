using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Kodak.GCRIS.Common.Model.Report;
using RISGC.AssignmentCommon.Entity;

namespace Kodak.GCRIS.Server.Business.Oam
{
    public interface IAssignReportService
    {
        DataSet GetUnarrangedDoctorList(string reportType);
        DataSet GetArrangedDoctorList(string reportType);
        bool InsertWriteReportDoctor(WriteReportDoctor doctor);
        bool DeleteWriteReportDoctor(string doctorGuid);
        int GetAssignedReportCountByDoctor(string doctorGuid);
        bool UpdatePreferredTypeByDoctor(WriteReportDoctor doctor);
        DataSet GetArrangedDoctorInfoByGuid(string doctorGuid);
        bool UpdateWriteReportDoctorStatus(string doctorGuid, bool canReceiveReport);

        DataSet GetUnwrittenReportList(object param, bool isAssigned);
        DataSet GetLockInfoByRPGuid(string procedureGuid);
        bool UpdateRPOwner(UnwrittenReport unwrittenReport);
        int GetUnassignedUnwrittenReportCount();
        DataSet GetLogProfile(DateTime beginTime, DateTime endTime);
        DataSet GetLogDetail(string doctorGuid, DateTime beginTime, DateTime endTime);
        bool UpdateWriteReportDoctorIMStatus(string doctorGuid, string imStatus);
        DataSet GetOperationHistory(string operationType, string assigner, string assignee, DateTime beginTime, DateTime endTime);
    }
}
