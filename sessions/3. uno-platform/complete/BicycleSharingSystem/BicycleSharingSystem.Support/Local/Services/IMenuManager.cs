
namespace BicycleSharingSystem.Support.Local.Services;

public interface IMenuManager
{
    event Action<string> MenuChanged;
    void ChangeMenu(string menu);
    void NavigateTo(string menu);
}
