using agenda.Data;
using agenda.Models;
using agenda.Models.ViewModels;
using agenda.Services;
using agenda.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace agenda.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ContactService _contactService;

        public ContactsController(ContactService contactService)
        {
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            return View(_contactService.FindAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ContactFormViewModel { Contact = contact };
                return View(viewModel);
            }
            _contactService.Insert(contact);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { msg = "Id not provided" });

            var obj = _contactService.FindById(id.Value); // necessário obter o dado de 'Value' quando a propriedade não é obrigatória
            if (obj == null)
                return RedirectToAction(nameof(Error), new { msg = "Id not found" });

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {

            if (id == null)
                return RedirectToAction(nameof(Error), new { msg = "Id not provided" });

            var obj = _contactService.FindById(id.Value); // necessário obter o dado de 'Value' quando a propriedade não é obrigatória
            if (obj == null)
                return RedirectToAction(nameof(Error), new { msg = "Id not found" });

            ContactFormViewModel viewModel = new ContactFormViewModel { Contact = obj };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Contact contact)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ContactFormViewModel { Contact = contact };
                return View(viewModel);
            }

            if (id != contact.Id)
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });

            try
            {
                _contactService.Update(contact);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { msg = e.Message });
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { msg = "Id not provided" });

            var obj = _contactService.FindById(id.Value); // necessário obter o dado de 'Value' quando a propriedade não é obrigatória
            if (obj == null)
                return RedirectToAction(nameof(Error), new { msg = "Id not found" });

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                _contactService.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { msg = e.Message });
            }
        }

        public IActionResult Error(string msg)
        {
            var viewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Message = msg
            };

            return View(viewModel);
        }
    }
}
