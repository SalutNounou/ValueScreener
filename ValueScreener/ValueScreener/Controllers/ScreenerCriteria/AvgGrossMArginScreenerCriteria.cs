using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class AvgGrossMArginScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.AvgGrossMargin;
        public override string DisplayName => ColumnConstants.AvgGrossMarginDisplay;
        public override CellKind ValueType => CellKind.Percentage;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.AverageGrossMargin;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}