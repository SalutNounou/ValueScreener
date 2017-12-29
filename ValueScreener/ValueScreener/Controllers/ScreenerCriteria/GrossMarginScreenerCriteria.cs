using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class GrossMarginScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.GrossMargin;
        public override string DisplayName => ColumnConstants.GrossMarginDisplay;
        public override CellKind ValueType => CellKind.Percentage;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.CurrentGrossMargin;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}