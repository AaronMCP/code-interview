using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Text;
using HYS.Common.Objects.Config;

namespace HYS.Common.Objects.Rule
{
    public class RuleControl
    {
        public static string GetDropTableSQL(string interfaceName)
        {
            if (interfaceName == null || interfaceName.Length < 1) return null;

            string strIndex = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Index);
            string strPatient = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Patient);
            string strOrder = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Order);
            string strReport = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Report);
            
            StringBuilder sb = new StringBuilder();

            //sb.AppendLine("USE GWDataDB");
            sb.AppendLine(GWDataDB.GetUseDataBaseSql());
            sb.AppendLine("");
            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + strIndex + "]') AND type in (N'U'))");
            sb.AppendLine("BEGIN");
            sb.AppendLine("DROP TABLE [dbo].[" + strIndex + "]");
            sb.AppendLine("END");
            sb.AppendLine("");
            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + strPatient + "]') AND type in (N'U'))");
            sb.AppendLine("BEGIN");
            sb.AppendLine("DROP TABLE [dbo].[" + strPatient + "]");
            sb.AppendLine("END");
            sb.AppendLine("");
            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + strOrder + "]') AND type in (N'U'))");
            sb.AppendLine("BEGIN");
            sb.AppendLine("DROP TABLE [dbo].[" + strOrder + "]");
            sb.AppendLine("END");
            sb.AppendLine("");
            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + strReport + "]') AND type in (N'U'))");
            sb.AppendLine("BEGIN");
            sb.AppendLine("DROP TABLE [dbo].[" + strReport + "]");
            sb.AppendLine("END");

            sb.AppendLine("");
            sb.AppendLine(GetDropGarbageCollectionSQL(interfaceName));

            return sb.ToString();
        }
        public static string GetCreateTableSQL(string interfaceName, GarbageRule gcRule)
        {
            if (interfaceName == null || interfaceName.Length < 1) return null;

            string strIndex = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Index);
            string strPatient = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Patient);
            string strOrder = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Order);
            string strReport = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Report);

            StringBuilder sb = new StringBuilder();

            //sb.AppendLine("USE GWDataDB");
            sb.AppendLine(GWDataDB.GetUseDataBaseSql());
            sb.AppendLine("SET ANSI_NULLS ON");
            sb.AppendLine("GO");
            sb.AppendLine("SET QUOTED_IDENTIFIER ON");
            sb.AppendLine("GO");
            sb.AppendLine("IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + strReport + "]') AND type in (N'U'))");
            sb.AppendLine("BEGIN");
            sb.AppendLine("CREATE TABLE [dbo].[" + strReport + "](");
            sb.AppendLine("	[DATA_ID] [uniqueidentifier] NOT NULL,");
            sb.AppendLine("	[DATA_DT] [datetime] NOT NULL,");

            GWDataDBField[] rfList = GWDataDBField.GetFieldsWithoutSort(GWDataDBTable.Report);
            foreach (GWDataDBField field in rfList)
            {
                if (field.IsAuto) continue;
                sb.AppendLine("	[" + field.FieldName + "] [nvarchar](max) NULL CONSTRAINT [DF_" + strReport + "_" + field.FieldName + "] DEFAULT (''),");
            }

            sb.AppendLine(" CONSTRAINT [PK_" + strReport + "] PRIMARY KEY CLUSTERED ");
            sb.AppendLine("(");
            sb.AppendLine("	[DATA_ID] ASC");
            sb.AppendLine(")WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]");
            sb.AppendLine(") ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]");
            sb.AppendLine("END");
            sb.AppendLine("GO");
            sb.AppendLine("SET ANSI_NULLS ON");
            sb.AppendLine("GO");
            sb.AppendLine("SET QUOTED_IDENTIFIER ON");
            sb.AppendLine("GO");
            sb.AppendLine("IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + strPatient + "]') AND type in (N'U'))");
            sb.AppendLine("BEGIN");
            sb.AppendLine("CREATE TABLE [dbo].[" + strPatient + "](");
            sb.AppendLine("	[DATA_ID] [uniqueidentifier] NOT NULL,");
            sb.AppendLine("	[DATA_DT] [datetime] NOT NULL,");

            GWDataDBField[] pfList = GWDataDBField.GetFieldsWithoutSort(GWDataDBTable.Patient);
            foreach (GWDataDBField field in pfList)
            {
                if (field.IsAuto) continue;
                sb.AppendLine("	[" + field.FieldName + "] [nvarchar](max) NULL CONSTRAINT [DF_" + strPatient + "_" + field.FieldName + "] DEFAULT (''),");
            }

            sb.AppendLine(" CONSTRAINT [PK_" + strPatient + "] PRIMARY KEY CLUSTERED ");
            sb.AppendLine("(");
            sb.AppendLine("	[DATA_ID] ASC");
            sb.AppendLine(")WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]");
            sb.AppendLine(") ON [PRIMARY]");
            sb.AppendLine("END");
            sb.AppendLine("GO");
            sb.AppendLine("SET ANSI_NULLS ON");
            sb.AppendLine("GO");
            sb.AppendLine("SET QUOTED_IDENTIFIER ON");
            sb.AppendLine("GO");
            sb.AppendLine("IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + strOrder + "]') AND type in (N'U'))");
            sb.AppendLine("BEGIN");
            sb.AppendLine("CREATE TABLE [dbo].[" + strOrder + "](");
            sb.AppendLine("	[DATA_ID] [uniqueidentifier] NOT NULL,");
            sb.AppendLine("	[DATA_DT] [datetime] NOT NULL,");

            GWDataDBField[] ofList = GWDataDBField.GetFieldsWithoutSort(GWDataDBTable.Order);
            foreach (GWDataDBField field in ofList)
            {
                if (field.IsAuto) continue;
                sb.AppendLine("	[" + field.FieldName + "] [nvarchar](max) NULL CONSTRAINT [DF_" + strOrder + "_" + field.FieldName + "] DEFAULT (''),");
            }

            sb.AppendLine(" CONSTRAINT [PK_" + strOrder + "] PRIMARY KEY CLUSTERED ");
            sb.AppendLine("(");
            sb.AppendLine("	[DATA_ID] ASC");
            sb.AppendLine(")WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]");
            sb.AppendLine(") ON [PRIMARY]");
            sb.AppendLine("END");
            sb.AppendLine("GO");
            sb.AppendLine("SET ANSI_NULLS ON");
            sb.AppendLine("GO");
            sb.AppendLine("SET QUOTED_IDENTIFIER ON");
            sb.AppendLine("GO");
            sb.AppendLine("IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + strIndex + "]') AND type in (N'U'))");
            sb.AppendLine("BEGIN");
            sb.AppendLine("CREATE TABLE [dbo].[" + strIndex + "](");
            sb.AppendLine("	[Data_ID] [uniqueidentifier] NOT NULL,");
            sb.AppendLine("	[Data_DT] [datetime] NOT NULL,");

            GWDataDBField[] ifList = GWDataDBField.GetFieldsWithoutSort(GWDataDBTable.Index);
            foreach (GWDataDBField field in ifList)
            {
                if (field.IsAuto) continue;
                sb.AppendLine("	[" + field.FieldName + "] [nvarchar](max) NULL CONSTRAINT [DF_" + strIndex + "_" + field.FieldName + "] DEFAULT (''),");
            }

            sb.AppendLine("	CONSTRAINT [PK_" + strIndex + "] PRIMARY KEY CLUSTERED ");
            sb.AppendLine("(");
	        sb.AppendLine("[DATA_ID] ASC");
            sb.AppendLine(")WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]");
            sb.AppendLine(") ON [PRIMARY]");
            sb.AppendLine("END");

            sb.AppendLine("");
            //sb.AppendLine(GetCreateGarbageCollectionSQL(interfaceName));
            sb.AppendLine(GetCreateGarbageCollectionSQL2(interfaceName, gcRule));

            return sb.ToString();
        }

        public static string GetCreateLUTSQL(LookupTable table)
        {
            if( table == null ) return null;

            string tableName = table.TableName;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(GetDropLUTSQL(table));

            sb.AppendLine("CREATE TABLE [dbo].[" + tableName + "](");
            sb.AppendLine("	[ID] [int] IDENTITY(1,1) NOT NULL,");
            sb.AppendLine("	[SourceValue] [nvarchar](max) COLLATE Chinese_PRC_CI_AS NULL,");
            sb.AppendLine("	[TargetValue] [nvarchar](max) COLLATE Chinese_PRC_CI_AS NULL,");
            sb.AppendLine("PRIMARY KEY CLUSTERED ");
            sb.AppendLine("(");
            sb.AppendLine("	[ID] ASC");
            sb.AppendLine(")WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]");
            sb.AppendLine(") ON [PRIMARY]");

            foreach (LookupItem item in table.Table)
            {
                sb.AppendLine("INSERT INTO " + tableName + " (SourceValue,TargetValue) VALUES ('" + item.SourceValue + "', '" + item.TargetValue + "')");
            }

            return sb.ToString();
        }
        public static string GetDropLUTSQL(LookupTable table)
        {
            if (table == null) return null;

            string tableName = table.TableName;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + tableName + "]') AND type in (N'U'))");
            sb.AppendLine("BEGIN");
            sb.AppendLine("	DROP TABLE [dbo].[" + tableName + "]");
            sb.AppendLine("END");

            return sb.ToString();
        }

        public static string GetCreateGarbageCollectionSPName(string interfaceName)
        {
            return "sp_" + interfaceName + "_GarbageCollection";
        }
        private static string GetDropGarbageCollectionSQL(string interfaceName)
        {
            string spName = GetCreateGarbageCollectionSPName(interfaceName);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].[" + spName + "]') AND type ='P')");
            sb.AppendLine("DROP PROCEDURE [dbo].[" + spName + "]");
            sb.AppendLine("go");
            sb.AppendLine("");

            return sb.ToString();
        }
        private static string GetCreateGarbageCollectionSQL2(string interfaceName, GarbageRule gcRule)
        {
            if (gcRule == null || interfaceName == null || interfaceName.Length < 1) return null;

            string spName = GetCreateGarbageCollectionSPName(interfaceName);
            string strIndexTable = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Index);
            string strPateintTable = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Patient);
            string strOrderTable = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Order);
            string strReportTable = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Report);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].[" + spName + "]') AND type ='P')");
            sb.AppendLine("DROP PROCEDURE [dbo].[" + spName + "]");
            sb.AppendLine("go");
            sb.AppendLine("");

            sb.AppendLine("-- =============================================");
            sb.AppendLine("-- Author: HYS IM 1.0.0.0 Rule Engine");
            sb.AppendLine("-- Create date: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            sb.AppendLine("-- Description: Garbage collection rule implementation for interface '" + interfaceName + "'");
            sb.AppendLine("-- =============================================");
            sb.AppendLine("CREATE PROCEDURE [dbo].[" + spName + "]");
            sb.AppendLine("@ToDateTime datetime,");
            sb.AppendLine("@result int output");
            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            //US29442
            #region
            sb.AppendLine();
            sb.AppendLine("DECLARE  @loopCount int");
            sb.AppendLine("SET @loopCount=1000");//1次500条，共50万条
            sb.AppendLine("While(@loopCount<>0)");
            sb.AppendLine("BEGIN");
            sb.AppendLine("SET @loopCount=@loopCount-1");
            sb.AppendLine();
            #endregion
            if ((gcRule.CheckExpireTime == false) &&
                (gcRule.CheckProcessFlag == false) &&
                (gcRule.AdditionalCriteria.Count < 1))
            {
                sb.AppendLine("SET @result = -1");
                sb.AppendLine("END");
                sb.AppendLine("GO");
                return sb.ToString();
            }

            #region data preparation

            bool bPatient = false;
            bool bOrder = false;
            bool bReport = false;

            foreach (QueryCriteriaItem i in gcRule.AdditionalCriteria)
            {
                if (i.Singal != QueryCriteriaSignal.None) continue;

                switch (i.GWDataDBField.Table)
                {
                    case GWDataDBTable.Patient:
                        bPatient = true;
                        break;
                    case GWDataDBTable.Order:
                        bOrder = true;
                        break;
                    case GWDataDBTable.Report:
                        bReport = true;
                        break;
                }

                if (i.Translating.Type == TranslatingType.FixValue)
                {
                    if (i.Singal == QueryCriteriaSignal.None &&
                        i.Operator == QueryCriteriaOperator.In) break;

                    i.SourceField = "FX_" + GetRandomNumber();
                    sb.AppendLine("	DECLARE @" + i.SourceField + " nvarchar(MAX)");
                    //sb.AppendLine("	SET @" + item.SourceField + " = '" + item.Translating.ConstValue.Replace("'", "''") + "'");
                    sb.AppendLine("	SET @" + i.SourceField + " = " + ParseConstValue(i.Translating.ConstValue));
                }
            }

            #endregion

            sb.AppendLine();
            sb.AppendLine("	SET NOCOUNT ON");
            sb.AppendLine("	SET DEADLOCK_PRIORITY LOW");
            sb.AppendLine("	BEGIN TRANSACTION");
            sb.AppendLine();
            sb.AppendLine("	SET ROWCOUNT " + gcRule.MaxRecordCountLimitation);
            sb.AppendLine("	SELECT " + strIndexTable + ".DATA_ID AS DATA_ID INTO #temp ");
            sb.AppendLine("	FROM " + strIndexTable);

            if (bPatient) sb.AppendLine("		JOIN " + strPateintTable + " ON(" + strIndexTable + ".DATA_ID = " + strPateintTable + ".DATA_ID)");
            if (bOrder) sb.AppendLine("		JOIN " + strOrderTable + " ON(" + strIndexTable + ".DATA_ID = " + strOrderTable + ".DATA_ID)");
            if (bReport) sb.AppendLine("		JOIN " + strReportTable + " ON(" + strIndexTable + ".DATA_ID = " + strReportTable + ".DATA_ID)");

            sb.AppendLine("	WHERE 1=1");

            if(gcRule.CheckProcessFlag) sb.AppendLine("		AND " + strIndexTable + ".PROCESS_FLAG = '" + RuleControl.ProcessFlagValueForProcessed + "'");
            if(gcRule.CheckExpireTime) sb.AppendLine("		AND " + strIndexTable + ".DATA_DT < @ToDateTime");

            #region additional criteria
            
            if (gcRule.AdditionalCriteria.Count > 0)
            {
                StringBuilder sbWhere = new StringBuilder();

                foreach (QueryCriteriaItem i in gcRule.AdditionalCriteria)
                {
                    if (i.Type == QueryCriteriaType.Or) sbWhere.Append(":");
                    if (i.Type == QueryCriteriaType.And) sbWhere.Append(";");

                    switch (i.Singal)
                    {
                        case QueryCriteriaSignal.None:
                            {
                                string fname = i.GWDataDBField.GetFullFieldName(interfaceName);
                                switch (i.Operator)
                                {
                                    case QueryCriteriaOperator.Like:
                                        {
                                            sbWhere.AppendLine(fname + " LIKE @" + i.SourceField);
                                            break;
                                        }
                                    case QueryCriteriaOperator.Equal:
                                        {
                                            sbWhere.AppendLine(fname + " = @" + i.SourceField);
                                            break;
                                        }
                                    case QueryCriteriaOperator.NotEqual:
                                        {
                                            sbWhere.AppendLine("(" + fname + " <> @" + i.SourceField + " OR " + fname + " IS NULL)");
                                            break;
                                        }
                                    case QueryCriteriaOperator.LargerThan:
                                        {
                                            sbWhere.AppendLine(fname + " > @" + i.SourceField);
                                            break;
                                        }
                                    case QueryCriteriaOperator.SmallerThan:
                                        {
                                            sbWhere.AppendLine(fname + " < @" + i.SourceField);
                                            break;
                                        }
                                    case QueryCriteriaOperator.EqualLargerThan:
                                        {
                                            sbWhere.AppendLine(fname + " >= @" + i.SourceField);
                                            break;
                                        }
                                    case QueryCriteriaOperator.EqualSmallerThan:
                                        {
                                            sbWhere.AppendLine(fname + " <= @" + i.SourceField);
                                            break;
                                        }
                                    case QueryCriteriaOperator.In:
                                        {
                                            sbWhere.AppendLine(fname + " IN (" + ParseInValue(i.Translating.ConstValue) + ")");
                                            break;
                                        }
                                }
                                break;
                            }
                        case QueryCriteriaSignal.LeftBracket:
                            {
                                sbWhere.Append(" (");
                                break;
                            }
                        case QueryCriteriaSignal.RightBracket:
                            {
                                sbWhere.Append(") ");
                                break;
                            }
                        case QueryCriteriaSignal.FreeText:
                            {
                                sbWhere.Append(" " + i.Translating.ConstValue.Replace("'", "''"));
                                break;
                            }
                    }
                }

                string strWhere = sbWhere.ToString();
                if (strWhere.Length > 0) strWhere = strWhere.TrimStart(':').TrimStart(';').Replace(":", "	OR ").Replace(";", "	AND ");
                if (strWhere.Length > 0) sb.AppendLine("		AND (" + strWhere + ")");
            }

            #endregion

            sb.AppendLine("	SET @result = @@ROWCOUNT");
            sb.AppendLine("	SET ROWCOUNT 0");
            //US29442
            #region
            sb.AppendLine();
            sb.AppendLine("IF(@result=0)");
            sb.AppendLine("BEGIN");
            sb.AppendLine("SET @loopCount=0");
            sb.AppendLine("END");
            sb.AppendLine();
            #endregion
            sb.AppendLine();
            sb.AppendLine("	DELETE FROM " + strReportTable + " WHERE EXISTS");
            sb.AppendLine("		(SELECT 1 FROM #temp AS TEMP WHERE " + strReportTable + ".DATA_ID=TEMP.DATA_ID)");
            sb.AppendLine("	DELETE FROM " + strOrderTable + " WHERE EXISTS");
            sb.AppendLine("		(SELECT 1 FROM #temp AS TEMP WHERE " + strOrderTable + ".DATA_ID=TEMP.DATA_ID)");
            sb.AppendLine("	DELETE FROM " + strPateintTable + " WHERE EXISTS");
            sb.AppendLine("		(SELECT 1 FROM #temp AS TEMP WHERE " + strPateintTable + ".DATA_ID=TEMP.DATA_ID)");
            sb.AppendLine("	DELETE FROM " + strIndexTable + " WHERE EXISTS");
            sb.AppendLine("		(SELECT 1 FROM #temp AS TEMP WHERE " + strIndexTable + ".DATA_ID=TEMP.DATA_ID)");
            sb.AppendLine();
            sb.AppendLine("	DROP TABLE #temp");
            sb.AppendLine("	COMMIT TRANSACTION");
            //US29442
            #region
            sb.AppendLine();
            sb.AppendLine("END");
            #endregion
            sb.AppendLine("END");
            sb.AppendLine("GO");
            return sb.ToString();
        }

        #region expired code
        //private static string GetCreateGarbageCollectionSQL(string interfaceName)
        //{
        //    if (interfaceName == null || interfaceName.Length < 1) return null;

        //    string spName = GetCreateGarbageCollectionSPName(interfaceName);

        //    StringBuilder sb = new StringBuilder();

        //    sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].[" + spName + "]') AND type ='P')");
        //    sb.AppendLine("DROP PROCEDURE [dbo].[" + spName + "]");
        //    sb.AppendLine("go");
        //    sb.AppendLine("");

        //    sb.AppendLine("-- =============================================");
        //    sb.AppendLine("-- Author: GC Gateway 2.0 Rule Engine");
        //    sb.AppendLine("-- Create date: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
        //    sb.AppendLine("-- Description: Garbage collection rule implementation for interface '" + interfaceName + "'");
        //    sb.AppendLine("-- =============================================");
        //    sb.AppendLine("CREATE PROCEDURE [dbo].[" + spName + "]");
        //    sb.AppendLine("-- Add the parameters for the stored procedure here");
        //    sb.AppendLine("@ProcessFlag nvarchar(max),");
        //    sb.AppendLine("@FromDateTime datetime,");
        //    sb.AppendLine("@ToDateTime datetime,");
        //    sb.AppendLine("@result int output");
        //    sb.AppendLine("AS");
        //    sb.AppendLine("BEGIN");
        //    sb.AppendLine("	-- SET NOCOUNT ON added to prevent extra result sets from");
        //    sb.AppendLine("	-- interfering with SELECT statements.");
        //    sb.AppendLine("	SET NOCOUNT ON;");
        //    sb.AppendLine("	");
        //    sb.AppendLine("	-- Insert statements for procedure here");
        //    sb.AppendLine("	SET @result = 1;");
        //    sb.AppendLine("	");
        //    sb.AppendLine("	IF( @ProcessFlag IS NULL )");
        //    sb.AppendLine("	BEGIN");
        //    sb.AppendLine("		IF( @FromDateTime IS NULL )");
        //    sb.AppendLine("		BEGIN");
        //    sb.AppendLine("			IF( @ToDateTime IS NULL )");
        //    sb.AppendLine("			BEGIN");
        //    sb.AppendLine("				SET @result = 0;");
        //    sb.AppendLine("			END");
        //    sb.AppendLine("			ELSE");
        //    sb.AppendLine("			BEGIN");
        //    sb.AppendLine(GetGarbageCollectionDeleteSQL(interfaceName, 0));
        //    sb.AppendLine("			END");
        //    sb.AppendLine("		END");
        //    sb.AppendLine("		ELSE");
        //    sb.AppendLine("		BEGIN");
        //    sb.AppendLine("			IF( @ToDateTime IS NULL )");
        //    sb.AppendLine("			BEGIN");
        //    sb.AppendLine(GetGarbageCollectionDeleteSQL(interfaceName, 1));
        //    sb.AppendLine("			END");
        //    sb.AppendLine("			ELSE");
        //    sb.AppendLine("			BEGIN");
        //    sb.AppendLine(GetGarbageCollectionDeleteSQL(interfaceName, 2));
        //    sb.AppendLine("			END");
        //    sb.AppendLine("		END");
        //    sb.AppendLine("	END");
        //    sb.AppendLine("	ELSE");
        //    sb.AppendLine("	BEGIN");
        //    sb.AppendLine("		IF( @FromDateTime IS NULL )");
        //    sb.AppendLine("		BEGIN");
        //    sb.AppendLine("			IF( @ToDateTime IS NULL )");
        //    sb.AppendLine("			BEGIN");
        //    sb.AppendLine(GetGarbageCollectionDeleteSQL(interfaceName, 3));
        //    sb.AppendLine("			END");
        //    sb.AppendLine("			ELSE");
        //    sb.AppendLine("			BEGIN");
        //    sb.AppendLine(GetGarbageCollectionDeleteSQL(interfaceName, 4));
        //    sb.AppendLine("			END");
        //    sb.AppendLine("		END");
        //    sb.AppendLine("		ELSE");
        //    sb.AppendLine("		BEGIN");
        //    sb.AppendLine("			IF( @ToDateTime IS NULL )");
        //    sb.AppendLine("			BEGIN");
        //    sb.AppendLine(GetGarbageCollectionDeleteSQL(interfaceName, 5));
        //    sb.AppendLine("			END");
        //    sb.AppendLine("			ELSE");
        //    sb.AppendLine("			BEGIN");
        //    sb.AppendLine(GetGarbageCollectionDeleteSQL(interfaceName, 6));
        //    sb.AppendLine("			END");
        //    sb.AppendLine("		END");
        //    sb.AppendLine("	END");
        //    sb.AppendLine("END");
        //    sb.AppendLine("GO");

        //    return sb.ToString();
        //}
        //private static string GetGarbageCollectionCriteriaSQL(string interfaceName, int typeID)
        //{
        //    string strDateTimeField = GWDataDBField.i_DataDateTime.GetFullFieldName(interfaceName);
        //    string strProcessFlagField = GWDataDBField.i_PROCESS_FLAG.GetFullFieldName(interfaceName);

        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("				(SELECT " + GWDataDBField.i_IndexGuid.GetFullFieldName(interfaceName));
        //    sb.AppendLine("				FROM " + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Index));
        //    switch (typeID)
        //    {
        //        case 0:
        //            sb.AppendLine("				WHERE " + strDateTimeField + " <= @ToDateTime)");
        //            break;
        //        case 1:
        //            sb.AppendLine("				WHERE " + strDateTimeField + " >= @FromDateTime)");
        //            break;
        //        case 2:
        //            sb.AppendLine("				WHERE (" + strDateTimeField + " <= @ToDateTime) AND (" + strDateTimeField + " >= @FromDateTime))");
        //            break;
        //        case 3:
        //            sb.AppendLine("				WHERE " + strProcessFlagField + " = @ProcessFlag)");
        //            break;
        //        case 4:
        //            sb.AppendLine("				WHERE (" + strDateTimeField + " <= @ToDateTime) ");
        //            sb.AppendLine("				 AND (" + strProcessFlagField + " = @ProcessFlag))");
        //            break;
        //        case 5:
        //            sb.AppendLine("				WHERE (" + strDateTimeField + " >= @FromDateTime)");
        //            sb.AppendLine("				 AND (" + strProcessFlagField + " = @ProcessFlag))");
        //            break;
        //        case 6:
        //            sb.AppendLine("				WHERE (" + strDateTimeField + " <= @ToDateTime) AND (" + strDateTimeField + " >= @FromDateTime)");
        //            sb.AppendLine("				 AND (" + strProcessFlagField + " = @ProcessFlag))");
        //            break;
        //    }
        //    return sb.ToString();
        //}
        //private static string GetGarbageCollectionDeleteSQL(string interfaceName, int typeID)
        //{
        //    string strCriteria = GetGarbageCollectionCriteriaSQL(interfaceName, typeID);

        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("				DELETE FROM " + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Report));
        //    sb.AppendLine("				WHERE " + GWDataDBField.r_DATA_ID.GetFullFieldName(interfaceName) + " IN ");
        //    sb.AppendLine(strCriteria);
        //    sb.AppendLine("				DELETE FROM " + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Order));
        //    sb.AppendLine("				WHERE " + GWDataDBField.o_DATA_ID.GetFullFieldName(interfaceName) + " IN ");
        //    sb.AppendLine(strCriteria);
        //    sb.AppendLine("				DELETE FROM " + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Patient));
        //    sb.AppendLine("				WHERE " + GWDataDBField.p_DATA_ID.GetFullFieldName(interfaceName) + " IN ");
        //    sb.AppendLine(strCriteria);
        //    sb.AppendLine("				DELETE FROM " + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Index));
        //    sb.AppendLine("				WHERE " + GWDataDBField.i_IndexGuid.GetFullFieldName(interfaceName) + " IN ");
        //    sb.AppendLine(strCriteria);
        //    return sb.ToString();
        //}
        #endregion

        public const string ProcessFlagValueForProcessed = "1";
        public static string GetSetProcessFlagSQL(string interfaceName, string[] dataIDList)
        {
            if (interfaceName == null || interfaceName.Length < 1 || dataIDList == null || dataIDList.Length < 1) return null;

            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("UPDATE " + GWDataDB.GetTableName(interfaceName,GWDataDBTable.Index) + " ");
            sb.AppendLine("SET " + GWDataDB.GetTableName(interfaceName,GWDataDBTable.Index) + ".PROCESS_FLAG = '" + ProcessFlagValueForProcessed + "' ");
            sb.AppendLine("WHERE " + GWDataDB.GetTableName(interfaceName,GWDataDBTable.Index) + ".Data_ID IN ");
            sb.AppendLine("( ");
            
            StringBuilder sbIDs = new StringBuilder();
            foreach( string id in dataIDList )
            {
                sbIDs.Append("'" + id + "',");
            }
            string strIDs = sbIDs.ToString();
            if (strIDs.Length > 0) strIDs = strIDs.TrimEnd(',').Replace(",", ", \r\n");
            sb.AppendLine(strIDs);
            
            sb.AppendLine(")");
            return sb.ToString();
        }

        private static int count;
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string GetRandomNumber()
        {
            string str = DateTime.Now.Ticks.ToString();
            return str + unchecked(count++).ToString();
        }

        private static string SQL_SIGNAL = "{SQL}";
        private static string SIGNAL_DISABLER = @"\";
        private static bool IsConstValueSQLStatement(string constValue)
        {
            if (constValue == null || constValue.Length < SQL_SIGNAL.Length) return false;
            string signal = constValue.Substring(0, SQL_SIGNAL.Length);
            if (signal == SQL_SIGNAL) return true;
            return false;
        }
        private static string GetConstValueSQLStatement(string constValue)
        {
            if (constValue == null || constValue.Length < SQL_SIGNAL.Length) return "";
            string sql = constValue.Substring(SQL_SIGNAL.Length);
            return sql;
        }
        private static string GetConstValueNonSQLStatement(string constValue)
        {
            string value = "";

            if (constValue != null && constValue.Length > 0)
            {
                string disableSignal = SIGNAL_DISABLER + SQL_SIGNAL;
                if (constValue.Length >= disableSignal.Length)
                {
                    string prefix = constValue.Substring(0, disableSignal.Length);
                    if (prefix == disableSignal)
                    {
                        value = constValue.Substring(SIGNAL_DISABLER.Length);
                    }
                    else
                    {
                        value = constValue;
                    }
                }
                else
                {
                    value = constValue;
                }
            }

            return "'" + value.Replace("'", "''") + "'";
        }
        public static string ParseConstValue(string constValue)
        {
            if (IsConstValueSQLStatement(constValue))
            {
                return GetConstValueSQLStatement(constValue);
            }
            else
            {
                return GetConstValueNonSQLStatement(constValue);
            }
        }
        public static string ParseInValue(string inValue)
        {
            if (inValue == null) return "";
            string val = "'" + inValue.Replace(",", "','") + "'";
            return val;
        }

        #region Inbound Rule Engine

        public static string ReturnValueParameterName = "___RET___";

        public static string GetInboundSP(string interfaceName, IInboundRule rule)
        {
            return GetInboundSP(interfaceName, rule, true);
        }
        public static string GetInboundSP(string interfaceName, IInboundRule rule, bool needRet)
        {
            if (rule == null || interfaceName == null || interfaceName.Length < 1) return null;

            string spName = GWDataDB.GetSPName(interfaceName, rule);

            #region data classification

            bool bPatient = false;
            bool bOrder = false;
            bool bReport = false;

            foreach (QueryResultItem i in rule.GetQueryResultItems())
            {
                switch (i.GWDataDBField.Table)
                {
                    case GWDataDBTable.Patient:
                        bPatient = true;
                        break;
                    case GWDataDBTable.Order:
                        bOrder = true;
                        break;
                    case GWDataDBTable.Report:
                        bReport = true;
                        break;
                }
            }

            #endregion

            StringBuilder sb = new StringBuilder();

            //sb.AppendLine("USE GWDataDB");
            sb.AppendLine("set ANSI_NULLS ON");
            sb.AppendLine("set QUOTED_IDENTIFIER ON");
            sb.AppendLine("go");
            sb.AppendLine("");

            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].[" + spName + "]') AND type ='P')");
            sb.AppendLine("DROP PROCEDURE [dbo].[" + spName + "]");
            sb.AppendLine("go");
            sb.AppendLine("");

            sb.AppendLine("-- =============================================");
            sb.AppendLine("-- Author: GC Gateway 2.0 Rule Engine");
            sb.AppendLine("-- Create date: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            sb.AppendLine("-- Description: Data inbound storage procedure for interface '" + interfaceName + "', rule '" + rule.RuleName + "'");
            sb.AppendLine("-- =============================================");
            sb.AppendLine("CREATE PROCEDURE [dbo].[" + spName + "]");

            #region storage procedure parameters

            StringBuilder sbParam = new StringBuilder();
            List<QueryResultItem> redundancyParamList = new List<QueryResultItem>();
            foreach (QueryResultItem i in rule.GetQueryResultItems())
            {
                bool redundancy = false;
                foreach (QueryResultItem mpi in redundancyParamList)
                {
                    if (mpi.SourceField == i.SourceField)
                    {
                        redundancy = true;
                        break;
                    }
                }
                if (redundancy) continue;
                redundancyParamList.Add(i);

                if (i.Translating.Type != TranslatingType.FixValue)
                {
                    sbParam.Append(" @" + i.SourceField + " nvarchar(MAX),");
                }
            }

            if (needRet) sbParam.Append(" @" + ReturnValueParameterName + " int output,");

            string strParam = sbParam.ToString();
            if (strParam.Length > 0) strParam = strParam.TrimEnd(',').Replace(",", ",\r\n");
            sb.AppendLine(strParam);

            //sb.AppendLine("	@DEVICE_DIRECT nvarchar(MAX),");
            //sb.AppendLine("	@INTERFACE_ID nvarchar(MAX),");
            //sb.AppendLine("	@INTERFACE_NAME nvarchar(MAX),");
            //sb.AppendLine("	@DEVICE_ID nvarchar(MAX)");

            #endregion

            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");

            sb.AppendLine("	-- SET NOCOUNT ON added to prevent extra result sets from");
            sb.AppendLine("	-- interfering with SELECT statements.");
            sb.AppendLine("	SET NOCOUNT ON;");
            sb.AppendLine("");

            sb.AppendLine("	-- Translation.");

            #region transaltion

            StringBuilder sbTranslation = new StringBuilder();

            foreach (MappingItem item in rule.GetQueryResultItems())
            {
                switch (item.Translating.Type)
                {
                    case TranslatingType.FixValue:
                        {
                            item.SourceField = "FX_" + GetRandomNumber();
                            sbTranslation.AppendLine("	DECLARE @" + item.SourceField + " nvarchar(MAX)");
                            //sbTranslation.AppendLine("	SET @" + item.SourceField + " = '" + item.Translating.ConstValue.Replace("'", "''") + "'");
                            sbTranslation.AppendLine("	SET @" + item.SourceField + " = " + ParseConstValue(item.Translating.ConstValue));
                            break;
                        }
                    case TranslatingType.DefaultValue:
                        {
                            sbTranslation.AppendLine("	IF @" + item.SourceField + " IS NULL OR @" + item.SourceField + " = ''");
                            sbTranslation.AppendLine("	BEGIN");
                            //sbTranslation.AppendLine("		SET @" + item.SourceField + " = '" + item.Translating.ConstValue.Replace("'", "''") + "'");
                            sbTranslation.AppendLine("		SET @" + item.SourceField + " = " + ParseConstValue(item.Translating.ConstValue));
                            sbTranslation.AppendLine("	END");
                            break;
                        }
                    case TranslatingType.LookUpTable:
                        {
                            sbTranslation.AppendLine("	SELECT @" + item.SourceField + " = TargetValue FROM " + item.Translating.LutName + " WHERE SourceValue = @" + item.SourceField);
                            break;
                        }
                    case TranslatingType.LookUpTableReverse:
                        {
                            sbTranslation.AppendLine("	SELECT @" + item.SourceField + " = SourceValue FROM " + item.Translating.LutName + " WHERE TargetValue = @" + item.SourceField);
                            break;
                        }
                }
            }

            sb.AppendLine(sbTranslation.ToString());

            #endregion

            sb.AppendLine("	-- Insert statements for procedure here");
            sb.AppendLine("	DECLARE @guid uniqueidentifier");
            sb.AppendLine("	DECLARE @datetime datetime");
            sb.AppendLine("");

            sb.AppendLine("	SET @guid = NEWID()");
            sb.AppendLine("	SET @datetime = GETDATE()");
            if (needRet) sb.AppendLine("	SET @" + ReturnValueParameterName + " = 0");
            sb.AppendLine("");

            #region redundancy filter

            List<QueryResultItem> redundancyFieldList = new List<QueryResultItem>();
            List<GWDataDBTable> redundancyFieldTableList = new List<GWDataDBTable>();

            foreach (QueryResultItem i in rule.GetQueryResultItems())
            {
                if (i.RedundancyFlag)
                {
                    redundancyFieldList.Add(i);
                    GWDataDBTable table = i.GWDataDBField.Table;
                    if (table != GWDataDBTable.Index && !redundancyFieldTableList.Contains(table))
                        redundancyFieldTableList.Add(table);
                }
            }

            if (redundancyFieldList.Count > 0)
            {
                StringBuilder sbFilter = new StringBuilder();
                string strFilter = GetSelectSQL(redundancyFieldList, redundancyFieldTableList, interfaceName);
                if (strFilter.Length > 0) sb.AppendLine("	IF NOT EXISTS (").Append(strFilter).AppendLine(")");
            }

            //  IF NOT EXISTS 
            //     (SELECT DATA_ID FROM PATIENT WHERE PATIENT.PATIENTID=@INTERFACE_ID)

            #endregion

            sb.AppendLine("	BEGIN");
            sb.AppendLine("	SET DEADLOCK_PRIORITY HIGH");
            sb.AppendLine("	BEGIN TRANSACTION");

            #region insert table

            if (bPatient) sb.AppendLine(GetInsertSQL(GWDataDBTable.Patient, interfaceName, rule));
            if (bOrder) sb.AppendLine(GetInsertSQL(GWDataDBTable.Order, interfaceName, rule));
            if (bReport) sb.AppendLine(GetInsertSQL(GWDataDBTable.Report, interfaceName, rule));
            sb.AppendLine(GetInsertSQL(GWDataDBTable.Index, interfaceName, rule));

            //sb.AppendLine("		INSERT INTO PATIENT ");
            //sb.AppendLine("			( DATA_ID, DATA_DT, PATIENT.PATIENTID,PATIENT.PATIENT_NAME )");
            //sb.AppendLine("			VALUES");
            //sb.AppendLine("			( @guid, @datetime, @INTERFACE_ID,@INTERFACE_NAME )");

            #endregion

            if (needRet) sb.AppendLine("	SET @" + ReturnValueParameterName + " = 1");
            sb.AppendLine("	COMMIT TRANSACTION");
            sb.AppendLine("	END");
            sb.AppendLine("");

            sb.AppendLine("END");
            sb.AppendLine("go");

            return sb.ToString();
        }

        private static string GetSelectSQL(List<QueryResultItem> redundancyFieldList, List<GWDataDBTable> redundancyFieldTableList, string interfaceName)
        {
            StringBuilder sb = new StringBuilder();

            string indexTableKey = GWDataDBField.i_IndexGuid.GetFullFieldName(interfaceName);
            string indexTableName = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Index);

            sb.Append("SELECT ").Append(indexTableKey)
                .Append(" FROM ").AppendLine(indexTableName);

            foreach (GWDataDBTable table in redundancyFieldTableList)
            {
                switch (table)
                {
                    case GWDataDBTable.Patient:
                        sb.Append("    LEFT JOIN ").Append(GWDataDB.GetTableName(interfaceName, GWDataDBTable.Patient))
                            .Append(" ON ").Append(indexTableKey).Append("=").AppendLine(GWDataDBField.p_DATA_ID.GetFullFieldName(interfaceName));
                        break;
                    case GWDataDBTable.Order:
                        sb.Append("    LEFT JOIN ").Append(GWDataDB.GetTableName(interfaceName, GWDataDBTable.Order))
                            .Append(" ON ").Append(indexTableKey).Append("=").AppendLine(GWDataDBField.o_DATA_ID.GetFullFieldName(interfaceName));
                        break;
                    case GWDataDBTable.Report:
                        sb.Append("    LEFT JOIN ").Append(GWDataDB.GetTableName(interfaceName, GWDataDBTable.Report))
                            .Append(" ON ").Append(indexTableKey).Append("=").AppendLine(GWDataDBField.r_DATA_ID.GetFullFieldName(interfaceName));
                        break;
                }
            }

            sb.AppendLine("WHERE ");

            StringBuilder sbWhere = new StringBuilder();
            foreach (QueryResultItem i in redundancyFieldList)
            {
                sbWhere.Append(',').Append(i.GWDataDBField.GetFullFieldName(interfaceName)).Append("=@").AppendLine(i.SourceField);
            }
            string strWhere = sbWhere.ToString().TrimStart(',').Replace(",", "    AND ");
            if (strWhere.Length > 0) sb.Append("    ").Append(strWhere);

            return sb.ToString();

            //select T_MWLOut_dataindex.Data_ID from T_MWLOut_dataindex 
            //    left join T_MWLOut_patient 
            //    on (T_MWLOut_dataindex.Data_ID=T_MWLOut_patient.Data_ID)
            //    left join T_MWLOut_order 
            //    on (T_MWLOut_dataindex.Data_ID=T_MWLOut_order.Data_ID)
            //    --left join T_MWLOut_report 
            //    --on (T_MWLOut_dataindex.Data_ID=T_MWLOut_report.Data_ID)
            //where
            //    T_MWLOut_order.Study_instance_uid ='1.2.840.113564.105424126591.200708140930084406449.599'
            //    and T_MWLOut_dataindex.Data_id='14B41DD8-6CA6-43EF-9EA3-FFCF76C2EEDB'
            //    and T_MWLOut_patient.Patientid='12345'
            //    --and T_MWLOut_report.report_no is NULL
        }
        
        private static string GetInsertSQL(GWDataDBTable table, string interfaceName, IInboundRule rule)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("		INSERT INTO " + GWDataDB.GetTableName(interfaceName, table) + " ");

            #region insert fields

            StringBuilder sbField = new StringBuilder();
            foreach (QueryResultItem i in rule.GetQueryResultItems())
            {
                if (i.GWDataDBField.Table == table)
                {
                    sbField.Append(i.GWDataDBField.GetFullFieldName(interfaceName) + ",");
                }
            }
            string strField = sbField.ToString();
            if (strField.Length > 0) strField = "," + strField.TrimEnd(',');

            #endregion

            sb.AppendLine("			( DATA_ID,DATA_DT" + strField + " )");
            sb.AppendLine("			VALUES");

            #region insert values

            StringBuilder sbValue = new StringBuilder();
            foreach (QueryResultItem i in rule.GetQueryResultItems())
            {
                if (i.GWDataDBField.Table == table)
                {
                    sbValue.Append("@" + i.SourceField + ",");
                }
            }
            string strValue = sbValue.ToString();
            if (strValue.Length > 0) strValue = "," + strValue.TrimEnd(',');

            #endregion

            sb.AppendLine("			( @guid, @datetime" + strValue + " )");
            return sb.ToString();
        }

        public static string GetInboundSPUninstall(string interfaceName, IRule rule)
        {
            if (rule == null || interfaceName == null || interfaceName.Length < 1) return null;

            string spName = GWDataDB.GetSPName(interfaceName, rule);

            StringBuilder sb = new StringBuilder();

            //sb.AppendLine("USE GWDataDB");
            sb.AppendLine("set ANSI_NULLS ON");
            sb.AppendLine("set QUOTED_IDENTIFIER ON");
            sb.AppendLine("go");
            sb.AppendLine("");

            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].[" + spName + "]') AND type ='P')");
            sb.AppendLine("DROP PROCEDURE [dbo].[" + spName + "]");
            sb.AppendLine("go");
            sb.AppendLine("");

            return sb.ToString();
        }

        #endregion

        #region Outbound Rule Engine

        public static string GetOutboundSP(string interfaceName, IOutboundRule rule)
        {
            if (rule == null || interfaceName == null || interfaceName.Length < 1) return null;

            string spName = GWDataDB.GetSPName(interfaceName, rule);

            #region data classification

            bool bPatient = false;
            bool bOrder = false;
            bool bReport = false;

            foreach (QueryCriteriaItem i in rule.GetQueryCriteriaItems())
            {
                if (i.Singal != QueryCriteriaSignal.None) continue;

                switch (i.GWDataDBField.Table)
                {
                    case GWDataDBTable.Patient:
                        bPatient = true;
                        break;
                    case GWDataDBTable.Order:
                        bOrder = true;
                        break;
                    case GWDataDBTable.Report:
                        bReport = true;
                        break;
                }
            }

            foreach (QueryResultItem i in rule.GetQueryResultItems())
            {
                if (i.Translating.Type == TranslatingType.FixValue) continue;

                switch (i.GWDataDBField.Table)
                {
                    case GWDataDBTable.Patient:
                        bPatient = true;
                        break;
                    case GWDataDBTable.Order:
                        bOrder = true;
                        break;
                    case GWDataDBTable.Report:
                        bReport = true;
                        break;
                }
            }

            #endregion

            StringBuilder sb = new StringBuilder();

            //sb.AppendLine("USE GWDataDB");
            sb.AppendLine("set ANSI_NULLS ON");
            sb.AppendLine("set QUOTED_IDENTIFIER ON");
            sb.AppendLine("go");
            sb.AppendLine("");

            sb.AppendLine("-- Query result translation.");

            #region transaltion for query result

            Hashtable translationList = new Hashtable();
            StringBuilder sbQRTranslation = new StringBuilder();

            foreach (QueryResultItem item in rule.GetQueryResultItems())
            {
                if (item.Translating.Type == TranslatingType.None ||
                    item.Translating.Type == TranslatingType.FixValue) continue;

                string functionName = GWDataDB.GetFunctionName(interfaceName, rule, item.TargetField);
                string functionDefinition = GetTranslationFN(functionName, item);
                sbQRTranslation.Append(functionDefinition);
                translationList[item] = functionName;
            }

            sb.AppendLine(sbQRTranslation.ToString());

            #endregion

            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].[" + spName + "]') AND type ='P')");
            sb.AppendLine("DROP PROCEDURE [dbo].[" + spName + "]");
            sb.AppendLine("go");
            sb.AppendLine("");

            sb.AppendLine("-- =============================================");
            sb.AppendLine("-- Author: GC Gateway 2.0 Rule Engine");
            sb.AppendLine("-- Create date: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            sb.AppendLine("-- Description: Data outbound storage procedure for interface '" + interfaceName + "', rule '" + rule.RuleName + "'");
            sb.AppendLine("-- =============================================");
            sb.AppendLine("CREATE PROCEDURE [dbo].[" + spName + "]");

            #region storage procedure parameters

            StringBuilder sbParam = new StringBuilder();
            List<QueryCriteriaItem> redundancyParamList = new List<QueryCriteriaItem>();
            foreach (QueryCriteriaItem i in rule.GetQueryCriteriaItems())
            {
                bool redundancy = false;
                foreach (QueryCriteriaItem mpi in redundancyParamList)
                {
                    if (mpi.SourceField == i.SourceField)
                    {
                        redundancy = true;
                        break;
                    }
                }
                if (redundancy) continue;
                redundancyParamList.Add(i);

                if (i.Singal == QueryCriteriaSignal.None &&
                    i.Translating.Type != TranslatingType.FixValue)
                {
                    sbParam.Append(" @" + i.SourceField + " nvarchar(MAX),");
                }
            }

            string strParam = sbParam.ToString();
            if (strParam.Length > 0) strParam = strParam.TrimEnd(',').Replace(",", ",\r\n");

            sb.AppendLine(strParam);

            //sb.AppendLine("	@DEVICE_DIRECT nvarchar(MAX),");
            //sb.AppendLine("	@INTERFACE_ID nvarchar(MAX),");
            //sb.AppendLine("	@INTERFACE_NAME nvarchar(MAX),");
            //sb.AppendLine("	@DEVICE_ID nvarchar(MAX)");

            #endregion

            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");

            sb.AppendLine("	-- SET NOCOUNT ON added to prevent extra result sets from");
            sb.AppendLine("	-- interfering with SELECT statements.");
            sb.AppendLine("	SET NOCOUNT ON;");
            sb.AppendLine("");

            sb.AppendLine("	-- Query criteria translation.");

            #region transaltion for query criteria

            StringBuilder sbQCTranslation = new StringBuilder();

            foreach (QueryCriteriaItem item in rule.GetQueryCriteriaItems())
            {
                switch (item.Translating.Type)
                {
                    case TranslatingType.FixValue:
                        {
                            if (item.Singal == QueryCriteriaSignal.None &&
                                item.Operator == QueryCriteriaOperator.In) break;

                            item.SourceField = "FX_" + GetRandomNumber();
                            sbQCTranslation.AppendLine("	DECLARE @" + item.SourceField + " nvarchar(MAX)");
                            //sbQCTranslation.AppendLine("	SET @" + item.SourceField + " = '" + item.Translating.ConstValue.Replace("'", "''") + "'");
                            sbQCTranslation.AppendLine("	SET @" + item.SourceField + " = " + ParseConstValue(item.Translating.ConstValue));
                            break;
                        }
                    case TranslatingType.DefaultValue:
                        {
                            sbQCTranslation.AppendLine("	IF @" + item.SourceField + " IS NULL OR @" + item.SourceField + " = ''");
                            sbQCTranslation.AppendLine("	BEGIN");
                            //sbQCTranslation.AppendLine("		SET @" + item.SourceField + " = '" + item.Translating.ConstValue.Replace("'", "''") + "'");
                            sbQCTranslation.AppendLine("		SET @" + item.SourceField + " = " + ParseConstValue(item.Translating.ConstValue));
                            sbQCTranslation.AppendLine("	END");
                            break;
                        }
                    case TranslatingType.LookUpTable:
                        {
                            sbQCTranslation.AppendLine("	SELECT @" + item.SourceField + " = TargetValue FROM " + item.Translating.LutName + " WHERE SourceValue = @" + item.SourceField);
                            break;
                        }
                    case TranslatingType.LookUpTableReverse:
                        {
                            sbQCTranslation.AppendLine("	SELECT @" + item.SourceField + " = SourceValue FROM " + item.Translating.LutName + " WHERE TargetValue = @" + item.SourceField);
                            break;
                        }
                }
            }

            sb.AppendLine(sbQCTranslation.ToString());

            #endregion

            sb.AppendLine("	-- Insert statements for procedure here");

            #region query data tables

            string strTop = "";
            string strSelect = "";
            string strFrom = "";
            string strWhere = "";

            #region Record count

            int maxRecordCount = rule.MaxRecordCount;
            if (maxRecordCount >= 0)
            {
                strTop = " TOP " + maxRecordCount.ToString();
            }

            #endregion

            #region query criteria

            StringBuilder sbWhere = new StringBuilder();

            foreach (QueryCriteriaItem i in rule.GetQueryCriteriaItems())
            {
                if (i.Type == QueryCriteriaType.Or) sbWhere.Append(":");
                if (i.Type == QueryCriteriaType.And) sbWhere.Append(";");

                switch (i.Singal)
                {
                    case QueryCriteriaSignal.None:
                        {
                            string fname = i.GWDataDBField.GetFullFieldName(interfaceName);
                            switch (i.Operator)
                            {
                                case QueryCriteriaOperator.Like:
                                    {
                                        sbWhere.AppendLine(fname + " LIKE @" + i.SourceField);
                                        break;
                                    }
                                case QueryCriteriaOperator.Equal:
                                    {
                                        sbWhere.AppendLine(fname + " = @" + i.SourceField);
                                        break;
                                    }
                                case QueryCriteriaOperator.NotEqual:
                                    {
                                        sbWhere.AppendLine("(" + fname + " <> @" + i.SourceField + " OR " + fname + " IS NULL)");
                                        break;
                                    }
                                case QueryCriteriaOperator.LargerThan:
                                    {
                                        sbWhere.AppendLine(fname + " > @" + i.SourceField);
                                        break;
                                    }
                                case QueryCriteriaOperator.SmallerThan:
                                    {
                                        sbWhere.AppendLine(fname + " < @" + i.SourceField);
                                        break;
                                    }
                                case QueryCriteriaOperator.EqualLargerThan:
                                    {
                                        sbWhere.AppendLine(fname + " >= @" + i.SourceField);
                                        break;
                                    }
                                case QueryCriteriaOperator.EqualSmallerThan:
                                    {
                                        sbWhere.AppendLine(fname + " <= @" + i.SourceField);
                                        break;
                                    }
                                case QueryCriteriaOperator.In:
                                    {
                                        sbWhere.AppendLine(fname + " IN (" + ParseInValue(i.Translating.ConstValue) + ")");
                                        break;
                                    }
                            }
                            break;
                        }
                    case QueryCriteriaSignal.LeftBracket:
                        {
                            sbWhere.Append(" (");
                            break;
                        }
                    case QueryCriteriaSignal.RightBracket:
                        {
                            sbWhere.Append(") ");
                            break;
                        }
                    case QueryCriteriaSignal.FreeText:
                        {
                            sbWhere.Append(" " + i.Translating.ConstValue.Replace("'", "''"));
                            break;
                        }
                }
            }

            strWhere = sbWhere.ToString();
            if (strWhere.Length > 0) strWhere = strWhere.TrimStart(':').TrimStart(';').Replace(":", "	OR ").Replace(";", "	AND ");

            if (rule.CheckProcessFlag)
            {
                string indexTable = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Index);
                string strCheckProcessFlag = "(" + indexTable + ".PROCESS_FLAG <> '1' OR " + indexTable + ".PROCESS_FLAG IS NULL)";
                if (strWhere.Length > 0)
                {
                    strWhere = "(" + strWhere + "	) AND " + strCheckProcessFlag;
                }
                else
                {
                    strWhere = strCheckProcessFlag;
                }
            }

            #endregion

            #region query result

            StringBuilder sbSelect = new StringBuilder();
            foreach (QueryResultItem i in rule.GetQueryResultItems())
            {
                if (i.Translating.Type == TranslatingType.None)
                {
                    sbSelect.Append(i.GWDataDBField.GetFullFieldName(interfaceName) + " AS " + i.TargetField + ",");
                }
                else
                {
                    if (i.Translating.Type == TranslatingType.FixValue)
                    {
                        //sbSelect.Append("'" + i.Translating.ConstValue.Replace("'", "''") + "' AS " + i.TargetField + ",");
                        sbSelect.Append(ParseConstValue(i.Translating.ConstValue) + " AS " + i.TargetField + ",");
                    }
                    else
                    {
                        string functionName = translationList[i] as string;
                        sbSelect.Append("[dbo].[" + functionName + "](" + i.GWDataDBField.GetFullFieldName(interfaceName) + ") AS " + i.TargetField + ",");
                    }
                }
            }
            strSelect = sbSelect.ToString();
            if (strSelect.Length > 0) strSelect = strSelect.TrimEnd(',').Replace(",", ",\r\n	");

            #endregion

            #region join table

            string strIndexTable = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Index);
            string strPateintTable = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Patient);
            string strOrderTable = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Order);
            string strReportTable = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Report);

            strFrom = strIndexTable;

            if (bPatient) strFrom = strFrom + " JOIN " + strPateintTable + " ON(" + strIndexTable + ".Data_ID = " + strPateintTable + ".Data_ID)";
            if (bOrder) strFrom = strFrom + " JOIN " + strOrderTable + " ON(" + strIndexTable + ".Data_ID = " + strOrderTable + ".Data_ID)";
            if (bReport) strFrom = strFrom + " JOIN " + strReportTable + " ON(" + strIndexTable + ".Data_ID = " + strReportTable + ".Data_ID)";

            //if (bPatient)
            //{
            //    strFrom = strFrom + " JOIN " + strPateintTable + " ON(" + strIndexTable + ".Data_ID = " + strPateintTable + ".Data_ID)";
            //    if (bOrder)
            //    {
            //        strFrom = "(" + strFrom + " JOIN " + strOrderTable + " ON(" + strIndexTable + ".Data_ID = " + strOrderTable + ".Data_ID) )";
            //        if (bReport)
            //        {
            //            strFrom = "(" + strFrom + " JOIN " + strReportTable + " ON(" + strIndexTable + ".Data_ID = " + strReportTable + ".Data_ID) )";
            //        }
            //    }
            //}
            //else
            //{
            //    if (bOrder)
            //    {
            //        strFrom = strFrom + " JOIN " + strOrderTable + " ON(" + strIndexTable + ".Data_ID = " + strOrderTable + ".Data_ID)";
            //        if (bReport)
            //        {
            //            strFrom = "(" + strFrom + " JOIN " + strReportTable + " ON(" + strIndexTable + ".Data_ID = " + strReportTable + ".Data_ID) )";
            //        }
            //    }
            //    else
            //    {
            //        if (bReport)
            //        {
            //            strFrom = strFrom + " JOIN " + strReportTable + " ON(" + strIndexTable + ".Data_ID = " + strReportTable + ".Data_ID)";
            //        }
            //    }
            //}

            #endregion

            sb.Append("	SELECT").AppendLine(strTop);

            if (strSelect.Length > 0)
            {
                sb.AppendLine("	" + strSelect);
            }
            else
            {
                sb.AppendLine("	*");
            }

            sb.AppendLine("	FROM");
            sb.AppendLine("	" + strFrom);

            if (strWhere.Length > 0)
            {
                sb.AppendLine("	WHERE");
                sb.AppendLine("	" + strWhere);
            }

            sb.AppendLine("	ORDER BY " + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Index) + ".DATA_DT");

            #endregion

            sb.AppendLine("END");
            sb.AppendLine("go");

            return sb.ToString();
        }

        private static string GetTranslationFN(string functionName, MappingItem item)
        {
            //string functionName = "fn_" + ruleName + "_" + item.TargetField;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].[" + functionName + "]') AND type ='FN')");
            sb.AppendLine("DROP FUNCTION [dbo].[" + functionName + "]");
            sb.AppendLine("go");
            sb.AppendLine("");

            sb.AppendLine("-- =============================================");
            sb.AppendLine("-- Author: GC Gateway 2.0 Rule Engine");
            sb.AppendLine("-- Create date: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            sb.AppendLine("-- Description: Translation function for target parameter of '" + item.TargetField + "'");
            sb.AppendLine("-- =============================================");

            sb.AppendLine("CREATE FUNCTION [dbo].[" + functionName + "]");
            sb.AppendLine("(");
            sb.AppendLine("	-- Add the parameters for the function here");
            sb.AppendLine("	@SourceValue nvarchar(MAX)");
            sb.AppendLine(")");
            sb.AppendLine("RETURNS nvarchar(MAX)");
            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            sb.AppendLine("	-- Fill the table variable with the rows for your result set");
            sb.AppendLine("	DECLARE @ret nvarchar(MAX)");
            sb.AppendLine("");

            string lutName = item.Translating.LutName;
            //string constValue = item.Translating.ConstValue.Replace("'", "''");
            string constValue = ParseConstValue(item.Translating.ConstValue);

            switch (item.Translating.Type)
            {
                case TranslatingType.LookUpTable:
                    {
                        sb.AppendLine("	SELECT @ret = " + lutName + ".TargetValue");
                        sb.AppendLine("		FROM " + lutName + " WHERE " + lutName + ".SourceValue = @SourceValue");
                        sb.AppendLine("");
                        sb.AppendLine("	IF @ret IS NULL");
                        sb.AppendLine("	BEGIN");
                        sb.AppendLine("		SET @ret = @SourceValue");
                        sb.AppendLine("	END");
                        break;
                    }
                case TranslatingType.LookUpTableReverse:
                    {
                        sb.AppendLine("	SELECT @ret = " + lutName + ".SourceValue");
                        sb.AppendLine("		FROM " + lutName + " WHERE " + lutName + ".TargetValue = @SourceValue");
                        sb.AppendLine("");
                        sb.AppendLine("	IF @ret IS NULL");
                        sb.AppendLine("	BEGIN");
                        sb.AppendLine("		SET @ret = @SourceValue");
                        sb.AppendLine("	END");
                        break;
                    }
                case TranslatingType.DefaultValue:
                    {
                        sb.AppendLine("	SET @ret = @SourceValue");
                        sb.AppendLine("");
                        sb.AppendLine("	IF @ret IS NULL OR @ret = ''");
                        sb.AppendLine("	BEGIN");
                        //sb.AppendLine("		SET @ret = '" + constValue + "'");
                        sb.AppendLine("		SET @ret = " + constValue);
                        sb.AppendLine("	END");
                        break;
                    }
                case TranslatingType.FixValue:
                    {
                        //sb.AppendLine("	SET @ret = '" + constValue + "'");
                        sb.AppendLine("	SET @ret = " + constValue);
                        break;
                    }
            }

            sb.AppendLine("");
            sb.AppendLine("	RETURN @ret");
            sb.AppendLine("END");
            sb.AppendLine("go");
            sb.AppendLine("");

            return sb.ToString();
        }

        public static string GetOutboundSPUninstall(string interfaceName, IOutboundRule rule)
        {
            if (rule == null || interfaceName == null || interfaceName.Length < 1) return null;

            string spName = GWDataDB.GetSPName(interfaceName, rule);

            StringBuilder sb = new StringBuilder();

            //sb.AppendLine("USE GWDataDB");
            sb.AppendLine("set ANSI_NULLS ON");
            sb.AppendLine("set QUOTED_IDENTIFIER ON");
            sb.AppendLine("go");
            sb.AppendLine("");

            foreach (QueryResultItem item in rule.GetQueryResultItems())
            {
                if (item.Translating.Type == TranslatingType.None ||
                    item.Translating.Type == TranslatingType.FixValue) continue;

                string functionName = GWDataDB.GetFunctionName(interfaceName, rule, item.TargetField);

                sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].[" + functionName + "]') AND type ='FN')");
                sb.AppendLine("DROP FUNCTION [dbo].[" + functionName + "]");
                sb.AppendLine("go");
                sb.AppendLine("");
            }

            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].[" + spName + "]') AND type ='P')");
            sb.AppendLine("DROP PROCEDURE [dbo].[" + spName + "]");
            sb.AppendLine("go");
            sb.AppendLine("");

            return sb.ToString();
        }

        #endregion

        #region Obsoleted Inbound Rule -> SP

        [Obsolete("This method can only by used for testing.", false)]
        public static void CreateSP<TC, TR>(string dumpFile, InboundRule<TC, TR> rule)
            where TC : QueryCriteriaItem
            where TR : QueryResultItem
        {
            using (StreamWriter sw = File.CreateText(dumpFile))
            {
                bool bIndex = true;
                bool bPatient = false;
                bool bOrder = false;
                bool bReport = false;

                #region dump index table

                sw.WriteLine("Data to be insert : Index");
                sw.WriteLine("---------------------------------------------------------------------");
                sw.WriteLine("Source\t\tTarget\t\tTranslating\t\tRedundancyFlag(used for accessing GWDataDB)");
                sw.WriteLine("---------------------------------------------------------------------");
                foreach (MappingItem i in rule.QueryResult.MappingList)
                {
                    if (i.GWDataDBField.Table == GWDataDBTable.Index)
                    {
                        sw.WriteLine(i.SourceField + "\t" + i.TargetField + "\t" + i.Translating.ToString() + "\t\t" + i.RedundancyFlag.ToString());
                    }
                }

                #endregion

                #region dump patient table

                sw.WriteLine();
                sw.WriteLine("Data to be insert : Patient");
                sw.WriteLine("---------------------------------------------------------------------");
                sw.WriteLine("Source\t\tTarget\t\tTranslating\t\tRedundancyFlag(used for accessing GWDataDB)");
                sw.WriteLine("---------------------------------------------------------------------");
                foreach (MappingItem i in rule.QueryResult.MappingList)
                {
                    if (i.GWDataDBField.Table == GWDataDBTable.Patient)
                    {
                        sw.WriteLine(i.SourceField + "\t" + i.TargetField + "\t" + i.Translating.ToString() + "\t\t" + i.RedundancyFlag.ToString());
                        bPatient = true;
                    }
                }

                #endregion

                #region dump order table

                sw.WriteLine();
                sw.WriteLine("Data to be insert : Order");
                sw.WriteLine("---------------------------------------------------------------------");
                sw.WriteLine("Source\t\tTarget\t\tTranslating\t\tRedundancyFlag(used for accessing GWDataDB)");
                sw.WriteLine("---------------------------------------------------------------------");
                foreach (MappingItem i in rule.QueryResult.MappingList)
                {
                    if (i.GWDataDBField.Table == GWDataDBTable.Order)
                    {
                        sw.WriteLine(i.SourceField + "\t" + i.TargetField + "\t" + i.Translating.ToString() + "\t\t" + i.RedundancyFlag.ToString());
                        bOrder = true;
                    }
                }

                #endregion

                #region dump report table

                sw.WriteLine();
                sw.WriteLine("Data to be insert : Report");
                sw.WriteLine("---------------------------------------------------------------------");
                sw.WriteLine("Source\t\tTarget\t\tTranslating\t\tRedundancyFlag(used for accessing GWDataDB)");
                sw.WriteLine("---------------------------------------------------------------------");
                foreach (MappingItem i in rule.QueryResult.MappingList)
                {
                    if (i.GWDataDBField.Table == GWDataDBTable.Report)
                    {
                        sw.WriteLine(i.SourceField + "\t" + i.TargetField + "\t" + i.Translating.ToString() + "\t\t" + i.RedundancyFlag.ToString());
                        bReport = true;
                    }
                }

                #endregion

                #region storage procedure header

                sw.WriteLine();
                sw.WriteLine("Storage procedure demo code:");
                sw.WriteLine("---------------------------------------------------------------------");
                sw.WriteLine("CREATE PROCEDURE " + rule.RuleName);
                List<MappingItem> redundancyList = new List<MappingItem>();
                foreach (MappingItem i in rule.QueryResult.MappingList)
                {
                    bool redundancy = false;
                    foreach (MappingItem mpi in redundancyList)
                    {
                        if (mpi.SourceField == i.SourceField)
                        {
                            redundancy = true;
                            break;
                        }
                    }
                    if (redundancy) continue;

                    if (i.Translating.Type == TranslatingType.FixValue ||
                        i.Translating.Type == TranslatingType.DefaultValue)
                    {
                    }
                    else
                    {
                        sw.WriteLine("\t@" + i.SourceField + " nvarchar,");
                    }

                    redundancyList.Add(i);
                }
                sw.WriteLine("AS");
                sw.WriteLine("@guid = GenerateGuid()");
                sw.WriteLine("@datetime = GetCurrentDateTime()");

                #endregion

                #region insert patient table

                if (bPatient)
                {
                    sw.WriteLine();
                    sw.WriteLine("INSERT INTO " + GWDataDBTable.Patient.ToString());
                    sw.Write("( ID, DT, ");
                    foreach (MappingItem i in rule.QueryResult.MappingList)
                    {
                        if (i.GWDataDBField.Table == GWDataDBTable.Patient)
                        {
                            sw.Write(i.TargetField + ",");
                        }
                    }
                    sw.WriteLine(")");
                    sw.WriteLine("VALUES");
                    sw.Write("( @guid, @datetime, ");
                    foreach (MappingItem i in rule.QueryResult.MappingList)
                    {
                        if (i.GWDataDBField.Table == GWDataDBTable.Patient)
                        {
                            if (i.Translating.Type == TranslatingType.LookUpTable ||
                                i.Translating.Type == TranslatingType.LookUpTableReverse)
                            {
                                sw.Write("GetValueFromLUT(" + i.Translating.LutName + ",@" + i.SourceField + "),");
                            }
                            else
                            {
                                sw.Write("@" + i.SourceField + ",");
                            }
                        }
                    }
                    sw.WriteLine(")");
                }

                #endregion

                #region insert order table

                if (bOrder)
                {
                    sw.WriteLine();
                    sw.WriteLine("INSERT INTO " + GWDataDBTable.Order.ToString());
                    sw.Write("( ID, DT, ");
                    foreach (MappingItem i in rule.QueryResult.MappingList)
                    {
                        if (i.GWDataDBField.Table == GWDataDBTable.Order)
                        {
                            sw.Write(i.TargetField + ",");
                        }
                    }
                    sw.WriteLine(")");
                    sw.WriteLine("VALUES");
                    sw.Write("( @guid, @datetime, ");
                    foreach (MappingItem i in rule.QueryResult.MappingList)
                    {
                        if (i.GWDataDBField.Table == GWDataDBTable.Order)
                        {
                            if (i.Translating.Type == TranslatingType.LookUpTable ||
                                i.Translating.Type == TranslatingType.LookUpTableReverse)
                            {
                                sw.Write("GetValueFromLUT(" + i.Translating.LutName + ",@" + i.SourceField + "),");
                            }
                            else
                            {
                                sw.Write("@" + i.SourceField + ",");
                            }
                        }
                    }
                    sw.WriteLine(")");
                }

                #endregion

                #region insert report table

                if (bReport)
                {
                    sw.WriteLine();
                    sw.WriteLine("INSERT INTO " + GWDataDBTable.Report.ToString());
                    sw.Write("( ID, DT, ");
                    foreach (MappingItem i in rule.QueryResult.MappingList)
                    {
                        if (i.GWDataDBField.Table == GWDataDBTable.Report)
                        {
                            sw.Write(i.TargetField + ",");
                        }
                    }
                    sw.WriteLine(")");
                    sw.WriteLine("VALUES");
                    sw.Write("( @guid, @datetime, ");
                    foreach (MappingItem i in rule.QueryResult.MappingList)
                    {
                        if (i.GWDataDBField.Table == GWDataDBTable.Report)
                        {
                            if (i.Translating.Type == TranslatingType.LookUpTable ||
                                i.Translating.Type == TranslatingType.LookUpTableReverse)
                            {
                                sw.Write("GetValueFromLUT(" + i.Translating.LutName + ",@" + i.SourceField + "),");
                            }
                            else
                            {
                                sw.Write("@" + i.SourceField + ",");
                            }
                        }
                    }
                    sw.WriteLine(")");
                }

                #endregion

                #region insert index table

                if (bIndex)
                {
                    sw.WriteLine();
                    sw.WriteLine("INSERT INTO " + GWDataDBTable.Index.ToString());
                    sw.Write("( ID, DT, ");
                    foreach (MappingItem i in rule.QueryResult.MappingList)
                    {
                        if (i.GWDataDBField.Table == GWDataDBTable.Index)
                        {
                            sw.Write(i.TargetField + ",");
                        }
                    }
                    sw.WriteLine(")");
                    sw.WriteLine("VALUES");
                    sw.Write("( @guid, @datetime, ");
                    foreach (MappingItem i in rule.QueryResult.MappingList)
                    {
                        if (i.GWDataDBField.Table == GWDataDBTable.Index)
                        {
                            if (i.Translating.Type == TranslatingType.LookUpTable ||
                                i.Translating.Type == TranslatingType.LookUpTableReverse)
                            {
                                sw.Write("GetValueFromLUT(" + i.Translating.LutName + ",@" + i.SourceField + "),");
                            }
                            else
                            {
                                sw.Write("@" + i.SourceField + ",");
                            }
                        }
                    }
                    sw.WriteLine(")");
                }

                #endregion

                #region insert redundancy filter

                sw.WriteLine();
                sw.WriteLine("WHEN NOT EXIT ");
                List<MappingItem> pkList = new List<MappingItem>();
                foreach (MappingItem i in rule.QueryResult.MappingList)
                {
                    if (i.RedundancyFlag) pkList.Add(i);
                }
                foreach (MappingItem i in pkList)
                {
                    if (i.Translating.Type == TranslatingType.LookUpTable ||
                            i.Translating.Type == TranslatingType.LookUpTableReverse)
                    {
                        sw.Write(i.TargetField + "=GetValueFromLUT(" + i.Translating.LutName + ",@" + i.SourceField + ")\r\n");
                    }
                    else
                    {
                        sw.Write(i.TargetField + "=@" + i.SourceField + "\r\n");
                    }
                }

                #endregion
            }
        }

        [Obsolete("This method is expired, please use method RuleControl.GetInboundSP(string, IRule) instead.", false)]
        public static string GetSP<TC, TR>(string interfaceName, InboundRule<TC, TR> rule)
            where TC : QueryCriteriaItem
            where TR : QueryResultItem
        {
            if (rule == null || interfaceName == null || interfaceName.Length < 1) return null;

            #region data classification

            bool bPatient = false;
            bool bOrder = false;
            bool bReport = false;

            foreach (QueryResultItem i in rule.QueryResult.MappingList)
            {
                switch (i.GWDataDBField.Table)
                {
                    case GWDataDBTable.Patient:
                        bPatient = true;
                        break;
                    case GWDataDBTable.Order:
                        bOrder = true;
                        break;
                    case GWDataDBTable.Report:
                        bReport = true;
                        break;
                }
            }

            #endregion

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("USE GWDataDB");
            sb.AppendLine("set ANSI_NULLS ON");
            sb.AppendLine("set QUOTED_IDENTIFIER ON");
            sb.AppendLine("go");
            sb.AppendLine("");

            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].[" + rule.RuleName + "]') AND type ='P')");
            sb.AppendLine("DROP PROCEDURE [dbo].[" + rule.RuleName + "]");
            sb.AppendLine("go");
            sb.AppendLine("");

            sb.AppendLine("-- =============================================");
            sb.AppendLine("-- Author: GC Gateway 2.0 Rule Engine");
            sb.AppendLine("-- Create date: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            sb.AppendLine("-- Description: Data inbound storage procedure for interface '" + interfaceName + "'");
            sb.AppendLine("-- =============================================");
            sb.AppendLine("CREATE PROCEDURE [dbo].[" + rule.RuleName + "]");

            #region storage procedure parameters

            StringBuilder sbParam = new StringBuilder();
            List<QueryResultItem> redundancyParamList = new List<QueryResultItem>();
            foreach (QueryResultItem i in rule.QueryResult.MappingList)
            {
                bool redundancy = false;
                foreach (QueryResultItem mpi in redundancyParamList)
                {
                    if (mpi.SourceField == i.SourceField)
                    {
                        redundancy = true;
                        break;
                    }
                }
                if (redundancy) continue;
                redundancyParamList.Add(i);

                if (i.Translating.Type != TranslatingType.FixValue)
                {
                    sbParam.Append(" @" + i.SourceField + " nvarchar(MAX),");
                }
            }

            string strParam = sbParam.ToString();
            if (strParam.Length > 0) strParam = strParam.TrimEnd(',').Replace(",", ",\r\n");
            sb.AppendLine(strParam);

            //sb.AppendLine("	@DEVICE_DIRECT nvarchar(MAX),");
            //sb.AppendLine("	@INTERFACE_ID nvarchar(MAX),");
            //sb.AppendLine("	@INTERFACE_NAME nvarchar(MAX),");
            //sb.AppendLine("	@DEVICE_ID nvarchar(MAX)");

            #endregion

            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");

            sb.AppendLine("	-- SET NOCOUNT ON added to prevent extra result sets from");
            sb.AppendLine("	-- interfering with SELECT statements.");
            sb.AppendLine("	SET NOCOUNT ON;");
            sb.AppendLine("");

            sb.AppendLine("	-- Translation.");

            #region transaltion

            StringBuilder sbTranslation = new StringBuilder();

            foreach (MappingItem item in rule.QueryResult.MappingList)
            {
                switch (item.Translating.Type)
                {
                    case TranslatingType.FixValue:
                        {
                            item.SourceField = "FX_" + GetRandomNumber();
                            sbTranslation.AppendLine("	DECLARE @" + item.SourceField + " nvarchar(MAX)");
                            sbTranslation.AppendLine("	SET @" + item.SourceField + " = '" + item.Translating.ConstValue.Replace("'", "''") + "'");
                            break;
                        }
                    case TranslatingType.DefaultValue:
                        {
                            sbTranslation.AppendLine("	IF @" + item.SourceField + " IS NULL OR @" + item.SourceField + " = ''");
                            sbTranslation.AppendLine("	BEGIN");
                            sbTranslation.AppendLine("		SET @" + item.SourceField + " = '" + item.Translating.ConstValue.Replace("'", "''") + "'");
                            sbTranslation.AppendLine("	END");
                            break;
                        }
                    case TranslatingType.LookUpTable:
                        {
                            sbTranslation.AppendLine("	SELECT @" + item.SourceField + " = TargetValue FROM " + item.Translating.LutName + " WHERE SourceValue = @" + item.SourceField);
                            break;
                        }
                    case TranslatingType.LookUpTableReverse:
                        {
                            sbTranslation.AppendLine("	SELECT @" + item.SourceField + " = SourceValue FROM " + item.Translating.LutName + " WHERE TargetValue = @" + item.SourceField);
                            break;
                        }
                }
            }

            sb.AppendLine(sbTranslation.ToString());

            #endregion

            sb.AppendLine("	-- Insert statements for procedure here");
            sb.AppendLine("	DECLARE @guid uniqueidentifier");
            sb.AppendLine("	DECLARE @datetime datetime");
            sb.AppendLine("");

            sb.AppendLine("	SET @guid = NEWID()");
            sb.AppendLine("	SET @datetime = GETDATE()");
            sb.AppendLine("");

            #region redundancy filter

            List<QueryResultItem> redundancyFieldList = new List<QueryResultItem>();
            foreach (QueryResultItem i in rule.QueryResult.MappingList)
            {
                if (i.RedundancyFlag) redundancyFieldList.Add(i);
            }

            if (redundancyFieldList.Count > 0)
            {
                StringBuilder sbFilter = new StringBuilder();
                foreach (QueryResultItem i in redundancyFieldList)
                {
                    sbFilter.AppendLine(", NOT @" + i.SourceField + " IN (SELECT " + i.GWDataDBField.GetFullFieldName(interfaceName) + " FROM " + i.GWDataDBField.GetTableName(interfaceName) + ")");
                }
                string strFilter = sbFilter.ToString();
                if (strFilter.Length > 0) strFilter = strFilter.TrimStart(',').Replace(",", "\r\n   AND");
                sb.AppendLine("	IF" + strFilter);
            }

            //sb.AppendLine("	IF NOT @INTERFACE_ID IN (SELECT PATIENT.PATIENTID FROM PATIENT)");
            //sb.AppendLine("		AND NOT @INTERFACE_ID IN (SELECT PATIENT.PATIENTID FROM PATIENT)");

            #endregion

            sb.AppendLine("	BEGIN");
            sb.AppendLine("	BEGIN TRANSACTION");

            #region insert table

            if (bPatient) sb.AppendLine(GetInsertSQL<TC, TR>(GWDataDBTable.Patient, interfaceName, rule));
            if (bOrder) sb.AppendLine(GetInsertSQL<TC, TR>(GWDataDBTable.Order, interfaceName, rule));
            if (bReport) sb.AppendLine(GetInsertSQL<TC, TR>(GWDataDBTable.Report, interfaceName, rule));
            sb.AppendLine(GetInsertSQL<TC, TR>(GWDataDBTable.Index, interfaceName, rule));

            //sb.AppendLine("		INSERT INTO PATIENT ");
            //sb.AppendLine("			( DATA_ID, DATA_DT, PATIENT.PATIENTID,PATIENT.PATIENT_NAME )");
            //sb.AppendLine("			VALUES");
            //sb.AppendLine("			( @guid, @datetime, @INTERFACE_ID,@INTERFACE_NAME )");

            #endregion

            sb.AppendLine("	COMMIT TRANSACTION");
            sb.AppendLine("	END");
            sb.AppendLine("");

            sb.AppendLine("END");

            return sb.ToString();
        }

        private static string GetInsertSQL<TC, TR>(GWDataDBTable table, string interfaceName, InboundRule<TC, TR> rule)
            where TC : QueryCriteriaItem
            where TR : QueryResultItem
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("		INSERT INTO " + GWDataDB.GetTableName(interfaceName, table) + " ");

            #region insert fields

            StringBuilder sbField = new StringBuilder();
            foreach (QueryResultItem i in rule.QueryResult.MappingList)
            {
                if (i.GWDataDBField.Table == table)
                {
                    sbField.Append(i.GWDataDBField.GetFullFieldName(interfaceName) + ",");
                }
            }
            string strField = sbField.ToString();
            if (strField.Length > 0) strField = "," + strField.TrimEnd(',');

            #endregion

            sb.AppendLine("			( DATA_ID,DATA_DT" + strField + " )");
            sb.AppendLine("			VALUES");

            #region insert values

            StringBuilder sbValue = new StringBuilder();
            foreach (QueryResultItem i in rule.QueryResult.MappingList)
            {
                if (i.GWDataDBField.Table == table)
                {
                    sbValue.Append("@" + i.SourceField + ",");
                }
            }
            string strValue = sbValue.ToString();
            if (strValue.Length > 0) strValue = "," + strValue.TrimEnd(',');

            #endregion

            sb.AppendLine("			( @guid, @datetime" + strValue + " )");
            return sb.ToString();
        }

        [Obsolete("This method is expired, please use method RuleControl.GetInboundSPUninstall(string, IRule) instead.", false)]
        public static string GetSPUninstall<TC, TR>(string interfaceName, InboundRule<TC, TR> rule)
            where TC : QueryCriteriaItem
            where TR : QueryResultItem
        {
            if (rule == null || interfaceName == null || interfaceName.Length < 1) return null;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("USE GWDataDB");
            sb.AppendLine("set ANSI_NULLS ON");
            sb.AppendLine("set QUOTED_IDENTIFIER ON");
            sb.AppendLine("go");
            sb.AppendLine("");

            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].[" + rule.RuleName + "]') AND type ='P')");
            sb.AppendLine("DROP PROCEDURE [dbo].[" + rule.RuleName + "]");
            sb.AppendLine("go");
            sb.AppendLine("");

            return sb.ToString();
        }

        #endregion

        #region Obsoleted Outbound Rule -> SP

        [Obsolete("This method can only by used for testing.", false)]
        public static void CreateSP<TC, TR>(string dumpFile, OutboundRule<TC, TR> rule)
            where TC : QueryCriteriaItem
            where TR : QueryResultItem
        {
            using (StreamWriter sw = File.CreateText(dumpFile))
            {
                string strWhere = "";
                string strSelect = "";

                #region dump criteria

                sw.WriteLine("Criteria");
                sw.WriteLine("---------------------------------------------------------------------");
                sw.WriteLine("Source\t\tTarget\t\tTranslating");
                sw.WriteLine("---------------------------------------------------------------------");
                foreach (QueryCriteriaItem i in rule.QueryCriteria.MappingList)
                {
                    if (i.Translating.Type == TranslatingType.FixValue ||
                        i.Translating.Type == TranslatingType.DefaultValue)
                    {
                        sw.WriteLine("[NONE]\t" + i.TargetField + "\t" + i.Translating.ToString());
                        strWhere += i.TargetField + "='" + i.Translating.ConstValue.Replace("'", "''") + "'";

                        if (i.Type == QueryCriteriaType.Or) strWhere += " OR ";
                        if (i.Type == QueryCriteriaType.And) strWhere += " AND ";
                    }
                    else
                    {
                        sw.WriteLine(i.SourceField + "\t" + i.TargetField + "\t" + i.Translating.ToString());
                        strWhere += i.TargetField + "=@" + i.SourceField;

                        if (i.Type == QueryCriteriaType.Or) strWhere += " OR ";
                        if (i.Type == QueryCriteriaType.And) strWhere += " AND ";
                    }
                }

                #endregion

                #region dump result

                sw.WriteLine();
                sw.WriteLine("Result");
                sw.WriteLine("---------------------------------------------------------------------");
                sw.WriteLine("Source\t\tTarget\t\tTranslating\t\tRedundancyFlag(used for accessing 3rd party db)");
                sw.WriteLine("---------------------------------------------------------------------");
                foreach (MappingItem i in rule.QueryResult.MappingList)
                {
                    sw.WriteLine(i.SourceField + "\t" + i.TargetField + "\t" + i.Translating.ToString() + "\t\t" + i.RedundancyFlag.ToString());
                    strSelect += "@" + i.TargetField + "=" + i.SourceField + ",";
                }

                #endregion

                #region create storage procedure

                sw.WriteLine();
                sw.WriteLine("Storage procedure demo code:");
                sw.WriteLine("---------------------------------------------------------------------");
                sw.WriteLine("CREATE PROCEDURE " + rule.RuleName);
                foreach (MappingItem i in rule.QueryCriteria.MappingList)
                {
                    if (i.Translating.Type == TranslatingType.FixValue ||
                        i.Translating.Type == TranslatingType.DefaultValue)
                    {
                    }
                    else
                    {
                        sw.WriteLine("\t@" + i.SourceField + " nvarchar,");
                    }
                }
                foreach (MappingItem i in rule.QueryResult.MappingList)
                {
                    sw.WriteLine("\t@" + i.TargetField + " nvarchar output,");
                }
                sw.WriteLine("AS");
                if (strSelect.Length > 0)
                {
                    strSelect = strSelect.Substring(0, strSelect.Length - 1);
                    strSelect = strSelect.Replace(",", ",\r\n");
                }
                string procFlag = "";
                if (rule.CheckProcessFlag) procFlag = " AND Index.ProcessFlag = 0 ";
                if (rule.QueryCriteria.Type == QueryCriteriaRuleType.SQLStatement)
                {
                    sw.WriteLine("SELECT \r\n" + strSelect + " \r\nFROM TABLES \r\nWHERE " + rule.QueryCriteria.SQLStatement + procFlag);
                }
                else
                {
                    sw.WriteLine("SELECT \r\n" + strSelect + " \r\nFROM TABLES \r\nWHERE " + strWhere + procFlag);
                }

                sw.WriteLine();

                #endregion
            }
        }

        [Obsolete("This method is expired, please use method RuleControl.GetOutboundSP(string, IRule) instead.", false)]
        public static string GetSP<TC, TR>(string interfaceName, OutboundRule<TC, TR> rule)
            where TC : QueryCriteriaItem
            where TR : QueryResultItem
        {
            if (rule == null || interfaceName == null || interfaceName.Length < 1) return null;

            #region data classification

            bool bPatient = false;
            bool bOrder = false;
            bool bReport = false;

            foreach (QueryCriteriaItem i in rule.QueryCriteria.MappingList)
            {
                switch (i.GWDataDBField.Table)
                {
                    case GWDataDBTable.Patient:
                        bPatient = true;
                        break;
                    case GWDataDBTable.Order:
                        bOrder = true;
                        break;
                    case GWDataDBTable.Report:
                        bReport = true;
                        break;
                }
            }

            foreach (QueryResultItem i in rule.QueryResult.MappingList)
            {
                switch (i.GWDataDBField.Table)
                {
                    case GWDataDBTable.Patient:
                        bPatient = true;
                        break;
                    case GWDataDBTable.Order:
                        bOrder = true;
                        break;
                    case GWDataDBTable.Report:
                        bReport = true;
                        break;
                }
            }

            #endregion

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("USE GWDataDB");
            sb.AppendLine("set ANSI_NULLS ON");
            sb.AppendLine("set QUOTED_IDENTIFIER ON");
            sb.AppendLine("go");
            sb.AppendLine("");

            sb.AppendLine("-- Query result translation.");

            #region transaltion for query result

            Hashtable translationList = new Hashtable();
            StringBuilder sbQRTranslation = new StringBuilder();

            foreach (QueryResultItem item in rule.QueryResult.MappingList)
            {
                if (item.Translating.Type == TranslatingType.None) continue;
                string functionName = "fn_" + rule.RuleName + "_" + item.TargetField;
                string functionDefinition = GetTranslationFN(functionName, item);
                sbQRTranslation.Append(functionDefinition);
                translationList[item] = functionName;
            }

            sb.AppendLine(sbQRTranslation.ToString());

            #endregion

            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].[" + rule.RuleName + "]') AND type ='P')");
            sb.AppendLine("DROP PROCEDURE [dbo].[" + rule.RuleName + "]");
            sb.AppendLine("go");
            sb.AppendLine("");

            sb.AppendLine("-- =============================================");
            sb.AppendLine("-- Author: GC Gateway 2.0 Rule Engine");
            sb.AppendLine("-- Create date: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            sb.AppendLine("-- Description: Data outbound storage procedure for interface '" + interfaceName + "'");
            sb.AppendLine("-- =============================================");
            sb.AppendLine("CREATE PROCEDURE [dbo].[" + rule.RuleName + "]");

            #region storage procedure parameters

            StringBuilder sbParam = new StringBuilder();
            foreach (QueryCriteriaItem i in rule.QueryCriteria.MappingList)
            {
                if (i.Translating.Type != TranslatingType.FixValue)
                {
                    sbParam.Append(" @" + i.SourceField + " nvarchar(MAX),");
                }
            }

            string strParam = sbParam.ToString();
            if (strParam.Length > 0) strParam = strParam.TrimEnd(',').Replace(",", ",\r\n");

            sb.AppendLine(strParam);

            //sb.AppendLine("	@DEVICE_DIRECT nvarchar(MAX),");
            //sb.AppendLine("	@INTERFACE_ID nvarchar(MAX),");
            //sb.AppendLine("	@INTERFACE_NAME nvarchar(MAX),");
            //sb.AppendLine("	@DEVICE_ID nvarchar(MAX)");

            #endregion

            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");

            sb.AppendLine("	-- SET NOCOUNT ON added to prevent extra result sets from");
            sb.AppendLine("	-- interfering with SELECT statements.");
            sb.AppendLine("	SET NOCOUNT ON;");
            sb.AppendLine("");

            sb.AppendLine("	-- Query criteria translation.");

            #region transaltion for query criteria

            StringBuilder sbQCTranslation = new StringBuilder();

            foreach (QueryCriteriaItem item in rule.QueryCriteria.MappingList)
            {
                switch (item.Translating.Type)
                {
                    case TranslatingType.FixValue:
                        {
                            item.SourceField = "FX_" + GetRandomNumber();
                            sbQCTranslation.AppendLine("	DECLARE @" + item.SourceField + " nvarchar(MAX)");
                            sbQCTranslation.AppendLine("	SET @" + item.SourceField + " = '" + item.Translating.ConstValue.Replace("'", "''") + "'");
                            break;
                        }
                    case TranslatingType.DefaultValue:
                        {
                            sbQCTranslation.AppendLine("	IF @" + item.SourceField + " IS NULL OR @" + item.SourceField + " = ''");
                            sbQCTranslation.AppendLine("	BEGIN");
                            sbQCTranslation.AppendLine("		SET @" + item.SourceField + " = '" + item.Translating.ConstValue.Replace("'", "''") + "'");
                            sbQCTranslation.AppendLine("	END");
                            break;
                        }
                    case TranslatingType.LookUpTable:
                        {
                            sbQCTranslation.AppendLine("	SELECT @" + item.SourceField + " = TargetValue FROM " + item.Translating.LutName + " WHERE SourceValue = @" + item.SourceField);
                            break;
                        }
                    case TranslatingType.LookUpTableReverse:
                        {
                            sbQCTranslation.AppendLine("	SELECT @" + item.SourceField + " = SourceValue FROM " + item.Translating.LutName + " WHERE TargetValue = @" + item.SourceField);
                            break;
                        }
                }
            }

            sb.AppendLine(sbQCTranslation.ToString());

            #endregion

            sb.AppendLine("	-- Insert statements for procedure here");

            #region query data tables

            string strSelect = "";
            string strFrom = "";
            string strWhere = "";

            #region query criteria

            StringBuilder sbWhere = new StringBuilder();

            foreach (QueryCriteriaItem i in rule.QueryCriteria.MappingList)
            {
                if (i.Type == QueryCriteriaType.Or) sbWhere.Append(":");
                if (i.Type == QueryCriteriaType.And) sbWhere.Append(";");
                sbWhere.AppendLine(i.GWDataDBField.GetFullFieldName(interfaceName) + " = @" + i.SourceField);
            }
            strWhere = sbWhere.ToString();
            if (strWhere.Length > 0) strWhere = strWhere.TrimStart(':').TrimStart(';').Replace(":", "	OR ").Replace(";", "	AND ");

            if (rule.CheckProcessFlag)
            {
                string indexTable = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Index);
                string strCheckProcessFlag = "(" + indexTable + ".PROCESS_FLAG <> '1' OR " + indexTable + ".PROCESS_FLAG IS NULL)";
                if (strWhere.Length > 0)
                {
                    strWhere = "(" + strWhere + "	) AND " + strCheckProcessFlag;
                }
                else
                {
                    strWhere = strCheckProcessFlag;
                }
            }

            #endregion

            #region query result

            StringBuilder sbSelect = new StringBuilder();
            foreach (QueryResultItem i in rule.QueryResult.MappingList)
            {
                if (i.Translating.Type == TranslatingType.None)
                {
                    sbSelect.Append( i.GWDataDBField.GetFullFieldName(interfaceName) + " AS " + i.TargetField + ",");
                }
                else
                {
                    string functionName = translationList[i] as string;
                    sbSelect.Append( "[dbo].[" + functionName + "](" + i.GWDataDBField.GetFullFieldName(interfaceName) + ") AS " + i.TargetField + ",");
                }
            }
            strSelect = sbSelect.ToString();
            if (strSelect.Length > 0) strSelect = strSelect.TrimEnd(',').Replace(",", ",\r\n	");

            #endregion

            #region join table

            string strIndexTable = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Index);
            string strPateintTable = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Patient);
            string strOrderTable = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Order);
            string strReportTable = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Report);

            strFrom = strIndexTable;

            if (bPatient)
            {
                strFrom = strFrom + " JOIN " + strPateintTable + " ON(" + strIndexTable + ".Data_ID = " + strPateintTable + ".Data_ID)";
                if (bOrder)
                {
                    strFrom = "(" + strFrom + " JOIN " + strOrderTable + " ON(" + strIndexTable + ".Data_ID = " + strOrderTable + ".Data_ID) )";
                    if (bReport)
                    {
                        strFrom = "(" + strFrom + " JOIN " + strReportTable + " ON(" + strIndexTable + ".Data_ID = " + strReportTable + ".Data_ID) )";
                    }
                }
            }
            else
            {
                if (bOrder)
                {
                    strFrom = strFrom + " JOIN " + strOrderTable + " ON(" + strIndexTable + ".Data_ID = " + strOrderTable + ".Data_ID)";
                    if (bReport)
                    {
                        strFrom = "(" + strFrom + " JOIN " + strReportTable + " ON(" + strIndexTable + ".Data_ID = " + strReportTable + ".Data_ID) )";
                    }
                }
                else
                {
                    if (bReport)
                    {
                        strFrom = strFrom + " JOIN " + strReportTable + " ON(" + strIndexTable + ".Data_ID = " + strReportTable + ".Data_ID)";
                    }
                }
            }

            #endregion

            sb.AppendLine("	SELECT");

            if (strSelect.Length > 0)
            {
                sb.AppendLine("	" + strSelect);
            }
            else
            {
                sb.AppendLine("	*");
            }

            sb.AppendLine("	FROM");
            sb.AppendLine("	" + strFrom);

            if (strWhere.Length > 0)
            {
                sb.AppendLine("	WHERE");
                sb.AppendLine("	" + strWhere);
            }

            #endregion

            sb.AppendLine("END");

            return sb.ToString();
        }

        //private static string GetTranslationFN(string functionName, MappingItem item)
        //{
        //    //string functionName = "fn_" + ruleName + "_" + item.TargetField;
        //    StringBuilder sb = new StringBuilder();

        //    sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].[" + functionName + "]') AND type ='FN')");
        //    sb.AppendLine("DROP FUNCTION [dbo].[" + functionName + "]");
        //    sb.AppendLine("go");
        //    sb.AppendLine("");

        //    sb.AppendLine("-- =============================================");
        //    sb.AppendLine("-- Author: GC Gateway 2.0 Rule Engine");
        //    sb.AppendLine("-- Create date: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
        //    sb.AppendLine("-- Description: Translation function for target parameter of '" + item.TargetField + "'");
        //    sb.AppendLine("-- =============================================");

        //    sb.AppendLine("CREATE FUNCTION [dbo].[" + functionName + "]");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	-- Add the parameters for the function here");
        //    sb.AppendLine("	@SourceValue nvarchar(MAX)");
        //    sb.AppendLine(")");
        //    sb.AppendLine("RETURNS nvarchar(MAX)");
        //    sb.AppendLine("AS");
        //    sb.AppendLine("BEGIN");
        //    sb.AppendLine("	-- Fill the table variable with the rows for your result set");
        //    sb.AppendLine("	DECLARE @ret nvarchar(MAX)");
        //    sb.AppendLine("");

        //    string lutName = item.Translating.LutName;
        //    string constValue = item.Translating.ConstValue.Replace("'", "''");

        //    switch (item.Translating.Type)
        //    {
        //        case TranslatingType.LookUpTable:
        //            {
        //                sb.AppendLine("	SELECT @ret = " + lutName + ".TargetValue");
        //                sb.AppendLine("		FROM " + lutName + " WHERE " + lutName + ".SourceValue = @SourceValue");
        //                sb.AppendLine("");
        //                sb.AppendLine("	IF @ret IS NULL");
        //                sb.AppendLine("	BEGIN");
        //                sb.AppendLine("		SET @ret = @SourceValue");
        //                sb.AppendLine("	END");
        //                break;
        //            }
        //        case TranslatingType.LookUpTableReverse:
        //            {
        //                sb.AppendLine("	SELECT @ret = " + lutName + ".SourceValue");
        //                sb.AppendLine("		FROM " + lutName + " WHERE " + lutName + ".TargetValue = @SourceValue");
        //                sb.AppendLine("");
        //                sb.AppendLine("	IF @ret IS NULL");
        //                sb.AppendLine("	BEGIN");
        //                sb.AppendLine("		SET @ret = @SourceValue");
        //                sb.AppendLine("	END");
        //                break;
        //            }
        //        case TranslatingType.DefaultValue:
        //            {
        //                sb.AppendLine("	SET @ret = @SourceValue");
        //                sb.AppendLine("");
        //                sb.AppendLine("	IF @ret IS NULL");
        //                sb.AppendLine("	BEGIN");
        //                sb.AppendLine("		SET @ret = '" + constValue + "'");
        //                sb.AppendLine("	END");
        //                break;
        //            }
        //        case TranslatingType.FixValue:
        //            {
        //                sb.AppendLine("	SET @ret = '" + constValue + "'");
        //                break;
        //            }
        //    }

        //    sb.AppendLine("");
        //    sb.AppendLine("	RETURN @ret");
        //    sb.AppendLine("END");
        //    sb.AppendLine("go");
        //    sb.AppendLine("");

        //    return sb.ToString();
        //}

        [Obsolete("This method is expired, please use method RuleControl.GetOutboundSPUninstall(string, IRule) instead.", false)]
        public static string GetSPUninstall<TC, TR>(string interfaceName, OutboundRule<TC, TR> rule)
            where TC : QueryCriteriaItem
            where TR : QueryResultItem
        {
            if (rule == null || interfaceName == null || interfaceName.Length < 1) return null;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("USE GWDataDB");
            sb.AppendLine("set ANSI_NULLS ON");
            sb.AppendLine("set QUOTED_IDENTIFIER ON");
            sb.AppendLine("go");
            sb.AppendLine("");

            foreach (QueryResultItem item in rule.QueryResult.MappingList)
            {
                if (item.Translating.Type == TranslatingType.None) continue;
                string functionName = "fn_" + rule.RuleName + "_" + item.TargetField;

                sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].[" + functionName + "]') AND type ='FN')");
                sb.AppendLine("DROP FUNCTION [dbo].[" + functionName + "]");
                sb.AppendLine("go");
                sb.AppendLine("");
            }

            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].[" + rule.RuleName + "]') AND type ='P')");
            sb.AppendLine("DROP PROCEDURE [dbo].[" + rule.RuleName + "]");
            sb.AppendLine("go");
            sb.AppendLine("");

            return sb.ToString();
        }

        #endregion
    }
}
