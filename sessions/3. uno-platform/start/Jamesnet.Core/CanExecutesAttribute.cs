namespace Jamesnet.Core;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class CanExecuteAttribute : Attribute
{
    public string CommandName { get; }

    public CanExecuteAttribute(string commandName)
    {
        CommandName = commandName;
    }
}

