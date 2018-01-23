using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class PeScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.Per;
        public override string DisplayName => ColumnConstants.PerDisplay;
        public override CellKind ValueType => CellKind.Number;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.PriceEarningRatio;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}