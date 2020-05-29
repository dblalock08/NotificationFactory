using System;
using System.Collections.Generic;
using System.Linq;

namespace TTCMiddleTier.Services.Notifications
{
    public class NotificationsService
    {
        private readonly List<NotificationData> _notificationListMockDatabase = new List<NotificationData>();

        public NotificationsService()
        {
            _notificationListMockDatabase.Add(new NotificationData
            {
                Id = 1,
                Name = "Attention Everyone",
                Text = "This is some important information that everyone should read.",
                TypeEnum = EnumNotificationType.Banner,
                ScopeEnum = EnumNotificationScope.AllUsers
            });

            _notificationListMockDatabase.Add(new NotificationData
            {
                Id = 2,
                Name = "Notification just for Jobseekers",
                Text = "Attention all jobseekers, please read this.",
                TypeEnum = EnumNotificationType.Banner,
                ScopeEnum = EnumNotificationScope.AllJobSeekers
            });
        }

        public IEnumerable<IUserNotification> GetNotificationsForLogin(MockLogin login)
        {
            List<IUserNotification> notificationsList = new List<IUserNotification>();

            // Get list of all notifications and build Notifications using the factory
            foreach (var item in _notificationListMockDatabase)
            {
                switch (item.TypeEnum)
                {
                    case EnumNotificationType.Banner:
                    default:
                        notificationsList.Add(BannerFactory.GetBanner(item));
                        break;
                }
            }

            return notificationsList.Where(c => c.IsApplicableToLogin(login));
        }
    }

    #region Mock Stuff
    class NotificationData
    {
        public int Id;
        public string Name;
        public string Text;

        public Criteria ScopeCriteria;

        public EnumNotificationType TypeEnum;
        public EnumNotificationScope ScopeEnum;
    }

    class Criteria
    {
        public DateTime CreatedBeforeDate;
        // ReSharper disable once UnusedMember.Global
        public DateTime CreatedAfterDate;
        // ReSharper disable once UnusedMember.Global
        public decimal ResumeCompletedPercentage;
    }

    enum EnumNotificationType
    {
        Banner = 1
    }

    enum EnumNotificationScope
    {
        AllUsers,
        AllJobSeekers,
        AllEmployers,
        JobseekersBeforeDate
    }

    public class MockLogin
    {
        public enum EnumLoginRoleType
        {
            JobSeeker = 1,
            District = 2,
            // ReSharper disable once UnusedMember.Global
            Admin = 3,
            // ReSharper disable once UnusedMember.Global
            Public = 4
        }

        public string Name;
        public DateTime CreateDateTime;
        public EnumLoginRoleType LoginRoleType;
    }
    #endregion
}
