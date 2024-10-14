using NedoAkinatorLibrary1145.Repository;
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
using System.Windows.Shapes;

namespace NedoAkinatorView
{
    /// <summary>
    /// Логика взаимодействия для WindowGetAnswer.xaml
    /// </summary>
    public partial class WindowGetAnswer : Window
    {
        private readonly Game game;

        public string Answer { get; set; }
        public WindowGetAnswer(Game game)
        {
            InitializeComponent();
            DataContext = this;
            this.game = game;
        }

        private void Save(object sender, RoutedEventArgs e)
        {

            var character = new NedoAkinatorLibrary1145.DB.Character
            {
                Title = Answer
            };
           

            game.Save(character);
            
            

        }
    }
}
