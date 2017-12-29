using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class AvgPFcfScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.AvgPriceToFreeCashFlow;
        public override string DisplayName => ColumnConstants.AvgPriceToFreeCashFlowDisplay;
        public override CellKind ValueType => CellKind.Number;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.AveragePriceToFcfRatio;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}