using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HYS.IM.MessageDevices.CSBAdpater.Outbound.Config;
using HYS.IM.Common.Logging;

namespace HYS.IM.MessageDevices.CSBAdpater.Outbound.Controler
{
    internal static class CSBrokerOutboundHelper
    {
        public static void ReplaceValueInCSBrokerDataSet(DataTable tb, ProgramContext context)
        {
            if (tb == null || !context.ConfigMgr.Config.EnableValueReplacement) return;
            context.Log.Write("Begin value replacement in CS Broker DataSet.");

            try
            {
                int i = 1;
                int c = tb.Rows.Count;
                foreach (DataRow dr in tb.Rows)
                {
                    context.Log.Write(string.Format("Processing record {0}/{1}", i++, c));
                    foreach (ValueReplacementRule r in context.ConfigMgr.Config.ValueReplacement)
                    {
                        if (!tb.Columns.Contains(r.FieldName))
                        {
                            context.Log.Write(LogType.Warning, "Cannot find column: " + r.FieldName);
                            continue;
                        }

                        string oldValue = dr[r.FieldName] as string;
                        //string newValue = Regex.Replace(oldValue, r.MatchExpression, r.ReplaceExpression);
                        string newValue = r.Replace(oldValue);
                        dr[r.FieldName] = newValue;
                        context.Log.Write(string.Format("{0}:{1}->{2}", r.FieldName, oldValue, newValue));
                    }
                }
            }
            catch (Exception err)
            {
                context.Log.Write(err);
            }

            context.Log.Write("Finish value replacement in CS Broker DataSet.");
        }
    }
}
