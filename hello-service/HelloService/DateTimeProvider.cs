using System;

namespace Hello;

public interface DateTimeProvider
{
    public TimeOnly Get();
}