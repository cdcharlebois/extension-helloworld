using System.ComponentModel.Composition;
using Eto.Forms;
using Mendix.StudioPro.ExtensionsAPI.UI.Menu;
using Mendix.StudioPro.ExtensionsAPI.Services;
using Mendix.StudioPro.ExtensionsAPI.UI.Services;

namespace MyCompany.MyProject.MendixExtension;

[Export(typeof(MenuBarExtension))]
class MyMenuExtension : MenuBarExtension
{
    readonly IUserAuthenticationService authService;
    readonly IAppService appService;
    readonly IFindResultsPaneService findResultsPaneService;

    public override IEnumerable<MenuViewModelBase> GetMenus()
    {
        yield return new MenuItemViewModel("My extension", placeUnder: new[] { "app" }, placeAfter: "tools") { Action = () => MessageBox.Show("Hello world!") };
    }
}