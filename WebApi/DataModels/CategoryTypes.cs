using System;

namespace WebApi.DataModels
{
    [Flags]
    public enum CategoryTypes
    {
        None = 0b0000,
        Bar = 0b0001,
        Restaurant = 0b0010,
        Club = 0b0100
    }
}