using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class InterestCoveredScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.InterestCovered;
        public override string DisplayName => ColumnConstants.InterestCoveredDisplay;
        public override CellKind ValueType => CellKind.Number;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.TimesInterestCovered;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}