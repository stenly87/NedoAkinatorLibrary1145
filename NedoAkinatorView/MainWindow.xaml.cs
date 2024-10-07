using NedoAkinatorLibrary1145.Model;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenCharacters(object sender, RoutedEventArgs e)
        {
            new WinCharacterList().Show();
        }

        private void OpenQuestions(object sender, RoutedEventArgs e)
        {
            new WinQuestionList().Show();   
        }

        Game game;
        private void Start(object sender, RoutedEventArgs e)
        {
            if (game != null)
                game.Dispose();
            game = new Game();
            game.ChangeQuestion += Game_ChangeQuestion;
            game.Start();
        }

        private void Game_ChangeQuestion(object? sender, EventArgs e)
        {
            gridHost.Children.Clear();
            gridHost.Children.Add(new AskQuestion(game.GetNextQuestion(), game));
        }
    }
}