using System.Threading.Tasks;
using MultiClouding.Interfaces;
using MultiClouding.Views;

namespace MultiClouding.ViewModels;

public class MegaServiceRegister: ServiceRegisterViewModelBase
{
    public override object WindowType { get; set; } = typeof(LoginMegaAccount);
    
    public override ICloudService Service { get; set; }
}