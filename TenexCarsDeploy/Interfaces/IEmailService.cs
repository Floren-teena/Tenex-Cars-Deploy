using TenexCarsDeploy.TenexCarsDeploy.Data.DTOs;

namespace TenexCarsDeploy.Interfaces
{
    public interface IEmailService
    {
		Task SendOperatorSetPasswordEmailAsync(EmailDto emailDto);
        Task ContactApplicantEmailAsync(EmailDto emailDto);
        Task SendEmailToCompany(EmailDto emailDto);
        Task ApproveSubscriptionEmail(EmailDto emailDto);
    }
}
