using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Hys.Consultation.Domain.Entities;
using Hys.Consultation.EntityFramework;
using Hys.CrossCutting.Common.Utils;
using Hys.Platform.Application;
using Newtonsoft.Json;
using System.Security.Claims;
using Hys.Consultation.Application.Dtos;

namespace Hys.Consultation.Application.Services.ServiceImpl
{
    public class LoginUserService : DisposableServiceBase, ILoginUserService
    {
        public static readonly string AdminRoleID = "2ee2fd0c-100d-b934-d0c2-f24ff16039e9";
        public static readonly string ConsAdminRoleID = "93321a30-891f-6c86-6b4d-46f17f13dfae";
        private readonly IConsultationContext _DBContext;

        public LoginUserService(IConsultationContext dbContext)
        {
            _DBContext = dbContext;
        }

        private ServiceContext _svcContext;
        public ServiceContext ServiceContext
        {
            get
            {
                if (_svcContext == null)
                {
                    if (HttpContext.Current != null)
                    {
                        var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
                        var userClaims = identity.Claims.Where(c => c.Type == "user").First();
                        var user = JsonConvert.DeserializeObject<UserDto>(userClaims.Value);

                        if (user != null)
                        {
                            _svcContext = new ServiceContext(user.LoginName, user.UniqueID, user.Domain, user.Site, user.RoleName, user.LocalName, false, user.Language);
                        }
                    }
                }
                return _svcContext;
            }
            set
            {
                _svcContext = value;
            }
        }

        public string CurrentUserID
        {
            get
            {
                if (ServiceContext == null)
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }
                return ServiceContext != null ? ServiceContext.UserID : String.Empty;
            }
        }

        public bool IsSystemAdmin
        {
            get
            {
                return LoginUser.DefaultRoleID == AdminRoleID;
            }
        }

        private UserExtention _loginUser;
        public UserExtention LoginUser
        {
            get
            {
                if (_loginUser==null)
                {
                    _loginUser = _DBContext.Set<UserExtention>().FirstOrDefault(u => u.UniqueID == CurrentUserID);
                }
                return _loginUser;
            }
        }

        public string DefaultSiteID
        {
            get { return LoginUser != null ? LoginUser.HospitalID : String.Empty; }
        }
    }
}
