/****************************************************************************/
/*                                                                          */
/*                          Copyright 2006                                  */
/*                       EASTMAN KODAK COMPANY                              */
/*                        All Rights Reserved.                              */
/*                                                                          */
/*     This software contains proprietary and confidential information      */
/*     belonging to EASTMAN KODAK COMPANY, and may not be decompiled,       */
/*     disassembled, disclosed, reproduced or copied without the prior      */
/*     written consent of EASTMAN KODAK COMPANY.                            */
/*                                                                          */
/*                        Author : Bruce Deng
/****************************************************************************/


using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Server.Business.Common.Impl;

namespace Server.Business.Common
{
    public class BusinessFactory
    {
        private static BusinessFactory instance = new BusinessFactory();
        private Hashtable flyWeightPool = new Hashtable();

        private BusinessFactory()
        {

        }

        public static BusinessFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public ICommonFunctionBusiness GetCommonFunctionService()
        {
            ICommonFunctionBusiness service = flyWeightPool["CommonFunctionService"] as ICommonFunctionBusiness;

            if (service == null)
            {
                service = new CommonFunctionImpl();
                flyWeightPool.Add("CommonFunctionService", service);
            }

            return service;
        }
     
    }
}
