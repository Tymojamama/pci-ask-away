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
    public class SubmittedSurveyResponse : DatabaseEntity
    {
        public Guid SubmittedSurveyId;
        public Guid SurveyQuestionId;
        public string Response;

        private static string _tableName = "SubmittedSurveyResponse";

        public SubmittedSurveyResponse()
            : base(_tableName)
        {

        }

        public SubmittedSurveyResponse(Guid primaryKey)
            : base(_tableName, primaryKey)
        {

        }

        /// <summary>
        /// Registers the instance's members with the abstract class in order to perform database operations. Do not register members
        /// that exist within the abstract class (e.g. CreatedOn).
        /// </summary>
        protected override void RegisterMembers()
        {
            base.AddColumn("SubmittedSurveyId", SubmittedSurveyId);
            base.AddColumn("SurveyQuestionId", SurveyQuestionId);
            base.AddColumn("Response", Response);
        }

        /// <summary>
        /// Resets the values of all public members to their values in the database.
        /// </summary>
        protected override void SetRegisteredMembers()
        {
            SubmittedSurveyId = (Guid)base.GetColumn("SubmittedSurveyId");
            SurveyQuestionId = (Guid)base.GetColumn("SurveyQuestionId");
            Response = (string)base.GetColumn("Response");
        }
    }
}
