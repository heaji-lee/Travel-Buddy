namespace TravelBuddy.Repository.Models.DTOs;

public class TripBudgetDto {
    public BudgetCategoryType Category { get; set; }
    public int AllocatedAmount { get; set; }

    public static TripBudgetDto FromModel(TripBudget budget) {
        return new TripBudgetDto {
            Category = budget.Category,
            AllocatedAmount = budget.AllocatedAmount
        };
    }
}
