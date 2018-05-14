using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.SQLInboundAdapterObjects
{
    public class SQLInboundRule : InboundRule<SQLInQueryCriteriaItem, SQLInQueryResultItem>
    {
        private string _inputParameterSPName = null;
        public string GenerateInputParameterSPName(string interfaceName)
        {
            if (_inputParameterSPName == null)
            {
                _inputParameterSPName = "SP_" + interfaceName + "_" + RuleID + "_GetInputParameter";
            }
            return _inputParameterSPName;
        }

        private Nullable<bool> _inputParameterSPEnable = null;
        public bool IsInputParameterSPEnable()
        {
            if (_inputParameterSPEnable == null)
            {
                _inputParameterSPEnable = false;

                foreach (SQLInQueryCriteriaItem item in QueryCriteria.MappingList)
                {
                    if (item.IsGetFromStorageProcedure)
                    {
                        _inputParameterSPEnable = true;
                        break;
                    }
                }
            }
            return (bool)_inputParameterSPEnable;
        }

        public string GenerateInputParameterSPInstallScript(string interfaceName)
        {
            string spName = GenerateInputParameterSPName(interfaceName);

            StringBuilder sb = new StringBuilder();
            sb.Append(GenerateInputParameterSPUninstallScript(interfaceName));
            sb.Append("CREATE PROCEDURE [dbo].[").Append(spName).AppendLine("]");
            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            sb.AppendLine("SET NOCOUNT ON");
            sb.AppendLine("-- Please add your SQL statement here. --");
            sb.AppendLine("END");
            sb.AppendLine("go");
            sb.AppendLine("");

            string str = sb.ToString();
            return str;
        }
        public string GenerateInputParameterSPUninstallScript(string interfaceName)
        {
            string spName = GenerateInputParameterSPName(interfaceName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].[" + spName + "]') AND type ='P')");
            sb.AppendLine("DROP PROCEDURE [dbo].[" + spName + "]");
            sb.AppendLine("go");
            sb.AppendLine("");

            string str = sb.ToString();
            return str;
        }

        private string _inputParameterSPStatement;
        [XCData(true)]
        public string InputParameterSPStatement
        {
            get { return _inputParameterSPStatement; }
            set { _inputParameterSPStatement = value; }
        }

        // this function is call by AdapterConfig.exe, before that GenerateInputParameterSPName() should be called to generate SP name
        public override string GetInstallDBScript()
        {
            if (!IsInputParameterSPEnable()) return "";
            //if (_inputParameterSPName == null) return "";
            return InputParameterSPStatement;
        }
        // this function is call by AdapterConfig.exe, before that GenerateInputParameterSPName() should be called to generate SP name
        public override string GetUninstallDBScript()
        {
            if (!IsInputParameterSPEnable()) return "";
            //if (_inputParameterSPName == null) return "";
            return GenerateInputParameterSPUninstallScript("");
        }
    }
}
