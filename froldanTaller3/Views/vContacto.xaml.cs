using System.Globalization;

namespace froldanTaller3.Views;

public partial class vContacto : ContentPage
{
    public vContacto()
    {
        InitializeComponent();
        dpFecha.MaximumDate = DateTime.Today;
    }

    // Cambia las reglas de la caja según el tipo de identificación
    private void pkTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtIdentificacion.Text = "";
        string tipo = pkTipo.SelectedItem?.ToString() ?? "";

        if (tipo == "CI")
        {
            txtIdentificacion.MaxLength = 10;
            txtIdentificacion.Keyboard = Keyboard.Numeric;
            txtIdentificacion.Placeholder = "Cédula (10 números)";
        }
        else if (tipo == "RUC")
        {
            txtIdentificacion.MaxLength = 13;
            txtIdentificacion.Keyboard = Keyboard.Numeric;
            txtIdentificacion.Placeholder = "RUC (13 números)";
        }
        else
        {
            txtIdentificacion.MaxLength = 20;
            txtIdentificacion.Keyboard = Keyboard.Default;
            txtIdentificacion.Placeholder = "Pasaporte (libre)";
        }
    }

    // CI y RUC: solo números. Pasaporte: libre
    private void txtIdentificacion_TextChanged(object sender, TextChangedEventArgs e)
    {
        string tipo = pkTipo.SelectedItem?.ToString() ?? "";
        if (tipo == "Pasaporte")
            return;

        foreach (char c in e.NewTextValue ?? "")
        {
            if (!char.IsDigit(c))
            {
                txtIdentificacion.Text = e.OldTextValue;
                return;
            }
        }
    }

    // Nombres y apellidos: solo letras y espacios
    private void SoloLetras_TextChanged(object sender, TextChangedEventArgs e)
    {
        Entry caja = (Entry)sender;
        foreach (char c in e.NewTextValue ?? "")
        {
            if (!char.IsLetter(c) && c != ' ')
            {
                caja.Text = e.OldTextValue;
                return;
            }
        }
    }

    // Salario: solo números y un punto
    private void txtSalario_TextChanged(object sender, TextChangedEventArgs e)
    {
        int puntos = 0;
        foreach (char c in e.NewTextValue ?? "")
        {
            if (c == '.')
                puntos++;

            if ((!char.IsDigit(c) && c != '.') || puntos > 1)
            {
                txtSalario.Text = e.OldTextValue;
                return;
            }
        }
    }

    private async void btnVerContacto_Clicked(object sender, EventArgs e)
    {
        string tipo = pkTipo.SelectedItem?.ToString() ?? "";
        string id = txtIdentificacion.Text ?? "";
        string nombres = (txtNombres.Text ?? "").Trim();
        string apellidos = (txtApellidos.Text ?? "").Trim();
        string correo = (txtCorreo.Text ?? "").Trim();

        if (tipo == "")
        {
            await DisplayAlertAsync("Validación", "Seleccione el tipo de identificación.", "OK");
            return;
        }
        if (tipo == "CI" && id.Length != 10)
        {
            await DisplayAlertAsync("Validación", "La cédula debe tener 10 números.", "OK");
            return;
        }
        if (tipo == "RUC" && id.Length != 13)
        {
            await DisplayAlertAsync("Validación", "El RUC debe tener 13 números.", "OK");
            return;
        }
        if (tipo == "Pasaporte" && id == "")
        {
            await DisplayAlertAsync("Validación", "Ingrese el número de pasaporte.", "OK");
            return;
        }
        if (nombres == "" || apellidos == "")
        {
            await DisplayAlertAsync("Validación", "Ingrese nombres y apellidos.", "OK");
            return;
        }
        if (!correo.Contains("@") || !correo.Contains("."))
        {
            await DisplayAlertAsync("Validación", "Ingrese un correo válido.", "OK");
            return;
        }
        if (!double.TryParse(txtSalario.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double salario) || salario <= 0)
        {
            await DisplayAlertAsync("Validación", "Ingrese un salario válido mayor a 0.", "OK");
            return;
        }

        string fecha = dpFecha.Date?.ToString("dd/MM/yyyy") ?? "";

        // Navegación: abre la ventana 2 enviando los datos por el constructor
        await Navigation.PushAsync(new vResumen(tipo, id, nombres, apellidos, fecha, correo, salario));
    }
}