using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LogServer;
using CommonGlobalSettings;
using System.Windows.Forms;
using DataAccessLayer;
using CommonGlobalSettings;

namespace Server.DAO.Common.Impl
{

    class OracleCommonFunctionImpl : AbstractCommonFunctionImpl
    {
        LogManager logger = new LogManager();
        public override DataTable GetBackupFileList(string stTables, bool bFailRetry, string stRetryFlag)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                if (bFailRetry == false)
                //get files that have not been downloaded
                {
                    //for requisition
                    string sql = string.Format("select 'trequisition' as tablename, RequisitionGUID as FileGUID, backupmark,backupcomment,filename, relativepath from trequisition where ((backupmark != '1' and backupmark != 'F' )  or backupmark is null )")
                        //for report files
                        + string.Format("union ")
                        + string.Format("select 'treportfile' as tablename, FileGUID,backupmark,backupcomment,filename, relativepath from treportfile where( (backupmark != '1' and backupmark != 'F'  ) or backupmark is null) ")
                        //for report printlog files
                        + string.Format("union ")
                        + string.Format("select 'treportprintlog' as tablename,FileGUID, backupmark,backupcomment,SnapShotSrvPath as filename, null as relativepath from treportprintlog where( (backupmark != '1' and backupmark != 'F' ) or backupmark is null) ");
                    logger.Info((long)ModuleEnum.CommonModule, ModuleInstanceName.CommonModule, 53, sql, Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(),
                        (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                    return dataAccess.ExecuteQuery(sql);
                }
                else
                //get files that fail to download sometime before
                {
                    //for requisition
                    string sql = string.Format("select 'trequisition' as tablename, RequisitionGUID as FileGUID, backupmark,backupcomment,filename, relativepath from trequisition where (backupmark != '1' and len(backupmark)>0)")
                        //for report files
                        + string.Format("union ")
                        + string.Format("select 'treportfile' as tablename, FileGUID,backupmark,backupcomment,filename, relativepath from treportfile where (backupmark != '1' and len(backupmark)>0)")
                        //for report printlog files
                        + string.Format("union ")
                        + string.Format("select 'treportprintlog' as tablename,FileGUID, backupmark,backupcomment,SnapShotSrvPath as filename, null as relativepath from treportprintlog where (backupmark != '1' and len(backupmark)>0)");
                    logger.Info((long)ModuleEnum.CommonModule, ModuleInstanceName.CommonModule, 53, sql, Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(),
                        (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                    return dataAccess.ExecuteQuery(sql);
                }
            }
            catch (Exception ex)
            {

                logger.Error((long)ModuleEnum.CommonModule, ModuleInstanceName.CommonModule, 53, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return null;
        }
        public override bool GetStaff(string strDegreeName, DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();

            try
            {
                strDegreeName.Trim();

                string strSQL;
                if (strDegreeName.Length == 0)
                {
                    //throw new Exception("DegreeName must not be null");
                    strSQL = string.Format("Select DISTINCT  tUser.LocalName,tUser.loginname,tUser.UserGuid from tUser where tUser.DeleteMark=0 ", strDegreeName);
                }
                else
                {
                    strSQL = string.Format("Select DISTINCT  tUser.LocalName,tUser.loginname,tUser.UserGuid from tUser,tRole2User where (tUser.DeleteMark=0) and  (tUser.UserGuid=tRole2User.UserGuid) and INSTR((Select Value From tSystemProfile where Name='{0}'),tRole2User.RoleName)>0 ", strDegreeName);
                }


                oKodak.ExecuteQuery(strSQL, dt);
                dt.TableName = "Staff";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error(100, "CommonFunction", 53, ex.Message, Application.StartupPath.ToString(), new System.Diagnostics.StackFrame(true).GetFileName().ToString(),
                    Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));


            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        public override bool GetAllProcedureCode(DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();

            try
            {
                string strSQL = "SELECT	ProcedureCode,Description,EnglishDescription,ModalityType,BodyPart,CheckingItem,Charge,Preparation,Frequency,BodyCategory,Duration,FilmSpec,FilmCount,ContrastName,ContrastDose,ImageCount,ExposalCount,BookingNotice,ShortcutCode FROM tProcedureCode ";
                dt = oKodak.ExecuteQuery(strSQL);
                dt.TableName = "ProcedureCode";
                
                ds.Tables.Add(dt);
                ds.DataSetName = "ProcedureCodeDataSet";
                
            }
            catch (Exception ex)
            {
                bReturn = false;

                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }


    }
}
