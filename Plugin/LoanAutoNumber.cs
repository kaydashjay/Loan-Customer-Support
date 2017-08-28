using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Plugins
{
    //Creates a specific case Number and automatically increments when a case is created.
    public class LoanAutoNumber : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracingService =
          (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            IPluginExecutionContext context = (IPluginExecutionContext)
                 serviceProvider.GetService(typeof(IPluginExecutionContext));

            IOrganizationServiceFactory serviceFactory =
                (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            // The InputParameters collection contains all the data passed in the message request.
            if (context.InputParameters.Contains("Target") &&
                context.InputParameters["Target"] is Entity)
            {
                // Obtain the target entity from the input parameters.
                Entity loan = (Entity)context.InputParameters["Target"];
                
                //cerate query expression to get the autonumber value created in the UI
                //There was a Config Entity with a rev_name Primary Field and a rev_value field.
                //name=Autonumber and value =1000
                // Using Query Expression
                QueryExpression query = new QueryExpression("rev_config");
                query.ColumnSet.AddColumn("rev_value");
                query.Criteria.AddCondition(new
                    ConditionExpression("rev_key", ConditionOperator.Equal, "Auto Number"));
                //query.Criteria.AddCondition(new ConditionExpression("rev_key", ConditionOperator.Equal, "Interest Rate")),

                EntityCollection collection = service.RetrieveMultiple(query); //retrieve all where query is met
                Entity config = collection.Entities.FirstOrDefault(); //gets the first one in the collection but it only returns one thing
                string autoNumber = config.Attributes["rev_value"].ToString();

                QueryExpression interest = new QueryExpression("rev_config");
                interest.ColumnSet.AddColumn("rev_value");
                interest.Criteria.AddCondition(new
                    ConditionExpression("rev_key", ConditionOperator.Equal, "Interest Rate"));

                EntityCollection col = service.RetrieveMultiple(interest); //retrieve all where query is met
                Entity configs = col.Entities.FirstOrDefault(); //gets the first one in the collection but it only returns one thing
                string rate = configs.Attributes["rev_value"].ToString();
                // gets attributes from config table after its been update

                loan.Attributes.Add("rev_loanidnumber", autoNumber);//modifies the field of the case to the case number that was retrieved 
                loan.Attributes.Add("rev_interest", Convert.ToDecimal(rate));

                // Update Config
                config.Attributes["rev_value"] = (Convert.ToInt32(autoNumber) + 1).ToString(); //increments the autonumber in the configs entity
                service.Update(config);


            }

        }
    }
}