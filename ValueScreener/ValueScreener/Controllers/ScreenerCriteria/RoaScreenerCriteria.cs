using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class RoaScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.Roa;
        public override string DisplayName => ColumnConstants.RoaDisplay;
        public override CellKind ValueType => CellKind.Percentage;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.CurrentRoa;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}