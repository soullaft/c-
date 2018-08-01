using System.Windows.Controls;

namespace Vesna.Pages
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : UserControl
    {
        public History()
        {
            InitializeComponent();

            this.DataContext = new HistoryService();
        }
    }
}
