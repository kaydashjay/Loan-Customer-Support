using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Xrm.Sdk.Query;
using System.Activities;

namespace Custom_Workflow
{

    public class LoanPayments : CodeActivity
    {
        [Input("Monthly Payments")]
        public InArgument<decimal> MP { get; set; }

        //[Input("Loan ")]
        //public InArgument<decimal> Tenure { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            //Create the tracing service
            ITracingService tracingService = executionContext.GetExtension<ITracingService>();

            //Create the context
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            // Obtain the runtime value of the Text input argument


            
            Entity loan = (Entity)context.InputParameters["Target"];
            Entity loanpayments = new Entity("rev_loanpayment");
            int n;

            decimal monthlypayments =MP.Get(executionContext);
            //if (loan.Attributes.Contains("rev_monthlypayments"))
            //{
            //    monthlypayments = ((Money)loan.Attributes["rev_monthlypayments"]).Value;
            //}
            EntityReference loancontact = (EntityReference)loan.Attributes["rev_contactid"];
            EntityReference loanname = loan.ToEntityReference();
            decimal interest = Convert.ToDecimal(loan.Attributes["rev_interest"]);
            



            // Entity loanImage = (Entity)context.PostEntityImages["LoanImage"];

            if (loan.Attributes.Contains("rev_loantenure"))
            {
                n = Convert.ToInt32(loan.Attributes["rev_loantenure"]);
                DateTime duedate = DateTime.Now.AddMonths(1);

                string ID = loan.Attributes["rev_loanidnumber"].ToString();
                Entity loanpayment = new Entity();
                for (int i = 1; i <= n; i++)
                {
                    loanpayments.Attributes.Add("rev_name", loan.Attributes["rev_name"].ToString() + ID + "(" + i + ")");
                    loanpayments.Attributes.Add("rev_contact", loancontact);
                    loanpayments.Attributes.Add("rev_loanname", loanname);
                    loanpayments.Attributes.Add("rev_interestrate",interest );
                    loanpayments.Attributes.Add("rev_amount", new Money(monthlypayments));
                    loanpayments.Attributes.Add("rev_paymentdate", duedate);
                    Guid id = service.Create(loanpayments);
                    loanpayments = new Entity("rev_loanpayment");
                    duedate = duedate.AddMonths(1);
                }
                
            }
            else
            {
                throw new InvalidPluginExecutionException("Tenure Empty");
            }
      


        }
    }
}
