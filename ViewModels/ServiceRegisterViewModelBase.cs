using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using MultiClouding.Interfaces;

namespace MultiClouding.ViewModels;

public abstract class ServiceRegisterViewModelBase:ViewModelBase
{
    public virtual object WindowType { get; set; } = typeof(Window);
    public abstract ICloudService Service { get; set; }
    public virtual string ServiceName { get; set; } = "Cloud Service";
}