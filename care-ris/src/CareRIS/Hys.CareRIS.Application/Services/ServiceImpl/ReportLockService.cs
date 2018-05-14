using AutoMapper;
using C1.C1Report;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Dtos.Report;
using Hys.CareRIS.Application.Dtos.UserManagement;
using Hys.CareRIS.Application.Mappers;
using Hys.CareRIS.EnterpriseLib;
using Hys.CrossCutting.Common.Utils;
using Hys.Platform.Application;
using Hys.Platform.Domain.Ris;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Interface;
using Hys.CareRIS.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web.Security;
using System.Xml;

namespace Hys.CareRIS.Application.Services.ServiceImpl
{
    public class ReportLockService : DisposableServiceBase, IReportLockService
    {
        private IRisProContext _dbContext;

        public ReportLockService(
            IRisProContext dbContext)
        {
            _dbContext = dbContext;
            AddDisposableObject(dbContext);
        }
        #region lock
        
        public SyncDto GetLock(string orderID, LockType lockType)
        {
            var sync = _dbContext.Set<Sync>().Where(p => p.OrderID == orderID && p.SyncType == (int)lockType).FirstOrDefault();
            if (sync != null)
            {
                if (sync.ProcedureIDs == null)
                {
                    sync.ProcedureIDs = "";
                }
                return Mapper.Map<Sync, SyncDto>(sync);
            }
            return null;
        }
        public LockDto GetLockByPatientId(string patientId)
        {
            var sync = (from s in _dbContext.Set<Sync>()
                         join u in _dbContext.Set<User>() on s.Owner equals u.UniqueID
                         select new SyncDto
                         {
                             OrderID = s.OrderID,
                             SyncType = s.SyncType,
                             Owner = s.Owner,
                             OwnerName = u.LocalName,
                             LoginName = u.LoginName,
                             OwnerIP = s.OwnerIP,
                             CreateTime = s.CreateTime,
                             ModuleID = s.ModuleID,
                             PatientID = s.PatientID,
                             AccNo = s.AccNo,
                             PatientName = s.PatientName,
                             Counter = s.Counter,
                             ProcedureIDs = s.ProcedureIDs,
                             Domain = s.Domain
                         }).FirstOrDefault();

            LockDto lockitem = new LockDto();
            if (sync != null)
            {
                lockitem.IsLock = true;
                lockitem.Lock = sync;    
                return lockitem;
            }
            return lockitem;
        }
        public SyncDto GetLock(string orderID)
        {
            var sync = _dbContext.Set<Sync>().Where(p => p.OrderID == orderID).FirstOrDefault();
            if (sync != null)
            {
                if (sync.ProcedureIDs == null)
                {
                    sync.ProcedureIDs = "";
                }
                return Mapper.Map<Sync, SyncDto>(sync);
            }
            return null;
        }
        private void AddLock(SyncDto syncDto)
        {
            Sync sync = Mapper.Map<SyncDto, Sync>(syncDto);
            _dbContext.Set<Sync>().Add(sync);

            _dbContext.SaveChanges();
        }

        private void UpdateLock(SyncDto syncDto)
        {

            var sync = _dbContext.Set<Sync>().Where(p => p.OrderID == syncDto.OrderID && p.SyncType == syncDto.SyncType).FirstOrDefault();
            //new data
            sync.Owner = syncDto.Owner;
            sync.OwnerIP = syncDto.OwnerIP;
            sync.CreateTime = syncDto.CreateTime;
            sync.ModuleID = syncDto.ModuleID;
            sync.PatientName = syncDto.PatientName;
            sync.AccNo = syncDto.AccNo;
            sync.Counter = syncDto.Counter;
            sync.ProcedureIDs = syncDto.ProcedureIDs;
            sync.Domain = syncDto.Domain;

            _dbContext.SaveChanges();
        }

        public void DeleteLock(string orderID, LockType lockType, string userID)
        {
            List<Procedure> procedureList = _dbContext.Set<Procedure>().Where(p => p.OrderID == orderID).ToList();
            List<string> procedureIDs = new List<string>();
            foreach (Procedure procedure in procedureList)
            {
                procedureIDs.Add(procedure.UniqueID);
            }

            if (procedureIDs.Count > 0)
            {
                DeleteLock(orderID, procedureIDs, lockType, userID);
            }
        }

        public void DeleteLockByReportID(string reportID, LockType lockType, string userID)
        {
            List<Procedure> procedureList = _dbContext.Set<Procedure>().Where(p => p.ReportID == reportID).ToList();
            List<string> procedureIDs = new List<string>();
            foreach (Procedure procedure in procedureList)
            {
                procedureIDs.Add(procedure.UniqueID);
            }

            if (procedureIDs.Count > 0)
            {
                DeleteLock(procedureList[0].OrderID, procedureIDs, lockType, userID);
            }
        }

        public void DeleteLock(string orderID, List<string> procedureIDs, LockType lockType, string userID)
        {
            var sync = _dbContext.Set<Sync>().Where(p => p.OrderID == orderID && p.SyncType == (int)lockType).FirstOrDefault();

            if (sync != null)
            {
                if (sync.ProcedureIDs == null)
                {
                    sync.ProcedureIDs = "";
                }
                foreach (string item in procedureIDs)
                {
                    int indexStart = sync.ProcedureIDs.IndexOf(item + "&" + userID);
                    if (indexStart > -1)
                    {
                        int indexEnd = sync.ProcedureIDs.IndexOf("|", indexStart);
                        if (indexEnd > -1)
                        {
                            sync.ProcedureIDs = sync.ProcedureIDs.Substring(0, indexStart) + sync.ProcedureIDs.Substring(indexEnd + 1);
                        }
                        else
                        {
                            sync.ProcedureIDs = sync.ProcedureIDs.Substring(0, indexStart);
                        }
                    }
                }

                if (sync.ProcedureIDs.EndsWith("|"))
                {
                    sync.ProcedureIDs = sync.ProcedureIDs.Trim("|".ToCharArray());
                }
                if (sync.ProcedureIDs == "")
                {
                    _dbContext.Set<Sync>().Remove(sync);
                }

                _dbContext.SaveChanges();

            }
        }

        public void DeleteLockByUserID(LockType lockType, string userID)
        {
            List<Sync> syncList = _dbContext.Set<Sync>().Where(p => p.ProcedureIDs.Contains(userID) && p.SyncType == (int)lockType).ToList();

            foreach (Sync sync in syncList)
            {
                int index = sync.ProcedureIDs.IndexOf(userID);
                while (index > -1)
                {

                    string left = sync.ProcedureIDs.Substring(0, index);
                    int indexStart = left.LastIndexOf("|");
                    if (indexStart < 0)
                    {
                        indexStart = 0;
                    }
                    int indexEnd = sync.ProcedureIDs.IndexOf("|", indexStart + 1);
                    if (indexEnd > 0)
                    {
                        sync.ProcedureIDs = sync.ProcedureIDs.Substring(0, indexStart) + sync.ProcedureIDs.Substring(indexEnd + 1);
                    }
                    else
                    {
                        sync.ProcedureIDs = sync.ProcedureIDs.Substring(0, indexStart);
                    }

                    if (sync.ProcedureIDs.EndsWith("|"))
                    {
                        sync.ProcedureIDs = sync.ProcedureIDs.Trim("|".ToCharArray());
                    }
                    index = sync.ProcedureIDs.IndexOf(userID);
                }

                if (sync.ProcedureIDs == "")
                {
                    _dbContext.Set<Sync>().Remove(sync);
                }
            }

            _dbContext.SaveChanges();

        }

        public void DeleteLockByUserID(LockType lockType, string userID, string ip)
        {
            string userIDIP = userID + "&" + ip;
            List<Sync> syncList = _dbContext.Set<Sync>().Where(p => p.ProcedureIDs.Contains(userIDIP) && p.SyncType == (int)lockType).ToList();

            foreach (Sync sync in syncList)
            {
                int index = sync.ProcedureIDs.IndexOf(userIDIP);
                while (index > -1)
                {

                    string left = sync.ProcedureIDs.Substring(0, index);
                    int indexStart = left.LastIndexOf("|");
                    if (indexStart < 0)
                    {
                        indexStart = 0;
                    }
                    int indexEnd = sync.ProcedureIDs.IndexOf("|", indexStart + 1);
                    if (indexEnd > 0)
                    {
                        sync.ProcedureIDs = sync.ProcedureIDs.Substring(0, indexStart) + sync.ProcedureIDs.Substring(indexEnd + 1);
                    }
                    else
                    {
                        sync.ProcedureIDs = sync.ProcedureIDs.Substring(0, indexStart);
                    }

                    if (sync.ProcedureIDs.EndsWith("|"))
                    {
                        sync.ProcedureIDs = sync.ProcedureIDs.Trim("|".ToCharArray());
                    }
                    index = sync.ProcedureIDs.IndexOf(userID);
                }

                if (sync.ProcedureIDs == "")
                {
                    _dbContext.Set<Sync>().Remove(sync);
                }
            }

            List<Sync> syncList2 = _dbContext.Set<Sync>().Where(p => (p.ProcedureIDs == null || p.ProcedureIDs == "") && p.SyncType == (int)lockType
                && p.Owner == userID && p.OwnerIP == ip).ToList();
            foreach (Sync sync in syncList2)
            {
                _dbContext.Set<Sync>().Remove(sync);
            }

            _dbContext.SaveChanges();

        }

        public bool AddLockByProcedureID(string procedureID, string userName, string userID, string domain, string site, string ip)
        {
            Procedure procedure = _dbContext.Set<Procedure>().Where(p => p.UniqueID == procedureID).FirstOrDefault();
            var result = GetLock(procedure.OrderID, LockType.Register);

            if (result == null)
            {
                List<String> procedureList = new List<string>();
                procedureList.Add(procedure.UniqueID);

                SyncDto syncDto = CreateLock(procedure.OrderID, procedureList, userName, userID, domain, site, ip);

                AddLock(syncDto);
                result = GetLock(procedure.OrderID, LockType.Register);
                if (result != null)
                {
                    return true;
                }
            }
            //owner has the lock
            else if (result.ProcedureIDs.Contains(procedureID + "&" + userID))
            {
                return true;
            }
            // no the lock
            else if (!result.ProcedureIDs.Contains(procedureID))
            {
                string ownerIP = ip;
                if (result.ProcedureIDs == "")
                {
                    result.ProcedureIDs += procedureID + "&" + userID + "&" + ownerIP;
                }
                else
                {
                    result.ProcedureIDs += "|" + procedureID + "&" + userID + "&" + ownerIP;
                }
                //add lock
                UpdateLock(result);
                return true;
            }
            return false;
        }

        public bool AddLockByOrderID(string orderID, string userName, string userID, string domain, string site, string ip)
        {
            List<Procedure> procedureList = _dbContext.Set<Procedure>().Where(p => p.OrderID == orderID).ToList();
            var result = true;
            foreach (Procedure procedure in procedureList)
            {
                AddLockByProcedureID(procedure.UniqueID, userName, userID, domain, site, ip);
                if (!result)
                {
                    return false;
                }
            }

            return result;
        }

        public bool AddLockByReportID(string reportID, string userName, string userID, string domain, string site, string ip)
        {
            List<Procedure> procedureList = _dbContext.Set<Procedure>().Where(p => p.ReportID == reportID).ToList();
            var result = true;
            foreach (Procedure procedure in procedureList)
            {
                result = AddLockByProcedureID(procedure.UniqueID, userName, userID, domain, site, ip);
                if (!result)
                {
                    return false;
                }
            }
            return result;
        }

        private SyncDto CreateLock(string orderID, List<string> procedures, string userName, string userID, string domain, string site, string ip)
        {
            Order order = _dbContext.Set<Order>().Where(p => p.UniqueID == orderID).FirstOrDefault();
            Patient patient = _dbContext.Set<Patient>().Where(p => p.UniqueID == order.PatientID).FirstOrDefault();
            string rpguids = "";
            string ownerIP = ip;
            foreach (string procedureID in procedures)
            {
                rpguids += procedureID + "&" + userID + "&" + ownerIP + "|";
            }
            if (rpguids.EndsWith("|"))
            {
                rpguids = rpguids.Trim("|".ToCharArray());
            }

            SyncDto syncDto = new SyncDto();
            syncDto.OrderID = orderID;
            //syncDto.SyncType = (int)LockType.Register;
            syncDto.SyncType = (int)LockType.Register;
            syncDto.Owner = userID;
            syncDto.OwnerIP = ownerIP;
            syncDto.CreateTime = DateTime.Now;
            syncDto.ModuleID = "0400";
            syncDto.PatientName = patient.LocalName;
            syncDto.PatientID = patient.PatientNo;
            syncDto.AccNo = order.AccNo;
            syncDto.Counter = 1;
            syncDto.ProcedureIDs = rpguids;
            syncDto.Domain = domain;

            return syncDto;
        }

        public bool HasLockForUser(string orderID, string userID)
        {
            //has lock and user can not delete lock
            bool hasLock = false;
            var sync = _dbContext.Set<Sync>().Where(p => p.OrderID == orderID).FirstOrDefault();

            if (sync != null)
            {
                if (string.IsNullOrEmpty(sync.ProcedureIDs))
                {
                    if (sync.Owner != userID)
                    {
                        hasLock = true;
                    }
                }
                else
                {
                    if (sync.ProcedureIDs.IndexOf(userID) < 0)
                    {
                        hasLock = true;
                    }
                }
            }

            return hasLock;
        }
        #endregion

    }
}
