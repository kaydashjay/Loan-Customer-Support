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

namespace WebApplication1
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
            loanid.ColumnSet.AddColumns("rev_loanidnumber", "rev_contactid" );
            loanid.Criteria.AddCondition("rev_loanidnumber", ConditionOperator.Equal, loannumber.Text);
            EntityCollection col = service.RetrieveMultiple(loanid); //retrieve all where query is met
            Entity loan = col.Entities.FirstOrDefault(); //gets the first one in the collection but it only returns one thing

            if (loan.Attributes.Contains("rev_loanidnumber"))
            {
                if (loan.Attributes.Contains("rev_contactid"))
                {
                    EntityReference contact = (EntityReference)loan.Attributes["rev_contactid"];
                    Incident incident = new Incident();
                    incident.Title = title.Text;
                    incident.CustomerId = contact;
                    incident.rev_LoanName = loan.ToEntityReference();
                    incident.Description = description.Value;
                    Guid incidentid = service.Create(incident);

                    Entity caseincident = new Entity("incident", "incidentid", incidentid);
                    caseAlert.InnerText="Your case has been created. Case ID: "+caseincident.Attributes["ticketnumber"].ToString();
                    caseAlert.Attributes.Add("class", "alert alert-success");
                    caseAlert.Visible = true;

                }
                else
                {
                    caseAlert.InnerText = "Contact ";
                    caseAlert.Attributes.Add("class", "alert alert-danger");
                    caseAlert.Visible = true;
                }
            }
            else
            {
                throw new InvalidPluginExecutionException("Loan Number is Empty");
            }
        }
    }
}