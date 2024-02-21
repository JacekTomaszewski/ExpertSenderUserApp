using AutoMapper;
using ExpertSenderUserApp.Interfaces;
using ExpertSenderUserApp.Models.DTOs;
using ExpertSenderUserApp.Models.Entities;
using ExpertSenderUserApp.Web.Enums;
using ExpertSenderUserApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExpertSenderUserApp.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, IRepository<User> userRepository, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<List<UserDTO>>(await _userRepository.GetAllAsync()));
        }

        public async Task<IActionResult> Remove(Guid id)
        {
            User user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _userRepository.Remove(user);
            _userRepository.Save();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CreateNewUser(UserDTO userDto)
        {
            if (await _userRepository.Any(x => x.Email == userDto.Email))
            {
                return RedirectToAction("CreateViewEdit",
                    new { userModeEnum = UserModeEnum.Create, errorMessage = "E-mail is in our database. Please choose another one." });
            }
            var user = _mapper.Map<User>(userDto);
            if (user == null)
            {
                return RedirectToAction("CreateViewEdit", new { userModeEnum = UserModeEnum.Create, errorMessage = "User model is empty." });
            }
            user.DateCreated = DateTime.Now;
            user.DateModified = DateTime.Now;
            _userRepository.Add(user);
            _userRepository.Save();
            return RedirectToAction("Index");
        }

        public IActionResult EditUser(UserDTO userDto)
        {
            if (userDto == null)
            {
                return RedirectToAction("Index");
            }
            var user = _mapper.Map<User>(userDto);
            user.DateModified = DateTime.Now;
            _userRepository.Update(user);
            _userRepository.Save();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CreateViewEdit(Guid id, UserModeEnum userModeEnum, string? errorMessage = null)
        {
            var user = await _userRepository.GetByIdAsync(id);
            ViewBag.UserModeEnum = userModeEnum;
            ViewBag.ErrorMessage = errorMessage;
            if (userModeEnum == UserModeEnum.Edit || userModeEnum == UserModeEnum.View)
            {
                if (user == null)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.IsEditMode = userModeEnum == UserModeEnum.Edit ? true : false;
                ViewBag.PageTitle = userModeEnum == UserModeEnum.Edit ? "Edit user data" : "View user data";
                return View(_mapper.Map<UserDTO>(user));
            }
            else if (user == null && userModeEnum == UserModeEnum.Create)
            {
                ViewBag.IsEditMode = true;
                ViewBag.PageTitle = "Create new user";
                return View();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
