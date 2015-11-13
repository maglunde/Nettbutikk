using System.Collections.Generic;
using Nettbutikk.Model;

namespace DAL.Account
{
    public interface IAccountRepo
    {
        bool AttemptLogin(string email, string password);
        bool AddPerson(PersonModel person, Role role, string password);
        bool ChangePassword(string email, string newPassword);
        AdminModel GetAdmin(int adminId);
        AdminModel GetAdmin(string email);
        List<PersonModel> GetAllPeople();
        //CustomerModel GetCustomer(int customerId);
        //CustomerModel GetCustomer(string email);
        PersonModel GetPerson(string email);
        bool UpdatePerson(PersonModel personUpdate, string email);
        bool DeletePerson(string email);
        bool CreateCredentials(string email, string password);
        bool SetRole(string email, Role role, bool isRole);
        bool isAdmin(string email);
        
    }
}