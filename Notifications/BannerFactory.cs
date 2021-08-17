using System;

namespace TTCMiddleTier.Services.Notifications
{
    static class BannerFactory
    {
        public static IUserNotification GetBanner(NotificationData data)
        {
            switch (data.ScopeEnum)
            {
                case EnumNotificationScope.AllUsers:
                    return new AllUsers()
                    {
                        Id = data.Id,
                        Name = data.Name,
                        Text = data.Text
                    };
                case EnumNotificationScope.AllJobSeekers:
                    return new JobseekersOnly
                    {
                        Id = data.Id,
                        Name = data.Name,
                        Text = data.Text
                    };
                case EnumNotificationScope.AllEmployers:
                    return new EmployersOnly
                    {
                        Id = data.Id,
                        Name = data.Name,
                        Text = data.Text
                    };
                case EnumNotificationScope.JobseekersBeforeDate:
                    return new JobseekersBeforeDate
                    {
                        Id = data.Id,
                        Name = data.Name,
                        Text = data.Text,
                        CreatedBeforeDate = data.ScopeCriteria.CreatedBeforeDate
                    };
                default:
                    return null;
            }
        }
    }

    class AllUsers : IUserNotification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public bool IsApplicableToLogin(MockLogin login)
        {
            if (login == null)
                return false;

            return (login.LoginRoleType == MockLogin.EnumLoginRoleType.JobSeeker ||
                    login.LoginRoleType == MockLogin.EnumLoginRoleType.District);
        }
    }

    class JobseekersOnly : IUserNotification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public bool IsApplicableToLogin(MockLogin login)
        {
            if (login == null)
                return false;

            return login.LoginRoleType == MockLogin.EnumLoginRoleType.JobSeeker;
        }
    }

    class EmployersOnly : IUserNotification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public bool IsApplicableToLogin(MockLogin login)
        {
            if (login == null)
                return false;

            return login.LoginRoleType == MockLogin.EnumLoginRoleType.District;
        }
    }

    class JobseekersBeforeDate : IUserNotification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public DateTime CreatedBeforeDate { get; set; }

        public bool IsApplicableToLogin(MockLogin login)
        {
            if (login == null)
                return false;

            return login.LoginRoleType == MockLogin.EnumLoginRoleType.JobSeeker &&
                   login.CreateDateTime < CreatedBeforeDate;
        }
    }
}