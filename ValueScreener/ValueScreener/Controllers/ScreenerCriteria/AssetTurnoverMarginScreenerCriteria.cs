using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class AssetTurnoverMarginScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.AssetTurnover;
        public override string DisplayName => ColumnConstants.AssetTurnoverDisplay;
        public override CellKind ValueType => CellKind.Percentage;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.CurrentAssetTurnover;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}