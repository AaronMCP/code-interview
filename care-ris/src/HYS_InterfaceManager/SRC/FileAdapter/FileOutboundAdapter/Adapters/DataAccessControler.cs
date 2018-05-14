using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.Data;
using System.Collections;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.Xml;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Logging;
using HYS.FileAdapter.Common;
using HYS.FileAdapter.Configuration;

namespace HYS.FileAdapter.FileOutboundAdapter
{
    public class DataAccessControler
    {
        #region Contructor
        public DataAccessControler()
        {
            InitializeTimer();
        }
        private void InitializeTimer()
        {
            _timer = new System.Timers.Timer();
            _timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
        }
        #endregion

        #region Timer Control

        public void Start()
        {
            Program.Log.Write("================ Adapter Start... =================");            
                        
            _timer.Interval = FileOutboundAdapterConfigMgt.FileOutAdapterConfig.OutGeneralParams.TimerInterval;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();

            Program.Log.Write("===============  Adapter Stop ====================");
           
        }

        public bool IsRunning
        {
            get
            {
                return _timer.Enabled;
            }
        }

        
        

        private System.Timers.Timer _timer;        

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {

                _timer.Enabled = false;
                Program.Log.Write("-----------Query data started...----------\r\n");
                if(Program.bRunSingle)
                    Program.Log.Write("----------- RunSignle = true; Single mode ----------\r\n");
                else
                    Program.Log.Write("----------- RunSignle = false;Integration mode ----------\r\n");
                QueryData();
                Program.Log.Write("-----------Query data finished.-----------\r\n");                
            }
            catch (Exception Ex)
            {
                Program.Log.Write(Ex);
            }
            finally
            {
                _timer.Enabled = true;
            }
        }

        #endregion

        #region Data Control

        public event DataRequestEventHanlder OnDataRequest=null;
        public event DataDischargeEventHanlder OnDataDischarge=null;

        /// <summary>
        /// 
        /// 1.Load Channel list,Mapping list
        /// 2.Request dataset by sigle channel
        /// 3.set mapping to 3rd procedure or insert record to 3rd table
        /// 
        /// </summary>
        private void QueryData()
        {
            DataSet dsCriteria = new DataSet();
            DataSet dsResult=null;

            
            foreach (FileOutChannel ch in FileOutboundAdapterConfigMgt.FileOutAdapterConfig.OutboundChanels)
            {
                try
                {
                    if (!ch.Enable) continue;
                    if (OnDataRequest != null)
                        dsResult = OnDataRequest((IOutboundRule)ch.Rule, null);

                    //---- debug...
                    if (Program.bRunSingle)
                    {
                        dsResult = new DataSet();
                        if (ch.ChannelName == "CH_PATIENT")
                            dsResult.ReadXml("c:\\filemiddle\\testPatient.xml");
                        if (ch.ChannelName == "CH_ORDER")
                            dsResult.ReadXml("c:\\filemiddle\\testOrder.xml");
                        if (ch.ChannelName == "CH_REPORT")
                            dsResult.ReadXml("c:\\filemiddle\\testReport.xml");
                    }
                    //----- debug end----

                    if (dsResult == null)
                    {
                        Program.Log.Write("Receive record dataset is null\r\n");
                        continue;
                    }
                    else
                    {
                        int count = 0;
                        if (dsResult.Tables.Count > 0) count = dsResult.Tables[0].Rows.Count;
                        Program.Log.Write(LogType.Debug, "\r\nChannel " + ch.ChannelName + " Receive record count: " + count.ToString());
                        if (Program.Log.LogTypeLevel == LogType.Debug)
                        {
                            string path = Application.StartupPath + "\\debugdata\\";
                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);

                            dsResult.WriteXml(path + DateTime.Now.ToString("hh-mm-ss") + ".xml");
                        }

                        WriteData(ch, dsResult);
                    }
                }
                catch (Exception err)
                {
                    Program.Log.Write(err);
                }
            }       
        }


        private void WriteData(FileOutChannel ch, DataSet ds)
        {
            if (ds == null || ds.Tables.Count < 1) return;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string sFileName = FileOutboundAdapterConfigMgt.BuildOutFileName(ch,dr);
                IniFile2 iniF = new IniFile2(sFileName, FileOutboundAdapterConfigMgt.FileOutAdapterConfig.OutGeneralParams.CodePageName);      
                foreach (FileOutQueryResultItem item in ch.Rule.QueryResult.MappingList)
                {
                    if (item.ThirdPartyDBPatamter.FileFieldFlag)
                    {
                        string fname = WriteFieldData(item.ThirdPartyDBPatamter, ds, dr);
                        iniF.WriteValue(item.ThirdPartyDBPatamter.SectionName, item.ThirdPartyDBPatamter.FieldName, fname);
                    }
                    else
                    {
                        if (item.ThirdPartyDBPatamter.FieldName.Trim() == "") continue;
                        
                        string sFieldName = item.TargetField;
                        
                        string val = "";
                        if (!Convert.IsDBNull(dr[sFieldName]))
                            val = Convert.ToString(dr[sFieldName]);

                        iniF.WriteValue(item.ThirdPartyDBPatamter.SectionName, item.ThirdPartyDBPatamter.FieldName, val);
                    }
                    
                }

                //---- debug...
                if (!Program.bRunSingle)
                {
                    if (OnDataDischarge != null)
                        OnDataDischarge(new string[] { dr["Data_ID"].ToString() });
                }
                //----- debug end----

            }        
        }

        private string WriteFieldData(ThrPartyDBParamterExOut param,DataSet ds, DataRow dr)
        {
            string fname = FileOutboundAdapterConfigMgt.BuildOutFieldFileName(param);            

            string sTemplate = param.FileFieldTemplate;

            Program.Log.Write(LogType.Debug, "Begin Write File Field "+param.FieldName+" Data To:"+fname);

            string sVal, sFieldName;

            foreach (GWDataDBField f in param.FileFields)
            {
                sFieldName = f.GetFullFieldName().Replace(".","_");
                
                if (ds.Tables[0].Columns.IndexOf(sFieldName) < 0)
                {
                    sVal = "";
                    Program.Log.Write(LogType.Error, "Field "+ sFieldName +"is not exist on result!");
                }
                else if (Convert.IsDBNull(dr[sFieldName]))
                    sVal = "";
                else
                    sVal = Convert.ToString(dr[sFieldName]);

                sTemplate = sTemplate.Replace("[%"+sFieldName+"%]", sVal);
                
            }

            //using (FileStream fs = new FileStream(fname,FileMode.Create))
            //{
            //    StreamWriter sw = new StreamWriter(fs);

            //    sw.Write(sTemplate);

            //    sw.Close();
            //}

            Encoding t = IniFile2.GetEncoder(FileOutboundAdapterConfigMgt.FileOutAdapterConfig.OutGeneralParams.CodePageName);
            IniFile2.WriteFile(fname, sTemplate, t);

            Program.Log.Write(LogType.Debug, "Finish Write File Field Data" );

            return fname;
        }

        #endregion
    }

       
}
