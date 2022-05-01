using Notes.Models;
using Notes.Services;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Notes
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\noteDataList.json";
        private BindingList<NoteModel> _noteDataList;
        private FileIOService _fileIOService;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Save()
        {
            try
            {
                _fileIOService.SaveData(_noteDataList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOService = new FileIOService(PATH);

            try
            {
                _noteDataList = _fileIOService.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
            Save();
            lbNotesList.ItemsSource = _noteDataList;
        }

        private void LbNotesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbNotesList.SelectedItem != null)
            {
                dpNote.IsEnabled = true;
                tbNoteTitle.Text = (lbNotesList.SelectedItem as NoteModel).Title;
                tbNoteText.Text = (lbNotesList.SelectedItem as NoteModel).Text;
            }
            else
                dpNote.IsEnabled = false;
            Save();
        }

        private void BtnNewNote_Click(object sender, RoutedEventArgs e)
        {
            _noteDataList.Insert(0, new NoteModel() { Title = "Новая заметка" });
            lbNotesList.SelectedIndex = 0;
        }

        private void BtnSaveNote_Click(object sender, RoutedEventArgs e)
        {
            (lbNotesList.SelectedItem as NoteModel).Title = tbNoteTitle.Text;
            (lbNotesList.SelectedItem as NoteModel).Text = tbNoteText.Text;
            (lbNotesList.SelectedItem as NoteModel).LastChangedDate = DateTime.Now;
            _noteDataList.Insert(0, lbNotesList.SelectedItem as NoteModel);
            _noteDataList.RemoveAt(lbNotesList.SelectedIndex);
            lbNotesList.SelectedIndex = 0;
            Save();
        }

        private void BtnDeleteNote_Click(object sender, RoutedEventArgs e)
        {
            _noteDataList.Remove(lbNotesList.SelectedItem as NoteModel);
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbSearch.Text == "")
            {
                // Create an ImageBrush.
                ImageBrush textImageBrush = new ImageBrush();
                textImageBrush.ImageSource =
                    new BitmapImage(
                        new Uri(@"pack://application:,,,/Resources/search.PNG")
                    );
                textImageBrush.AlignmentX = AlignmentX.Left;
                textImageBrush.Stretch = Stretch.UniformToFill;
                // Use the brush to paint the button's background.
                tbSearch.Background = textImageBrush;
            }
            else
            {
                tbSearch.Background = Brushes.White;
            }
        }
    }
}
