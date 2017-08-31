using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System.Configuration;
using System.Data;
using Microsoft.Xrm.Sdk.Query;

namespace CaseWebForm
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            caseAlert.Visible = false;

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CRM Online"].ToString();
            // Connect to the CRM web service using a connection string.
            CrmServiceClient conn = new CrmServiceClient(connectionString);

            // Cast the proxy client to the IOrganizationService interface.
            IOrganizationService service = (IOrganizationService)conn.OrganizationWebProxyClient != null ? (IOrganizationService)conn.OrganizationWebProxyClient : (IOrganizationService)conn.OrganizationServiceProxy;


            QueryExpression loanid = new QueryExpression("rev_loan");
            loanid.ColumnSet.AddColumns("rev_loanidnumber", "rev_contactid");
            loanid.Criteria.AddCondition("rev_loanidnumber", ConditionOperator.Equal, loannumber.Text);
            EntityCollection col = service.RetrieveMultiple(loanid); //retrieve all where query is met
            Entity loan = col.Entities.FirstOrDefault(); //gets the first one in the collection but it only returns one thing

            string fullname = fname.Text + " " + lname.Text;

            if (loan.Attributes.Contains("rev_loanidnumber"))
            {


                if (loan.Attributes.Contains("rev_contactid"))
                {
                    EntityReference contactref = (EntityReference)loan.Attributes["rev_contactid"];

                    QueryExpression contactquery = new QueryExpression("contact");
                    contactquery.ColumnSet.AddColumns("fullname", "firstname", "lastname");
                    contactquery.Criteria.AddCondition(fullname, ConditionOperator.Equal, "fullname");
                    Entity contact = service.Retrieve("contact", contactref.Id, new ColumnSet("fullname"));

                    if (contact.Attributes.Contains("fullname"))
                    {
                        if (contact.Attributes["fullname"].ToString().Trim() == fullname.Trim())
                        {
                            Incident incident = new Incident();
                            incident.Title = title.Text;
                            incident.CustomerId = contactref;
                            incident.rev_LoanName = loan.ToEntityReference();
                            incident.Description = description.Value;
                            Guid incidentid = service.Create(incident);

                            Entity caseincident = service.Retrieve("incident", incidentid, new ColumnSet("ticketnumber"));
                            if (caseincident.Attributes.Contains("ticketnumber"))
                            {
                                caseAlert.InnerText = "You have successfully submuitted your case. Case ID: " + caseincident["ticketnumber"].ToString();
                                caseAlert.Attributes.Add("class", "alert alert-success");
                                caseAlert.Visible = true;
                            }
                            else
                            {
                                caseAlert.InnerHtml = "You have successfully submuitted your case. <strong>Unfortunaely, the system was not able to get your Case ID at this time</strong>";
                                caseAlert.Attributes.Add("class", "alert alert-success");
                                caseAlert.Visible = true;
                            }
                           
                        }

                        else
                        {
                            caseAlert.InnerText = "Contact name is not associated with the loan number provided";
                            caseAlert.Attributes.Add("class", "alert alert-danger");
                            caseAlert.Visible = true;
                        }
                    }
                }
               
            }
            else
            {
                caseAlert.InnerText = "Loan number is not found in the System";
                caseAlert.Attributes.Add("class", "alert alert-danger");
                caseAlert.Visible = true;
            }

        }

                    
    }
}