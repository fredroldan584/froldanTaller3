namespace froldanTaller3;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        // NavigationPage permite ir de una ventana a otra (PushAsync / PopAsync)
        return new Window(new NavigationPage(new Views.vContacto()));
    }
}