using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.Rule
{
    public class RuleScript
    {
        private RuleScriptType _type = RuleScriptType.None;
        public RuleScriptType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private string _fileName = "";
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        private const string _backupSign = "~";
        public string GetBackupFileName()
        {
            return _backupSign + FileName;
        }

        public RuleScript()
        {
        }
        public RuleScript(RuleScriptType type, string filename)
        {
            _type = type;
            _fileName = filename;
        }

        public static RuleScript InstallTable = new RuleScript(RuleScriptType.Install, "InstallTable.sql");
        public static RuleScript InstallTrigger = new RuleScript(RuleScriptType.Install, "InstallTrigger.sql");
        public static RuleScript InstallConfigDB = new RuleScript(RuleScriptType.Install, "InstallConfigDB.sql");
        public static RuleScript InstallLUT = new RuleScript(RuleScriptType.Install, "InstallLUT.sql");
        public static RuleScript InstallSP = new RuleScript(RuleScriptType.Install, "InstallSP.sql");

        public static RuleScript UninstallTable = new RuleScript(RuleScriptType.Uninstall, "UninstallTable.sql");
        public static RuleScript UninstallTrigger = new RuleScript(RuleScriptType.Uninstall, "UninstallTrigger.sql");
        public static RuleScript UninstallConfigDB = new RuleScript(RuleScriptType.Uninstall, "UninstallConfigDB.sql");
        public static RuleScript UninstallLUT = new RuleScript(RuleScriptType.Uninstall, "UninstallLUT.sql");
        public static RuleScript UninstallSP = new RuleScript(RuleScriptType.Uninstall, "UninstallSP.sql");
    }
}
