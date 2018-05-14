using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Server.Business.QualityControl.Impl;

namespace Server.Business.QualityControl
{
    public sealed class BusinessFactory
    {
        private static BusinessFactory instance = new BusinessFactory();
        private Hashtable InstancePool = new Hashtable();

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

        public IQualityControlBusiness GetQualityControl()
        {
            IQualityControlBusiness QualityControl = InstancePool["QualityControl"] as IQualityControlBusiness;

            if (QualityControl == null)
            {
                QualityControl = new QualityControlImpl();
                InstancePool.Add("QualityControl", QualityControl);
            }

            return QualityControl;
        }
    }
}
