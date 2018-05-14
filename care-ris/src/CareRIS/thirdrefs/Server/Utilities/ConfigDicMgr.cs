#region FileBanner
/****************************************************************************/
/*                                                                          */
/*                          Copyright 2006                                  */
/*                       EASTMAN KODAK COMPANY                              */
/*                        All Rights Reserved.                              */
/*                                                                          */
/*     This software contains proprietary and confidential information      */
/*     belonging to EASTMAN KODAK COMPANY, and may not be decompiled,       */
/*     disassembled, disclosed, reproduced or copied without the prior      */
/*     written consent of EASTMAN KODAK COMPANY.                            */
/*                                                                          */
/*   Author : Fred Li                                                       */
/****************************************************************************/

#endregion
using System;
using System.Collections.Generic;
using System.Text;
using Server.ClientFramework.Common.Data.Profile;
using Server.Business.ClientFramework;

namespace Server.Utilities
{
    /// <summary>
    /// Class for server side ConfigDic Manager
    /// It will:
    /// 1.  Load the data from DB ConfigDic, where Type = 2
    /// 2.  Merge Data from XML file on the server machine
    ///     (Location: Web.Config: Key = SeverConfigDicFilePath )
    /// And then give method to access the values in it.
    /// </summary>
    public class ConfigDicMgr : IDisposable
    {
        private static volatile ConfigDicMgr instance;
        private static object syncRoot = new Object();

        private DsConfigDic m_dsConfigDic;

        private string m_szCfgFilePath;

        /// <summary>
        /// Dispose all the buffer data
        /// </summary>
        /// 
        public void Dispose()
        {
            this.m_dsConfigDic.Dispose();
        }

        private ConfigDicMgr()
        {
            this.m_szCfgFilePath = System.Configuration.ConfigurationManager.AppSettings["ConfigDicFilePath"];
            this.m_dsConfigDic = new DsConfigDic();

            ConfigDic obj = new ConfigDic();
            DsConfigDic ds = obj.Load(2);
            this.m_dsConfigDic.Merge(ds);

            DsConfigDic ds1 = new DsConfigDic();
            try
            {
                ds1.ReadXml(this.m_szCfgFilePath);
                this.m_dsConfigDic.Merge(ds1, false);
            }
            catch
            {
            }

        }

        public static ConfigDicMgr Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ConfigDicMgr();
                    }
                }

                return instance;
            }
        }

        public int GetConfigDicOrderingPos(string ConfigName, string ModuleID)
        {
            DsConfigDic.ConfigDicRow Row = GetConfigDicRow(ConfigName, ModuleID);
            if (Row != null)
                return Row.OrderingPos;
            else
                return 0;
        }

        public int GetConfigDicOrderingPos(string ConfigName, int ModuleID)
        {
            DsConfigDic.ConfigDicRow Row = GetConfigDicRow(ConfigName, ModuleID);
            if (Row != null)
                return Row.OrderingPos;
            else
                return 0;
        }

        public int GetConfigDicExportable(string ConfigName, string ModuleID)
        {
            DsConfigDic.ConfigDicRow Row = GetConfigDicRow(ConfigName, ModuleID);
            if (Row != null)
                return Row.Exportable;
            else
                return 0;
        }

        public int GetConfigDicExportable(string ConfigName, int ModuleID)
        {
            DsConfigDic.ConfigDicRow Row = GetConfigDicRow(ConfigName, ModuleID);
            if (Row != null)
                return Row.Exportable;
            else
                return 0;
        }

        public int GetConfigDicIsHidden(string ConfigName, string ModuleID)
        {
            DsConfigDic.ConfigDicRow Row = GetConfigDicRow(ConfigName, ModuleID);
            if (Row != null)
                return Row.IsHidden;
            else
                return 0;
        }

        public int GetConfigDicIsHidden(string ConfigName, int ModuleID)
        {
            DsConfigDic.ConfigDicRow Row = GetConfigDicRow(ConfigName, ModuleID);
            if (Row != null)
                return Row.IsHidden;
            else
                return 0;
        }

        public int GetConfigDicInheritance(string ConfigName, string ModuleID)
        {
            DsConfigDic.ConfigDicRow Row = GetConfigDicRow(ConfigName, ModuleID);
            if (Row != null)
                return Row.Inheritance;
            else
                return 0;
        }

        public int GetConfigDicInheritance(string ConfigName, int ModuleID)
        {
            DsConfigDic.ConfigDicRow Row = GetConfigDicRow(ConfigName, ModuleID);
            if (Row != null)
                return Row.Inheritance;
            else
                return 0;
        }

        public string GetConfigDicValue(string ConfigName, string ModuleID)
        {
            DsConfigDic.ConfigDicRow Row = GetConfigDicRow(ConfigName, ModuleID);
            if (Row != null)
                return Row.Value;
            else
                return "";
        }

        public int SetConfigDicValue(string ConfigName, string ModuleID, string Value)
        {
            DsConfigDic.ConfigDicRow Row = GetConfigDicRow(ConfigName, ModuleID);
            if (Row != null)
            {
                if (Row.Value.CompareTo(Value) != 0)
                {
                    Row.Value = Value;

                    Server.Business.ClientFramework.ConfigDic obj = new ConfigDic();
                    obj.WriteConfigDicRow(Row, 2);

                }
            }
            else
            {
                DsConfigDic.ConfigDicRow row;
                row = this.m_dsConfigDic.ConfigDic.AddConfigDicRow(ConfigName, Value, ModuleID, "", 0, "", "", 0, 0, 1, 0, "0");
                Server.Business.ClientFramework.ConfigDic obj = new ConfigDic();
                obj.WriteConfigDicRow(row, 2);
            }
            return 0;
        }

        public string GetConfigDicValue(string ConfigName, int ModuleID)
        {
            DsConfigDic.ConfigDicRow Row = GetConfigDicRow(ConfigName, ModuleID);
            if (Row != null)
                return Row.Value;
            else
                return "";
        }

        public string GetConfigDicDomain(string ConfigName, string ModuleID)
        {
            DsConfigDic.ConfigDicRow Row = GetConfigDicRow(ConfigName, ModuleID);
            if (Row != null)
                return Row.Domain;
            else
                return "";
        }

        public string GetConfigDicDomain(string ConfigName, int ModuleID)
        {
            DsConfigDic.ConfigDicRow Row = GetConfigDicRow(ConfigName, ModuleID);
            if (Row != null)
                return Row.Domain;
            else
                return "";
        }

        public string GetConfigDicPropertyDesc(string ConfigName, string ModuleID)
        {
            DsConfigDic.ConfigDicRow Row = GetConfigDicRow(ConfigName, ModuleID);
            if (Row != null)
                return Row.PropertyDesc;
            else
                return "";
        }

        public string GetConfigDicPropertyDesc(string ConfigName, int ModuleID)
        {
            DsConfigDic.ConfigDicRow Row = GetConfigDicRow(ConfigName, ModuleID);
            if (Row != null)
                return Row.PropertyDesc;
            else
                return "";
        }

        public DsConfigDic.ConfigDicRow GetConfigDicRow(string ConfigName, int ModuleID)
        {
            string szModelID = Server.ClientFramework.Common.Functions.ToModuleIdString(ModuleID);
            return this.GetConfigDicRow(ConfigName, szModelID);
        }

        public DsConfigDic.ConfigDicRow GetConfigDicRow(string ConfigName, string ModuleID)
        {
            DsConfigDic.ConfigDicRow Row = null;
            Row = this.m_dsConfigDic.ConfigDic.FindByConfigNameModuleID(ConfigName, ModuleID);
            return Row;
        }

    }
}
