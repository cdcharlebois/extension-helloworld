using System;
using System.IO;
using System.Text;
using Mendix.StudioPro.ExtensionsAPI.UI.WebView;

namespace WebViewBrowserExtension;

static class WebViewExtensions
{
    public static void Navigate(this IWebView webView, string address)
    {
        webView.Address = new Uri(address);
    }

    public static void NavigateToString(this IWebView webView, string htmlContents)
    {
        var resourceId = Guid.NewGuid().ToString();
        var resourceUri = $"http://{resourceId}";

        webView.AddWebResourceRequestedFilter($"*{resourceId}*", WebViewWebResourceContext.All);
        webView.WebResourceRequested += (sender, args) =>
        {
            if (!args.Request.Uri.Contains(resourceId)) return;

            var responseStream = new MemoryStream(Encoding.UTF8.GetBytes(htmlContents));
            args.Response = webView.CreateWebResourceResponse(responseStream, 200, "OK", "Content-Type: text/html");
        };

        webView.Address = new Uri(resourceUri);
    }
}
