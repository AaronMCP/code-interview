using System;
using System.Collections.Generic;
using System.Text;
using Server.DAO.Oam;
using Common.ActionResult;
using System.Data;

namespace Server.Business.Oam.Impl
{
    public class KMSServiceImpl:IKMSService
    {
        private IKMSDAO kmsDAO = DataBasePool.Instance.GetDBProvider();

        public bool AddNewInnerNode(string strGuid, string strParentID, string strPath, string strName, string strUserGuid, string strComments, string strNodeOrder)
        {
            try
            {
                return kmsDAO.AddNewInnerNode(strGuid, strParentID, strPath, strName, strUserGuid, strComments, strNodeOrder);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }
      
        }

        public bool AddNewLeafNode(string strGuid, string strParentID, string strFileName, string strNodeOrder)
        {
            try
            {

                return kmsDAO.AddNewLeafNode(strGuid, strParentID, strFileName, strNodeOrder);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }

        }

        public bool UpdateInnerNodeName(string strGuid, string strName, string strComments)
        {
            try
            {
                return kmsDAO.UpdateInnerNodeName(strGuid, strName, strComments);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }

        }

        public bool DeleteInnerNode(string strGuid)
        {
            try
            {
                return kmsDAO.DeleteInnerNode(strGuid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }

        }

        public bool DeleteLeafNode(string strGuid)
        {
            try
            {
                return kmsDAO.DeleteLeafNode(strGuid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }

        }

        public DataSet GetAllInnerNodes()
        {
            try
            {
                return kmsDAO.GetAllInnerNodes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetAllLeafNodes()
        {
            try
            {
                return kmsDAO.GetAllLeafNodes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsLeaf(string strGuid)
        {
            return true;
        }

        public DataSet GetInnerNodeInfo(string strGuid)
        {
            try
            {
                return kmsDAO.GetInnerNodeInfo(strGuid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdatePath(string strGuid, string strPath)
        {
            try
            {
                return kmsDAO.UpdatePath(strGuid, strPath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }

        }

        public bool MoveTo(string strGuid, string strParentID, string isLeaf, string strNodeOrder)
        {
            try
            {
                return kmsDAO.MoveTo(strGuid, strParentID, isLeaf, strNodeOrder);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }

        }

        public bool CopyTo(string strGuid, string strParentID, string strSrcID, string strUserGuid, string isLeaf,string strNodeOrder)
        {
            try
            {
                return kmsDAO.CopyTo(strGuid, strParentID, strSrcID, strUserGuid, isLeaf, strNodeOrder);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }

        }
        public bool NodeNameExisted(string strName, string strParentID, string isLeaf)
        {
            try
            {
                return kmsDAO.NodeNameExisted(strName, strParentID,isLeaf);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }

        }
        public bool SwapNodeOrder(string strGuidUp, string strGuidDown, string isLeaf)
        {
            try
            {
                return kmsDAO.SwapNodeOrder(strGuidUp, strGuidDown, isLeaf);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }
        }
    }
}
