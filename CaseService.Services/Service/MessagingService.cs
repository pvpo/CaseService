using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CaseService.Services.DTO;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace CaseService.Services.Service {

    public class MessagingService {

        private static readonly CaseService caseService = CaseService.Instance;
        private static readonly QueueClient queueClient = new QueueClient(Configuration.ServiceBus.connectionURI, Configuration.ServiceBus.orderQueue);

        public MessagingService( ) {
            RegisterNewCaseOnMessageHandlerAndReceiveMessages();
        }

        static void RegisterNewCaseOnMessageHandlerAndReceiveMessages() {
            // Configure the MessageHandler Options in terms of exception handling, number of concurrent messages to deliver etc.
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                // Maximum number of Concurrent calls to the callback `ProcessMessagesAsync`, set to 1 for simplicity.
                // Set it according to how many messages the application wants to process in parallel.
                MaxConcurrentCalls = 1,

                // Indicates whether MessagePump should automatically complete the messages after returning from User Callback.
                // False below indicates the Complete will be handled by the User Callback as in `ProcessMessagesAsync` below.
                AutoComplete = false
            };

            // Register the function that will process messages
            queueClient.RegisterMessageHandler(ProcessNewCaseMessagesAsync, messageHandlerOptions);
        }

        static async Task ProcessNewCaseMessagesAsync(Message message, CancellationToken token) {
            // Process the message
            string messageBody = Encoding.UTF8.GetString(message.Body);
            Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{messageBody}");

            CreateCaseDTO dto = JsonConvert.DeserializeObject<CreateCaseDTO>(messageBody);
            caseService.createAndPersistAsync(dto);
            // Complete the message so that it is not received again.
            // This can be done only if the queueClient is created in ReceiveMode.PeekLock mode (which is default).
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);

            // Note: Use the cancellationToken passed as necessary to determine if the queueClient has already been closed.
            // If queueClient has already been Closed, you may chose to not call CompleteAsync() or AbandonAsync() etc. calls 
           // to avoid unnecessary exceptions.
        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs) {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }

    }
}