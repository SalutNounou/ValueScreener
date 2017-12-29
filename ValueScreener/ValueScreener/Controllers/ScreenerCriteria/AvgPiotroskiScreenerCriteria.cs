using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class AvgPiotroskiScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.PiotroskiAvg;
        public override string DisplayName => ColumnConstants.AvgPiotroskiDisplay;
        public override CellKind ValueType => CellKind.Number;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.AveragePiotroskiScore;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}