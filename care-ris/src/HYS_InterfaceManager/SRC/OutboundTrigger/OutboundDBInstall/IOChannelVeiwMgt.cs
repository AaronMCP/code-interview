using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace OutboundDBInstall
{
    class IOChannelVeiwMgt
    {
        static DataGridView _dgv = null;
        static OutboundConfig _Config= null;
        static IOChannels _chs = null;
        //static bool bInited = false;
        //static DataSet _ds = null;

        static public void Init(DataGridView gdv, OutboundConfig Config)
        {
            _dgv = gdv;
            _Config = Config;
            _chs = _Config.IOChannels ;
            
        }

        static private bool AddChannel2View(IOChannel ch)
        {
            int r = _dgv.Rows.Add(ch.INameInbound, 
                          ch.EventTypeListStrInbound,                                          
                          ch.INameInbound + "_" + _Config.INameOutbound + "_trigger");
            return r >= 0;
        }

        static private bool EditChannel2View(IOChannel ch)
        {
            foreach (DataGridViewRow row in _dgv.Rows)
            {
                if (row.Cells[0].Value.ToString().Trim().ToUpper() == ch.INameInbound.Trim().ToUpper())
                {
                    row.Cells[1].Value = ch.EventTypeListStrInbound;                    
                    row.Cells[2].Value = ch.INameInbound + "_" + _Config.INameOutbound + "_trigger";
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// update datagridview from iochannels
        /// </summary>
        static public void UpdateAllView()
        {
            _dgv.Rows.Clear();
            foreach (IOChannel ch in _chs)
            {
                AddChannel2View(ch);                
            }
        }

        /// <summary>
        /// Add channel to iochannels and datagrid
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        static public bool AddChannel(IOChannel ch)
        {
            AddChannel2View(ch);
            _chs.Add(ch);
            return true;
        }

        /// <summary>
        /// update a existed channel, refresh datagridview and iochannels
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        static public bool EditChannel(IOChannel ch)
        {
            if (!EditChannel2View(ch))
                AddChannel2View(ch);
            IOChannel tempch = _chs.FindChannel(ch.INameInbound);
            if (tempch == null)
                _chs.Add(ch);
            else
                tempch = ch;

            return true;
        }

        static public bool DeleteChannel(string sInboundName)
        {
            IOChannel ch= _chs.FindChannel(sInboundName);
            if (ch != null)
            {
                _chs.Remove(ch);
                for (int i = 0; i < _dgv.Rows.Count; i++)
                {
                    DataGridViewRow row = _dgv.Rows[i];
                    if (row.Cells[0].Value.ToString().Trim().ToUpper() ==
                        sInboundName.Trim().ToUpper())
                        _dgv.Rows.Remove(row);
                }
            }
            return true;
                       
        }

        static public bool DeleteChannel(IOChannel ch)
        {
            return DeleteChannel(ch.INameInbound);            
        }
             
    }
}
