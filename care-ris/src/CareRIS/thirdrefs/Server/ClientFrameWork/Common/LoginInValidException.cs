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
/*   Author : Fred Li                                                       */
/****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Server.ClientFramework.Common
{
    /// <summary>
    /// Throwed by WebserviceManager when user click Cancel or Exit button of the login form,
    /// when the application is locked by the login form.
    /// </summary>
    public class LoginServerTestInValidException : Exception
    {
        public LoginServerTestInValidException()
        { }
    }

    /// <summary>
    /// If fail to invoke login HIS server. throw the LoginHISServerException.
    /// </summary>
    public class LoginHISServerException : Exception
    {
        public LoginHISServerException()
        { }
    }

    /// <summary>
    /// when login, if Username not Eixted,throw LoginServerNoUserNameException
    /// </summary>
    public class LoginServerInvalidNameException : Exception
    {
        public LoginServerInvalidNameException()
        { }
    }

    /// <summary>
    /// when login, if the user dosen't have the role,throw LoginServerNoUserNameException
    /// </summary>
    public class LoginServerInvalidRoleException : Exception
    {
        public LoginServerInvalidRoleException()
        { }
    }

    /// <summary>
    /// when login, if pwd is not right,throw LoginServerInvalidPwdException
    /// </summary>
    public class LoginServerInvalidPwdException : Exception
    {
        public LoginServerInvalidPwdException()
        { }

        public LoginServerInvalidPwdException(string message)
            : base(message)
        { }
    }

    /// <summary>
    /// when login, if error access database,throw LoginServerDBErrorException
    /// </summary>
    public class LoginServerDBErrorException : Exception
    {
        public LoginServerDBErrorException()
        { }

        public LoginServerDBErrorException(string message)
            : base(message)
        { }
    }

    /// <summary>
    /// when login, if account is locked,throw LoginAccountLockedException
    /// </summary>
    public class LoginAccountLockedException : Exception
    {
        public LoginAccountLockedException()
        { }

        public LoginAccountLockedException(string message)
            : base(message)
        { }
    }

    #region Modified by Blue for RC603.10 - US16934, 06/09/2014  
    /// <summary>
    /// when login, if max online user count is already reached,throw LoginMaxOnlineUserErrorException
    /// </summary>
    public class LoginMaxOnlineUserErrorException : Exception
    {
        public LoginMaxOnlineUserErrorException()
        { }

        public LoginMaxOnlineUserErrorException(string message)
            : base(message)
        { }
    }
    #endregion  

    #region Modified by Blue for RC603.1 - US16931, 06/10/2014 
    /// <summary>
    /// when login, if user login for the first tine,throw LoginChangePasswordErrorException
    /// </summary>
    public class LoginChangePasswordErrorException : Exception
    {
        public LoginChangePasswordErrorException()
        { }

        public LoginChangePasswordErrorException(string message)
            : base(message)
        { }
    }
    #endregion
}
