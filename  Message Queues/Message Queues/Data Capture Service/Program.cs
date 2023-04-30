using Azure.Messaging.ServiceBus;


string serviceBusConnectionString = "my service bus conn string";
string serviceBusTopicName = "captureddatamessages-topic";

// fullPath=bin\Debug\net6.0\Data Folder To Send
string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data Folder To Send");
string[] docFilePaths = Directory.GetFiles(fullPath, "*.docx");

await using var client = new ServiceBusClient(serviceBusConnectionString);
ServiceBusSender sender = client.CreateSender(serviceBusTopicName);

try
{
    int messageCount = 0;

    foreach (string docFilePath in docFilePaths)
    {
        messageCount++;

        byte[] docBytes;
        using (var fileStream = new FileStream(docFilePath, FileMode.Open, FileAccess.Read))
        {
            using var memoryStream = new MemoryStream();
            fileStream.CopyTo(memoryStream);
            docBytes = memoryStream.ToArray();
        }
        Console.WriteLine($"Sending messages: {messageCount}");

        var message = new ServiceBusMessage(docBytes);
        await sender.SendMessageAsync(message);
    }
}
catch (Exception exception)
{
    Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
}
finally
{
    await sender.DisposeAsync();
    await client.DisposeAsync();
    Console.WriteLine();
    Console.WriteLine("Messages are sent");

}


