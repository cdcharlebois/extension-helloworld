namespace Mendix.Extensions.HelloWorldMac;
using System.ComponentModel.Composition;
using Eto.Forms;
using Mendix.StudioPro.ExtensionsAPI.UI.Menu;
using Mendix.StudioPro.ExtensionsAPI.Services;
using Mendix.StudioPro.ExtensionsAPI.UI.Services;
using Mendix.StudioPro.ExtensionsAPI.UI.DockablePane;
using WebViewBrowserExtension;

[Export(typeof(MenuBarExtension))]
class Class1 : MenuBarExtension
{
    readonly IUserAuthenticationService authService;
    private readonly IDockingWindowService dockingWindowService;
    private readonly MyWebViewController myWebViewController;
    private readonly WebViewBrowserTabView webViewBrowserTabView;


    [ImportingConstructor]
    public Class1(IUserAuthenticationService authService, IDockingWindowService dockingWindowService, WebViewBrowserTabView webViewBrowserTabView, MyWebViewController myWebViewController)
    {
        this.authService = authService;
        this.dockingWindowService = dockingWindowService;
        this.webViewBrowserTabView = webViewBrowserTabView;
        this.myWebViewController = myWebViewController;
    }

    public override IEnumerable<MenuViewModelBase> GetMenus()
    {
        yield return new MenuItemViewModel("My extension", placeUnder: new[] { "app" }, placeAfter: "tools") { Action = () => MessageBox.Show("Hello world!") };
        yield return new MenuItemViewModel("Say my name", placeUnder: new[] { "app" }, placeAfter: "My extension")
        {
            Action = () => MessageBox.Show(authService.TryRetrieveUserName(out var userName) ? $"Hello {userName}" : "Hello there","Hi",MessageBoxType.Information)
        };
        yield return new MenuItemViewModel("My Dockable Pane", placeUnder: new[] { "app" }, placeAfter: "tools")
        {
            Action = () => dockingWindowService.OpenPane(Class2.ID)
        };
        yield return new MenuItemViewModel("My WebView Tab", placeUnder: new[] { "app" }, placeAfter: "tools")
        {
            Action = () => myWebViewController.OpenWebViewTab("https://www.mendix.com")
        };
        yield return new MenuItemViewModel("My WebView Modal", placeUnder: new[] { "app" }, placeAfter: "tools")
        {
            Action = () => webViewBrowserTabView.RunNavigationTest()
        };
    }
}