using Azure.Messaging.ServiceBus;

internal class Program
{
    private static readonly string serviceBusConnectionString = "my service bus conn string";
    private static readonly string serviceBusTopicName = "captureddatamessages-topic";
    private static readonly string serviceBusSubName = "processingDataMessages-sub";
    private static readonly string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @$"Data Folder To Receive");
    private static int messageCount = 0;

    private static async Task Main(string[] args)
    {
        // fullPath=bin\Debug\net6.0\Data Folder To Receive;
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        await using var client = new ServiceBusClient(serviceBusConnectionString);

        var processorOptions = new ServiceBusProcessorOptions
        {
            MaxConcurrentCalls = 1,
            AutoCompleteMessages = false
        };

        await using ServiceBusProcessor processor = client.CreateProcessor(serviceBusTopicName, serviceBusSubName, processorOptions);

        processor.ProcessMessageAsync += MessageHandler;
        processor.ProcessErrorAsync += ErrorHandler;

        await processor.StartProcessingAsync();

        Console.Read();

        await processor.CloseAsync();
    }
    static async Task MessageHandler(ProcessMessageEventArgs args)
    {
        messageCount++;

        string fullPath = Path.Combine(directory, $@"doc{messageCount}.docx");
        if (File.Exists(fullPath))
            File.Delete(fullPath);

        byte[] pdfBytes = args.Message.Body.ToArray();

        using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
        {
            fileStream.Write(pdfBytes, 0, pdfBytes.Length);
        }

        await args.CompleteMessageAsync(args.Message);

        Console.WriteLine($"Message:{messageCount} received");

    }

    static Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        Console.WriteLine($"Message:{messageCount} was failed");
        if (messageCount != 0)
            messageCount--;
        return Task.CompletedTask;
    }
}
