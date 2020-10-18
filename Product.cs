using System;
// ReSharper disable UnusedParameter.Local

namespace RecordFactoryDemo
{
    public record Product (string ProductName, string Sku, IUser User, DateTime Created);
}
