using NedoAkinatorLibrary1145.DB;
using NedoAkinatorLibrary1145.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NedoAkinatorView
{
    /// <summary>
    /// Логика взаимодействия для AskQuestion.xaml
    /// </summary>
    public partial class AskQuestion : UserControl, INotifyPropertyChanged
    {
        private QuestionRecord question;
        private readonly Game game;

        public QuestionRecord Question
        {
            get => question;
            set
            {
                question = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Question)));
            }
        }

        public AskQuestion(QuestionRecord questionRecord, Game game)
        {
            InitializeComponent();
            Question = questionRecord;
            this.game = game;
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void ClickAnswer(object sender, RoutedEventArgs e)
        {
            var reaction = int.Parse(((Button)sender).Tag.ToString());
            game.RememberReaction(question, reaction);
        }
    }
}
