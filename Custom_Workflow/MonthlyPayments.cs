using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System.Activities;

namespace Custom_Workflow
{

    public class MonthlyPayments : CodeActivity
    {
        [Input("Loan Enttiy Interest")]
        public InArgument<decimal> APR { get; set; }

        [Input("Loan Enttiy Loan Amount")]
        public InArgument<decimal> PV { get; set; }

        [Input("Loan Enttiy Loan Tenure")]
        public InArgument<int> N { get; set; }

        [Output("Loan Entity Monthly Payments")]
        public OutArgument<decimal> MP { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            //Create the tracing service
            ITracingService tracingService = executionContext.GetExtension<ITracingService>();

            //Create the context
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            // Obtain the runtime value of the Text input argument


            //string text = context.GetValue(this.Text);
            //Entity loan = (Entity)context.InputParameters["Target"];
            decimal apr = APR.Get(executionContext);
            decimal pv = PV.Get(executionContext);
            int n = N.Get(executionContext);

            decimal r = (apr / 100) / 12;
            decimal mp = (r * pv) / (1 - Convert.ToDecimal(Math.Pow((1 + Decimal.ToDouble(r)), -n)));

            MP.Set(executionContext, mp);



            //Entity loanImage = (Entity)context.PreEntityImages["LoanImage"];

            //  if (loan.Attributes.Contains("rev_monthlypayments"))
            // {
            //monthlypayments = ((Money)loan.Attributes["rev_monthlypayments"]).Value;
            //monthlypayments = mp;

            // }
            //((Money)loan.Attributes["rev_monthlypayments"]).Value = mp;

            //service.Update(loan);


        }
    }
}
