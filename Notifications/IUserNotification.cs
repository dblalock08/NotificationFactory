namespace TTCMiddleTier.Services.Notifications
{
    public interface IUserNotification
    {
        // ReSharper disable once UnusedMemberInSuper.Global
        int Id { get; set; }
        string Name { get; set; }
        string Text { get; set; }

        bool IsApplicableToLogin(MockLogin login);
    }
}