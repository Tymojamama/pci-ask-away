<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Survey.aspx.cs" Inherits="Survey" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div runat="server" id="error" visible="false">
        <div class="jumbotron">
            <h1>Error!</h1>
            <p class="lead">There was an error loading the survey. Perhaps you got a bad link!</p>
            <p><a runat="server" href="~/" class="btn btn-primary btn-lg">&laquo; Back home</a></p>
        </div>
    </div>

    <div runat="server" id="success" visible="false">
        <div class="jumbotron">
            <h1><%: survey.Name %></h1>
            <p class="lead"><%: survey.Description %></p>
        </div>
        
        <div ID="ErrorDiv" class="jumbotron" runat="server" visible="false">
            <asp:Label runat="server" ID="FormErrorLabel" Text="Shoot dang! It didn't save! Looks like there are some errors below!" ForeColor="Red"></asp:Label>
        </div>

        <asp:Repeater ID="SurveyQuestionRepeater" runat="server" OnItemDataBound="SurveyQuestionRepeater_ItemDataBound">
            <ItemTemplate>
                <div>
                    <asp:HiddenField ID="SurveyQuestionId" Value='<%# Eval("Id") %>' runat="server" />
                    <asp:HiddenField ID="SurveyQuestionType" Value='<%# Eval("Type") %>' runat="server" />
                    <asp:Label runat="server" ID="SurveyQuestionRequired" text="*" Visible="false" ForeColor="Red" />
                    <asp:Label runat="server" ID="SurveyQuestionOrdinal" text='<%# Eval("Ordinal") + "." %>' />
                    <asp:Label runat="server" ID="SurveyQuestionName" text='<%# Eval("Name") %>' />
                    <br />
                    <asp:DropDownList ID="SurveyQuestionRating5" runat="server" Visible="false">
                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                        <asp:ListItem Value="1"> 1 - Strongly Disagree </asp:ListItem>
                        <asp:ListItem Value="2"> 2 - Disagree </asp:ListItem>
                        <asp:ListItem Value="3"> 3 - Neutral </asp:ListItem>
                        <asp:ListItem Value="4"> 4 - Agree </asp:ListItem>
                        <asp:ListItem Value="5"> 5 - Strongly Agree </asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="SurveyQuestionTextBox" runat="server" Visible="false"></asp:TextBox>
                    <div>
                        <asp:Label runat="server" ID="RequiredFieldLabel" Text="This field cannot be blank as it is a required field. " Visible="false" ForeColor="Red"></asp:Label>
                        <asp:Label runat="server" ID="InvalidInputLabel" Text="Invalid Input. " Visible="false" ForeColor="Red"></asp:Label>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <div style="margin-top:20px;">
            <asp:Button ID="SubmitButton" CssClass="btn btn-primary" runat="server" OnClick="SubmitButton_Click" text="Submit &raquo;" />
        </div>
    </div>
</asp:Content>
