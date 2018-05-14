using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Dtos.Report;
using Hys.CareRIS.Application.Dtos.UserManagement;
using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace Hys.CareRIS.Application.Services
{
    public interface IReportLockService : IDisposable
    {
        SyncDto GetLock(string orderID, LockType lockType);
        SyncDto GetLock(string orderyID);
        void DeleteLock(string orderID, List<string> procedureIDs, LockType lockType, string userID);
        void DeleteLock(string orderID, LockType lockType, string userID);
        void DeleteLockByReportID(string reportID, LockType lockType, string userID);
        void DeleteLockByUserID(LockType lockType, string userID);
        void DeleteLockByUserID(LockType lockType, string userID, string ip);
        bool AddLockByProcedureID(string procedureID, string userName, string userID, string domain, string site, string ip);
        bool AddLockByOrderID(string orderID, string userName, string userID, string domain, string site, string ip);
        bool AddLockByReportID(string reportID, string userName, string userID, string domain, string site, string ip);
        bool HasLockForUser(string orderID, string userID);

        LockDto GetLockByPatientId(string patientId);
    }
}
