using System.Linq;
using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Models.ViewData;
using ContactManager.Models.Validation;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {
        private IContactManagerService _service;

        public ContactController()
        {
            _service = new ContactManagerService(new ModelStateWrapper(this.ModelState));
        }

        public ContactController(IContactManagerService service)
        {
            _service = service;
        }

        public ActionResult Index(int? id)
        {
            // Get selected group
            var selectedGroup = _service.GetGroup(id);
            if (selectedGroup == null)
                return RedirectToAction("Index", "Group");

            // Ajax Request
            if (Request.IsAjaxRequest())
                return PartialView("ContactList", selectedGroup);
            
            // Normal Request
            var model = new IndexModel
            {
                Groups = _service.ListGroups(),
                SelectedGroup = selectedGroup
            };
            return View("Index", model);
        }

        public ActionResult IndexOld(int? id)
        {
            var model = new IndexModel
            {
                Groups = _service.ListGroups(),
                SelectedGroup = _service.GetGroup(id)
            };

            if (model.SelectedGroup == null)
                return RedirectToAction("Index", "Group");

            return View(model);
        }

        public ActionResult Create()
        {
            if (!AddGroupsToViewData(-1))
                return RedirectToAction("Index", "Group");
            return View("Create");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(int groupId, [Bind(Exclude = "Id")] Contact contactToCreate)
        {
            if (_service.CreateContact(groupId, contactToCreate))
                return RedirectToAction("Index");
            AddGroupsToViewData(-1);
            return View("Create");
        }

        public ActionResult Edit(int id)
        {
            var contactToEdit = _service.GetContact(id);
            AddGroupsToViewData(contactToEdit.Group.Id);
            return View("Edit", contactToEdit);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int groupId, Contact contactToEdit)
        {
            if (_service.EditContact(groupId, contactToEdit))
                return RedirectToAction("Index");
            AddGroupsToViewData(groupId);
            return View("Edit");
        }

        public ActionResult Delete(int id)
        {
            var contactToDelete = _service.GetContact(id);
            return View(contactToDelete);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Contact contactToDelete)
        {
            if (_service.DeleteContact(contactToDelete))
                return RedirectToAction("Index");
            return View();
        }

        [AcceptVerbs(HttpVerbs.Delete)]
        [ActionName("Delete")]
        public ActionResult AjaxDelete(int id)
        {
            // Get contact to delete
            Contact contactToDelete = _service.GetContact(id);

            // Get group from the contact
            var selectedGroup = _service.GetGroup(contactToDelete.Group.Id);

            // Delete from database
            _service.DeleteContact(contactToDelete);

            // Return Contact List
            return PartialView("ContactList", selectedGroup);
        }

        protected bool AddGroupsToViewData(int selectedId)
        {
            var groups = _service.ListGroups();
            ViewData["GroupId"] = new SelectList(groups, "Id", "Name", selectedId);
            return groups.Count() > 0;
        }

    }
}
