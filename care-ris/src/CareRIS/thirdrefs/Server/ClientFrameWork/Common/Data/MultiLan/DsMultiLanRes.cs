namespace Server.ClientFramework.Common.Data.MultiLan
{


    partial class DsMultiLanRes
    {
        //public void MergerFromStringRes(DsStringRes ds, int LCID, string ModuleID)
        public void MergerFromStringRes(DsStringRes ds, int LCID)
        {
            DsMultiLanRes.MultiLanResRow MyRow;
            foreach (DsStringRes.StringResRow row in ds.StringRes.Rows)
            {
                //MyRow = this.MultiLanRes.FindByLCIDModuleIDName(LCID, ModuleID, row.Name);
                MyRow = this.MultiLanRes.FindByLCIDName(LCID, row.Name);
                if (MyRow == null)
                {
                    MyRow = this.MultiLanRes.NewMultiLanResRow();
                    MyRow.LCID = LCID;
                    //MyRow.ModuleID = ModuleID;
                    MyRow.Name = row.Name;
                    MyRow.Value = row.Value;
                    this.MultiLanRes.AddMultiLanResRow(MyRow);
                }
                else
                {
                    MyRow.Name = row.Name;
                    MyRow.Value = row.Value;
                }

            }
        }
    }
}
