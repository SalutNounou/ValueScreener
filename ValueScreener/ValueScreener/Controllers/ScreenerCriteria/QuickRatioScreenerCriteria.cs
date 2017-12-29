using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class QuickRatioScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.QuickRatio;
        public override string DisplayName => ColumnConstants.QuickRatioDisplay;
        public override CellKind ValueType => CellKind.Percentage;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.CurrentQuickRatio;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}