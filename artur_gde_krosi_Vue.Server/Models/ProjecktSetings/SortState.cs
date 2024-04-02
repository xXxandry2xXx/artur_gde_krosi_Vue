using System.ComponentModel.DataAnnotations;

public enum SortState
{
    [Display(Name = "По алфавиту")]
    NameAsc,    // по имени по возрастанию
    [Display(Name = "По убыванию алфавиту")]
    NameDesc,   // по имени по убыванию
    [Display(Name = "По возрастанию цены")]
    PriseAsc, // по возрасту по возрастанию
    [Display(Name = "По убыванию цены")]
    PriseDesc,    // по возрасту по убыванию
    [Display(Name = "По возрастанию популярности")]
    PopularityAsc,    // по возрасту по убыванию
    [Display(Name = "По убыванию популярности")]
    PopularityDesc    // по возрасту по убыванию
}