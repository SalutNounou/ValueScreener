using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class NetMarginScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.NetMargin;
        public override string DisplayName => ColumnConstants.NetMarginDisplay;
        public override CellKind ValueType => CellKind.Percentage;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.CurrentNetMargin;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}