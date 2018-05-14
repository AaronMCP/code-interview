using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI;
using Dapper;
using System.Drawing;
using System.Configuration;
using System.Transactions;

namespace Hys.PrintTemplateManager
{
    static class Utility
    {
        private static IDbConnection dbContext = Context.DbContext;
        public readonly static Color TypeDefaultColor = Color.Red;
        public readonly static Color ModalityDefaultColor = Color.DarkSeaGreen;
        public readonly static Color AllDefaultColor = Color.Blue;
        private static Dictionary<string, string> LangDic = Context.LangDic;

        public static string Lang(string key)
        {
            string val;
            if (!LangDic.TryGetValue(key, out val))
            {
                val = "";
            }
            return val;
        }

        private static DataSet LoadPrintTemplateType()
        {
            var dataSet = new DataSet();
            var sql = "select Value, Text,ShortcutCode,IsDefault from tbDictionaryValue where Tag=14 order by Value";
            var sysSql = "select Value from tbSystemProfile where ModuleID = '0400' and Name = 'Report_CanUseReturnVisit'";
            var val = dbContext.ExecuteScalar(sysSql).ToString();
            if (val == "0")
            {
                sql = "select Value,Text,ShortcutCode,IsDefault from tbDictionaryValue where Tag=14 and Text <> 'ReturnVisit' order by Value";
            }

            var dt = dbContext.ExecuteDataTable(sql);
            dt.TableName = "PrintTemplateType";
            dataSet.Tables.Add(dt);

            foreach (DataRow dr in dt.Rows)
            {
                string data = dr["ShortCutCode"].ToString();
                if (string.IsNullOrEmpty(data)) continue;
                DataTable dtlevel2 = dbContext.ExecuteDataTable(data);
                dtlevel2.TableName = dr["Value"].ToString();
                dataSet.Tables.Add(dtlevel2);
            }
            return dataSet;
        }

        public static DataSet LoadSubPrintTemplateInfo(int type)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            StringBuilder sql = new StringBuilder("select * from tbPrintTemplate where 1=1 ");
            if (type >= 0)
            {
                sql.AppendFormat(" AND type={0}", type);
            }

            var table = dbContext.ExecuteDataTable(sql.ToString());
            ds.Tables.Add(table);
            return ds;
        }

        public static DataSet loadData(ref DataTable printTemplate)
        {
            DataSet ds = LoadPrintTemplateType();
            var subDs = LoadSubPrintTemplateInfo(-1);

            printTemplate.Merge(subDs.Tables[0]);

            return ds;
        }

        static RadTreeNode loadPrintTemplatesBySite(RadTreeView treeview, int PrintType, DataSet dsTemplateTypes, ref DataTable dtPrintTemplate, string site)
        {
            RadTreeNode root = new RadTreeNode();
            root.Name = site;
            root.Text = string.IsNullOrEmpty(site) ? Lang("Global") : site;
            root.Tag = "";
            treeview.Nodes.Add(root);

            DataTable dtTemplateType = dsTemplateTypes.Tables["PrintTemplateType"];
            if (dtTemplateType == null)
                return null;

            // folders
            foreach (DataRow curRow in dtTemplateType.Rows)
            {

                string strType = curRow["Value"].ToString().Trim();
                if (PrintType != -1 && strType != PrintType.ToString())
                {
                    continue;
                }

                RadTreeNode node = new RadTreeNode();
                node.Name = curRow["Value"].ToString().Trim();
                node.Text = Lang("PrintPanel." + curRow["Text"].ToString() + ".Node.Text");
                node.Tag = "";
                root.Nodes.Add(node);

                string shortCutCode = curRow["shortCutCode"].ToString();
                if (string.IsNullOrWhiteSpace(shortCutCode))
                {
                    node.Tag = "TypeNode";
                    continue;
                }

                if (dsTemplateTypes.Tables.Contains(curRow["Value"].ToString()))
                {
                    foreach (DataRow dr in dsTemplateTypes.Tables[curRow["Value"].ToString()].Rows)
                    {
                        RadTreeNode treeNode = new RadTreeNode();
                        treeNode.Text = dr["Text"].ToString();
                        treeNode.Name = dr["Value"].ToString();
                        treeNode.Tag = "TypeNode";
                        node.Nodes.Add(treeNode);
                    }
                }
            }

            // leaves
            foreach (RadTreeNode curNode in root.Nodes)
            {
                DataRow[] drs = dtPrintTemplate.Select("Type='" + curNode.Name + "' AND Site='" + site + "'");
                foreach (DataRow curRow in drs)
                {
                    RadTreeNode node = new RadTreeNode();
                    node.Name = curRow["TemplateGuid"].ToString().Trim();
                    node.Text = curRow["TemplateName"].ToString().Trim();
                    node.Tag = "LeafFile";
                    if (curRow["IsDefaultByType"].ToString() == "1" && curRow["IsDefaultByModality"].ToString() == "1")
                    {
                        node.ForeColor = AllDefaultColor;
                    }
                    else if (curRow["IsDefaultByType"].ToString() == "1")
                    {
                        node.ForeColor = TypeDefaultColor;
                    }
                    else if (curRow["IsDefaultByModality"].ToString() == "1")
                    {
                        node.ForeColor = ModalityDefaultColor;
                    }

                    string modalityTye = curRow["ModalityType"].ToString();
                    if (modalityTye == null || modalityTye.Trim().Length < 1)
                    {
                        curNode.Nodes.Add(node);
                    }
                    else
                    {
                        if (curNode.Nodes.Find(Convert.ToString(curRow["ModalityType"]).Trim(), false).GetLength(0) > 0)
                            curNode.Nodes[Convert.ToString(curRow["ModalityType"]).Trim()].Nodes.Add(node);
                    }
                }
            }

            return root;
        }
        static DataTable GetSiteList(string domain)
        {
            var table = dbContext.ExecuteDataTable(string.Format("select site from tbSiteList where Domain='{0}'", domain));
            return table;
        }
        public static void loadPrintTemplates(RadTreeView treeview, int printType, out DataTable templateType, ref DataTable printTemplate, bool allSite)
        {

            DataSet dsTemplateTypes = loadData(ref printTemplate);

            templateType = dsTemplateTypes.Tables["PrintTemplateType"];
            RadTreeNode globalRoot = loadPrintTemplatesBySite(treeview, printType, dsTemplateTypes, ref printTemplate, string.Empty);
            if (globalRoot != null) globalRoot.Expand();

            if (allSite)
            {
                string domain = ConfigurationManager.AppSettings["Domain"];
                DataTable dt = GetSiteList(domain);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        loadPrintTemplatesBySite(treeview, printType, dsTemplateTypes, ref printTemplate, System.Convert.ToString(dr["Site"]));
                    }
                }
            }
            else
            {
                loadPrintTemplatesBySite(treeview, printType, dsTemplateTypes, ref printTemplate, ConfigurationManager.AppSettings["Site"]);
            }
        }
        public static DataSet LoadPrintTemplateField(int type)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = type == 7 ?
                "select tqr.FieldName,tq.QueryName as SubType from tbQueryResultColumn tqr inner join tbQuery tq on tqr.QueryID =tq.ID" :
                string.Format("select FieldName,SubType from tbPrintTemplateFields where type={0}", type);
            dt = dbContext.ExecuteDataTable(sql);
            ds.Tables.Add(dt);
            return ds;
        }
        public static string getRootNodeName(RadTreeNode node)
        {
            if (node != null)
            {
                while (node.Parent != null)
                {
                    node = node.Parent;
                }

                return node.Name;
            }

            return string.Empty;
        }

        public static bool TemplateNameExists(string templateName, int type, string site)
        {
            string sql = string.Format(
                "select TemplateName from tbPrintTemplate where TemplateName='{0}' and Type = {1} AND SITE='{2}'",
                templateName, type, site);
            DataTable myTable = dbContext.ExecuteDataTable(sql);
            if (myTable.Rows.Count == 0)
                return false;
            foreach (DataRow myRow in myTable.Rows)
            {
                if (Convert.ToString(myRow["TemplateName"]) == templateName)
                {
                    return true;

                }
            }
            return false;
        }

        public static bool AddPrintTemplate(string id, int type, string templateName, string templateInfo,
            int isDefaultByType, string modalityType, int isDefaultByModality, string site)
        {
            var sql =
@"Insert into tbPrintTemplate
(TemplateGuid,Type,TemplateName,TemplateInfo,IsDefaultByType,Version,ModalityType,IsDefaultByModality,Domain,Site)
values(@TemplateGuid,@Type,@TemplateName,@TemplateInfo,@IsDefaultByType,0,@ModalityType,@IsDefaultByModality,@Domain,@Site)";
            var result = dbContext.Execute(sql, new
            {
                TemplateGuid = id,
                Type = type,
                TemplateName = templateName,
                TemplateInfo = System.Text.Encoding.Unicode.GetBytes(templateInfo),
                IsDefaultByType = isDefaultByType,
                ModalityType = modalityType,
                IsDefaultByModality = isDefaultByModality,
                Domain = ConfigurationManager.AppSettings["Domain"],
                Site = site
            });

            return result == 1;
        }

        public static int GetLatestVersion(string templateGuid)
        {
            string sql = string.Format("select Version from tbPrintTemplate where TemplateGuid = '{0}'", templateGuid);
            var latestVersion = dbContext.QuerySingle<int>(sql);
            return latestVersion;
        }

        public static string LoadPrintTemplateInfo(string templateGuid)
        {
            var sql = string.Format("select  TemplateInfo from tbPrintTemplate where TemplateGuid='{0}'", templateGuid);
            var rawResult = dbContext.QuerySingle<byte[]>(sql);
            var strResult = System.Text.Encoding.Unicode.GetString(rawResult);
            return strResult;
        }

        public static bool isGlobalNode(RadTreeNode node)
        {
            if (node != null)
            {
                while (node.Parent != null)
                {
                    node = node.Parent;
                }

                return string.IsNullOrEmpty(node.Name);
            }

            return false;
        }

        public static bool DeletePrintTemplate(string templateGuid)
        {
            string sql = string.Format("Delete from tbPrintTemplate where TemplateGuid = '{0}'", templateGuid);
            dbContext.Execute(sql);
            return true;
        }

        public static bool SetDefault(int type, string strModalityType, string strTemplateGuid, string site)
        {
            string sql1 = string.Format("select TemplateGuid from tbPrintTemplate where Type = {0} and IsDefaultByType=1 AND Site='{1}'", type, site);
            string sql2 = string.Format("select TemplateGuid from tbPrintTemplate where Type = {0} and ModalityType = '{1}' and IsDefaultByModality=1 AND Site='{2}'", type, strModalityType, site);
            string sql3 = string.Format("Update tbPrintTemplate set IsDefaultByType = 1 where TemplateGuid = '{0}' AND Site='{1}'", strTemplateGuid, site);
            string sql4 = string.Format("Update tbPrintTemplate set IsDefaultByModality = 1 where TemplateGuid = '{0}' AND Site='{1}'", strTemplateGuid, site);
            if (strModalityType == "")
            {
                Object o = dbContext.ExecuteScalar(sql1);
                if (o != null)
                {
                    string lastTemplateGuid = o.ToString();
                    string sql5 = string.Format("Update tbPrintTemplate set IsDefaultByType = 0 where TemplateGuid = '{0}' AND Site='{1}'", lastTemplateGuid, site);
                    using (var trans = new TransactionScope())
                    {
                        dbContext.Execute(sql5);
                        dbContext.Execute(sql3);
                        trans.Complete();
                    }
                }
                else
                {
                    dbContext.Execute(sql3);
                }
            }
            else
            {
                Object o = dbContext.ExecuteScalar(sql2);
                if (o != null)
                {
                    string lastTemplateGuid = o.ToString();
                    string sql5 = string.Format("Update tbPrintTemplate set IsDefaultByModality = 0 where TemplateGuid = '{0}' AND Site='{1}'", lastTemplateGuid, site);
                    using (var trans = new TransactionScope())
                    {
                        dbContext.Execute(sql5);
                        dbContext.Execute(sql4);
                        trans.Complete();
                    }
                }
                else
                {
                    dbContext.Execute(sql4);
                }
            }
            return true;
        }
        public static bool isLeafFileNode(RadTreeNode node)
        {
            if (node != null)
            {
                return System.Convert.ToString(node.Tag).ToLower() == "leaffile";
            }

            return false;
        }
        public static bool ModifyPrintTemplateFieldInfo(string templateGuid, string templateInfo)
        {
            dbContext.Execute(
                @"Update tbPrintTemplate 
                  set TemplateInfo = @TemplateInfo,Version = Version+1  
                  where TemplateGuid = @TemplateGuid",
                new
                {
                    TemplateInfo = System.Text.Encoding.Unicode.GetBytes(templateInfo),
                    TemplateGuid = templateGuid
                });
            return true;
        }

        public static bool ModifyPrintTemplateName(string templateGuid, string templateName)
        {
            dbContext.Execute(
                @"Update tbPrintTemplate 
                    set TemplateName = @templateName , Version = Version+1 
                    where TemplateGuid = @templateGuid",
                new
                {
                    templateName = templateName,
                    TemplateGuid = templateGuid
                });
            return true;
        }

        public static DataSet LoadExportTemplateType()
        {
            var ds = new DataSet();
            var table = dbContext.ExecuteDataTable("select Value, Text from tbDictionaryValue where Tag=55 order by Value");
            ds.Tables.Add(table);
            return ds;
        }

        public static DataSet LoadGeneralStatType()
        {
            var ds = new DataSet();
            var table = dbContext.ExecuteDataTable("Select ID,Queryname from tbQuery");
            ds.Tables.Add(table);
            return ds;
        }

        public static DataSet LoadSubExportTemplateInfo()
        {
            var ds = new DataSet();
            var table = dbContext.ExecuteDataTable("select TemplateGuid,TemplateName,Type,ChildType,Descriptions from tbExportTemplate");
            ds.Tables.Add(table);
            return ds;
        }

        public static DataSet LoadExportTemplateInfo(string templateGuid)
        {
            DataSet ds = new DataSet();
            var table = dbContext.ExecuteDataTable(
                "select TemplateInfo from tbExportTemplate where TemplateGuid=@TemplateGuid",
                new { TemplateGuid = templateGuid });
            ds.Tables.Add(table);
            return ds;
        }

        public static bool DeleteExportTemplate(string templateGuid)
        {
            dbContext.Execute("Delete from tbExportTemplate where TemplateGuid = @TemplateGuid",
                new
                {
                    TemplateGuid = templateGuid
                });
            return true;
        }

        public static bool ModifyExportTemplate(string templateName, string descriptions, int isDefaultByType, int isDefaultByChildType, string tplGuid)
        {
            dbContext.Execute(
                @"Update tbExportTemplate 
                  set TemplateName=@templateName,Descriptions=@descriptions,IsDefaultByChildType=@isDefaultByChildType 
                  where TemplateGuid = @templateGuid",
                new
                {
                    templateName = templateName,
                    descriptions = descriptions,
                    isDefaultByChildType = isDefaultByChildType,
                    templateGuid = tplGuid
                }
            );
            return true;
        }

        public static bool ModifyExportTemplateInfo
            (string tplGuid, byte[] templateInfo, string templateName, string descriptions, int isDefaultByType, int isDefaultByChildType)
        {
            dbContext.Execute(
                @"Update tbExportTemplate 
                  set TemplateInfo = @templateInfo,TemplateName=@templateName,Descriptions=@descriptions,
                      IsDefaultByType=@isDefaultByType
                      IsDefaultByChildType=@isDefaultByChildType 
                  where TemplateGuid = @tplGuid",
                new
                {
                    tplGuid = tplGuid,
                    templateInfo = templateInfo,
                    templateName = templateName,
                    descriptions = descriptions,
                    isDefaultByType = isDefaultByType,
                    isDefaultByChildType = isDefaultByChildType
                });
            return true;
        }

        public static bool AddExportTemplate
            (string templateGuid, int type, string childType, string templateName, byte[] templateInfo,
             string descriptions, int isDefaultByType, int isDefaultByChildType)
        {
            dbContext.Execute(
                @"Insert into tbExportTemplate
                    (TemplateGuid,Type,ChildType,TemplateName,TemplateInfo,Descriptions,IsDefaultByType,
                        IsDefaultByChildType,Domain) 
                  values
                    (@templateGuid,@type,@childType,@templateName,@templateInfo,@descriptions,@isDefaultByType,
                        @isDefaultByChildType,@domain)",
                new
                {
                    templateGuid = templateGuid,
                    type = type,
                    childType = childType,
                    templateName = templateName,
                    templateInfo = templateInfo,
                    descriptions = descriptions,
                    isDefaultByType = isDefaultByType,
                    isDefaultByChildType = isDefaultByChildType,
                    domain = ConfigurationManager.AppSettings["Domain"]
                });
            return true;
        }

        public static DataTable GetDictionary(int tag)
        {
            var result = dbContext.ExecuteDataTable(
@"SELECT [Tag]
    ,[Value]
    ,[Text]
    ,[IsDefault]
    ,[ShortcutCode]
    ,[OrderID]
    ,[Domain]
    ,[UniqueID]
    ,[mapTag]
    ,[MapValue]
    ,[Site]
FROM [tbDictionaryValue]
where tag=@tag", new { tag = tag });
            return result;
        }
    }
}
