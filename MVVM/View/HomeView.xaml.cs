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

        private async void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(TitleTexbox.Text) || string.IsNullOrEmpty(DescriptionTextbox.Text) || string.IsNullOrEmpty(FinishDate_Calendar.SelectedDate.ToString()) )
            {
                await Task.Run(() =>
                {
                    // Use Dispatcher to marshal the call back to the UI thread
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show("Please Fill Title and Description. Also, choose an appropriate finish date.", "Warning");  // This is a non-blocking message box!
                    });
                });
                return;
            }



            int row_height = 25;
            // Create a new StackPanel for the row
            StackPanel rowPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal, // Arrange controls horizontally
                Margin = new Thickness(0, 5, 0, 5)  // Add some margin between rows
            };


            CheckBox Is_Task_Done_Checkbox = new CheckBox
            {
                Style = (Style)FindResource("CustomCheckBoxStyle"),
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                Tag = rownumber // Use the same row number for identification
            };

            TextBox textBox0 = new TextBox { Text = FinishDate_Calendar.SelectedDate.Value.ToString("MM/dd/yyyy"), Height = row_height, Width = 75, Margin = new Thickness(5), Tag = rownumber };
            TextBox textBox1 = new TextBox { Text = TitleTexbox.Text, Height = row_height, Width = 175, Margin = new Thickness(5), Tag = rownumber };
            TextBox textBox2 = new TextBox { Text = DescriptionTextbox.Text, Height = row_height, Width = 370, Margin = new Thickness(5), Tag = rownumber };
            textBox1.IsEnabled = false;
            textBox2.IsEnabled = false;

            rowPanel.Children.Add(Is_Task_Done_Checkbox);
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

        // Method to create a custom CheckBox style programmatically
        private Style CreateBeautifulCheckBoxStyle()
        {
            // Create a new Style for the CheckBox
            Style style = new Style(typeof(CheckBox));

            // Set default properties
            style.Setters.Add(new Setter(CheckBox.ForegroundProperty, Brushes.Black));
            style.Setters.Add(new Setter(CheckBox.FontSizeProperty, 14.0));
            style.Setters.Add(new Setter(CheckBox.VerticalAlignmentProperty, VerticalAlignment.Center));
            style.Setters.Add(new Setter(CheckBox.HorizontalAlignmentProperty, HorizontalAlignment.Left));

            // Define the ControlTemplate
            ControlTemplate template = new ControlTemplate(typeof(CheckBox));
            FrameworkElementFactory gridFactory = new FrameworkElementFactory(typeof(Grid));
            FrameworkElementFactory borderFactory = new FrameworkElementFactory(typeof(Border));
            FrameworkElementFactory checkMarkFactory = new FrameworkElementFactory(typeof(Path));
            FrameworkElementFactory contentPresenterFactory = new FrameworkElementFactory(typeof(ContentPresenter));

            // Configure the Border
            borderFactory.SetValue(Border.WidthProperty, 20.0);
            borderFactory.SetValue(Border.HeightProperty, 20.0);
            borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(4));
            borderFactory.SetValue(Border.BackgroundProperty, Brushes.White);
            borderFactory.SetValue(Border.BorderBrushProperty, Brushes.LightGray);
            borderFactory.SetValue(Border.BorderThicknessProperty, new Thickness(1));
            borderFactory.SetValue(Border.SnapsToDevicePixelsProperty, true);
            borderFactory.SetValue(FrameworkElement.NameProperty, "CheckBoxBorder"); // Name the Border

            // Configure the CheckMark (Path)
            checkMarkFactory.SetValue(Path.WidthProperty, 12.0);
            checkMarkFactory.SetValue(Path.HeightProperty, 12.0);
            checkMarkFactory.SetValue(Path.StretchProperty, Stretch.Fill);
            checkMarkFactory.SetValue(Path.DataProperty, Geometry.Parse("M 0 6 L 4 10 L 12 2"));
            checkMarkFactory.SetValue(Path.StrokeProperty, Brushes.White);
            checkMarkFactory.SetValue(Path.StrokeThicknessProperty, 2.0);
            checkMarkFactory.SetValue(Path.FillProperty, Brushes.White);
            checkMarkFactory.SetValue(Path.VisibilityProperty, Visibility.Collapsed);
            checkMarkFactory.SetValue(FrameworkElement.NameProperty, "CheckMark"); // Name the Path

            // Configure the ContentPresenter
            contentPresenterFactory.SetValue(ContentPresenter.MarginProperty, new Thickness(5, 0, 0, 0));
            contentPresenterFactory.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
            contentPresenterFactory.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Left);

            // Add elements to the Grid
            gridFactory.AppendChild(borderFactory);
            gridFactory.AppendChild(checkMarkFactory);
            gridFactory.AppendChild(contentPresenterFactory);

            // Set the Grid as the root of the ControlTemplate
            template.VisualTree = gridFactory;

            // Define Triggers
            Trigger checkedTrigger = new Trigger
            {
                Property = CheckBox.IsCheckedProperty,
                Value = true
            };
            checkedTrigger.Setters.Add(new Setter(Border.BackgroundProperty, Brushes.DodgerBlue, "CheckBoxBorder"));
            checkedTrigger.Setters.Add(new Setter(Border.BorderBrushProperty, Brushes.DodgerBlue, "CheckBoxBorder"));
            checkedTrigger.Setters.Add(new Setter(Path.VisibilityProperty, Visibility.Visible, "CheckMark"));

            Trigger mouseOverTrigger = new Trigger
            {
                Property = CheckBox.IsMouseOverProperty,
                Value = true
            };
            mouseOverTrigger.Setters.Add(new Setter(Border.BorderBrushProperty, Brushes.DodgerBlue, "CheckBoxBorder"));

            Trigger disabledTrigger = new Trigger
            {
                Property = CheckBox.IsEnabledProperty,
                Value = false
            };
            disabledTrigger.Setters.Add(new Setter(CheckBox.ForegroundProperty, Brushes.LightGray));
            disabledTrigger.Setters.Add(new Setter(Border.BackgroundProperty, Brushes.LightGray, "CheckBoxBorder"));
            disabledTrigger.Setters.Add(new Setter(Border.BorderBrushProperty, Brushes.LightGray, "CheckBoxBorder"));

            // Add Triggers to the ControlTemplate
            template.Triggers.Add(checkedTrigger);
            template.Triggers.Add(mouseOverTrigger);
            template.Triggers.Add(disabledTrigger);

            // Set the ControlTemplate to the Style
            style.Setters.Add(new Setter(CheckBox.TemplateProperty, template));

            return style;
        }
    }
}
