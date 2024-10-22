using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TenexCarsDeploy.Data.Models;
using TenexCarsDeploy.Data.Repositories.Interfaces;
using TenexCarsDeploy.Data.ViewModels;
using TenexCarsDeploy.Interfaces;

namespace TenexCarsDeploy.Controllers.Subscription_Controller
{
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IOperatorRepository _operatorRepository;
        private readonly ILogger<SubscriptionController> _logger;
        private readonly ISubscriberRepository _subscriberRepository;

        public SubscriptionController(ISubscriptionRepository subscriptionRepository, UserManager<AppUser> userManager, IEmailService emailService, IOperatorRepository operatorRepository, ILogger<SubscriptionController> logger, ISubscriberRepository subscriberRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _userManager = userManager;
            _emailService = emailService;
            _operatorRepository = operatorRepository;
            _logger = logger;
            _subscriberRepository = subscriberRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Subscribers()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            Operator? existingOperator = null;

            if (user.Type == "Main_Operator")
            {
                existingOperator = await _operatorRepository.GetOperatorByUserId(user.Id);
            }
            else if (user.Type == "Operator_Team_Member")
            {
                var operatorMember = await _operatorRepository.GetOperatorMemberByUserId(user.Id);
                existingOperator = await _operatorRepository.GetOperatorById(operatorMember!.OperatorId!);
                ViewBag.CompanyName = existingOperator!.CompanyName;
            }
            else
            {
                return BadRequest("Unauthorized user!.");
            }

            if (existingOperator == null)
            {
                _logger.LogInformation("Operator ID is required.");
                return BadRequest();
            }

            var subscriptions = await _subscriptionRepository.GetSubscriptionsByOperatorAsync(existingOperator.Id);
            if (subscriptions == null || !subscriptions.Any())
            {
                return NotFound("No subscriptions found for the operator.");
            }


            foreach (var subscription in subscriptions)
            {
                var subscriber = await _subscriberRepository.GetSubscriberByIdAsync(subscription.SubscriberId!);
                subscription.Subscriber = subscriber;
            }


            var operatorSubscriptionViewModel = new OperatorSubscriptionsViewModel
            {
                Subscriptions = subscriptions
            };

            return View(operatorSubscriptionViewModel);
        }
    }
}
