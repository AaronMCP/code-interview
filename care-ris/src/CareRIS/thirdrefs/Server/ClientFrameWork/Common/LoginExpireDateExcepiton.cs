using System;
using System.Collections.Generic;
using System.Text;

namespace Server.ClientFramework.Common
{
    public class UserExpireTimeError : Exception
    {
        public UserExpireTimeError()
        {
        }
    }

    public class UserExpireLessBeginDate : Exception
    {
        public UserExpireLessBeginDate()
        {
        }
    }
    public class UserExpireThanEndDate : Exception
    {
        public UserExpireThanEndDate()
        {
        }
    }
    public class UserExpireRemainDate : Exception
    {
        public int remainDays;
        public UserExpireRemainDate()
        {
        }
    }

}