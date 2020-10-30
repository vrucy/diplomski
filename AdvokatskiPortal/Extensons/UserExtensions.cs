using CraftmanPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CraftmanPortal.Extensons
{
    public static class UserExtensions
    {
        public static Contract ModificationContractUser(this Contract contract, Contract newContract)
        {
            contract.Price = newContract.Price;
            contract.TypeOfPayment = newContract.TypeOfPayment;
            contract.Comment = newContract.Comment;
            contract.ChangeCaseDate = DateTime.UtcNow.ToLocalTime();
            return contract;
        }

        public static User EditUser (this User selectedUser, User newUser)
        {
            selectedUser.FirstName = newUser.FirstName;
            selectedUser.LastName = newUser.LastName;
            selectedUser.UserName = newUser.UserName;
            selectedUser.Password = newUser.Password;
            selectedUser.Email = newUser.Email;
            selectedUser.Place = newUser.Place;
            selectedUser.Street = newUser.Street;

            return selectedUser;
        }
    }
}
