using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Rest;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Management.DataFactory.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace RunPipeline
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set variables
            string tenantID = "72f988bf-86f1-41af-91ab-2d7cd011db47"; //"<your tenant ID>";
            string applicationId = "79135904-7846-4725-9509-f33bd515f04d"; //"<your application ID>";
            string authenticationKey = "vQlAD/XjEqQfULp8IbqqjPB453aefGXj0vs2yMuZaTY=";//"<your authentication key for the application>";
            string subscriptionId = "7b68d2b5-dfbe-46e1-938f-98ed143b7953"; //"<your subscription ID where the data factory resides>";
            string resourceGroup = "demo"; //"<your resource group where the data factory resides>";
            string dataFactoryName = "adflab1"; //" < specify the name of data factory to create. It must be globally unique.>";
            string pipelineName = "pipeline2";    // name of the pipeline

            // Authenticate and create a data factory management client
            var context = new AuthenticationContext("https://login.windows.net/" + tenantID);
            ClientCredential cc = new ClientCredential(applicationId, authenticationKey);
            AuthenticationResult result = context.AcquireTokenAsync("https://management.azure.com/", cc).Result;
            ServiceClientCredentials cred = new TokenCredentials(result.AccessToken);
            var client = new DataFactoryManagementClient(cred) { SubscriptionId = subscriptionId };

            CreateRunResponse runResponse = client.Pipelines.CreateRunWithHttpMessagesAsync(resourceGroup, dataFactoryName, pipelineName).Result.Body;
            Console.WriteLine("Pipeline run ID: " + runResponse.RunId);

            //wait in console window on completion
            Console.ReadKey();
        }
    }
}
