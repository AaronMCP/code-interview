using System;
using System.Collections.Generic;
using System.Text;

namespace Kodak.GCRIS.Server.DAO.Oam.Model
{
    public class User
    {
        private string  name = null;
        private int     age = 0;

        public string Name 
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }
    }
}
