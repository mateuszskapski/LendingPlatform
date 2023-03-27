using LendingPlatform;

ConsoleKeyInfo key = new ConsoleKeyInfo();

    var serviceBus = new MockServiceBusClient(); 
    var submitter = new LoanApplicationSubmitter(serviceBus);
    var processor = new LoanApplicationProcessor();

    var storage = new LoanApplicationStorageClient();
    var observer = new LoanApplicationObserver(storage);

    var loanService = new LoanProcessingService(serviceBus, processor);
    loanService.Subscribe(observer);

    // Process Application should run either as an Azure Function, or at least as a background operation i.e. a Hosted Service. 
    Task.Run(async () => await loanService.ProcessApplicationsAsync(default));

while (key.Key != ConsoleKey.C)
{
    Console.Write("Loan amount: ");
    var amount = decimal.Parse(Console.ReadLine());

    Console.Write("Asset value: ");
    var assetValue = decimal.Parse(Console.ReadLine());

    Console.Write("Credit score: ");
    var creditScore = int.Parse(Console.ReadLine());

    await submitter.SubmitApplicationAsync(amount, assetValue, creditScore);

    var stats = storage.GetLoanStatistics();
    foreach(var stat in stats)
    {
        Console.WriteLine($"Application result: {stat.Key}");
        Console.WriteLine($"Applications count: {stat.Value}");
    }

    Console.WriteLine("Press any key to continue, or 'c' to close application");
    key = Console.ReadKey();
}
