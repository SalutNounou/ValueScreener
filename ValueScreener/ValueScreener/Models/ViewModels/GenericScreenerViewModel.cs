using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using ValueScreener.Controllers.ScreenerCriteria;
using ValueScreener.Controllers.Screeners;

namespace ValueScreener.Models.ViewModels
{
    public class GenericScreenerViewModel
    {
        public GenericScreenerViewModel()
        {
            AvailableCriterias = new List<SelectListItem>();
            ColumnTitles = new List<ColumnTitle>();
            Rows = new List<ScreenerRowViewModel>();
        }
        public List<ScreenerCriteriaViewModel> Criterias { get; set; }
        public bool FollowingSpecified { get; set; }

        public List<SelectListItem> AvailableCriterias { get; set; }
        [Display(Name = "Add New Criteria : ")]
        public string CriteriaToAdd { get; set; }

        public List<SelectListItem> CriteriaOperators { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value =CriteriaConstants.Greater, Text = "Greater Than" },
            new SelectListItem { Value = CriteriaConstants.Lower, Text = "Lower Than" },
            new SelectListItem { Value = CriteriaConstants.IsEqual, Text = "Equals"  },
            new SelectListItem { Value = CriteriaConstants.Different, Text = "Different From"  },
        };

        public List<ColumnTitle> ColumnTitles { get; }
        public List<ScreenerRowViewModel> Rows { get; }

        public string GetUrl(int pageIndex)
        {
           
            var endPoint = "/Screener/ScreenGeneric?";
            if (Criterias != null & Criterias.Any())
            {
                for (int i = 0; i < Criterias.Count; i++)
                {
                    endPoint +=$"Criterias%5B{i}%5D.Name={Criterias[i].Name}&";
                    endPoint += $"Criterias%5B{i}%5D.Operation={Criterias[i].Operation}&";
                    endPoint += $"Criterias%5B{i}%5D.NumberValue={Criterias[i].NumberValue}&";
                }
               
            }
           
            if (pageIndex > 0)
                endPoint += "&PageIndex=" + pageIndex;
            if (endPoint.EndsWith("&"))
                endPoint.Remove(endPoint.Length - 1);

            endPoint = endPoint.Replace(' ', '+');
            endPoint = endPoint.Replace("(", "%28");
            endPoint = endPoint.Replace(")", "%29");
            return endPoint;
        }

        public int PageIndex { get; set; }

    }


  
    public class ScreenerCriteriaViewModel 
    {
        [Display(Name = "Criteria : ")]
        public string Name { get; set; }
        public string Id { get; set; }
        public decimal NumberValue { get; set; }
        public string Operation { get; set; }

        public CellKind ValueType { get; set; }
        public string StringValue { get; set; }
    }
}
