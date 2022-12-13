using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Helpper;
using Khoaluan.ModelViews;
using Microsoft.AspNetCore.Mvc;

namespace Khoaluan.Controllers
{
    public class ContactController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public ContactController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }
        [Route("contact.html", Name = "ContactView")]
        public IActionResult ContactView()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Email(Contact contact)
        {
            //Utilities.sendemailcontact(contact);
            //Utilities.sendemailuser(contact);
            _notyfService.Success("Thank you for responding");
            return RedirectToAction(nameof(ContactView));
        }
    }
}
