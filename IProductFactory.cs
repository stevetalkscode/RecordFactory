namespace RecordFactoryDemo
{
    public interface IProductFactory
    {
        Product Create(string productName);
    }
}