using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagementSystem.Bussiness.Props;

namespace TeamManagementSystem.Bussiness.Interfaces
{
    public interface ILoginLogics
    {
        bool ValidateCredentials(LoginProps pr);
    }
}
