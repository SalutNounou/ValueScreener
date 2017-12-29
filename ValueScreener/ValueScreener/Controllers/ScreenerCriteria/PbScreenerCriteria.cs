using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class PbScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.PbRatio;
        public override string DisplayName => ColumnConstants.PbRatioDisplay;
        public override CellKind ValueType => CellKind.Number;
        public override string DefaultOperator => CriteriaConstants.Lower;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.PriceToBookRatio;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}