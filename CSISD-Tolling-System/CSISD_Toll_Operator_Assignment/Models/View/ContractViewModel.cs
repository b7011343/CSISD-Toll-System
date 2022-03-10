using System.Collections.Generic;

namespace CSISD_Toll_Operator_Assignment.Models.View
{
    /// <summary>
    /// Model for the ContractRoadUser.cshtml and ContractTollOperator.cshtml views.
    /// </summary>
    public class ContractViewModel
    {
        /// <summary>
        /// List of contracts visible on the page
        /// </summary>
        public List<Contract> Contracts { get; set; }
    }
}
