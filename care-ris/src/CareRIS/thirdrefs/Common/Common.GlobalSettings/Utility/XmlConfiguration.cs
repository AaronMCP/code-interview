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
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Reflection;

namespace CommonGlobalSettings
{
    public class XmlConfiguration
    {
        private static XmlDocument configuration = null;

        public static XmlDocument Configuration
        {
            get 
            { 
                return configuration; 
            }
            set
            {
                //validate the value with the Schema first
                configuration = value;
            }
        }

        public static string GetActionByMessage(string messageName)
        {
            if (Configuration != null)
            {
                XmlNode node = Configuration.SelectSingleNode("/GCRIS-config/action-mappings");
                foreach (XmlNode n in node.ChildNodes)
                {
                    if(n.NodeType == XmlNodeType.Comment)
                    {
                        continue;
                    }

                    if (n.Attributes.GetNamedItem("message").Value.Equals(messageName))
                    {
                        return n.Attributes.GetNamedItem("type").Value;
                    }
                }
            }

            return null;
        }

       
    }
}
