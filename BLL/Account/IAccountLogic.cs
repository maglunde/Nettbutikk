using Nettbutikk.Model;
using System.Collections.Generic;

namespace BLL.Account
{
    public interface IAccountLogic
    {
        // bool AttemptLogin(int personId, string password);
        bool AttemptLogin(string email, string password);
        bool AddPerson(PersonModel person, Role role, string password);
        // bool ChangePassword(int personId, string newPassword);
        bool ChangePassword(string email, string newPassword);
        // bool DeletePerson(int personId);
        bool DeletePerson(string email);
        AdminModel GetAdmin(int adminId);
        AdminModel GetAdmin(string email);
        List<PersonModel> GetAllPeople();
        CustomerModel GetCustomer(int customerId);
        CustomerModel GetCustomer(string email);
        bool isAdmin(string email);

        // PersonModel GetPerson(int personId);
        PersonModel GetPerson(string email);
        // int GetPersonId(string email);
        //bool UpdatePerson(PersonModel personUpdate, int personId);
        bool UpdatePerson(PersonModel personUpdate, string email);
    }
}