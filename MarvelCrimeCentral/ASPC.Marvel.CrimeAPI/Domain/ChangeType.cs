using System;


[Flags]
public enum ChangeType
{
    Create = 1,
    Update = 2,
    Delete = 4
}