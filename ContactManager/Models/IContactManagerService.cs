using System.Collections.Generic;

namespace ContactManager.Models
{
    public interface IContactManagerService
    {
        bool CreateContact(int groupId, Contact contactToCreate);
        bool EditContact(int groupId, Contact contactToEdit);
        bool DeleteContact(Contact contactToDelete);
        Contact GetContact(int id);
        IEnumerable<Contact> ListContacts();

        bool ValidateGroup(Group groupToValidate);
        bool CreateGroup(Group groupToCreate);
        IEnumerable<Group> ListGroups();
        bool DeleteGroup(Group groupToDelete);
        Group GetGroup(int? id);
    }
}
