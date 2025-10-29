namespace FilesSortingSystem.Core.Interfaces
{
    public interface IDeleteRuleInteractor
    {
        Task Handle(int ruleId);
    }
}