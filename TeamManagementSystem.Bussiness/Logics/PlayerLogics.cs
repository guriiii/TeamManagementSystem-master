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
    public class PlayerLogics : IPlayerLogics
    {
        TeamMgtSystemEntities _db = new TeamMgtSystemEntities();
        public bool DeletePlayer(int ID)
        {
            bool isUpdate = false;
            try
            {

                var _player = (from a in _db.Players where a.ID == ID select a).FirstOrDefault();
                if (_player != null)
                {
                    _db.Players.Attach(_player);
                    _db.Players.Remove(_player);
                    _db.SaveChanges();
                    isUpdate = true;
                }
            }
            catch (Exception ex)
            {

            }
            return isUpdate;
        }

        public List<PlayerProps> GetPlayerList()
        {
            try
            {
                var rec = (from a in _db.Players
                           join c in _db.Teams on a.TeamID equals c.ID
                           select new PlayerProps
                           {
                               ID = a.ID,
                               Name = a.Name,
                               Position = a.Position,
                               Gender = a.Gender,
                               TeamName = c.TeamName

                           }).ToList();
                return rec;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<PlayerProps> GetPlayerListByID(int ID)
        {
            try
            {
                var rec = (from p in _db.Players
                           join t in _db.Teams on p.TeamID equals t.ID
                           join c in _db.Coaches on t.CoachID equals c.ID
                           where p.ID == ID
                           select new PlayerProps
                           {
                               ID = p.ID,
                               Name = p.Name,
                               Position = p.Position,
                               Gender = p.Gender,
                               TeamID = p.TeamID,
                               TeamName = t.TeamName,
                               City = t.City,
                               State = t.State,
                               CoachName = c.CoachName,
                               CoachID = t.CoachID
                               
                           }).ToList();
                return rec;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public bool InsertUpdatePlayerMaster(PlayerProps pr)
        {
            bool isUpdate = false;
            try
            {
                if (pr.ID == 0)
                {
                    Player _player = new Player();
                    _player.Name = pr.Name;
                    _player.Position = pr.Position;
                    _player.Gender = pr.Gender;
                    _player.TeamID = pr.TeamID;
                    _player.CoachName = pr.CoachName;
                    _db.Players.Add(_player);
                    _db.SaveChanges();
                    isUpdate = true;
                }
                else
                {
                    var _player = _db.Players.Where(x => x.ID == pr.ID).FirstOrDefault();
                    if (_player != null)
                    {
                        _player.Name = pr.Name;
                        _player.Position = pr.Position;
                        _player.Gender = pr.Gender;
                        _player.TeamID = pr.TeamID;
                        _player.CoachName = pr.CoachName;
                        _db.SaveChanges();
                        isUpdate = true;
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return isUpdate;
        }
    }
}
