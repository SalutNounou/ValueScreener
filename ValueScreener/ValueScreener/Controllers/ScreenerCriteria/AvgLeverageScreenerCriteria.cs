using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class AvgLeverageScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.AvgLeverage;
        public override string DisplayName => ColumnConstants.AvgLeverageDisplay;
        public override CellKind ValueType => CellKind.Number;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.AverageLeverage;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}