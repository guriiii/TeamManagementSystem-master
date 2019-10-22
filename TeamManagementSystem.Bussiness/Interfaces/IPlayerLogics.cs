using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagementSystem.Bussiness.Props;

namespace TeamManagementSystem.Bussiness.Interfaces
{
    public interface IPlayerLogics
    {
        List<PlayerProps> GetPlayerList();
        bool InsertUpdatePlayerMaster(PlayerProps pr);
        bool DeletePlayer(int ID);
        List<PlayerProps> GetPlayerListByID(int ID);
    }
}
