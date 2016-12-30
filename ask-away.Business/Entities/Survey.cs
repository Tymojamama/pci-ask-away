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
    public class Survey : DatabaseEntity
    {
        public string Name;
        public string Description;

        private static string _tableName = "Survey";

        public Survey()
            : base(_tableName)  
        {

        }

        public Survey(Guid primaryKey)
            : base(_tableName, primaryKey)
        {

        }

        public List<SurveyQuestion> GetSurveyQuestions()
        {
            if (!base.ExistingRecord)
            {
                return new List<SurveyQuestion>();
            }

            var surveyQuestions = SurveyQuestion.All();
            surveyQuestions.Sort((x, y) => x.Ordinal.CompareTo(y.Ordinal));
            return surveyQuestions.FindAll(x => x.SurveyId == base.Id);
        }

        /// <summary>
        /// Registers the instance's members with the abstract class in order to perform database operations. Do not register members
        /// that exist within the abstract class (e.g. CreatedOn).
        /// </summary>
        protected override void RegisterMembers()
        {
            base.AddColumn("Name", Name);
            base.AddColumn("Description", Description);
        }

        /// <summary>
        /// Resets the values of all public members to their values in the database.
        /// </summary>
        protected override void SetRegisteredMembers()
        {
            Name = (string)base.GetColumn("Name");
            Description = (string)base.GetColumn("Description");
        }
    }
}
