using System;
using System.Data;
using System.Text;
using System.Timers;
using System.IO;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Logging;
using HYS.FileAdapter.Common;
using HYS.FileAdapter.Configuration;


namespace HYS.FileAdapter.FileInboundAdapter
{
    public class DataAccessControler
    {
        #region Constructor
        public DataAccessControler()
        {
            try
            {
                InitializeTimer();                
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message, "DataAccessControler", true);
            }
        }

        private System.Timers.Timer _timer = null;
        private void InitializeTimer()
        {
            _timer = new System.Timers.Timer();
            _timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
        }
        #endregion

        #region Timer Control

        public void Start()
        {
            _timer.Interval = FileInboundAdapterConfigMgt.FileInAdapterConfig.InGeneralParams.TimerInterval;
            _timer.Start();                        
            Program.Log.Write("================ Adapter Start... ===================\r\n");
        }

        public void Stop()
        {
            _timer.Stop();
            Program.Log.Write("================ Adapter Stop     ===================\r\n");
            
        }

        public bool IsRunning
        {
            get
            {
                return _timer.Enabled;
            }
        }

        
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                _timer.Enabled = false;
                Program.Log.Write("----------------- ProcessData started...-------------------\r\n");
                ProcessData();
                Program.Log.Write("----------------- ProcessData finished. -------------------\r\n");
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

        public event DataReceiveEventHandler OnDataReceived=null;

        private void ProcessData()
        {
            // Load File List
            string[] flist = LoadFiles();
            if (flist == null) return;

            // Treat File
            foreach (string fname in flist)
            {
                bool r = TreatFile(fname);
                PostTreatFile(fname, r);                             
            }
        }

        private string[] LoadFiles()
        {
            // Path
            string sPath = FileInboundAdapterConfigMgt.FileInAdapterConfig.InGeneralParams.FilePath;
            if (sPath.Trim() == "")
                sPath = Application.StartupPath;
            if (sPath.Substring(sPath.Length - 1, 1) != "\\")
                sPath = sPath + "\\";
            //Token
            string sFilePrefix = FileInboundAdapterConfigMgt.FileInAdapterConfig.InGeneralParams.FilePrefix;
            string sFileSuffix = FileInboundAdapterConfigMgt.FileInAdapterConfig.InGeneralParams.FileSuffix;
            if (sFileSuffix.Trim() == "")
                sFileSuffix = ".*";
            else if (sFileSuffix.IndexOf(".") < 0)
                sFileSuffix = "." + sFileSuffix;

            string sFileToken = sFilePrefix + "*" + sFileSuffix;
            
            //Files
            string[] files = Directory.GetFiles(sPath, sFileToken);

            if (files.Length < 1)
            {
                Program.Log.Write(LogType.Warning, "Cannot find any file.Directory:"+sPath+";FileToken:"+sFileToken);
            }

            SortFile(files);    //2007-3-5 add sort by file name

            //Add Path
            for (int i = 0; i < files.Length; i++)
                if(Path.GetDirectoryName(files[i]).Length<=0)
                   files[i] = sPath + files[i];

            return files;
        }

        /// <summary>
        /// sort rule: datetime asc,name asc
        /// </summary>
        /// <param name="files"></param>
        private void SortFile(string[] files)
        {
            SortedList<string, string> slFiles = new SortedList<string, string>();
            foreach (string f in files)
                slFiles.Add(f, f);
            for (int i = 0; i < files.Length; i++)
                files[i] = slFiles.Values[i];
        }

        /// <summary>
        /// one file only be treated by one channel,
        /// when find the first appricate channel,
        /// system submit the file to framework, and return the result
        /// 
        /// </summary>
        /// <param name="fname"></param>
        /// <returns></returns>
        private bool TreatFile(string fname)
        {
            Program.Log.Write(LogType.Debug, "Treating File:" + fname + "...");

            foreach(FileInChannel ch in FileInboundAdapterConfigMgt.FileInAdapterConfig.InboundChanels)
            {
                try
                {
                    if (ch.Rule.QueryCriteria.Type == QueryCriteriaRuleType.SQLStatement)
                        continue;

                    if (ch.Rule.QueryCriteria.Type != QueryCriteriaRuleType.None)
                    {
                        //Check Criteria
                        if (!CheckCriteria(fname, ch)) continue;
                    }

                    Program.Log.Write(LogType.Debug, "    Find Match Channel:" + ch.ChannelName + ".");

                    Program.Log.Write(LogType.Debug, "    Read Data...");
                    //read file
                    DataSet data = ReadData(fname, ch);
                    if (data == null)
                    {
                        Program.Log.Write(LogType.Debug, "    Read Data End. No Record!");
                        continue;
                    }
                    if (data.Tables.Count < 1)
                    {
                        Program.Log.Write(LogType.Debug, "    Read Data End. No Record!");
                        continue;
                    }
                    if (data.Tables[0].Rows.Count < 1)
                    {
                        Program.Log.Write(LogType.Debug, "    Read Data End. No Record!");
                        continue;
                    }


                    Program.Log.Write(LogType.Debug, "    Read Data Success. ");
                    // Debug to xml file
                    string sDumpFile = "test.xml";
                    if (Program.Log.LogTypeLevel == LogType.Debug)
                    {
                        string path = Application.StartupPath + "\\debugdata\\";
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        sDumpFile = path + Path.GetFileNameWithoutExtension(fname) + "_" + ch.ChannelName + "_" + DateTime.Now.ToString("hh-mm-ss") + ".xml";
                        if (File.Exists(sDumpFile))
                            File.Delete(sDumpFile);
                        data.WriteXml(sDumpFile);
                        Program.Log.Write(LogType.Debug, "    DataSet is dumped to " + sDumpFile);
                    }


                    //debug
                    //string sTemp = "c:\\filemiddle\\"+Path.GetFileName(sDumpFile);
                    //if (File.Exists(sTemp))
                    //    File.Delete(sTemp);
                    //data.WriteXml(sTemp);
                    //return true;

                    bool r = false;
                    // Submit data into framework
                    if (OnDataReceived == null)
                    {
                        Program.Log.Write(LogType.Debug, "    Treat File End. OnDataReceived==null. Return false");
                        return false;
                    }
                    else
                    {
                        r = OnDataReceived(ch.Rule, data);
                    }

                    if (r)
                        Program.Log.Write(LogType.Debug, "    Treat File End Successfully.");
                    else
                        Program.Log.Write(LogType.Debug, "    Treat File End. Framework return false;");

                    return r;
                }
                catch (Exception err)
                {
                    Program.Log.Write(err);
                }
            }

            Program.Log.Write(LogType.Debug, "Treat File End. No Match channel!");
            return false;            
        }

        private bool CheckCriteria(string fname, FileInChannel ch)
        {
            IniFile2 iniF = new IniFile2(fname, FileInboundAdapterConfigMgt.FileInAdapterConfig.InGeneralParams.CodePageName);
            List<KKMath.LogicItem> ilist = new List<KKMath.LogicItem>();
            //int iMatchCount = 0; 
            foreach (FileInQueryCriteriaItem item in ch.Rule.QueryCriteria.MappingList)
            {
                string sSectionName = item.ThirdPartyDBPatamter.SectionName;
                string sFieldName   = item.ThirdPartyDBPatamter.FieldName;
                string criteriaValue = item.Translating.ConstValue;

                string v = iniF.ReadValue(sSectionName, sFieldName,"");

                QueryCriteriaType type = item.Type;
                bool value = KKMath.OperationIsTrue(v, item.Operator, criteriaValue);
                KKMath.LogicItem i = new KKMath.LogicItem(value, type);
                ilist.Add(i);

                //if (KKMath.OperationIsTrue(v, item.Operator, criteriaValue))
                //    iMatchCount++;
            }

            bool ret = KKMath.JoinLogicItem(ilist);
            return ret;

            //FileInQueryCriteriaItem it = null;
            //if (ch.Rule.QueryCriteria.MappingList.Count > 0) it = (FileInQueryCriteriaItem)ch.Rule.QueryCriteria.MappingList[0];

            //if (iMatchCount == ch.Rule.QueryCriteria.MappingList.Count)
            //    return true;
            //else if (iMatchCount > 0 && it != null && it.Type == QueryCriteriaType.Or)
            //    return true;
            //else
            //    return false;
        }

        private string LoadFileContent(string fname)
        {
            string filename = fname;

            if (!Path.IsPathRooted(filename))
            {
                string path = FileInboundAdapterConfigMgt.FileInAdapterConfig.InGeneralParams.FilePath;
                if (path == null || path.Length < 1) path = Application.StartupPath;
                filename = path + "\\" + filename;
            }

            if (!File.Exists(filename))
            {
                Program.Log.Write("File is not found: " + filename);
                return fname;
            }
            else
            {
                //using (System.IO.FileStream fs = new FileStream(filename, FileMode.Open))
                //{
                //    //StreamReader sr = new StreamReader(fs);
                //    //string s = sr.ReadToEnd();
                //    //sr.Close();
                //    //return s;
                //}

                Encoding t = IniFile2.GetEncoder(FileInboundAdapterConfigMgt.FileInAdapterConfig.InGeneralParams.CodePageName);
                return IniFile2.ReadFile(filename, t);
            }
        }

        private DataSet ReadData(string fname, FileInChannel ch)
        {
            // Build DataSet Schema

            DataSet ds1 = new DataSet();
            DataTable table = new DataTable(fname);

            ds1.Tables.Add(table);

            foreach (FileInQueryResultItem item in ch.Rule.QueryResult.MappingList)
            {
                //table.Columns.Add(item.ThirdPartyDBPatamter.FieldName, typeof(System.String));
                table.Columns.Add(item.SourceField, typeof(System.String));
            }

            DataRow dr = table.NewRow();

            bool bIsValid = false;

            IniFile2 iniF = new IniFile2(fname, FileInboundAdapterConfigMgt.FileInAdapterConfig.InGeneralParams.CodePageName);

            foreach (FileInQueryResultItem item in ch.Rule.QueryResult.MappingList)
            {
                string val = iniF.ReadValue(item.ThirdPartyDBPatamter.SectionName, item.ThirdPartyDBPatamter.FieldName, "");
                if (item.ThirdPartyDBPatamter.FileFieldFlag)
                {
                    dr[item.SourceField] = LoadFileContent(val);
                }
                else
                    dr[item.SourceField] = val;

                if (val.Trim() != "")
                    bIsValid = true;
            }
            if (bIsValid)
                table.Rows.Add(dr);
            else
            {
                Program.Log.Write(LogType.Error, "Invalid file format." + "File Name:" + fname + ";Channel Name:" + ch.ChannelName+"\r\n");
            }

            return  ds1;

        }
        /// <summary>
        /// Delete,Move,or do none action
        /// </summary>
        /// <param name="fname"></param>
        private void PostTreatFile(string fname, bool bSuccess)
        {
            switch (FileInboundAdapterConfigMgt.FileInAdapterConfig.InGeneralParams.FileTreatTypeAfterRead)
            {
                case FileInGeneralParams.InFileTreatTypeAfterRead.None:
                    {
                        Program.Log.Write(LogType.Debug, "File "+ fname +" is keep original location!");
                        break;
                    }
                case FileInGeneralParams.InFileTreatTypeAfterRead.Delete:
                    {
                        File.Delete(fname);
                        Program.Log.Write(LogType.Debug, "File " + fname + " is Deleted!");
                        break;
                    }
                case FileInGeneralParams.InFileTreatTypeAfterRead.Move:
                    {
                        string sMovePath = FileInboundAdapterConfigMgt.FileInAdapterConfig.InGeneralParams.InFileMovePath;
                        if(sMovePath.Trim()=="")
                            sMovePath = Path.GetDirectoryName(fname)+"\\Move\\";
                        if ( ! Directory.Exists(sMovePath))
                            Directory.CreateDirectory(sMovePath);
                        if (sMovePath.Substring(sMovePath.Length - 1, 1) != "\\")
                            sMovePath += "\\";
                                               

                        //success file to success\$month$\;  failure file to failure\$month$\
                        if (bSuccess)
                            sMovePath +=  "success\\" + DateTime.Now.ToString("yyyyMM") + "\\";
                        else
                            sMovePath +=  "failure\\" + DateTime.Now.ToString("yyyyMM") + "\\";
                        
                        Program.Log.Write(LogType.Debug,"Full Move Destion Path:"+sMovePath);                        

                        if (!Directory.Exists(sMovePath))
                            Directory.CreateDirectory(sMovePath);

                        string sMoveFile = sMovePath + Path.GetFileName(fname);
                        if(File.Exists(sMoveFile))
                            File.Delete(sMoveFile);
                        File.Move(fname, sMoveFile);

                        Program.Log.Write(LogType.Debug, "File " + fname + " is move to " + sMoveFile);

                        break;
                    }
                default:
                    break;
            }
        }

       #endregion
    }
}
