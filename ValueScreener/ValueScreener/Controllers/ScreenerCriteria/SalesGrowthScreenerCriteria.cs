using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class SalesGrowthScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.SalesGrowth;
        public override string DisplayName => ColumnConstants.SalesGrowthDisplay;
        public override CellKind ValueType => CellKind.Percentage;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.CurrentSalesGrowth;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}