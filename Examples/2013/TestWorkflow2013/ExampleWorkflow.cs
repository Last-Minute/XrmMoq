using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace TestWorkflow2013
{
    public class ExampleWorkflow : CodeActivity
    {
        [RequiredArgument]
        [Input("Date To Check")]
        public InArgument<DateTime> DateToCheck { get; set; }

        [Output("Is Public Holiday")]
        public OutArgument<bool> IsPublicHoliday { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            try
            {
                DateTime dateToCheck = DateToCheck.Get(executionContext);
                string dateOnly = dateToCheck.ToString("dd-MM-yyyy");

                //Use the adapter pattern to create a wrapper for the standard HttpClient
                //Calls the underlying functionality but provides a testable interface
                HttpClientAdapter adapter = new HttpClientAdapter();

                var callWebServiceTask = Task.Run(async () => await CallWebService(adapter, dateOnly));

                Task.WaitAll(callWebServiceTask);

                bool isValid = callWebServiceTask.Result;

                IsPublicHoliday.Set(executionContext, isValid);
            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }

        public async Task<bool> CallWebService(HttpClientAdapter httpClient, string dateOnly)
        {
            using (httpClient)
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get,
                    new Uri($"https://kayaposoft.com/enrico/json/v1.0/?action=isPublicHoliday&date={dateOnly}&country=usa"));
                request.Headers.Add("Connection", "close");

                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode != HttpStatusCode.OK)
                    return false;

                EnricoResponse enricoResponse = DeserializeResponse<EnricoResponse>(await response.Content.ReadAsStringAsync());
                return enricoResponse.isPublicHoliday;
            }
        }

        public static T DeserializeResponse<T>(string response)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(response);
                writer.Flush();
                stream.Position = 0;
                T responseObject = (T)serializer.ReadObject(stream);
                return responseObject;
            }
        }
    }
}