using System.Collections.Generic;

namespace ContactManager.Models
{
    public interface IContactManagerRepository
    {
        // Contact method
        Contact CreateContact(int groupId, Contact contactToCreate);
        void DeleteContact(Contact contactToDelete);
        Contact EditContact(int groupId, Contact contactToEdit);
        Contact GetContact(int id);
        IEnumerable<Contact> ListContacts();

        // Group method
        Group CreateGroup(Group groupToCreate);
        IEnumerable<Group> ListGroups();
        Group GetGroup(int groupId);
        Group GetFirstGroup();
        void DeleteGroup(Group groupToDelete);
    }
}
