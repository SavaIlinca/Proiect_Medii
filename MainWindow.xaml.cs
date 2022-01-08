using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using VestigeSalon;
using System.Data;


namespace Proiect_Medii
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        VestigeEntitiesSalon ctx = new VestigeEntitiesSalon();
        CollectionViewSource clientVSource;
        CollectionViewSource hairstylistVSource;
        CollectionViewSource clientProgramaresVSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            clientVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientViewSource")));
            clientVSource.Source = ctx.Clients.Local;
            ctx.Clients.Load();
            clientProgramaresVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientProgramaresViewSource")));
            //clientProgramaresVSource.Source = ctx.Programares.Local;
            BindDataGrid();
            hairstylistVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hairstylistViewSource")));
            hairstylistVSource.Source = ctx.Hairstylists.Local;
            ctx.Hairstylists.Load();
            ctx.Programares.Load();
            ctx.Hairstylists.Load();
            cmbClients.ItemsSource = ctx.Clients.Local;
            //cmbClients.DisplayMemberPath = "Nume";
            cmbClients.SelectedValuePath = "IdClient";
            cmbHairstylist.ItemsSource = ctx.Hairstylists.Local;
            //cmbHairstylist.DisplayMemberPath = "Experienta";
            cmbHairstylist.SelectedValuePath = "IdHairstylist";

            System.Windows.Data.CollectionViewSource clientViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // clientViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource hairstylistViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hairstylistViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // hairstylistViewSource.Source = [generic data source]
        }
        private void SaveClients()
        {
            Client client = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Client entity
                    client = new Client()
                    {
                        Nume = NumeTextBox.Text.Trim(),
                        Prenume = PrenumeTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Clients.Add(client);
                    clientVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Edit)
            {
                try
                {
                    client = (Client)clientDataGrid.SelectedItem;
                    client.Nume = NumeTextBox.Text.Trim();
                    client.Prenume = PrenumeTextBox.Text.Trim();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    client = (Client)clientDataGrid.SelectedItem;
                    ctx.Clients.Remove(client);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                clientVSource.View.Refresh();
            }

        }
        private void SaveHairstylists()
        {
            Hairstylist hairstylist = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem hairstylist entity
                    hairstylist = new Hairstylist()
                    {
                        NumeH = NumeHTextBox.Text.Trim(),
                        Experienta = ExperientaTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Hairstylists.Add(hairstylist);
                    hairstylistVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    hairstylist = (Hairstylist)hairstylistDataGrid.SelectedItem;
                    hairstylist.NumeH = NumeHTextBox.Text.Trim();
                    hairstylist.Experienta = ExperientaTextBox.Text.Trim();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    hairstylist = (Hairstylist)hairstylistDataGrid.SelectedItem;
                    ctx.Hairstylists.Remove(hairstylist);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                hairstylistVSource.View.Refresh();
            }

        }
        private void SaveProgramares()
        {
            Programare programare = null;
            if (action == ActionState.New)
            {
                try
                {
                    Client client = (Client)cmbClients.SelectedItem;
                    Hairstylist hairstylist = (Hairstylist)cmbHairstylist.SelectedItem;
                    //instantiem Programare entity
                    programare = new Programare()
                    {
                        IdClient = client.IdClient,
                        IdHairstylist = hairstylist.IdHairstylist
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Programares.Add(programare);
                    //salvam modificarile
                    ctx.SaveChanges();
                    BindDataGrid();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Edit)
            {
                dynamic selectedProgramare = programaresDataGrid.SelectedItem;
                try
                {
                    int curr_id = selectedProgramare.IdProgramare;
                    var editedProgramare = ctx.Programares.FirstOrDefault(s => s.IdProgramare == curr_id);
                    if (editedProgramare != null)
                    {
                        editedProgramare.IdClient = Int32.Parse(cmbClients.SelectedValue.ToString());
                        editedProgramare.IdHairstylist = Convert.ToInt32(cmbHairstylist.SelectedValue.ToString());
                        //salvam modificarile
                        ctx.SaveChanges();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                BindDataGrid();
                // pozitionarea pe item-ul curent
                clientVSource.View.MoveCurrentTo(selectedProgramare);
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    Programare = (Programare)programareDataGrid.SelectedItem;
                    ctx.Programari.Remove(programare);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void BindDataGrid()
        {
            var queryProgramare = from ord in ctx.Programares
                                  join cust in ctx.Clients on ord.IdClient equals
                                  cust.IdClient
                                  join inv in ctx.Hairstylists on ord.IdHairstylist
                    equals inv.IdHairstylist
                                  select new
                                  {
                                      ord.IdProgramare,
                                      ord.IdHairstylist,
                                      ord.IdClient,
                                      cust.Nume,
                                      cust.Prenume,
                                      inv.Experienta,
                                      inv.NumeH
                                  };
            clientProgramaresVSource.Source = queryProgramare.ToList();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            ReInitialize();
            BindingOperations.ClearBinding(NumeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(PrenumeTextBox, TextBox.TextProperty);
            SetValidationBinding();

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            BindingOperations.ClearBinding(NumeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(PrenumeTextBox, TextBox.TextProperty);
            SetValidationBinding();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;

        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            clientVSource.View.MoveCurrentToPrevious();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            clientVSource.View.MoveCurrentToNext();
        }

        private void btnPrevious1_Click(object sender, RoutedEventArgs e)
        {
            hairstylistVSource.View.MoveCurrentToPrevious();
        }

        private void btnNext1_Click(object sender, RoutedEventArgs e)
        {
            hairstylistVSource.View.MoveCurrentToNext();
        }
        private void gbOperations_Click(object sender, RoutedEventArgs e)
        {
            Button SelectedButton = (Button)e.OriginalSource;
            Panel panel = (Panel)SelectedButton.Parent;

            foreach (Button B in panel.Children.OfType<Button>())
            {
                if (B != SelectedButton)
                    B.IsEnabled = false;
            }
            gbActions.IsEnabled = true;
        }
        private void ReInitialize()
        {

            Panel panel = gbOperations.Content as Panel;
            foreach (Button B in panel.Children.OfType<Button>())
            {
                B.IsEnabled = true;
            }
            gbActions.IsEnabled = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = tabCtrlSalon.SelectedItem as TabItem;

            switch (ti.Header)
            {
                case "Clients":
                    SaveClients();
                    break;
                case "Hairstylist":
                    SaveHairstylists();
                    break;
                case "Programares":
                    break;
            }


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReInitialize();
        }
    }


    }


}