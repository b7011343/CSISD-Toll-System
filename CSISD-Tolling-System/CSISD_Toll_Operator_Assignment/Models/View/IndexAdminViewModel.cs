using System.Collections.Generic;

namespace CSISD_Toll_Operator_Assignment.Models
{
    /// <summary>
    /// Model for the IndexAdmin.cshtml view.
    /// </summary>
    public class IndexAdminViewModel
    {
        /// <summary>
        /// List of all users visible on the administrator page
        /// </summary>
        public List<User> Users { get; set; }
    }
}
