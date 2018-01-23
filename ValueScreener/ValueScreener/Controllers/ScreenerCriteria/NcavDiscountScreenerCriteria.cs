using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class NcavDiscountScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.NcavDiscount;
        public override string DisplayName => ColumnConstants.NcavDiscountDisplay;
        public override CellKind ValueType => CellKind.Percentage;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.DiscountOnNcav;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}