using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class AvgRoicScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.RoicAvg;
        public override string DisplayName => ColumnConstants.AvgRoicDisplay;
        public override CellKind ValueType => CellKind.Percentage;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.AverageRoic;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}