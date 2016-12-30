using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities = AskAway.Business.Entities;
using AskAway.Business.Components;

public partial class Survey : Page
{
    internal Entities.Survey survey;
    internal Entities.SubmittedSurvey submittedSurvey;
    internal List<Entities.SubmittedSurveyResponse> submittedSurveryResponses = new List<Entities.SubmittedSurveyResponse>();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var surveyId = Request.QueryString["id"];
            survey = new Entities.Survey(new Guid(surveyId));
            submittedSurvey = new Entities.SubmittedSurvey();

            Guid regarding = new Guid(Request.QueryString["regarding"]);
            submittedSurvey.RegardingId = regarding;
                
            if (!Page.IsPostBack)
            {
                LoadQuestions();
            }
            error.Visible = false;
            success.Visible = true;
        }
        catch
        {
            error.Visible = true;
            success.Visible = false;
        }
    }

    protected void SurveyQuestionRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList surveyQuestionRating5 = (DropDownList)e.Item.FindControl("SurveyQuestionRating5");
            TextBox surveyQuestionTextBox = (TextBox)e.Item.FindControl("SurveyQuestionTextBox");
            var type = DataBinder.Eval(e.Item.DataItem, "Type").ToString();
            
            switch (type)
            {
                case "Rating5":
                    surveyQuestionRating5.Visible = true;
                    break;
                case "Money":
                    surveyQuestionTextBox.Visible = true;
                    surveyQuestionTextBox.Attributes.Add("placeholder", "$150,000.00");
                    break;
                case "Decimal":
                    surveyQuestionTextBox.Visible = true;
                    surveyQuestionTextBox.Attributes.Add("placeholder", "14.32");
                    break;
                case "StringShort":
                    surveyQuestionTextBox.Visible = true;
                    break;
                case "Integer":
                    surveyQuestionTextBox.Visible = true;
                    surveyQuestionTextBox.Attributes.Add("placeholder", "1,415");
                    break;
                case "StringLong":
                    surveyQuestionTextBox.Visible = true;
                    break;
            }

            Label surveyQuestionRequired = (Label)e.Item.FindControl("SurveyQuestionRequired");
            var required = DataBinder.Eval(e.Item.DataItem, "IsRequired").ToString();
            if (required == "True")
            {
                surveyQuestionRequired.Visible = true;
            }
        }
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        if (ValidateResponses() == true)
        {
            SaveResponses();
        }
    }

    private void LoadQuestions()
    {
        SurveyQuestionRepeater.DataSource = survey.GetSurveyQuestions();
        SurveyQuestionRepeater.DataBind();
    }

    private bool ValidateResponses()
    {
        var valid = true;
        foreach (RepeaterItem item in SurveyQuestionRepeater.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                //validate question logic
                var surveyQuestionId = ((HiddenField)item.FindControl("SurveyQuestionId")).Value;
                var surveyQuestionType = ((HiddenField)item.FindControl("SurveyQuestionType")).Value;
                var surveyQuestionRating5 = (DropDownList)item.FindControl("SurveyQuestionRating5");
                var surveyQuestionTextBox = (TextBox)item.FindControl("SurveyQuestionTextBox");
                var surveyQuestionRequired = (Label)item.FindControl("SurveyQuestionRequired");
                var invalidInputLabel = (Label)item.FindControl("InvalidInputLabel");
                var requiredFieldLabel = (Label)item.FindControl("RequiredFieldLabel");

                switch (surveyQuestionType)
                {
                    case "Rating5":
                        var i = 0;
                        if (String.IsNullOrWhiteSpace(surveyQuestionRating5.SelectedValue) && surveyQuestionRequired.Visible)
                        {
                            valid = false;
                            requiredFieldLabel.Visible = true;
                        }
                        else if (!String.IsNullOrWhiteSpace(surveyQuestionRating5.SelectedValue) && int.TryParse(surveyQuestionRating5.SelectedValue, out i) == false)
                        {
                            valid = false;
                            invalidInputLabel.Visible = true;
                        }
                        break;
                    case "Money":
                        var m = 0m;
                        if (String.IsNullOrWhiteSpace(surveyQuestionTextBox.Text) && surveyQuestionRequired.Visible)
                        {
                            valid = false;
                            requiredFieldLabel.Visible = true;
                        }
                        else if (!String.IsNullOrWhiteSpace(surveyQuestionTextBox.Text) && decimal.TryParse(surveyQuestionTextBox.Text.Replace("$", ""), out m) == false)
                        {
                            valid = false;
                            invalidInputLabel.Visible = true;
                            invalidInputLabel.Text = invalidInputLabel.Text + "Please enter a value in USD, monetary format (e.g. $150,000.00).";
                        }
                        break;
                    case "Decimal":
                        surveyQuestionTextBox.Visible = true;
                        break;
                    case "StringShort":
                        surveyQuestionTextBox.Visible = true;
                        break;
                    case "Integer":
                        surveyQuestionTextBox.Visible = true;
                        break;
                    case "StringLong":
                        surveyQuestionTextBox.Visible = true;
                        break;
                }
            }
        }

        if (valid == false)
        {
            ErrorDiv.Visible = true;
        }

        return valid;
    }

    private void SaveResponses()
    {
        if (!String.IsNullOrWhiteSpace(Request.QueryString["regarding"]))
        {
            try
            {
                Guid regarding = new Guid(Request.QueryString["regarding"]);
                submittedSurvey.RegardingId = regarding;
            }
            catch
            {
                error.Visible = true;
                success.Visible = false;
                return;
            }
        }

        submittedSurvey.SurveyId = survey.Id;
        submittedSurvey.SaveRecordToDatabase(new Guid("17F6FCEB-CF02-E411-9726-D8D385C29900"));

        foreach (RepeaterItem item in SurveyQuestionRepeater.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                //validate question logic
                var surveyQuestionId = ((HiddenField)item.FindControl("SurveyQuestionId")).Value;
                var surveyQuestionType = ((HiddenField)item.FindControl("SurveyQuestionType")).Value;
                var surveyQuestionRating5 = (DropDownList)item.FindControl("SurveyQuestionRating5");
                var surveyQuestionTextBox = (TextBox)item.FindControl("SurveyQuestionTextBox");

                var submittedSurveyResponse = new Entities.SubmittedSurveyResponse();
                submittedSurveyResponse.SubmittedSurveyId = submittedSurvey.Id;
                submittedSurveyResponse.SurveyQuestionId = new Guid(surveyQuestionId);

                switch (surveyQuestionType)
                {
                    case "Rating5":
                        submittedSurveyResponse.Response = surveyQuestionRating5.SelectedValue;
                        break;
                    case "Money":
                        submittedSurveyResponse.Response = surveyQuestionTextBox.Text.Replace("$", "");
                        break;
                    case "Decimal":
                        surveyQuestionTextBox.Visible = true;
                        break;
                    case "StringShort":
                        surveyQuestionTextBox.Visible = true;
                        break;
                    case "Integer":
                        surveyQuestionTextBox.Visible = true;
                        break;
                    case "StringLong":
                        surveyQuestionTextBox.Visible = true;
                        break;
                }

                submittedSurveyResponse.SaveRecordToDatabase(new Guid("17F6FCEB-CF02-E411-9726-D8D385C29900"));
            }
        }

        Response.Redirect("~/Success.aspx");
    }
}