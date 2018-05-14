using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonGlobalSettings;
using Server.DAO.Oam;
using Server.DAO.Oam.Impl;
using System.Text.RegularExpressions;
namespace Server.Business.Oam.Impl
{
    public class ChargeCodeServiceImpl : IChargeCodeService
    {
        private IChargeCodeDao chargeCodeDao = DataBasePool.Instance.GetDBProvider();


        #region IChargeCodeService Members

        public DataSet GetAllChargeCodes()
        {
            return chargeCodeDao.GetAllChargeCodes();
        }

        public void AddChargeCode(ChargeCodeModel model)
        {
             chargeCodeDao.AddChargeCode(model);
        }

        public void UpdateChargeCode(ChargeCodeModel model)
        {
             chargeCodeDao.UpdateChargeCode(model);
        }

        public void DeleteChargeCode(string chargeCode)
        {
            if (chargeCodeDao.IsChargeCodeInUse(chargeCode))
            {
                throw new Exception("This chargeCode is in use!");
            }
            else
            {
                chargeCodeDao.DeleteChargeCode(chargeCode);
            }
        }

        #endregion
    }
}
