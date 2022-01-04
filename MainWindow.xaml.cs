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
        VesigeEntitiesSalon ctx = new VestigeEntitiesSalon();
        CollectionViewSource clientVSource;
        CollectionViewSource hairstylistVSource;
        CollectionViewSource clientProgramariVSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            clientVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientViewSource")));
            clientVSource.Source = ctx.Clienti.Local;
            ctx.Clienti.Load();
            clientProgramariVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientProgramariViewSource")));
            //customerOrdersVSource.Source = ctx.Orders.Local;
            BindDataGrid();
            hairstylistVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hairstylistViewSource")));
            hairstylistVSource.Source = ctx.Hairstylisti.Local;
            ctx.Hairstylisti.Load();
            ctx.Programari.Load();
            ctx.Hairstilisti.Load();
            cmbClienti.ItemsSource = ctx.Clienti.Local;
            //cmbCustomers.DisplayMemberPath = "FirstName";
            cmbClienti.SelectedValuePath = "IdClient";
            cmbHairstylist.ItemsSource = ctx.Hairstylisti.Local;
            //cmbInventory.DisplayMemberPath = "Make";
            cmbHairstylist.SelectedValuePath = "IdHairstylist";

            System.Windows.Data.CollectionViewSource clientViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // customerViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource hairstylistViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hairstylistViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // inventoryViewSource.Source = [generic data source]
        }
        private void SaveClienti()
        {
            Client client = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Customer entity
                    client = new Client()
                    {
                        Nume = numeTextBox.Text.Trim(),
                        Prenume = prenumeTextBox.Text.Trim()
                        Telefon = telefonTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Clienti.Add(client);
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
                    client.Telefon = TelefonTextBox.Text.Trim();
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
                    ctx.CLienti.Remove(client);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                clientVSource.View.Refresh();
            }

        }
        private void SaveHairstylisti()
        {
            Hairstylist hairstylist = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem inventory entity
                    hairstylist = new Hairstylist()
                    {
                        NumeH = numeHTextBox.Text.Trim(),
                        PrenumeH = prenumeHTextBox.Text.Trim()
                        Experienta = experientaTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Hairstylisti.Add(hairstylist);
                    haistylistVSource.View.Refresh();
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
                    hairstylist.NumeH = numeHTextBox.Text.Trim();
                    hairstylist.PrenumeH = prenumeHTextBox.Text.Trim();
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
                    ctx.Hairstylisti.Remove(hairstylist);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                hairstylistVSource.View.Refresh();
            }

        }
        private void SaveProgramari()
        {
            Programare programare = null;
            if (action == ActionState.New)
            {
                try
                {

                    programare = new Programare()
                    {
                        Data = dataTextBox.Text.Trim(),
                        IdClient = idClientTextBox.Text.Trim()

                        IdHairstylist = idHairstylistTextBox.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Programari.Add(programare);
                    programareVSource.View.Refresh();
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
                    Programare = (Programare)programareDataGrid.SelectedItem;
                    programare.Data = dataTextBox.Text.Trim();


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
                    Programare = (Programare)programareDataGrid.SelectedItem;
                    ctx.Programari.Remove(programare);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                programareVSource.View.Refresh();
            }


        }
        private void BindDataGrid()
        {
            var queryProgramare = from pro in ctx.Programari
                             join cli in ctx.Clienti on pro.IdClient equals
                             cli.IdClient
                             join inv in ctx.Hairstylisti on pro.IdHairstylist
                 equals hair.IdHairstylist
                             select new
                             {
                                 pro.IdProgramare,
                                 pro.IdHairstylist,
                                 pro.IdClient,
                                 cli.Nume,
                                 cli.Prenume,
                                 cli.Telefon,
                                 hair.NumeH,
                                 hair.PrenumeH,
                                 hair.Experienta,
                                 
                             };
            clientProgramariVSource.Source = queryProgramare.ToList();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;

        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            customerVSource.View.MoveCurrentToPrevious();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            customerVSource.View.MoveCurrentToNext();
        }

        private void btnPrevious1_Click(object sender, RoutedEventArgs e)
        {
            inventoryVSource.View.MoveCurrentToPrevious();
        }

        private void btnNext1_Click(object sender, RoutedEventArgs e)
        {
            inventoryVSource.View.MoveCurrentToNext();
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
            TabItem ti = tabCtrlAutoLot.SelectedItem as TabItem;

            switch (ti.Header)
            {
                case "Customers":
                    SaveCustomers();
                    break;
                case "Inventory":
                    SaveInventories();
                    break;
                case "Orders":
                    break;
            }
            ReInitialize();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReInitialize();
        }
    }



}