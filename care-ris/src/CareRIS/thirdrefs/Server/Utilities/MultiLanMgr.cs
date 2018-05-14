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
/*   Date :   July. 07. 2006                                                */
/****************************************************************************/
#endregion

using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using Server.ClientFramework.Common.Data.MultiLan;

namespace Server.Utilities
{
    /// <summary>
    /// This Class is for providing multi-languange information at server side.
    /// It will be instantiated at the server side by a reference of a Application Variable,
    /// at the time web application start.
    /// When ever a call for it, it will check if there is the data of the ModuleID.
    /// If There is, return it, otherwise get the Data from disk.
    /// </summary>
    public sealed class MultiLanMgr : IDisposable
    {

        private static volatile MultiLanMgr instance;
        private static object syncRoot = new Object();

        private DsMultiLanRes m_dsMutiLanRes; // buffer
        private int m_LCID;
        private ArrayList m_ModulesLoaded;
        private string m_szResFilePath;


        /// <summary>
        /// Dispose all the buffer data
        /// </summary>
        /// 
        public void Dispose()
        {
            this.m_dsMutiLanRes.Dispose();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        private MultiLanMgr() 
        {
            this.m_szResFilePath = System.Configuration.ConfigurationManager.AppSettings["MutilanguageResPath"];
            this.m_dsMutiLanRes = new DsMultiLanRes();
            this.m_ModulesLoaded = new ArrayList();
            this.m_LCID = CultureInfo.CurrentCulture.LCID;
			ReadFromXml();
        }

        public static MultiLanMgr Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new MultiLanMgr();
                    }
                }

                return instance;
            }
        }

		//private void ReadFromXml(int ModuleID)
		//{
		//    string szModelID = Server.ClientFramework.Common.Functions.ToModuleIdString(ModuleID);
		//    this.ReadFromXml(szModelID);
		//}

        private void ReadFromXml(/*string ModuleID*/)
        {
			//if (!this.m_ModulesLoaded.Contains(ModuleID))
			//{
                lock (syncRoot)
                {
                    //string FileName = m_szResFilePath + "\\MultiLanRes\\" + m_LCID.ToString() + "\\" + ModuleID.ToString() + ".xml";
					string FileName = m_szResFilePath + "\\MultiLanRes\\" + m_LCID.ToString() + ".xml";
                    XmlDocument dataDoc = new XmlDocument();
                    DsStringRes Data = new DsStringRes();
                    try
                    {
                        dataDoc.Load(FileName);
                        XmlNodeReader reader = new XmlNodeReader(dataDoc);
                        Data.ReadXml(reader);
                        reader.Close();
                        //this.m_ModulesLoaded.Add(ModuleID);
                    }
                    catch (Exception ex)
                    {
                    }
                    //this.m_dsMutiLanRes.MergerFromStringRes(Data, this.m_LCID, ModuleID);
					this.m_dsMutiLanRes.MergerFromStringRes(Data, this.m_LCID);
                }
			//}
        }

        public string GetString(string Name, string ModuleID)
        {
            return this.GetString(Name, ModuleID, "");
        }

        public string GetString(string Name, string ModuleID, string DefaultValue)
        {
			//this.ReadFromXml(ModuleID);
			//DsMultiLanRes.MultiLanResRow row = this.m_dsMutiLanRes.MultiLanRes.FindByLCIDModuleIDName(this.m_LCID, ModuleID, Name);
			DsMultiLanRes.MultiLanResRow row = this.m_dsMutiLanRes.MultiLanRes.FindByLCIDName(this.m_LCID, Name);
			if (!(row == null))
                return row.Value;
            else
            {
                return DefaultValue;
            }
        }

        public string GetString(string Name, int ModuleID)
        {
            return this.GetString(Name, ModuleID, "");
        }

        public string GetString(string Name, int ModuleID, string DefaultValue)
        {
			//this.ReadFromXml(ModuleID);
			//string szModelID = Server.ClientFramework.Common.Functions.ToModuleIdString(ModuleID);
			//DsMultiLanRes.MultiLanResRow row = this.m_dsMutiLanRes.MultiLanRes.FindByLCIDModuleIDName(this.m_LCID, szModelID, Name);
			DsMultiLanRes.MultiLanResRow row = this.m_dsMutiLanRes.MultiLanRes.FindByLCIDName(this.m_LCID, Name);
			if (!(row == null))
                return row.Value;
            else
            {
                return DefaultValue;
            }
        }

    }
}
