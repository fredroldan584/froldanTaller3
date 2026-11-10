namespace froldanTaller3.Views;

public partial class vResumen : ContentPage
{
    string contenido = "";

    public vResumen(string tipo, string id, string nombres, string apellidos,
                    string fecha, string correo, double salario)
    {
        InitializeComponent();

        double aporte = salario * 0.0945;

        lblTipo.Text = tipo;
        lblId.Text = id;
        lblNombres.Text = nombres;
        lblApellidos.Text = apellidos;
        lblFecha.Text = fecha;
        lblCorreo.Text = correo;
        lblSalario.Text = "$ " + salario.ToString("0.00");
        lblAporte.Text = "$ " + aporte.ToString("0.00");

        // Texto que se guardará en el archivo
        contenido = "========== CONTACTO ==========\n" +
                    "Registrado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "\n" +
                    "Tipo: " + tipo + "\n" +
                    "Identificación: " + id + "\n" +
                    "Nombres: " + nombres + "\n" +
                    "Apellidos: " + apellidos + "\n" +
                    "Fecha: " + fecha + "\n" +
                    "Correo: " + correo + "\n" +
                    "Salario: " + salario.ToString("0.00") + "\n" +
                    "Aporte IESS: " + aporte.ToString("0.00") + "\n\n";
    }

    private async void btnExportar_Clicked(object sender, EventArgs e)
    {
        string nombreArchivo = "froldanTaller3_contactos.txt";
        string ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), nombreArchivo);

        try
        {
            File.AppendAllText(ruta, contenido);
            await DisplayAlertAsync("Archivo exportado",
                "Nombre: " + nombreArchivo + "\nRuta: " + ruta, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", "No se pudo exportar: " + ex.Message, "OK");
        }
    }

    private async void btnRegresar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}