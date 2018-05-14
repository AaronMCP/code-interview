using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class HL7CharacterSets
    {
        public HL7CharacterSets()
        {
            sCharacterSets = "";
            //saHL7Sets = null;
            //saXMLSets = null;
            //int iCounter = 0;
            //string sHL7Set = "";
            //string sXMLSet = "";
            /*
            try
            {
                FileInputStream f = new FileInputStream("hl7charactersets.txt");
                InputStreamReader inReader = new InputStreamReader(f);
                BufferedReader r = new BufferedReader(inReader);
                do
                {
                    if(!r.ready())
                        break;
                    String sLine = r.readLine();
                    if(sLine != null)
                    {
                        sLine = sLine.trim();
                        int iPos = sLine.indexOf(" ");
                        if(iPos < 0)
                            iPos = sLine.indexOf("\t");
                        if(iPos >= 0)
                        {
                            sHL7Set = String.valueOf(sHL7Set) + String.valueOf(String.valueOf(String.valueOf(sLine.substring(0, iPos).trim())).concat("|"));
                            sXMLSet = String.valueOf(sXMLSet) + String.valueOf(String.valueOf(String.valueOf(sLine.substring(iPos).trim())).concat("|"));
                            iCounter++;
                        }
                    }
                } while(true);
                r.close();
                inReader.close();
                f.close();
            }
            catch(Exception e)
            {
                debug("Error reading 'hl7charactersets.txt'.  ".concat(String.valueOf(String.valueOf(e.getMessage()))));
                debug("There will be no support for character set mapping between HL7 and XML.");
                iCounter = 0;
                sCharacterSets = "";
            }
            if(iCounter > 0)
            {
                saHL7Sets = new String[iCounter];
                saXMLSets = new String[iCounter];
                for(int i = 0; i < iCounter; i++)
                {
                    int iHL7Sep = sHL7Set.indexOf("|");
                    int iXMLSep = sXMLSet.indexOf("|");
                    saHL7Sets[i] = sHL7Set.substring(0, iHL7Sep);
                    saXMLSets[i] = sXMLSet.substring(0, iXMLSep);
                    sHL7Set = sHL7Set.substring(iHL7Sep + 1);
                    sXMLSet = sXMLSet.substring(iXMLSep + 1);
                }

            }*/

        }

        public string getXML(string sHL7CharacterSet)
        {
            string sXMLCharacterSet = sHL7CharacterSet;
            /*if(saHL7Sets != null)
            {
                int i = 0;
                do
                {
                    if(i >= saHL7Sets.length)
                        break;
                    if(saHL7Sets[i].equals(sHL7CharacterSet))
                    {
                        sXMLCharacterSet = saXMLSets[i];
                        break;
                    }
                    i++;
                } while(true);
            } else
            {
                sXMLCharacterSet = "ISO-8859-1";
            }*/
            sXMLCharacterSet = "UTF-8";
            return sXMLCharacterSet;
        }

        public string getHL7(string sXMLCharacterSet)
        {
            string sHL7CharacterSet = sXMLCharacterSet;
            /*if(saXMLSets != null)
            {
                int i = 0;
                do
                {
                    if(i >= saXMLSets.length)
                        break;
                    if(saXMLSets[i].equals(sXMLCharacterSet))
                    {
                        sHL7CharacterSet = saHL7Sets[i];
                        break;
                    }
                    i++;
                } while(true);
            } else
            {
                sHL7CharacterSet = "8859/1";
            }
             */
            sHL7CharacterSet = "8859/1"; 
            return sHL7CharacterSet;
        }

        private void debug(string sStr)
        {
            /*
            String sDateFormat = "yyyy-MM-dd hh:mm:ss:SSS";
            DateTime dCurrent = new DateTime(System.currentTimeMillis());
            SimpleDateFormat sdfApp = new SimpleDateFormat(sDateFormat);
            sdfApp.applyPattern(sDateFormat);
            String sLogDate = sdfApp.format(dCurrent);
            //System.out.println(String.valueOf(String.valueOf((new StringBuffer(String.valueOf(String.valueOf(sLogDate)))).append("   ").append(sStr))));
             * */
        }

        private String sCharacterSets;
        //string saHL7Sets[];
        //string saXMLSets[];
    }
}
