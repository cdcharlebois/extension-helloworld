namespace Mendix.Extensions.HelloWorldMac;
using Mendix.StudioPro.ExtensionsAPI.UI.DockablePane;
using System.ComponentModel.Composition;
using Eto.Forms;


[Export(typeof(DockablePaneExtension))]
class Class2 : DockablePaneExtension
{

    //public DockablePanel() { }

    public override DockablePaneViewModel Open()
    {
        return new DockablePaneViewModel { Title = "My Dockable Pane", Controls = { new StackLayoutItem(new Panel(), true) } };
    }

    public const string ID = "my-dockable-pane-1";
    public override string Id => ID;
}
