using System;
using System.Collections.Generic;
using System.Text;
using Common.ActionResult;
using System.Data;
namespace Common.ActionResult.Oam
{
    
    public class ACRCodeDataTableActionResult:OamBaseActionResult
    {
        
     private DataTable dataTable = null;

        public ACRCodeDataTableActionResult()
        {

        }

      

        public DataTable DataTable
        {
            get
            {
                return dataTable;
            }
            set
            {
                dataTable = value;
            }
        }
    }
}
