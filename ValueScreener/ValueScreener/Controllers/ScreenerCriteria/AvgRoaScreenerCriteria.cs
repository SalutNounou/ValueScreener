using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class AvgRoaScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.AvgRoa;
        public override string DisplayName => ColumnConstants.AvgRoaDisplay;
        public override CellKind ValueType => CellKind.Percentage;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.AverageRoa;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}