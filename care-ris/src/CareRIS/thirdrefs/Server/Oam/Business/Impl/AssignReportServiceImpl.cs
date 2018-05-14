using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Kodak.GCRIS.Server.DAO.Oam;
using Kodak.GCRIS.Common.Model.Report;

namespace Kodak.GCRIS.Server.Business.Oam.Impl
{
    public class AssignReportServiceImpl : IAssignReportService
    {
        private IAssignReportDAO assignReportDAO = DataBasePool.Instance.GetDBProvider();

        #region IAssignReportService Members

        public DataSet GetUnarrangedDoctorList(string reportType)
        {
            try
            {
                return assignReportDAO.GetUnarrangedDoctorList(reportType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetArrangedDoctorList(string reportType)
        {
            try
            {
                return assignReportDAO.GetArrangedDoctorList(reportType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool InsertWriteReportDoctor(RISGC.AssignmentCommon.Entity.WriteReportDoctor doctor)
        {
            try
            {
                return assignReportDAO.InsertWriteReportDoctor(doctor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteWriteReportDoctor(string doctorGuid)
        {
            try
            {
                return assignReportDAO.DeleteWriteReportDoctor(doctorGuid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetAssignedReportCountByDoctor(string doctorGuid)
        {
            try
            {
                return assignReportDAO.GetAssignedReportCountByDoctor(doctorGuid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdatePreferredTypeByDoctor(RISGC.AssignmentCommon.Entity.WriteReportDoctor doctor)
        {
            try
            {
                return assignReportDAO.UpdatePreferredTypeByDoctor(doctor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetUnwrittenReportList(object param, bool isAssigned)
        {
            try
            {
                return assignReportDAO.GetUnwrittenReportList(param, isAssigned);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetLockInfoByRPGuid(string procedureGuid)
        {
            try
            {
                return assignReportDAO.GetLockInfoByRPGuid(procedureGuid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateRPOwner(RISGC.AssignmentCommon.Entity.UnwrittenReport unwrittenReport)
        {
            try
            {
                return assignReportDAO.UpdateRPOwner(unwrittenReport);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetUnassignedUnwrittenReportCount()
        {
            try
            {
                return assignReportDAO.GetUnassignedUnwrittenReportCount();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetArrangedDoctorInfoByGuid(string doctorGuid)
        {
            try
            {
                return assignReportDAO.GetArrangedDoctorInfoByGuid(doctorGuid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateWriteReportDoctorStatus(string doctorGuid, bool canReceiveReport)
        {
            try
            {
                return assignReportDAO.UpdateWriteReportDoctorStatus(doctorGuid, canReceiveReport);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetLogProfile(DateTime beginTime, DateTime endTime)
        {
            try
            {
                return assignReportDAO.GetLogProfile(beginTime, endTime);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetLogDetail(string doctorGuid, DateTime beginTime, DateTime endTime)
        {
            try
            {
                return assignReportDAO.GetLogDetail(doctorGuid, beginTime, endTime);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateWriteReportDoctorIMStatus(string doctorGuid, string imStatus)
        {
            try
            {
                return assignReportDAO.UpdateWriteReportDoctorIMStatus(doctorGuid, imStatus);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetOperationHistory(string operationType, string assigner, string assignee, DateTime beginTime, DateTime endTime)
        {
            try
            {
                return assignReportDAO.GetOperationHistory(operationType, assigner, assignee, beginTime, endTime);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

    }
}
