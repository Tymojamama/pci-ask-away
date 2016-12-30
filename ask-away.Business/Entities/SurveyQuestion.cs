using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AskAway.Business.Components;
using PensionConsultants.Data.Utilities;

namespace AskAway.Business.Entities
{
    public class SurveyQuestion : DatabaseEntity
    {
        public string Name { get; set; }
        public string Type { get; set; } // Rating5, Decimal, StringShort, StringLong, Integer
        public Guid SurveyId { get; set; }
        public int Ordinal { get; set; }
        public SqlBoolean IsRequired { get; set; }

        private static string _tableName = "SurveyQuestion";

        public SurveyQuestion()
            : base(_tableName)
        {

        }

        public SurveyQuestion(Guid primaryKey)
            : base(_tableName, primaryKey)
        {

        }

        /// <summary>
        /// Registers the instance's members with the abstract class in order to perform database operations. Do not register members
        /// that exist within the abstract class (e.g. CreatedOn).
        /// </summary>
        protected override void RegisterMembers()
        {
            base.AddColumn("Name", Name);
            base.AddColumn("Type", Type);
            base.AddColumn("SurveyId", SurveyId);
            base.AddColumn("Ordinal", Ordinal);
            base.AddColumn("IsRequired", IsRequired);
        }

        /// <summary>
        /// Resets the values of all public members to their values in the database.
        /// </summary>
        protected override void SetRegisteredMembers()
        {
            Name = (string)base.GetColumn("Name");
            Type = (string)base.GetColumn("Type");
            SurveyId = (Guid)base.GetColumn("SurveyId");
            IsRequired = (SqlBoolean)base.GetColumn("IsRequired");
            Ordinal = (int)base.GetColumn("Ordinal");
        }

        public static List<SurveyQuestion> All()
        {
            List<SurveyQuestion> list = new List<SurveyQuestion>();

            foreach (DataRow dataRow in GetAll().Rows)
            {
                Guid surveyQuestionId = new Guid(dataRow["SurveyQuestionId"].ToString());
                SurveyQuestion surveyQuestion = new SurveyQuestion(surveyQuestionId);
                list.Add(surveyQuestion);
            }

            return list;
        }

        private static DataTable GetAll()
        {
            return Access.AskAwayDbAccess.ExecuteStoredProcedureQuery("[dbo].[usp_GetAllSurveyQuestions]");
        }
    }
}
