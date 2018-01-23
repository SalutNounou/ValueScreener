using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class LeverageScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.Leverage;
        public override string DisplayName => ColumnConstants.LeverageDisplay;
        public override CellKind ValueType => CellKind.Number;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.CurrentLeverage;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}