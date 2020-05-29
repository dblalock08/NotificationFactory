using System;
using System.Collections.Generic;
using TTCMiddleTier.Services.Notifications;

namespace NotificationsPoC
{
    class Program
    {
        static void Main(string[] args)
        {
            List<MockLogin> _mockLogins = new List<MockLogin>();
            _mockLogins.Add(new MockLogin
            {
                Name = "John JobSeeker",
                CreateDateTime = new DateTime(2020, 01, 01),
                LoginRoleType = MockLogin.EnumLoginRoleType.JobSeeker
            });

            _mockLogins.Add(new MockLogin
            {
                Name = "Jenna JobSeeker",
                CreateDateTime = new DateTime(2010, 01, 01),
                LoginRoleType = MockLogin.EnumLoginRoleType.JobSeeker
            });

            _mockLogins.Add(new MockLogin
            {
                Name = "Donna District",
                CreateDateTime = new DateTime(2012, 01, 01),
                LoginRoleType = MockLogin.EnumLoginRoleType.District
            });

            NotificationsService notificationsService = new NotificationsService();

            foreach (var login in _mockLogins)
            {
                var notifications = notificationsService.GetNotificationsForLogin(login);

                foreach (var userNotification in notifications)
                {
                    Console.Out.WriteLine($"\"{userNotification.Name}: {userNotification.Text}\"");
                }
            }

            Console.Out.WriteLine("Press Enter to Continue...");
            Console.ReadLine();
        }
    }
}
