using System.ComponentModel.DataAnnotations;

namespace TheBTeam.BLL
{
    public enum CategoryOfTransaction
    {
        All = 0,
        [Display(Name = "Salary")]
        incomeSalary,
        [Display(Name = "Prize")]
        incomePrize,
        [Display(Name = "Extra money")]
        incomeExtraMoney,

        [Display(Name = "Home")]
        outcomeHome = 100,
        [Display(Name = "Car")]
        outcomeCar,
        [Display(Name = "School")]
        outcomeSchool,
        [Display(Name = "Kids")]
        outcomeKids,
        [Display(Name = "Commute")]
        outcomeCommute,
        [Display(Name = "Food")]
        outcomeFood,
        [Display(Name = "Eating out")]
        outcomeEatingOut,
        [Display(Name = "Entertainment")]
        outcomeEntertainment,
        [Display(Name = "Medicine")]
        outcomeMedicine,
        [Display(Name = "Clothing")]
        outcomeClothing,
        [Display(Name = "Special")]
        outcomeSpecial,
        [Display(Name = "Other")]
        outcomeOther,
        [Display(Name = "Credit")]
        outcomeCredit,
        [Display(Name = "Living charges")]
        outcomeLivingCharges
        //TODO make to Categories for Income i Outcome separately
    }
}