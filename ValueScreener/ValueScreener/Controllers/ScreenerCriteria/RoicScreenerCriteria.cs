using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class RoicScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.Roic;
        public override string DisplayName => ColumnConstants.RoicDisplay;
        public override CellKind ValueType => CellKind.Percentage;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.CurrentRoic;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}