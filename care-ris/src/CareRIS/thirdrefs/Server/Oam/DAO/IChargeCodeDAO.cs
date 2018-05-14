using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonGlobalSettings;

namespace Server.DAO.Oam
{
    public interface IChargeCodeDao
    {
        DataSet GetAllChargeCodes();
        void AddChargeCode(ChargeCodeModel model);
        void UpdateChargeCode(ChargeCodeModel model);
        void DeleteChargeCode(string chargeCode);
        bool IsChargeCodeInUse(string chargeCode);
    }
}
