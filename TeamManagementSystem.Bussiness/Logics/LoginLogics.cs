using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagementSystem.Bussiness.Interfaces;
using TeamManagementSystem.Bussiness.Props;
using TeamManagementSystem.Data;

namespace TeamManagementSystem.Bussiness.Logics
{
    public class LoginLogics : ILoginLogics
    {
        TeamMgtSystemEntities _db = new TeamMgtSystemEntities();
        public bool ValidateCredentials(LoginProps pr)
        {
            var rec = (from a in _db.Logins
                       where a.Username == pr.Username
                       && a.Password == pr.Password
                       select a).Count() > 0 ? true : false;
            if (rec)
                return true;
            else
                return false;
        }
    }
}
