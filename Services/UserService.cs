using System;
using System.Collections.Generic;
using System.Linq;
using BeaconTrackerApi.Database.Settings;
using BeaconTrackerApi.Model;
using MongoDB.Driver;

namespace BeaconTrackerApi.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _user;

        public UserService(IMongoCollectionSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _user = database.GetCollection<User>("User");
        }

        public List<User> GetUsers()
        {
            var users = _user.Find(user => true).ToList();
            return users;
        }

        public void UpdateUserRSSI(string userId, BeaconRSSI beaconRssi)
        {
            var user = _user.Find(u => u.Id == userId).FirstOrDefault();
            user.beaconsRSSI.Add(beaconRssi);
            user.lastLocation = beaconRssi;
            _user.ReplaceOne(u => u.Id == userId, user);
        }

        public void UpdateUserIdOneSignal(string userId, string userId_OneSignal)
        {
            var user = _user.Find(u => u.Id == userId).FirstOrDefault();
            user.userId_OneSignal = userId_OneSignal;
            _user.ReplaceOne(u => u.Id == userId, user);
        }

        public void UpdateUserStartWorking(string userId, int maxStayMinutes)
        {
            var user = _user.Find(u => u.Id == userId).FirstOrDefault();

            if (user.isWorking)
            {
                //Ja estava trabalhando, descansou e voltou a trabalhar. Salvar uma nova workSession
                var workSession = new WorkSession()
                {
                    nome = user.name,
                    maxStayMinutes = maxStayMinutes,
                    minRestMinutes = user.minRestMinutes,
                    regionName = user.lastLocation.regionName,
                    startRestingTime = user.startRestingTime,
                    startWorkingTime = user.startWorkingTime,
                    finishRestingTime = DateTime.Now
                };
                if (user.workSessions is null) user.workSessions = new List<WorkSession>();
                user.workSessions.Add(workSession);
            }

            user.maxStayMinutes = maxStayMinutes;
            user.isWorking = true;
            user.isResting = false;
            user.startWorkingTime = DateTime.Now;
            _user.ReplaceOne(u => u.Id == userId, user);
        }

        public void UpdateUserStartResting(string userId, int minRestMinutes)
        {
            var user = _user.Find(u => u.Id == userId).FirstOrDefault();
            if (!user.isWorking) throw new Exception("Usuário não está trabalhando!");

            user.minRestMinutes = minRestMinutes;
            user.isResting = true;
            user.isWorking = true;
            user.startRestingTime = DateTime.Now;
            _user.ReplaceOne(u => u.Id == userId, user);
        }

        public void UpdateUserFinishWorking(string userId)
        {
            var user = _user.Find(u => u.Id == userId).FirstOrDefault();
            if (!user.isWorking) throw new Exception("Usuário não está trabalhando!");

            var workSession = new WorkSession()
            {
                nome = user.name,
                maxStayMinutes = user.maxStayMinutes,
                minRestMinutes = user.minRestMinutes,
                regionName = user.lastLocation.regionName,
                startRestingTime = user.startRestingTime,
                startWorkingTime = user.startWorkingTime,
                finishRestingTime = DateTime.Now
            };
            user.workSessions.Add(workSession);

            user.isResting = false;
            user.isWorking = false;
            user.startWorkingTime = null;
            user.startRestingTime = null;
            _user.ReplaceOne(u => u.Id == userId, user);
        }

        public List<UserWorkSession> GetWorkingSessions()
        {
            var usersWorkSessions = new List<UserWorkSession>();
            var users = _user.Find(u => true).ToList();

            users.ForEach(user =>
            {
                if (user.workSessions is null) return;
                var listWorkSessions = user.workSessions.OrderByDescending(workSession => workSession.startWorkingTime);

                var userWorkSession = new UserWorkSession()
                {
                    name = user.name,
                    listWorkSessions = listWorkSessions.ToList()
                };
                usersWorkSessions.Add(userWorkSession);
            });

            return usersWorkSessions;
        }
    }
}
