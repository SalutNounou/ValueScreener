using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class PFcfScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.PriceToFreeCashFlow;
        public override string DisplayName => ColumnConstants.PriceToFreeCashFlowDisplay;
        public override CellKind ValueType => CellKind.Number;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.CurrentPriceToFcfRatio;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}