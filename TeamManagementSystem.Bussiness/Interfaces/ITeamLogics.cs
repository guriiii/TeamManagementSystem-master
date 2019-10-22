using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagementSystem.Bussiness.Props;

namespace TeamManagementSystem.Bussiness.Interfaces
{
    public interface ITeamLogics
    {
        List<TeamProps> GetTeamList();
        bool InsertUpdateTeamMaster(TeamProps pr);
        bool DeleteTeam(int ID);
        List<TeamProps> GetTeamListByID(int ID);
    }
}
