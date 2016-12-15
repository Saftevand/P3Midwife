using GalaSoft.MvvmLight.Messaging;
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

namespace P3_Midwife.Views
{
    public partial class NewChildWindow : BaseWindow
    {
        private Employee _currentEmployee;
        private Record _currentRecord;
        private int thisID;
        bool isNotClosed = true;
        WordSuggesetionProvider provider;
        public NewChildWindow(Record ActiveRecord)
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);
            Messenger.Default.Register<Employee>(this, "EmployeetoNewChildView", validateUser);
            Messenger.Default.Register<Record>(this, "ChildRecordToNewChildView", recordValidation);
            txtAuto.TextChanged += new TextChangedEventHandler(txtAuto_TextChanged);
            thisID = ActiveRecord.ThisRecordID;
            ApgarOneMinTextBox.TextChanged += new TextChangedEventHandler(txtAuto_TextChanged);
            ApgarFiveMinTextBox.TextChanged += new TextChangedEventHandler(txtAuto_TextChanged);
            ApgarTenMinTextBox.TextChanged += new TextChangedEventHandler(txtAuto_TextChanged);

            provider = new WordSuggesetionProvider();
            isNotClosed = false;
        }

        private void recordValidation(Record CurrentRecord)
        {
            _currentRecord = CurrentRecord;
        }
        private void show()
        {
         // if (_currentEmployee is SOSU)
         // {
         //     SaveAndCompleteBtn.Visibility = Visibility.Hidden;
         // }
          Show();
        }

        private void validateUser(Employee emp)
        {
            _currentEmployee = emp;
        }

        private void txtAuto_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox senderBox = sender as TextBox;
            ListBox currentList = senderBox.Tag as ListBox;

            List<string> autoList = new List<string>();
            autoList.Clear();

            if (senderBox.Text.EndsWith(" "))
            {
                senderBox.Text = TextEditor.WordReplacement(senderBox.Text.ToString());
                senderBox.SelectionStart = senderBox.Text.Length;
            }
            autoList = provider.GetSuggestions(senderBox.Text.ToLower());

            if (autoList.Count > 0)
            {
                currentList.ItemsSource = null;
                currentList.ItemsSource = autoList;
                currentList.Visibility = Visibility.Visible;
            }
            else
            {
                currentList.ItemsSource = null;
                currentList.Visibility = Visibility.Collapsed;
            }
        }

        private void NotificationMessageRecieved(NotificationMessage msg)
        {
            if (thisID == _currentRecord.ThisRecordID)
            {
                if (msg.Notification == "ToNewChild" && !isNotClosed)
                {
                    show();
                }
                else if (msg.Notification == "ChildSave")
                {
                    BaseWindow.cancel = true;
                    isNotClosed = true;
                    Close();
                }
                else
                    Hide();
            }
            else
            {
                Hide();
            }
        }

        private void txtAuto_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox senderBox = sender as TextBox;
            ListBox senderList = senderBox.Tag as ListBox;
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                senderList.SelectedIndex = 0;
                text_Append(senderBox);
            }
            if (e.Key == Key.Down)
            {
                senderList.Focus();
            }
        }

        private void text_Append(TextBox sender)
        {
            ListBox senderList = sender.Tag as ListBox;
            string text;
            text = senderList.SelectedItem.ToString();
            if (!sender.Text.EndsWith(" "))
            {
                sender.Text = sender.Text.Remove(sender.Text.LastIndexOf(' ') + 1);
            }
            sender.AppendText(text + " ");
            sender.SelectionStart = sender.Text.Length;
        }

        private void lbSuggestions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            ListBox senderListBox = sender as ListBox;
            TextBox current = senderListBox.Tag as TextBox;

            string text;
            if (senderListBox.ItemsSource != null)
            {
                senderListBox.Visibility = Visibility.Collapsed;
                if (senderListBox.SelectedIndex != -1)
                {
                    text = senderListBox.SelectedItem.ToString();
                    if (!current.Text.EndsWith(" "))
                    {
                        current.Text = current.Text.Remove(current.Text.LastIndexOf(' ') + 1);
                    }
                    current.AppendText(text + " ");
                    current.Focus();
                    current.SelectionStart = current.Text.Length;
                }
            }
        }

        private void lbSuggestions_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ListBox senderBox = sender as ListBox;
            if (e.Key == System.Windows.Input.Key.Tab)
            {
                e.Handled = true;
                text_Append(senderBox.Tag as TextBox);
                senderBox.Focus();
            }
        }
    }
}
