using RunTest;
using Serilog;
using TestB;

public class Program
{
    public static void Main(string[] args)
    {
        var logger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger();

        IFormulatrixRepository crudInMemory = new FormulatrixSqlServerRepository("Server=localhost;Database=test;MultipleActiveResultSets=true;User ID=sa;Password=123456;Pooling=true;Max Pool Size=1024; TrustServerCertificate=True;");
        crudInMemory.Initialize();
        var mydata = new MyData
        {
            Age = 30,
            Name = "Mohammad Almi Kurniawan",
            Hobbies = new List<string> { "Swimming", "Traveling", "Coding" }
        };
        crudInMemory.Register<MyData>("mydata", mydata, 1);
        logger.Information("Retrieve {@a}", crudInMemory.Retrieve("mydata"));

        //crudInMemory.Register<MyData>("mydata", mydata, 2);
        //logger.Warning("Retrieve {@a}", crudInMemory.Retrieve("mydata"));

        //try
        //{
        //    crudInMemory.Register<MyData>("mydata", mydata, 2);
        //}
        //catch (Exception ex)
        //{
        //    logger.Error("Error: {Message}", ex.Message);
        //}

        var myWifeData = new MyWifeData
        {
            Name = "Mutiaran Dewi Pebriyani",
            FathersName = "Wargito",
            MothersName = "Muryani"
        };
        crudInMemory.Register<MyWifeData>("mywifedata", myWifeData, 1);
        logger.Information("Retrieve {@a}", crudInMemory.Retrieve("mywifedata"));

        crudInMemory.Deregister("mywifedata");
        try
        {
            logger.Information("After Deregistered {@a}", crudInMemory.Retrieve("mywifedata"));
        }catch(Exception ex)
        {
            logger.Error("Error: {Message}", ex.Message);
        }
    }
}