using System;
using System.Collections.Generic;
using System.Data;
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

namespace plgTopsis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class plgTopsisWindow : Window
    {
        int Columns, Rows;
        DataTable dtStep3 = new DataTable();
        List<decimal> inputSign = new List<decimal>();
        List<double> inputWeight = new List<double>();
        public string foldername = "";

        public plgTopsisWindow()
        {
            InitializeComponent();
        }

        private void CBBRowCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Step 1 
            pnlRowDetails.Children.Clear();
            //Step 2
            ComboBoxItem typeItem = (ComboBoxItem)CBBRowCount.SelectedItem;
            string value = typeItem.Content.ToString();
            Rows = Convert.ToInt32(value);
            for (int i = 1; i <= Rows; i++)
            {
                RowDetail generated = new RowDetail();
                generated.Row = i;
                generated.Name = "row_" + i;
                generated.Margin = new Thickness(20);
                generated.HorizontalAlignment = HorizontalAlignment.Stretch;
                pnlRowDetails.Children.Add(generated);
            }
        }

        private void btnNextStep1_Click(object sender, RoutedEventArgs e)
        {
            //Disable Other Tabs
            if (TabControlMain.Items.Count - 1 == TabControlMain.SelectedIndex)
                return; // No more tabs to show!

            var currentTab = TabControlMain.SelectedItem as TabItem;
            currentTab.IsEnabled = false;
            var nextTab = TabControlMain.Items[TabControlMain.SelectedIndex + 1] as TabItem;
            nextTab.IsEnabled = true;
            TabControlMain.SelectedItem = nextTab;
            //tabControl.SelectedIndex = tabControl.SelectedIndex + 1;
        }

        private void btnNextStep2_Click(object sender, RoutedEventArgs e)
        {
            dtStep3.Columns.Add("*");
            foreach (ColumnDetail item in pnlColDetails.Children)
            {
                //dtStep3.Columns.Add(item.txtBoxColName.Text);
                dtStep3.Columns.Add(item.txtName.Text);
                inputWeight.Add(Convert.ToDouble(item.txtWeight.Text));
                inputSign.Add(item.NumValue);
            }

            foreach (RowDetail item in pnlRowDetails.Children)
            {
                DataRow newRow = dtStep3.NewRow();
                newRow[0] = item.txtName.Text;

                dtStep3.Rows.Add(newRow);

            }
            int aa = dtgStep3.Columns.Count;

            dtgStep3.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dtStep3 });
            for (int i = 0; i < dtgStep3.Items.Count; i++)
            {
                //dtgStep3.Items[i].Cells[1].ReadOnly = true;
               //dtgStep3.Rows[i].Cells[1].Style.BackColor = Color.Gray;
            }
            dtgStep3.Items.Refresh();

            //Disable Other Tabs
            if (TabControlMain.Items.Count - 1 == TabControlMain.SelectedIndex)
                return; // No more tabs to show!

            var currentTab = TabControlMain.SelectedItem as TabItem;
            currentTab.IsEnabled = false;
            var nextTab = TabControlMain.Items[TabControlMain.SelectedIndex + 1] as TabItem;
            nextTab.IsEnabled = true;
            TabControlMain.SelectedItem = nextTab;
            //tabControl.SelectedIndex = tabControl.SelectedIndex + 1;
        }

        private void CBBColCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Step 1 
            pnlColDetails.Children.Clear();
            //Step 2
            ComboBoxItem typeItem = (ComboBoxItem)CBBColCount.SelectedItem;
            string value = typeItem.Content.ToString();
            Columns = Convert.ToInt32(value);
            for (int i = 1; i <= Columns; i++)
            {
                ColumnDetail generated = new ColumnDetail();
                generated.Col = i;
                generated.Name = "column_" + i;
                generated.Margin = new Thickness(20);
                generated.HorizontalAlignment = HorizontalAlignment.Stretch;
                pnlColDetails.Children.Add(generated);
            }
        }
    }
}
