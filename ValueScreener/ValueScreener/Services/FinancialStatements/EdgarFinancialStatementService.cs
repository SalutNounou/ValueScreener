using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ValueScreener.Models.Domain;

namespace ValueScreener.Services.FinancialStatements
{
    public class EdgarFinancialStatementService : IFinancialStatementService
    {
        private readonly string _apiKey;
        private readonly string _serviceEntryPoint = "http://edgaronline.api.mashery.com/v2/corefinancials/ann.json?primarysymbols=";
        private readonly string _serviceEntryPointQuarterly = "http://edgaronline.api.mashery.com/v2/corefinancials/qtr.json?primarysymbols=";
        public EdgarFinancialStatementService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<List<FinancialStatement>> GetFinancialStatementsAsync(string stockTicker)
        {
            try
            {
               
                var url = $"{_serviceEntryPoint}{stockTicker}&appkey={_apiKey}";
                using (System.Net.Http.HttpClient hc = new System.Net.Http.HttpClient())
                {
                    var str= await hc.GetStringAsync(url);
                    return ParseFinancialStatements(str,"annual");                  
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IEnumerable<FinancialStatement>> GetQuarterlyFinancialStatementAsync(string stockTicker)
        {
            try
            {

                var url = $"{_serviceEntryPointQuarterly}{stockTicker}&appkey={_apiKey}";
                using (System.Net.Http.HttpClient hc = new System.Net.Http.HttpClient())
                {
                    var str = await hc.GetStringAsync(url);
                    return ParseFinancialStatements(str,"quarterly").Take(4);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private List<FinancialStatement> ParseFinancialStatements(string jsonData, string frequency)
        {
            dynamic statements = JObject.Parse(jsonData);
            var results = new List<FinancialStatement>();
            if (statements != null && statements.result != null && statements.result.rows != null)
            {
                foreach (var row in statements.result.rows.Children())
                {
                    FinancialStatement statement = new FinancialStatement{Source = frequency};
                    foreach (var entry in row.values.Children())
                    {
                        string field = entry.field;
                        string value = entry.value;
                        if (Setters.ContainsKey(field) && value != null && value != "null")
                        {
                            Setters[field](statement, value);
                        }
                    }
                    results.Add(statement);
                }
            }
            return results;
        }


        private static readonly Dictionary<string, Action<FinancialStatement, string>> Setters =
          new Dictionary<string, Action<FinancialStatement, string>>
          {
                #region Metadata
               
                {"usdconversionrate", (f,s)=>f.UsdConversionRate=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },

                {"receiveddate", (f,s)=>f.ReceivedDate=Convert.ToDateTime(s, CultureInfo.InvariantCulture) },                
                {"periodlengthcode", (f,s)=>f.PeriodLengthCode=s },
                {"periodlength", (f,s)=>f.PeriodLength=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"periodenddate", (f,s)=>f.PeriodEnd=Convert.ToDateTime(s,CultureInfo.InvariantCulture) },
                {"formtype", (f,s)=>f.FormType=s },
                {"fiscalyear", (f,s)=>f.FiscalYear=Convert.ToInt32(s) },
                {"fiscalquarter", (f,s)=>f.FiscalQuarter=Convert.ToInt32(s) },             
                {"currencycode", (f,s)=>f.CurrencyCode=s },
              {"primarysymbol", (f,s)=>f.PrimarySymbol=s },
                #endregion Metadata


                {"changeincurrentassets", (f,s)=>f.CashFlowStatement.ChangeInCurrentAsset=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"changeincurrentliabilities", (f,s)=>f.CashFlowStatement.ChangeInCurrentLiabilities=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"changeininventories", (f,s)=>f.CashFlowStatement.ChangeInInventories=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"dividendspaid", (f,s)=>f.CashFlowStatement.DividendsPaid=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"effectofexchangerateoncash", (f,s)=>f.CashFlowStatement.EffectOfExchangeRateOnCash=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"capitalexpenditures", (f,s)=>f.CashFlowStatement.CapitalExpanditure=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },


                {"cashfromfinancingactivities", (f,s)=>f.CashFlowStatement.CashFromFinancingActivities=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"cashfrominvestingactivities", (f,s)=>f.CashFlowStatement.CashFromInvestingActivities=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"cashfromoperatingactivities", (f,s)=>f.CashFlowStatement.CashFromOperatingActivities=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"cfdepreciationamortization", (f,s)=>f.CashFlowStatement.CfDepreciationAmortization=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"changeinaccountsreceivable", (f,s)=>f.CashFlowStatement.ChangeInAccountReceivable=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"investmentchangesnet", (f,s)=>f.CashFlowStatement.InvestmentChangesNet=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"accountingchange", (f,s)=>f.CashFlowStatement.AccountingChange=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"netchangeincash", (f,s)=>f.CashFlowStatement.NetChangeInCash=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"totaladjustments", (f,s)=>f.CashFlowStatement.TotalAdjustments=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },


                {"ebit", (f,s)=>f.IncomeStatement.Ebit=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"costofrevenue", (f,s)=>f.IncomeStatement.CostOfRevenue=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"discontinuedoperations", (f,s)=>f.IncomeStatement.DiscontinuedOperation=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"equityearnings", (f,s)=>f.IncomeStatement.EquityEarnings=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },

               
                {"extraordinaryitems", (f,s)=>f.IncomeStatement.ExtraordinaryItems=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"grossprofit", (f,s)=>f.IncomeStatement.GrossProfit=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"incomebeforetaxes", (f,s)=>f.IncomeStatement.IncomeBeforeTaxes=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"interestexpense", (f,s)=>f.IncomeStatement.InterestExpense=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"netincome", (f,s)=>f.IncomeStatement.NetIncome=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },

                {"netincomeapplicabletocommon", (f,s)=>f.IncomeStatement.NetIncomeApplicableToCommon=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"researchdevelopmentexpense", (f,s)=>f.IncomeStatement.ResearchDevelopementExpense=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"totalrevenue", (f,s)=>f.IncomeStatement.TotalRevenue=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"sellinggeneraladministrativeexpenses", (f,s)=>f.IncomeStatement.SellingGeneralAdministrativeExpense=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },


                {"commonstock", (f,s)=>f.BalanceSheet.CommonStock=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"deferredcharges", (f,s)=>f.BalanceSheet.DeferredCharges=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },


                {"cashandcashequivalents", (f,s)=>f.BalanceSheet.CashAndCashEquivalent=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"cashcashequivalentsandshortterminvestments", (f,s)=>f.BalanceSheet.CashCashEquivalentAndShortTermInvestments=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"goodwill", (f,s)=>f.BalanceSheet.Goodwill=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"intangibleassets", (f,s)=>f.BalanceSheet.IntangibleAssets=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"inventoriesnet", (f,s)=>f.BalanceSheet.InventoriesNet=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"minorityinterest", (f,s)=>f.BalanceSheet.MinorityInterest=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },

                {"otherassets", (f,s)=>f.BalanceSheet.OtherAssets=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"othercurrentassets", (f,s)=>f.BalanceSheet.OtherCurrentAssets=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"othercurrentliabilities", (f,s)=>f.BalanceSheet.OtherCurrentLiabilities=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"otherequity", (f,s)=>f.BalanceSheet.OtherEquity=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"otherliabilities", (f,s)=>f.BalanceSheet.OtherLiabilities=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"preferredstock", (f,s)=>f.BalanceSheet.PreferredStock=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },

                {"propertyplantequipmentnet", (f,s)=>f.BalanceSheet.PropertyPlantEquipmentNet=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"retainedearnings", (f,s)=>f.BalanceSheet.RetainedEarnings=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"totalassets", (f,s)=>f.BalanceSheet.TotalAssets=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"totalcurrentassets", (f,s)=>f.BalanceSheet.TotalCurrentAssets=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"totalcurrentliabilities", (f,s)=>f.BalanceSheet.TotalCurrentLiabilities=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"totalliabilities", (f,s)=>f.BalanceSheet.TotalLiabilities=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },

                {"totallongtermdebt", (f,s)=>f.BalanceSheet.TotalLongTermDebt=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"totalreceivablesnet", (f,s)=>f.BalanceSheet.TotalReceivableNet=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"totalshorttermdebt", (f,s)=>f.BalanceSheet.TotalShortTermDebt=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"totalstockholdersequity", (f,s)=>f.BalanceSheet.TotalStockHolderEquity=Convert.ToDecimal(s, CultureInfo.InvariantCulture) },
                {"treasurystock", (f,s)=>f.BalanceSheet.TreasuryStock=Convert.ToDecimal(s, CultureInfo.InvariantCulture) }
          };
    }
}