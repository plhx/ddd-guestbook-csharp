using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using Application.Command;
using Application.Service;
using Domain.Model;
using Presentation.Converter;
using Presentation.Request;


namespace Presentations.Controllers {
    public class IndexController : Controller {
        private readonly GuestbookService Service;

        public IndexController(GuestbookService service) {
            this.Service = service;
        }

        [Route("")]
        public IActionResult Index() {
            return View("Presentation/View/index.cshtml");
        }

        [HttpGet]
        [Route("guestbook")]
        public IActionResult V1GuestbookGet(GuestbookGetRequest request) {
            var getCommand = new GuestbookGetCommand(request.count);
            var entries = this.Service.Get(getCommand);
            return new JsonResult(entries.Select(
                entry => new SavedEntryModelConverter(entry).ToDictionary()
            ));
        }

        [HttpPost]
        [Route("guestbook")]
        public IActionResult V1GuestbookPost(GuestbookAddRequest request) {
            var addCommand = new GuestbookAddCommand(
                new Name(request.name),
                new Message(request.message),
                new IPAddress(this.HttpContext.Connection.RemoteIpAddress)
            );
            this.Service.Add(addCommand);
            var getCommand = new GuestbookGetCommand(request.count);
            var entries = this.Service.Get(getCommand);
            return new JsonResult(entries.Select(
                entry => new SavedEntryModelConverter(entry).ToDictionary()
            ));
        }
    }
}
