using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class PiotroskiScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.Piotroski;
        public override string DisplayName => ColumnConstants.PiotroskiDisplay;
        public override CellKind ValueType => CellKind.Number;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.CurrentPiotroskiScore;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}