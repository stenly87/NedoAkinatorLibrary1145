using NedoAkinatorLibrary1145.DB;
using NedoAkinatorLibrary1145.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace NedoAkinatorView
{
    /// <summary>
    /// Логика взаимодействия для WinQuestionList.xaml
    /// </summary>
    public partial class WinQuestionList : Window,
        INotifyPropertyChanged
    {
        private Question selected;
        private string textQuestion;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int SelectedIndex { get; set; }

        public ObservableCollection<Question> Questions { get; set; }
        public Question Selected
        {
            get => selected;
            set
            {
                selected = value;
                TextQuestion = value?.Text;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Title)));
            }
        }

        public string TextQuestion { 
            get => textQuestion;
            set
            {
                textQuestion = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(TextQuestion)));
            }
        }

        public WinQuestionList()
        {
            InitializeComponent();
            var rep = new QuestionRepository();
            Questions = new ObservableCollection<Question>(rep.GetList());
            DataContext = this;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            Selected = new Question { Id = Selected.Id, Text = TextQuestion };
            var rep = new QuestionRepository();
            if (Selected.Id == 0)
                rep.Create(Selected);
            else
                rep.Update(Selected);
            rep.Save();
            if (Questions.Count > SelectedIndex && Questions.Count > 0)
                Questions[SelectedIndex] = Selected;
            else
                Questions.Add(Selected);
        }

        private void NewQuestion(object sender, RoutedEventArgs e)
        {
            Selected = new Question();
            TextQuestion = "";
        }
    }
}
