using Ardalis.GuardClauses;
using FilesSortingSystem.Core.DomainEntities;
using FilesSortingSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesSortingSystem.Core.Interactors
{
    public class UpdateRuleInteractor(IFileSortRuleStorage fileSortRuleStorage) : IUpdateRuleInteractor
    {
        public async Task Handle(FileSortRule fileSortRule)
        {
            Guard.Against.Null(fileSortRule, nameof(fileSortRule));
            await fileSortRuleStorage.UpdateRuleAsync(fileSortRule);
        }
    }
}
