using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class AvgNetMarginScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.AvgNetMargin;
        public override string DisplayName => ColumnConstants.AvgNetMarginDisplay;
        public override CellKind ValueType => CellKind.Percentage;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.AverageNetMargin;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}