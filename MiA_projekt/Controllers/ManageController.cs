using AutoMapper;
using MiA_projekt.Data;
using MiA_projekt.Manager;
using MiA_projekt.Models;
using MiA_projekt.Models.AccountViewModels;
using MiA_projekt.Models.ManageViewModels;
using MiA_projekt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MiA_projekt.Controllers
{
    [Authorize]
    public class 
        ManageController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly string _externalCookieScheme;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ManageController(
          UserManager<AppUser> userManager,
          SignInManager<AppUser> signInManager,
          IOptions<IdentityCookieOptions> identityCookieOptions,
          IEmailSender emailSender,
          ILoggerFactory loggerFactory,
          AppDbContext db, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
            _emailSender = emailSender;
            _logger = loggerFactory.CreateLogger<ManageController>();
            _db = db;
            _mapper = mapper;
        }

        //
        // GET: /Manage/Index
        [HttpGet]
        public async Task<IActionResult> Index(ManageMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : message == ManageMessageId.ChangeAddressViewModel ? "Your address has been changed."
                : "";

            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }

            HostStatus status = HostStatus.NotApplying;

            string userId = _userManager.GetUserId(HttpContext.User);
            var request = _db.HostRequests.FirstOrDefault(i => i.UserId == userId);

            if (request != null)
            {
                if (request.IsRejected)
                    status = HostStatus.Rejected;
                else 
                    status = HostStatus.Applying;
            }

            if (await _userManager.IsInRoleAsync(user, "Host"))
                status = HostStatus.Accepted;

            var model = new IndexViewModel
            {
                HasPassword = await _userManager.HasPasswordAsync(user),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(user),
                TwoFactor = await _userManager.GetTwoFactorEnabledAsync(user),
                Logins = await _userManager.GetLoginsAsync(user),
                BrowserRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user),
                IsAdmin = await _userManager.IsInRoleAsync(user, "Admin"),
                IsModerator = await _userManager.IsInRoleAsync(user, "Mod"),
                HostStatus = status
            };
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLogin(RemoveLoginViewModel account)
        {
            ManageMessageId? message = ManageMessageId.Error;
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.RemoveLoginAsync(user, account.LoginProvider, account.ProviderKey);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    message = ManageMessageId.RemoveLoginSuccess;
                }
            }
            return RedirectToAction(nameof(ManageLogins), new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public IActionResult AddPhoneNumber()
        {
            return View();
        }

        ////
        //// POST: /Manage/AddPhoneNumber
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    // Generate the token and send it
        //    var user = await GetCurrentUserAsync();
        //    if (user == null)
        //    {
        //        return View("Error");
        //    }
        //    var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, model.PhoneNumber);
        //    await _smsSender.SendSmsAsync(model.PhoneNumber, "Your security code is: " + code);
        //    return RedirectToAction(nameof(VerifyPhoneNumber), new { PhoneNumber = model.PhoneNumber });
        //}

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableTwoFactorAuthentication()
        {
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, true);
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation(1, "User enabled two-factor authentication.");
            }
            return RedirectToAction(nameof(Index), "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisableTwoFactorAuthentication()
        {
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, false);
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation(2, "User disabled two-factor authentication.");
            }
            return RedirectToAction(nameof(Index), "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        [HttpGet]
        public async Task<IActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
            // Send an SMS to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePhoneNumberAsync(user, model.PhoneNumber, model.Code);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.AddPhoneSuccess });
                }
            }
            // If we got this far, something failed, redisplay the form
            ModelState.AddModelError(string.Empty, "Failed to verify phone number");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePhoneNumber()
        {
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.SetPhoneNumberAsync(user, null);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.RemovePhoneSuccess });
                }
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        //
        // GET: /Manage/ChangePassword
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User changed their password successfully.");
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAddress(ChangeAddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string userId = _userManager.GetUserId(HttpContext.User);

            AppUser user = _db.Users.Include(e => e.Address).First(u => u.Id == userId);
            Address address = user.Address;
            address.City = model.City;
            address.CountryCode = model.CountryCode;
            address.PostalCode = model.PostalCode;
            address.Street = model.Street;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult EditOffer(int id)
        {
            var offer = _db.Apartments.Include(i => i.Address).FirstOrDefault(i => i.Id == id);

            if (offer == null)
                return NotFound();

            string userId = _userManager.GetUserId(HttpContext.User);
            if (offer.HostId != userId)
                return BadRequest();

            return View(_mapper.Map<Apartment, EditOfferVM>(offer));
        }

        [HttpPost]
        public IActionResult EditOffer(EditOfferVM vm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model");

            var offer = _db.Apartments.Include(i => i.Address).FirstOrDefault(i => i.Id == vm.Id);

            if (offer == null)
                return NotFound();

            string userId = _userManager.GetUserId(HttpContext.User);
            if (offer.HostId != userId)
                return BadRequest();

            string filePath = ImageManager.Save(vm.ImageFile, userId);

            _mapper.Map(vm, offer);
            offer.Image = filePath;
            offer.Address.City = vm.City;
            offer.Address.PostalCode = vm.PostalCode;
            offer.Address.Street = vm.Street;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        //
        // GET: /Manage/SetPassword
        [HttpGet]
        public IActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        //GET: /Manage/ManageLogins
        [HttpGet]
        public async Task<IActionResult> ManageLogins(ManageMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.AddLoginSuccess ? "The external login was added."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await _userManager.GetLoginsAsync(user);
            var otherLogins = _signInManager.GetExternalAuthenticationSchemes().Where(auth => userLogins.All(ul => auth.AuthenticationScheme != ul.LoginProvider)).ToList();
            ViewData["ShowRemoveButton"] = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkLogin(string provider)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.Authentication.SignOutAsync(_externalCookieScheme);

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Action(nameof(LinkLoginCallback), "Manage");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));
            return Challenge(properties, provider);
        }

        //
        // GET: /Manage/LinkLoginCallback
        [HttpGet]
        public async Task<ActionResult> LinkLoginCallback()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var info = await _signInManager.GetExternalLoginInfoAsync(await _userManager.GetUserIdAsync(user));
            if (info == null)
            {
                return RedirectToAction(nameof(ManageLogins), new { Message = ManageMessageId.Error });
            }
            var result = await _userManager.AddLoginAsync(user, info);
            var message = ManageMessageId.Error;
            if (result.Succeeded)
            {
                message = ManageMessageId.AddLoginSuccess;
                // Clear the existing external cookie to ensure a clean login process
                await HttpContext.Authentication.SignOutAsync(_externalCookieScheme);
            }
            return RedirectToAction(nameof(ManageLogins), new { Message = message });
        }

        public async Task<IActionResult> ChangeAddress()
        {
            var user = await GetCurrentUserAsync();
            var addr = _db.Addresses.FirstOrDefault(i => i.Id == user.AddressId);

            if (addr == null)
                return BadRequest();

            return View(_mapper.Map<Address, ChangeAddressViewModel>(addr));
        }
        
        public IActionResult BecomeAhost()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> BecomeAhost(BecomeAHostVM vm)
        {
            if (!ModelState.IsValid)
                return View("Error");

            string userId = _userManager.GetUserId(HttpContext.User);
            var request = _db.HostRequests.FirstOrDefault(i => i.UserId == userId);

            if (request != null)
            {
                if (request.IsRejected)
                    return BadRequest("Request has been already rejected by moderator.");

                return BadRequest("Your request has been received, and is being reviewed by moderator.");
            }
            
            string filePath = ImageManager.Save(vm.File, userId);

            _db.HostRequests.Add(new HostRequest
            {
                UserId = userId,
                Date = DateTime.Now,
                DocumentId = vm.DocumentId,
                PersonalId = vm.PersonalId,
                Image = filePath
            });
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult AddApartment()
        {
            return View();
        }

        public IActionResult MyOffers()
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            var apartments = _db.Apartments
                                .Where(i => i.HostId == userId)
                                .Include(i => i.Address)
                                .Select(_mapper.Map<Apartment, MyOfferVM>)
                                .AsEnumerable();

            return View(apartments);
        }

        [HttpPost]
        public IActionResult AddApartment(AddApartmentVM model)
        {
            if (!ModelState.IsValid)
                return View("Error");

            Address addr = _db.Addresses.Add(new Address
            {
                City = model.City,
                CountryCode = model.CountryCode,
                PostalCode = model.PostalCode,
                Street = model.Street
            }).Entity;
            _db.SaveChanges();

            _db.Apartments.Add(new Apartment
            {
                AddressId = addr.Id,
                Description = model.Description,
                From = model.From,
                To = model.To,
                GuestCount = model.GuestCount,
                Image = model.Image,
                Name = model.Name,
                Price = model.Price
            });
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public IActionResult MyOrders()
        {
            //todog
            return View();
        }

        public IActionResult HostRequests()
        {
            return View(_db.HostRequests.Include(i => i.User).Where(i => !i.IsRejected).Select(_mapper.Map<HostRequest, HostRequestsVM>));
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            AddLoginSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error,
            ChangeAddressViewModel
        }

        private Task<AppUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        #endregion
    }
}
