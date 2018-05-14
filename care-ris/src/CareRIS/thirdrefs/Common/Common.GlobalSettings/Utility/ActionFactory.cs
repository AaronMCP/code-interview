#region FileBanner
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
/*     Author : Caron Zhao                                                  */
/****************************************************************************/
#endregion

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using Common.Action;
using Common.ActionResult;

namespace CommonGlobalSettings
{
    /// <summary>
    /// Summary description for ActionFactory
    /// </summary>
    public sealed class ActionFactory
    {
        private static ActionFactory instance = new ActionFactory();
        private Hashtable flyWeightPool = new Hashtable();

        private ActionFactory()
        {

        }

        public static ActionFactory Instance
        {
            get 
            { 
                return instance; 
            }
        }

        public BaseAction GetActionByMessage(string messageName)
        {
            string className =  XmlConfiguration.GetActionByMessage(messageName);
            BaseAction action = flyWeightPool[className] as BaseAction;
            
            if(action == null)
            {
                action = CreateObjectByClassName(className);
                flyWeightPool.Add(className, action);
            }

            return action;
        }

        private BaseAction CreateObjectByClassName(string className)
        {
            try
            {
                int index = className.IndexOf(".Action.");
                string assemblyName = className.Substring(0, index);
                Type type = Type.GetType(className + "," + assemblyName);
                
                object o = Activator.CreateInstance(type);
                if (o == null)
                {
                    throw new System.Exception();
                }

                return o as BaseAction;
            }
            catch (Exception e)
            {
                // Wrap the exception in one of our types.
                System.Diagnostics.Trace.WriteLine("Error loading assembly: " + className + ", message: " + e.ToString());

                string message = className;
                if (null != e.InnerException)
                {
                    message += " -- (" + e.InnerException.Message + ")";
                }
                throw new GCRISException(message, e);
            }
        }
    }
}
