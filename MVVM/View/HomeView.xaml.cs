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

namespace MiniProject.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private int rownumber = 0; // Counter to track row numbers

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            int row_height = 25;
            // Create a new StackPanel for the row
            StackPanel rowPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal, // Arrange controls horizontally
                Margin = new Thickness(0, 5, 0, 5)  // Add some margin between rows
            };

            TextBox textBox0 = new TextBox { Text = FinishDate_Calendar.SelectedDate.Value.ToString("MM/dd/yyyy"), Height = row_height, Width = 75, Margin = new Thickness(5), Tag = rownumber };
            TextBox textBox1 = new TextBox { Text = TitleTexbox.Text, Height = row_height, Width = 175, Margin = new Thickness(5), Tag = rownumber };
            TextBox textBox2 = new TextBox { Text = DescriptionTextbox.Text, Height = row_height, Width = 370, Margin = new Thickness(5), Tag = rownumber };
            textBox1.IsEnabled = false;
            textBox2.IsEnabled = false;
            rowPanel.Children.Add(textBox0);
            rowPanel.Children.Add(textBox1);
            rowPanel.Children.Add(textBox2);

            // Add 3 Buttons
            Button button1 = new Button { Content = "Delete", Margin = new Thickness(5), Tag = rownumber };
            Button button2 = new Button { Content = "Edit", Padding = new Thickness(5), Margin = new Thickness(5), Tag = rownumber};
            //Button button3 = new Button { Content = "Button 3", Margin = new Thickness(5), Tag = rownumber};
            rowPanel.Children.Add(button1);
            rowPanel.Children.Add(button2);
            //rowPanel.Children.Add(button3);
            button1.Click += DeleteRowButton_Click; 
            button2.Click += Edit_RowButton_Click;

            // Add the row to the DynamicWrapPanel
            DynamicWrapPanel.Children.Add(rowPanel);
        }




        // Event handler for deleting the row
        private void DeleteRowButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                // Retrieve the row number from the button's Tag
                int rowNumber = (int)button.Tag;

                // Find the row panel to delete
                StackPanel rowPanel = DynamicWrapPanel.Children
                    .OfType<StackPanel>()
                    .FirstOrDefault(p => p.Children.OfType<Button>().Any(b => b.Tag is int && (int)b.Tag == rowNumber));

                if (rowPanel != null)
                {
                    // Remove the row from the DynamicWrapPanel
                    DynamicWrapPanel.Children.Remove(rowPanel);
                }
            }
        }
        private void Edit_RowButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int rowNumber = (int)button.Tag;

                // Find the row panel using the row number
                StackPanel rowPanel = DynamicWrapPanel.Children
                    .OfType<StackPanel>()
                    .ElementAtOrDefault(rowNumber);

                if (rowPanel != null)
                {
                    // Find the header and description TextBoxes in the row
                    TextBox headerTextBox = rowPanel.Children.OfType<TextBox>().FirstOrDefault();
                    TextBox descriptionTextBox = rowPanel.Children.OfType<TextBox>().ElementAtOrDefault(1);

                    if (headerTextBox != null && descriptionTextBox != null)
                    {
                        // Check if the button is in "Edit" mode or "Save" mode
                        if (button.Content.ToString() == "Edit")
                        {
                            // Enable the TextBoxes for editing
                            headerTextBox.IsEnabled = true;
                            descriptionTextBox.IsEnabled = true;

                            // Set focus on the header TextBox
                            headerTextBox.Focus();

                            // Change the button text to "Save"
                            button.Content = "Save";
                        }
                        else
                        {
                            // Disable the TextBoxes after saving
                            headerTextBox.IsEnabled = false;
                            descriptionTextBox.IsEnabled = false;

                            // Change the button text back to "Edit"
                            button.Content = "Edit";

                        }
                    }
                }
            }
        }

        private void TitleTextBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           


        }

        private void FinishDate_Calendar_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void TitleTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool ShouldCalendarBeVisible = !string.IsNullOrEmpty(TitleTexbox.Text) && !string.IsNullOrEmpty(DescriptionTextbox.Text);
            if (ShouldCalendarBeVisible)
                FinishDate_Calendar.Visibility = Visibility.Visible;
            else
                FinishDate_Calendar.Visibility = Visibility.Collapsed;
        }

        private void DescriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool ShouldCalendarBeVisible = !string.IsNullOrEmpty(TitleTexbox.Text) && !string.IsNullOrEmpty(DescriptionTextbox.Text);
            if (ShouldCalendarBeVisible)
                FinishDate_Calendar.Visibility = Visibility.Visible;
            else
                FinishDate_Calendar.Visibility = Visibility.Collapsed;

        }
    }
}
