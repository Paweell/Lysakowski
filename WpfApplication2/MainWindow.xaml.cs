using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using WpfApplication2;
using WpfApplication2.klasy;

namespace WPFApp
{

    public partial class MainWindow : Window
    {
        private Brush _brush;  // definicja do sprawdzania numeru 
       

        public ObservableCollection<Osoba> ListaOsob { get; set; }

        public MainWindow()
        {

            InitializeComponent();
            this.DataContext = this;
            ListaOsob = new ObservableCollection<Osoba>();

            this.StatusComboBox.ItemsSource = Enum.GetValues(typeof(KimJest));
            this.StatusComboBox.SelectedIndex = 0;


            this.PlecComboBox.ItemsSource = Enum.GetValues(typeof(PlecMF));
            this.PlecComboBox.SelectedIndex = 0;

            this.KlasaComboBox.ItemsSource = Enum.GetValues(typeof(Klasy));
            // this.KlasaComboBox.SelectedIndex = 0;

            this.WyksztalcenieComboBox.ItemsSource = Enum.GetValues(typeof(Wyksztalcenie));

            this.FunkcjaComboBox.ItemsSource = Enum.GetValues(typeof(Funkcja));

            ListView.ItemsSource = ListaOsob;   // do wyszukiwania danych
            this.NarodowoscComboBox.Text="Polska";

            ListView.ItemsSource = ListaOsob;
            CollectionView vieww = (CollectionView)CollectionViewSource.GetDefaultView(ListView.ItemsSource);  // do wyszukiwania danych
            vieww.Filter = OsobaFilter;  // do wyszukiwania danych
           
               
                    

              
          
            
        }

        private void btnPreviousTab_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = Arkusze.SelectedIndex - 1;
            if (newIndex < 0)
                newIndex = Arkusze.Items.Count - 1;
            Arkusze.SelectedIndex = newIndex;
        }

        private void btnNextTab_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = Arkusze.SelectedIndex + 1;
            if (newIndex >= Arkusze.Items.Count)
                newIndex = 0;
            Arkusze.SelectedIndex = newIndex;
        }

        
        private void Edytuj1Button_Click(object sender, RoutedEventArgs e)
        {

            Arkusze.SelectedIndex = 1;

        }


        private void DodajButton_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                        KimJest kimJest = (KimJest)Enum.Parse(typeof(KimJest), this.StatusComboBox.Text);
                        string imie = this.ImieTextBox.Text;
                        string nazwisko = this.NazwiskoTextBox.Text;
                        PlecMF plec = (PlecMF)Enum.Parse(typeof(PlecMF), this.PlecComboBox.Text);  
                        string narodowosc = this.NarodowoscComboBox.Text;
                        int nrTelefonu = int.Parse(this.NumerKontaktowyTextBox.Text);
                if (!(String.IsNullOrWhiteSpace(NazwiskoTextBox.Text) && String.IsNullOrWhiteSpace(ImieTextBox.Text)))
                {
                    if (!( KlasaComboBox.Text == "") && StatusComboBox.Text == "Student")
                    // if (ImieTextBox.Text != "" && NazwiskoTextBox.Text !="" )
                    {
                        Klasy klasa = (Klasy)Enum.Parse(typeof(Klasy), this.KlasaComboBox.Text);
                        Osoba uczen = new Uczen(kimJest, imie, nazwisko, plec, narodowosc, nrTelefonu, klasa);
                        ListaOsob.Add(uczen);
                        ImieTextBox.Clear();
                        NazwiskoTextBox.Clear();
                        NumerKontaktowyTextBox.Clear();

                    }


                    else if (! (WyksztalcenieComboBox.Text == "" && FunkcjaComboBox.Text == "") && StatusComboBox.Text == "Pracownik"  )
                    { 
                        Wyksztalcenie wyksztalcenie = (Wyksztalcenie)Enum.Parse(typeof(Wyksztalcenie), this.WyksztalcenieComboBox.Text);
                        Funkcja funkcja = (Funkcja)Enum.Parse(typeof(Funkcja), this.FunkcjaComboBox.Text);
                        Osoba pracownik = new Pracownik(kimJest, imie, nazwisko, plec, narodowosc, nrTelefonu, wyksztalcenie, funkcja);
                        ListaOsob.Add(pracownik);

                        ImieTextBox.Clear();
                        NazwiskoTextBox.Clear();
                        NumerKontaktowyTextBox.Clear();
                    }
                }


             
                
            }
                catch
                    {
             
                         MessageBox.Show("Uzupełnij wszystkie pola");
               
                    }
            //ListView.ItemsSource = items;
            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListView.ItemsSource);//.....................................
           
        }
        private void ZapiszButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dig = new Microsoft.Win32.SaveFileDialog();
            dig.FileName = "ListaOsob"; // dsomyslna nazwa
            dig.DefaultExt = ".xml";
            dig.Filter = "XML documents (.xml)|*.xml"; //"jaka nazwa | jakie rozszerzenie"

            Nullable<bool> result = dig.ShowDialog(); // uruchamiamy okno dialogowe
            if (result == true)
            {
                string filePath = dig.FileName; // tworze plik o nazwie FileName
                                                // metoda która bedzie nam to robiła  ...
                ListToXmlFile(filePath);
            }

        }

        private void ListToXmlFile(string filePath)  //...
        {
            using (var sw = new StreamWriter(filePath))  // StreamWriter - em piszemy do pliku   on powienien stworzyć nam plik 
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<Osoba>));
                serializer.Serialize(sw, ListaOsob);   //nasz serializer uzywa streamwritera (sw) czyli  pisze do pliku zawartoś Listy Osób
            }
        }

        private void OtworzButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dig = new Microsoft.Win32.OpenFileDialog();

            dig.DefaultExt = ".xml";
            dig.Filter = "XML documents (.xml)|*.xml";  // okno z " jaka nazwa | jakie rozszerzenie "

            Nullable<bool> result = dig.ShowDialog();  // w ten sposób odpalamy okno dialogowe 
            string filename = "";
            if (result == true)
            {
                filename = dig.FileName;
            }
            if (File.Exists(filename))
            {
                GetXmlFileToList(filename);
            }
            else
            {
                MessageBox.Show(@"Such file doesn't exist");  //@ daje mozliwość uzycia aposrtofu 
            }

        }
        private void GetXmlFileToList(string filename)
        {

            using (var sr = new StreamReader(filename))
            {
                var deserializer = new XmlSerializer(typeof(ObservableCollection<Osoba>));

                // musimy stworzyć przykładową 
                ObservableCollection<Osoba> tmpList = (ObservableCollection<Osoba>)deserializer.Deserialize(sr);
                // chcemy z deserializera wrzucic bezpośrednio z pliku  na tą listę 
                foreach (var item in tmpList)
                {
                    ListaOsob.Add(item);
                }
            }
        }

        private void UsunButton_Click(object sender, RoutedEventArgs e)
        {
            //aby nam sie nie wysypał program musimy uzyc try catch 
            try
            {
                // z czego chcemy usunąć dane?  
                // nie z  pola , tylko z tego co zostało podpięte do kontrolki 
                this.ListaOsob.RemoveAt(this.ListView1.SelectedIndex);
            }
            catch (Exception ex)  // jesli Ci sie nie uda to wykonaj to 
            {
                // MessageBoxResult result = 
                MessageBox.Show("First you have to select character you want to delete. ", "Usuwanie osoby");


                // teraz przechimy do stworzenia serializacji 
            }
        }

        private void NumerKontaktowy_PrewiewTextInput(object sender, TextCompositionEventArgs e)
        {

            short val;
            if (!Int16.TryParse(e.Text, out val))
            {
                //wprowadzamy znak nie jest liczbą 
                NumerKontaktowyValidationStatus.Text = "Musisz wprowadzić liczbę ";
                NumerKontaktowyTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                //oznaczenie zdarzenia jako obsłużone
                e.Handled = true;

            }
            else
            {
                NumerKontaktowyValidationStatus.Text = "";
                NumerKontaktowyTextBox.BorderBrush = _brush;
            }
        }






        private void Imie_PrewiewTextInput(object sender, TextCompositionEventArgs r)
        {

            short litery;
            if (Int16.TryParse(r.Text, out litery))
            {
                //wprowadzamy znak nie jest liczbą 
                ImieValidationStatus.Text = "Musisz wprowadzić litery ";
                ImieTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                //oznaczenie zdarzenia jako obsłużone
                r.Handled = true;

            }
            else
            {
                ImieValidationStatus.Text = "";
                ImieTextBox.BorderBrush = _brush;
            }
        }

        private void Nazwisko_PrewiewTextInput(object sender, TextCompositionEventArgs r)
        {

            short litery;
            if (Int16.TryParse(r.Text, out litery))
            {
                //wprowadzamy znak nie jest liczbą 
                NazwiskoValidationStatus.Text = "Musisz wprowadzić litery ";
                NazwiskoTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                //oznaczenie zdarzenia jako obsłużone
                r.Handled = true;

            }
            else
            {
                NazwiskoValidationStatus.Text = "";
                NazwiskoTextBox.BorderBrush = _brush;
            }
        }
        

      private bool OsobaFilter(object os)   // do wyszukiwania danych
        {
            if(String.IsNullOrEmpty(WyszukajTextBox.Text))
            {
                return true;
            }
         
            {
                return ((os as Osoba).Narodowosc.IndexOf(WyszukajTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (os as Osoba).Imie.IndexOf(WyszukajTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (os as Osoba).Nazwisko.IndexOf(WyszukajTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                //        (os as Osoba).Plec.(WyszukajTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;

               
            }

          
        }
        

        private void WyszukajTextBox_TextChanged(object sender, TextChangedEventArgs e) 
        {
            CollectionViewSource.GetDefaultView(ListView.ItemsSource).Refresh();

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            

            if (StatusComboBox.Text== "Student")
            {
                WyksztalcenieComboBox.IsEnabled = true;
                FunkcjaComboBox.IsEnabled = true;
                KlasaComboBox.IsEnabled = false;
               
            }
            else if(StatusComboBox.Text == "Pracownik")
            {
                WyksztalcenieComboBox.IsEnabled = false;
                FunkcjaComboBox.IsEnabled = false;
                KlasaComboBox.IsEnabled = true;
            }
           
        }

       
    }
}