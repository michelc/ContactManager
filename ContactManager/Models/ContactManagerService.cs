using System.Collections.Generic;
using System.Text.RegularExpressions;
using ContactManager.Models.Validation;

namespace ContactManager.Models
{
    public class ContactManagerService : IContactManagerService
    {
        private IValidationDictionary _validationDictionary;
        private IContactManagerRepository _repository;

        public ContactManagerService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new EntityContactManagerRepository())
        {
        }

        public ContactManagerService(IValidationDictionary validationDictionary, IContactManagerRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
        }

        private bool ValidateContact(Contact contactToValidate)
        {
            if (contactToValidate.FirstName.Trim().Length == 0)
                _validationDictionary.AddError("FirstName", "First name is required.");
            if (contactToValidate.LastName.Trim().Length == 0)
                _validationDictionary.AddError("LastName", "Last name is required.");
            if (contactToValidate.Phone.Length > 0 && !Regex.IsMatch(contactToValidate.Phone, @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"))
                _validationDictionary.AddError("Phone", "Invalid phone number.");
            if (contactToValidate.Email.Length > 0 && !Regex.IsMatch(contactToValidate.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                _validationDictionary.AddError("Email", "Invalid email address.");
            return _validationDictionary.IsValid;
        }

        public bool CreateContact(int groupId, Contact contactToCreate)
        {
            // Validation logic
            if (!ValidateContact(contactToCreate))
                return false;

            // Database logic
            try
            {
                _repository.CreateContact(groupId, contactToCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditContact(int groupId, Contact contactToEdit)
        {
            // Validation logic
            if (!ValidateContact(contactToEdit))
                return false;

            // Database logic
            try
            {
                _repository.EditContact(groupId, contactToEdit);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteContact(Contact contactToDelete)
        {
            // Database logic
            try
            {
                _repository.DeleteContact(contactToDelete);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Contact GetContact(int id)
        {
            return _repository.GetContact(id);
        }

        public IEnumerable<Contact> ListContacts()
        {
            return _repository.ListContacts();
        }

        public bool ValidateGroup(Group groupToValidate)
        {
            if (groupToValidate.Name.Trim().Length == 0)
                _validationDictionary.AddError("Name", "Name is required.");
            return _validationDictionary.IsValid;
        }
        public bool CreateGroup(Group groupToCreate)
        {
            // Validation logic
            if (!ValidateGroup(groupToCreate))
                return false;
            // Database logic
            try
            {
                _repository.CreateGroup(groupToCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Group> ListGroups()
        {
            return _repository.ListGroups();
        }

        public bool DeleteGroup(Group groupToDelete)
        {
            // Database logic
            try
            {
                _repository.DeleteGroup(groupToDelete);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Group GetGroup(int? id)
        {
            if (id.HasValue)
                return _repository.GetGroup(id.Value);
            return _repository.GetFirstGroup();
        }

    }
}