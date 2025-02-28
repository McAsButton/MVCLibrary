using AspNetCoreHero.ToastNotification.Abstractions;
using Library.Common.Core;
using Library.Common.DTOs;
using Library.Web.Data.Entities;
using Library.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorsService _authorsService;
        private readonly INotyfService _notyfService;

        public AuthorsController(IAuthorsService authorsService, INotyfService notyfService)
        {
            _authorsService = authorsService;
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Response<List<Author>> response = await _authorsService.GetListAsync();

            if (!response.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(response.Result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notyfService.Error("Debe ajustar los errores de validación");
                return View(dto);
            }

            Response<Author> response = await _authorsService.CreateAsync(dto);
            if (!response.IsSuccess)
            {
                _notyfService.Error(response.Message);
                return View(dto);
            }

            _notyfService.Success(response.Message);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            Response<AuthorDTO> response = await _authorsService.GetOne(id);
            if (!response.IsSuccess)
            {
                _notyfService.Error(response.Message);
                return RedirectToAction("Index");
            }

            return View(response.Result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AuthorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notyfService.Error("Debe ajustar los errores de validación");
                return View(dto);
            }
            Response<Author> response = await _authorsService.EditAsync(dto);
            if (!response.IsSuccess)
            {
                _notyfService.Error(response.Message);
                return View(dto);
            }
            _notyfService.Success(response.Message);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Response<object> response = await _authorsService.DeleteAsync(id);
            if (!response.IsSuccess)
            {
                _notyfService.Error(response.Message);
            }
            else
            {
                _notyfService.Success(response.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
