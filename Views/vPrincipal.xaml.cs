using adiazS5A.Models;

namespace adiazS5A.Views;

public partial class vPrincipal : ContentPage
{
	public vPrincipal()
	{
		InitializeComponent();
	}

    private void btnInsertar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";
        App.personRepo.AddNewPerson(txtName.Text);
        statusMessage.Text = App.personRepo.StatusMessage;

    }
    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";

        if (int.TryParse(txtIdActualizar.Text, out int personId))
        {
            App.personRepo.UpdatePerson(personId, txtNuevoNombre.Text);
            statusMessage.Text = App.personRepo.StatusMessage;

            // Actualizar la lista despu�s de la actualizaci�n
            btnMostrar_Clicked(sender, e);
        }
        else
        {
            statusMessage.Text = "Ingrese un ID v�lido para la actualizaci�n.";
        }
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";

        if (int.TryParse(txtIdEliminar.Text, out int personId))
        {
            App.personRepo.DeletePerson(personId);
            statusMessage.Text = App.personRepo.StatusMessage;

            // Actualizar la lista despu�s de la eliminaci�n
            btnMostrar_Clicked(sender, e);
        }
        else
        {
            statusMessage.Text = "Ingrese un ID v�lido para la eliminaci�n.";
        }
    }

    private void btnMostrar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";
        List<Persona> people = App.personRepo.GetAllPerole();
        ListaPersona.ItemsSource = people;
    }
}