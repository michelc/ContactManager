using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Controllers
{
    public class GroupController : Controller
    {
        private IContactManagerService _service;

        public GroupController()
        {
            _service = new ContactManagerService(new ModelStateWrapper(this.ModelState));
        }

        public GroupController(IContactManagerService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return View(_service.ListGroups());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "Id")]Group groupToCreate)
        {
            if (_service.CreateGroup(groupToCreate))
                return RedirectToAction("Index");
            return View("Index", _service.ListGroups());
        }

        public ActionResult Delete(int id)
        {
            var groupToDelete = _service.GetGroup(id);
            return View(groupToDelete);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Group groupToDelete)
        {
            if (_service.DeleteGroup(groupToDelete))
                return RedirectToAction("Index");
            return View();
        }

    }
}