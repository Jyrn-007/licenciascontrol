using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using LicenciaSistemasAdmin.Models;
using LicenciaSistemasAdmin.Services;
using System.Windows;

namespace LicenciaSistemasAdmin
{
    public partial class MainWindow : Window
    {
        private readonly LicenciaApiService _api = new();
        private Licencia _seleccion;

        public MainWindow()
        {
            InitializeComponent();
            Cargar();
        }

        private async void Cargar()
        {
            dgLicencias.ItemsSource = await _api.ObtenerTodasAsync();
        }

        private async void Agregar_Click(object sender, RoutedEventArgs e)
        {
            await _api.CrearAsync(LeerFormulario());
            Cargar();
        }

        private async void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            if (_seleccion == null) return;
            await _api.ActualizarAsync(_seleccion.Id, LeerFormulario());
            Cargar();
        }

        private async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (_seleccion == null) return;
            await _api.EliminarAsync(_seleccion.Id);
            Cargar();
        }

        private void dgLicencias_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _seleccion = dgLicencias.SelectedItem as Licencia;
            if (_seleccion == null) return;

            txtEmpresa.Text = _seleccion.NombreEmpresa;
            txtCuotaPagar.Text = _seleccion.CuotaPagar.ToString();
            txtCuotaPagada.Text = _seleccion.CuotaPagada.ToString();
            txtCodigo.Text = _seleccion.NumeroHabilitacion;
            cbEstado.SelectedIndex = _seleccion.Habilitado == 1 ? 0 : 1;
        }

        private Licencia LeerFormulario()
        {
            return new Licencia
            {
                NombreEmpresa = txtEmpresa.Text,
                CuotaPagar = decimal.Parse(txtCuotaPagar.Text),
                CuotaPagada = decimal.Parse(txtCuotaPagada.Text),
                NumeroHabilitacion = txtCodigo.Text,
                Habilitado = int.Parse(
                    ((System.Windows.Controls.ComboBoxItem)cbEstado.SelectedItem).Tag.ToString())
            };
        }
    }
}
