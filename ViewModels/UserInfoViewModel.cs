using System.IO;
using ReactiveUI;

namespace MultiClouding.ViewModels;

public class UserInfoViewModel:ViewModelBase
{
    private string _username;
    public string Username
    {
        get=>_username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }

    private string _email;

    public string Email
    {
        get => _email;
        set => this.RaiseAndSetIfChanged(ref _email, value);
    }

    private Stream _profilePictureStream;

    public Stream ProfilePictureStream
    {
        get => _profilePictureStream;
        set => this.RaiseAndSetIfChanged(ref _profilePictureStream, value);
    }
    
    public UserInfoViewModel()
    {
        
    }
}