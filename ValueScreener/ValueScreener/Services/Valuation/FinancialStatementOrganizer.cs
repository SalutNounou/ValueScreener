using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using ValueScreener.Models.Domain;

namespace ValueScreener.Services.Valuation
{
    public class FinancialStatementOrganizer : IFinancialStatementOrganizer
    {
        private readonly List<FinancialStatement> _allFinancialStatements;
        private readonly List<FinancialStatement> _annualFinancialStatements;
        private readonly List<FinancialStatement> _quarterlyFinancialStatements;
        private bool _isReallyQuarterly;
        private bool _enoughQuarterlyReports;


        public FinancialStatementOrganizer()
        {
            _allFinancialStatements = new List<FinancialStatement>();
            _annualFinancialStatements= new List<FinancialStatement>();
            _quarterlyFinancialStatements = new List<FinancialStatement>();
            _isReallyQuarterly = false;
            _enoughQuarterlyReports = false;
        }


        public void Initialize(List<FinancialStatement> stockFinancialStatements)
        {
            _allFinancialStatements.Clear();
            _allFinancialStatements.AddRange(stockFinancialStatements);
            _annualFinancialStatements.Clear();
            _annualFinancialStatements.AddRange(stockFinancialStatements.Where(x => x.Source == "annual")
                .OrderByDescending(x => x.FiscalYear).ThenByDescending(f => f.FiscalQuarter));
            _quarterlyFinancialStatements.Clear();
            _quarterlyFinancialStatements.AddRange(stockFinancialStatements.Where(x => x.Source == "quarterly").OrderByDescending(x => x.FiscalYear).ThenByDescending(f => f.FiscalQuarter).Take(4));
            _isReallyQuarterly = _quarterlyFinancialStatements.Any() && _quarterlyFinancialStatements.All(x => x.FormType == "10-Q" || x.FormType == "10-K");
            _enoughQuarterlyReports = _quarterlyFinancialStatements.Count == 4;
        }

        public FinancialStatement GetLastFinancialStatement()
        {
            if (!_isReallyQuarterly || !_enoughQuarterlyReports)
                return _annualFinancialStatements.FirstOrDefault();

            else
            {
                var lastquarterlyStatement = _quarterlyFinancialStatements.First();
                return new FinancialStatement
                {
                    BalanceSheet = lastquarterlyStatement.BalanceSheet,
                    CurrencyCode = lastquarterlyStatement.CurrencyCode,
                    FiscalQuarter = lastquarterlyStatement.FiscalQuarter,
                    FiscalYear = lastquarterlyStatement.FiscalYear,
                    FormType = "composite",
                    PeriodEnd = lastquarterlyStatement.PeriodEnd,
                    Source = "composite",
                    PeriodLength = 12,
                    PeriodLengthCode = "M",
                    ReceivedDate = lastquarterlyStatement.ReceivedDate,
                    PrimarySymbol = lastquarterlyStatement.PrimarySymbol,
                    Stock = lastquarterlyStatement.Stock,
                    UsdConversionRate = 1,
                    StockId = lastquarterlyStatement.StockId,
                    FinancialStatementId = -1,
                    IncomeStatement = SumIncomeStatements(_quarterlyFinancialStatements.Select(f=>f.IncomeStatement).ToList()),
                    CashFlowStatement = SumCashFlowStatement(_quarterlyFinancialStatements.Select(f=>f.CashFlowStatement).ToList())
                };
            }


            
        }

        public FinancialStatement GetLastQuarterlyFinancialStatement()
        {
            if (_isReallyQuarterly)
                return _quarterlyFinancialStatements.First();
            return null;
        }

        public List<FinancialStatement> GetAnnualStatements()
        {
            return new List<FinancialStatement>(_annualFinancialStatements);
        }

        public List<FinancialStatement> GetQuarterlyStatements()
        {
            return new List<FinancialStatement>(_quarterlyFinancialStatements);
        }

        public List<FinancialStatement> GetConsecutiveAnnualStatementCouple(int from)
        {
            if(!_annualFinancialStatements.Any()) return  new List<FinancialStatement>();
            if(_annualFinancialStatements.Count<from+2) return new List<FinancialStatement>();
            var result = new List<FinancialStatement>();
            result.Add(_annualFinancialStatements[from]);
            result.Add(_annualFinancialStatements[from+1]);
            return result;
        }

        private CashFlowStatement SumCashFlowStatement(List<CashFlowStatement> csList)
        {
            return new CashFlowStatement
            {
                AccountingChange = csList.Sum(x=>x.AccountingChange),
                CapitalExpanditure = csList.Sum(x=>x.CapitalExpanditure),
                CashFromFinancingActivities = csList.Sum(x=>x.CashFromFinancingActivities),
                CashFromInvestingActivities = csList.Sum(x=>x.CashFromInvestingActivities),
                CashFromOperatingActivities = csList.Sum(x=>x.CashFromOperatingActivities),
                CfDepreciationAmortization = csList.Sum(x=>x.CfDepreciationAmortization),
                ChangeInAccountReceivable = csList.Sum(x=>x.ChangeInAccountReceivable),
                ChangeInCurrentAsset = csList.Sum(x=>x.ChangeInCurrentAsset),
                ChangeInCurrentLiabilities = csList.Sum(x=>x.ChangeInCurrentLiabilities),
                ChangeInInventories = csList.Sum(x=>x.ChangeInInventories),
                DividendsPaid = csList.Sum(x=>x.DividendsPaid),
                EffectOfExchangeRateOnCash = csList.Sum(x=>x.EffectOfExchangeRateOnCash),
                InvestmentChangesNet = csList.Sum(x=>x.InvestmentChangesNet),
                NetChangeInCash = csList.Sum(x=>x.NetChangeInCash),
                TotalAdjustments = csList.Sum(x=>x.TotalAdjustments)
            };
        }

        private IncomeStatement SumIncomeStatements(List<IncomeStatement> isList)
        {
            return new IncomeStatement
            {
                NetIncomeApplicableToCommon = isList.Sum(x=>x.NetIncomeApplicableToCommon),
                CostOfRevenue = isList.Sum(x=>x.CostOfRevenue),
                DiscontinuedOperation = isList.Sum(x=>x.DiscontinuedOperation),
                Ebit = isList.Sum(x=>x.Ebit),
                EquityEarnings = isList.Sum(x=>x.EquityEarnings),
                ExtraordinaryItems = isList.Sum(x=>x.ExtraordinaryItems),
                GrossProfit = isList.Sum(x=>x.GrossProfit),
                IncomeBeforeTaxes = isList.Sum(x=>x.IncomeBeforeTaxes),
                InterestExpense = isList.Sum(x=>x.InterestExpense),
                NetIncome = isList.Sum(x=>x.NetIncome),
                ResearchDevelopementExpense = isList.Sum(x=>x.ResearchDevelopementExpense),
                SellingGeneralAdministrativeExpense = isList.Sum(x=>x.SellingGeneralAdministrativeExpense),
                TotalRevenue = isList.Sum(x=>x.TotalRevenue)
            };
        }
    }
}