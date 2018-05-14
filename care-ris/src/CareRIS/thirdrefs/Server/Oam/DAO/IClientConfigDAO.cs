using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Server.DAO.Oam
{
    public interface IClientConfigDAO
    {
        DataSet GetClientConfigDataSet();
    }
}
