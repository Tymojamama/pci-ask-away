using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PensionConsultants.Data.Access;

namespace AskAway.Business.Components
{
    public class Access
    {
        /// <summary>
        /// Represents a database connection to the production ISP database.
        /// </summary>
        public static DataAccessComponent AskAwayDbAccess = new DataAccessComponent(DataAccessComponent.Connections.PCIDB_AskAway, DataAccessComponent.SecurityTypes.Impersonate);

        public static bool ConnectionSucceeded()
        {
            return AskAwayDbAccess.ConnectionSucceeded();
        }
    }
}
