using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    class EnterpriseMultipleScreenerCriteria : AbstractCriteria
    {
        public override string Id => ColumnConstants.EnterpriseMultiple;
        public override string DisplayName => ColumnConstants.EnterpriseMultipleDisplay;
        public override CellKind ValueType => CellKind.Number;
        public override string DefaultOperator => CriteriaConstants.Greater;
        protected override decimal GetDecimalValue(Stock stock)
        {
            return stock.PricingResult.EnterpriseMultiple;
        }

        protected override string GetStringValue(Stock stock)
        {
            return null;
        }
    }
}