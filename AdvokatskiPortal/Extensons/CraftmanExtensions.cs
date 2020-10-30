using CraftmanPortal.Data;
using CraftmanPortal.Models;
using CraftmanPortal.Models.View;
using System;
using System.Collections.Generic;

namespace CraftmanPortal.Extensons
{
    public static class CraftmanExtensions
    {        
        public static Contract ModificationContractCraftman(this Contract contract, Contract newContract)
        {
            contract.Price = newContract.Price;
            contract.TypeOfPayment = newContract.TypeOfPayment;
            contract.Comment = newContract.Comment;
            contract.StartDate = newContract.StartDate;
            contract.FinishDate = newContract.FinishDate;
            contract.isFinal = newContract.isFinal;
            contract.ChangeCaseDate = DateTime.UtcNow.ToLocalTime();

            return contract;
        }
        public static Craftman EditCraftman(this Craftman selectedCraftman, Craftman newCraftman)
        {
            selectedCraftman.FirstName = newCraftman.FirstName;
            selectedCraftman.LastName = newCraftman.LastName;
            selectedCraftman.UserName = newCraftman.UserName;
            selectedCraftman.Password = newCraftman.Password;
            selectedCraftman.Email = newCraftman.Email;
            selectedCraftman.Place = newCraftman.Place;
            selectedCraftman.Street = newCraftman.Street;

            return selectedCraftman;
        }
        public static IEnumerable<CaseCraftman> CheckDeadlineForAnswer(this IEnumerable<CaseCraftman> Case)
        {
            var dateNow = DateTime.Now;
            IList<CaseCraftman> ListToChange = new List<CaseCraftman>();
            foreach (var item in Case)
            {
                if(item.Case.DeadLineForAnswer < dateNow)
                {
                    item.CaseStatusId = 8;
                    ListToChange.Add(item);
                }
            }
            return ListToChange;
        }
    }
}
