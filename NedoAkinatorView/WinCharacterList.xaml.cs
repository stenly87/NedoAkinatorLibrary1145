using NedoAkinatorLibrary1145.Model;
using NedoAkinatorLibrary1145.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace NedoAkinatorView
{
    /// <summary>
    /// Логика взаимодействия для WinCharacterList.xaml
    /// </summary>
    public partial class WinCharacterList : Window,
        INotifyPropertyChanged
    {
        private CharacterRecord selected;
        private byte[] image;

        public int SelectedIndex { get; set; }

        public ObservableCollection<CharacterRecord> Characters { get; set; }
        public CharacterRecord Selected { 
            get => selected;
            set
            {
                selected = value;
                TitleCharacter = value?.Title;
                Image = value?.Image;
                PropertyChanged?.Invoke(this, 
                    new PropertyChangedEventArgs(nameof(Title)));
                }
        }

        public string TitleCharacter { get; set; }
        public byte[] Image
        {
            get => image;
            set
            { 
                image = value;
                PropertyChanged?.Invoke(this,
                   new PropertyChangedEventArgs(nameof(Image)));
            }
        }

        public WinCharacterList()
        {
            InitializeComponent();
            var rep = new CharacterRepository();
            Characters = new ObservableCollection<CharacterRecord>(rep.GetList());
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OpenImage(object sender, RoutedEventArgs e)
        {
            if (Selected == null)
                return;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Картинко|*.jpg";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Image = File.ReadAllBytes(dialog.FileName);
                
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            Selected = new CharacterRecord(Selected.Id, TitleCharacter, Image);
            var rep = new CharacterRepository();
            if (Selected.Id == 0)
                rep.Create(Selected);
            else
                rep.Update(Selected);
            rep.Save();
            Characters[SelectedIndex] = Selected;
            
        }

        private void NewCharacter(object sender, RoutedEventArgs e)
        {
            Selected = new CharacterRecord(0, "", null);
        }
    }
}
