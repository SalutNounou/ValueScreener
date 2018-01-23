using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class AvgCurrentRatioScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.AvgCurrentRatio;
        public override string DisplayName => ColumnConstants.AvgCurrentRatioDisplay;
        public override CellKind ValueType => CellKind.Percentage;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.AverageCurrentRatio;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}