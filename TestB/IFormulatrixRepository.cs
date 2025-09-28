namespace TestB
{
    public interface IFormulatrixRepository
    {
        void Register<N>(string itemName, N itemContent, int itemType);
        string Retrieve(string itemName);
        int GetType(string itemName);
        void Deregister(string itemName);
        void Initialize();
    }
}
