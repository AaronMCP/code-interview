using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Server.Business.Oam
{
    public interface IClientConfigService
    {
        DataSet GetClientConfigDataSet();
    }
}
