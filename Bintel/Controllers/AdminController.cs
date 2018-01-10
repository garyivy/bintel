using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Bintel.Models;
using Bintel.Services;
using Bintel.Models.AdminViewModels;
using System.Collections.Generic;
using Bintel.Extensions;
using System.Linq;
namespace Bintel.Controllers
{
    [Authorize("Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly string _externalCookieScheme;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<IdentityCookieOptions> identityCookieOptions,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<AdminController>();
        }

        //
        // GET: /Account/NewUser
        [HttpGet]
        public IActionResult NewUser()
        {
            var initializedModel = new NewUserViewModel
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                UserName = string.Empty,
                Email = string.Empty,
                ConfirmEmail = string.Empty,
                Password = string.Empty,
                ConfirmPassword = string.Empty,
                SuccessMessage = null
                
            };

            // TODO: Remove
            ModelState.Clear();
            initializedModel.FirstName = "John";
            initializedModel.LastName = "Smith";
            initializedModel.Password = "Zxcv1234!";
            initializedModel.ConfirmPassword = "Zxcv1234!";

            return View(initializedModel);
        }

        //
        // POST: /Account/NewUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewUser(NewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User added.");

                    ModelState.Clear();

                    model.SuccessMessage = model.FirstName + " " + model.LastName + " has been added.";
                    model.FirstName = string.Empty;
                    model.LastName = string.Empty;
                    model.UserName = string.Empty;
                    model.Email = string.Empty;
                    model.ConfirmEmail = string.Empty;
                    model.Password = string.Empty;
                    model.ConfirmPassword = string.Empty;
                }
                else
                {
                    AddErrors(result);
                }
            }

            return View(model);
        }

        //
        // GET: /Account/ManageUsers
        [HttpGet]
        public async Task<IActionResult> ManageUsers(string successMessage = "", string errorMessage = "")
        {
            var users = new List<UserViewModel>();

            foreach (var user in _userManager.Users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                users.Add(new UserViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.NullSafe().Select(r => r.ToString()).ToArray()
                });
            }

            var model = new UsersViewModel
            {
                Users = users.ToArray(),
                SuccessMessage = successMessage,
                ErrorMessage = errorMessage
            };
            return View(model);
        }

        public async Task<IActionResult> AssignRole (string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            var successMessage = string.Empty;
            var errorMessage = string.Empty;

            if(user == null)
            {
                errorMessage = "Unable to find user.";
            }
            else
            {
                var assignmentResult = await _userManager.AddToRoleAsync(user, roleName);
                if(assignmentResult.Succeeded)
                {
                    successMessage = user.FirstName + " " + user.LastName + " has been assigned the role of " + roleName;
                }
                else
                {
                    errorMessage = "An error occurred while trying to assign the role.";
                }
            }

            return RedirectToAction("ManageUsers", new { SuccessMessage = successMessage, ErrorMessage = errorMessage });
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
