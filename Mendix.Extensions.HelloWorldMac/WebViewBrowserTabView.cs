using System.ComponentModel.Composition;
using Eto.Forms;
using Mendix.StudioPro.ExtensionsAPI.UI.Services;
using Mendix.StudioPro.ExtensionsAPI.UI.Tab;
using Mendix.StudioPro.ExtensionsAPI.UI.WebView;

namespace WebViewBrowserExtension;

[Export(typeof(WebViewBrowserTabView))]
public class WebViewBrowserTabView
{
    const string WEBVIEW_HTML_MESSAGING_SAMPLE = @"<html>
<head>
    <script> 
        function init()
        {
            window.chrome.webview.addEventListener('message', msgHandler);
            sendMessage(""pageLoad"");
        }

        function msgHandler(event)
        {
            console.log('message sent to JS: ' + event.data);
        }

        function sendMessage(data)
        {
            data = data || ""click"";
            chrome.webview.postMessage({data});
        }
    </script>
</head>
<body onload=""init()"">
    <h1>Hello World</h1>
    <button onclick= ""sendMessage()"" > Send To Studio Pro</button>
    </body>
    </html>";

    static readonly Form Owner = Application.Instance.MainForm;

    TestWebViewTabViewModel? dockablePaneViewModel;
    readonly IDockingWindowService dockingWindowService;

    [ImportingConstructor]
    public WebViewBrowserTabView(IDockingWindowService dockingWindowService)
    {
        this.dockingWindowService = dockingWindowService;
    }

    public void RunNavigationTest()
    {
        // Need to ensure that the tab is open and the dockablePaneViewModel is instantiated
        dockablePaneViewModel = new TestWebViewTabViewModel("WebView Tab View Browser");
        dockingWindowService.OpenTab(dockablePaneViewModel);
        dockablePaneViewModel!.NavigateToString(WEBVIEW_HTML_MESSAGING_SAMPLE);
    }

    public void OpenTabView(string address)
    {
        dockablePaneViewModel = new TestWebViewTabViewModel("WebView Tab View Browser");
        dockingWindowService.OpenTab(dockablePaneViewModel);
    }

    class TestWebViewTabViewModel : WebViewTabViewModel
    {
        IWebView? webView;

        public TestWebViewTabViewModel(string title)
        {
            Title = title;
        }

        public override void InitWebView(IWebView webView)
        {
            this.webView = webView;
            this.webView.WebMessageReceived += Browser_WebMessageReceived;
        }

        void ShowMessage(string text)
        {
            MessageBox.Show(Owner, text, "Information");
        }

        void Browser_WebMessageReceived(object? sender, WebMessageReceivedEventArgs e)
        {
            //if (e.WebMessageAsJson.Contains("PageLoaded"))
            //{
                ShowMessage(e.WebMessageAsJson);
            //}
        }

        internal void NavigateToString(string htmlContent) => webView!.NavigateToString(htmlContent);
    }
}
