using System;
using System.Collections.Generic;
using System.Linq;
using BeaconTrackerApi.Database.Settings;
using BeaconTrackerApi.Model;
using MongoDB.Driver;

namespace BeaconTrackerApi.Services
{
    public class NotificationService
    {
        private readonly IMongoCollection<Notification> _notification;

        public NotificationService(IMongoCollectionSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _notification = database.GetCollection<Notification>("Notification");
        }

        public IEnumerable<Notification> GetNotifications()
        {
            var notifications = _notification
                .Find(notification => true)
                .Limit(10)
                .ToList()
                .OrderByDescending(notification => notification.horaEnvio);
            return notifications;
        }

        public void InsertNotification(Notification notification)
        {
            _notification.InsertOne(notification);
        }
    }
}