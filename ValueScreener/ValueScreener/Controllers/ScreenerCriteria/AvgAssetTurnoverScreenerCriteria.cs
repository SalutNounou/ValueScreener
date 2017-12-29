using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class AvgAssetTurnoverScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.AvgAssetTurnover;
        public override string DisplayName => ColumnConstants.AvgAssetTurnoverDisplay;
        public override CellKind ValueType => CellKind.Percentage;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.AverageAssetTurnover;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}