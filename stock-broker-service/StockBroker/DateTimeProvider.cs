using System;

namespace StockBroker;

public interface DateTimeProvider
{
    DateTime GetDateTime();
}