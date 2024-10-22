using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TenexCarsDeploy.Data.Enums;
using TenexCarsDeploy.Data.Models;
using TenexCarsDeploy.Data.Repositories.Interfaces;
using TenexCarsDeploy.Interfaces;
using TenexCarsDeploy.Models.ViewModels;

namespace TenexCarsDeploy.Controllers.Subscriber_Controller
{
    public class SubscriberController : Controller
    {
        private readonly ILogger<SubscriberController> _logger;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly ICoSubscriberRepository _coSubscriberRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly UserManager<AppUser> _manager;
        private readonly IOperatorRepository _operatorRepository;
        private readonly IVehicleRequestRepository _vehicleRequestRepository;
        private readonly IPhotoService _photoService;

        public SubscriberController(ILogger<SubscriberController> logger, ISubscriptionRepository subscriptionRepository,
            ISubscriberRepository subscriberRepository, IVehicleRepository vehicleRepository, UserManager<AppUser> manager,
            IOperatorRepository operatorRepository, IVehicleRequestRepository vehicleRequestRepository, IPhotoService photoService, ICoSubscriberRepository coSubscriberRepository)
        {
            _logger = logger;
            _subscriptionRepository = subscriptionRepository;
            _subscriberRepository = subscriberRepository;
            _vehicleRepository = vehicleRepository;
            _manager = manager;
            _operatorRepository = operatorRepository;
            _vehicleRequestRepository = vehicleRequestRepository;
            _photoService = photoService;
            _coSubscriberRepository = coSubscriberRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ReserveCar(string id)
        {
            var vehicle = await _vehicleRepository.GetVehicleById(id);

            var response = new ReserveCarViewModel
            {
                VehicleId = vehicle!.Id,
                ImageUrl = vehicle.ImageUrl,
            };

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> ReserveCar(ReserveCarViewModel rcvm)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Invalid input data!";
                return View(rcvm);
            }

            _logger.LogInformation("Reserving Car ...");
            try
            {
                var vehicle = await _vehicleRepository.GetVehicleById(rcvm.VehicleId!);

                var operatorEntity = vehicle is not null ? await _operatorRepository.GetOperatorById(vehicle.OperatorId!) : null;

                var newUser = new AppUser
                {
                    FirstName = rcvm.FirstName,
                    LastName = rcvm.LastName,
                    Email = rcvm.Email,
                    PhoneNumber = rcvm.PhoneNumber,
                    UserName = rcvm.Email,
                    //OperatorId = operatorEntity?.Id,
                    Type = "Main_Subscriber"

                };

                if (rcvm.Password != rcvm.ConfirmPassword)
                {
                    _logger.LogError("Password mismatch!");
                    TempData["error"] = "Password mismatch!";
                    return View(rcvm);
                }

                var result = await _manager.CreateAsync(newUser, rcvm.Password!);

                await _manager.AddToRoleAsync(newUser, "Main_Subscriber");

                if (result.Succeeded)
                {
                    var subscriber = new Subscriber
                    {
                        FirstName = newUser.FirstName!,
                        LastName = newUser.LastName!,
                        PhoneNumber = newUser.PhoneNumber,
                        Email = newUser.Email!,
                        AppUser = newUser,
                        AppUserId = newUser.Id,
                        HomeAddress = rcvm.HomeAddress,
                        City = rcvm.City,
                        Country = rcvm.Country,
                        State = rcvm.State,
                        ZipCode = rcvm.ZipCode
                    };

                    var newSubscriber = await _subscriberRepository.AddSubscriberAsync(subscriber);

                    var subscription = new Subscription
                    {
                        Subscriber = newSubscriber,
                        SubscriberId = newSubscriber.Id,
                        Vehicle = vehicle is not null ? vehicle : null,
                        VehicleId = vehicle is not null ? vehicle.Id : "",
                        OperatorId = operatorEntity is not null ? operatorEntity.Id : "",
                        Operator = operatorEntity,
                        SubscriptionStatus = SubscriptionStatus.DLNeeded

                    };

                    await _subscriptionRepository.AddSubscriptionAsync(subscription);

                    var newRequestLog = new VehicleRequest
                    {
                        Subscriber = newSubscriber,
                        SubscriberId = newSubscriber.Id,
                        Vehicle = vehicle is not null ? vehicle : null,
                        VehicleId = vehicle is not null ? vehicle.Id : "",
                    };

                    await _vehicleRequestRepository.AddVehicleRequestLog(newRequestLog);

                    _logger.LogInformation("Reservation Successful!");
                    TempData["success"] = "Reservation Successful! Please login to complete reservation";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    _logger.LogError("Ensure you filled all fields accurately!");
                    TempData["error"] = "Ensure you filled all fields accurately!";
                    return View(rcvm);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Something went wrong when reserving car");
                TempData["error"] = "Something went wrong when reserving car. Contact Support!";

                return View(rcvm);
            }
        }


    }
}
