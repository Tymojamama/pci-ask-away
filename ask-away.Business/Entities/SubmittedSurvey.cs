using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AskAway.Business.Components;
using PensionConsultants.Data.Utilities;

namespace AskAway.Business.Entities
{
    public class SubmittedSurvey : DatabaseEntity
    {
        public Guid SurveyId;
        public Guid RegardingId;

        private static string _tableName = "SubmittedSurvey";

        public SubmittedSurvey()
            : base(_tableName)  
        {

        }

        public SubmittedSurvey(Guid primaryKey)
            : base(_tableName, primaryKey)
        {

        }

        /// <summary>
        /// Registers the instance's members with the abstract class in order to perform database operations. Do not register members
        /// that exist within the abstract class (e.g. CreatedOn).
        /// </summary>
        protected override void RegisterMembers()
        {
            base.AddColumn("SurveyId", SurveyId);
            base.AddColumn("RegardingId", RegardingId);
        }

        /// <summary>
        /// Resets the values of all public members to their values in the database.
        /// </summary>
        protected override void SetRegisteredMembers()
        {
            SurveyId = (Guid)base.GetColumn("SurveyId");
            RegardingId = (Guid)base.GetColumn("RegardingId");
        }
    }
}
