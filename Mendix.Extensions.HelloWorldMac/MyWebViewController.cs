using System;
using Eto.Forms;
using Mendix.StudioPro.ExtensionsAPI.UI.Services;
using Mendix.StudioPro.ExtensionsAPI.UI.Tab;
using System.ComponentModel.Composition;

namespace Mendix.Extensions.HelloWorldMac;

[Export(typeof(MyWebViewController))]
public class MyWebViewController
{
    readonly IDockingWindowService dockingWindowService;
    TabViewModel? tabViewModel;
    WebView browser;
    //private String fileLocation;

    [ImportingConstructor]
    public MyWebViewController(IDockingWindowService dockingWindowService)
    {
        this.dockingWindowService = dockingWindowService;
        //this.tabViewModel = tabViewModel;
        
    }

    public void OpenWebViewTab(string address)
    {
        Environment.SetEnvironmentVariable("WEBVIEW2_USER_DATA_FOLDER", Path.GetTempPath());
        if (tabViewModel == null)
        {
            var browser = new WebView()
            {
                Url = new Uri(address)
            };

           
            tabViewModel = new TabViewModel { Title = "My WebView Tab", Controls = { new StackLayoutItem(browser, true) } };
            tabViewModel.OnClosed = HandleOnClosed;
        }

        dockingWindowService.OpenTab(tabViewModel);
    }

    void HandleOnClosed()
    {
        if (!browser!.IsDisposed)
            browser.Dispose();

        tabViewModel = null;
    }
}
