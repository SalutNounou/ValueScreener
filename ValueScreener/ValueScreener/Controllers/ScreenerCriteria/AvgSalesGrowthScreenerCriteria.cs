using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class AvgSalesGrowthScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.AvgSalesGrowth;
        public override string DisplayName => ColumnConstants.AvgSalesGrowthDisplay;
        public override CellKind ValueType => CellKind.Percentage;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.AverageSalesGrowth;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}