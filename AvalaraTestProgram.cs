using System;
using Avalara.AvaTax.RestClient;


namespace avalaraTest
{
    class Program
    {
        static void Main(string[] args)
        {

            // Create a client and set up authentication
            var client = new AvaTaxClient("MyApp", "1.0", Environment.MachineName, AvaTaxEnvironment.Production)
                .WithSecurity("AvalaraUsername", "AvalaraPassword");


            // Set to log data to review 
            client.LogToFile("path for log files\\alavaraTest\\avataxapi.log");


            // This is to test if we can connect to site, but does not work at Communicorp (PROBABLY BLOCKING)
            /*
            // Verify that we can ping successfully
            var pingResult = client.Ping();


            if (pingResult.authenticated == true)
            {
                Console.WriteLine("Success!");
            }
            else if(pingResult.authenticated == false)
            {
                Console.WriteLine("Fail!");

            }
            */



            /*
            // (This was from their example online)
            // Create a simple transaction for $100 using the fluent transaction builder
            //var t1 = new TransactionBuilder(client, "DEFAULT", DocumentType.SalesOrder, "123")
            */


            // Send a hardcoded address that works for our location
            var transaction = new TransactionBuilder(client, "AvalaraCompanyCode", DocumentType.SalesOrder, "AvalaraCustomerCode")
                          .WithAddress(TransactionAddressType.SingleLocation, "123 Main Street", null, null, "Columbus", "GA", "31999", "US")
                          .WithLine(150.0m)
                          .Create();


            // Display the returned tax amount
            Console.WriteLine("Your calculated tax was {0}", transaction.totalTax);
            var tax = transaction.totalTax;


            #region Other way to log details
            // You can capture logs to disk like this:
            // Or you can hook the client to capture information about every API call like this:
            // Client.CallCompleted += MyEventHandler;
            #endregion

            // This line is used to stop console from closing (I add a breakpoint for it)
            Console.WriteLine("wait...");
        }
    }
}
