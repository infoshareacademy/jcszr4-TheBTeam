using System.ComponentModel.DataAnnotations;

namespace TheBTeam.BLL
{

    public enum CategoryOfTransaction
    {
        All = 0,

        [Display(Name = "All Incomes")]
        allIncome,
        [Display(Name = "Salary")]
        Salary,
        [Display(Name = "Prize")]
        Prize,
        [Display(Name = "Extra money")]
        ExtraMoney,

        [Display(Name = "All Outcomes")]
        allOutcome=100,
        [Display(Name = "Home")]
        Home,
        [Display(Name = "Car")]
        Car,
        [Display(Name = "School")]
        School,
        [Display(Name = "Kids")]
        Kids,
        [Display(Name = "Commute")]
        Commute,
        [Display(Name = "Food")]
        Food,
        [Display(Name = "Eating out")]
        EatingOut,
        [Display(Name = "Entertainment")]
        Entertainment,
        [Display(Name = "Medicine")]
        Medicine,
        [Display(Name = "Clothing")]
        Clothing,
        [Display(Name = "Special")]
        Special,
        [Display(Name = "Other")]
        Other,
        [Display(Name = "Credit")]
        Credit,
        [Display(Name = "Living charges")]
        LivingCharges
        //TODO make to Categories for Income i Outcome separately
    }
}