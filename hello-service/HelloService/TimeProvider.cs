using System;

namespace Hello;

public interface TimeProvider
{
    public TimeOnly Get();
}