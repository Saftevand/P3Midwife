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
        bool isNotClosed = true;
        WordSuggesetionProvider provider;
        public NewChildWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);
            txtAuto.TextChanged += new TextChangedEventHandler(txtAuto_TextChanged);
            provider = new WordSuggesetionProvider();
            isNotClosed = false;
        }

        private void txtAuto_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<string> autoList = new List<string>();
            autoList.Clear();
            autoList = provider.GetSuggestions(txtAuto.Text.ToLower());

            if (autoList.Count > 0)
            {
                lbSuggestions.ItemsSource = null;
                lbSuggestions.ItemsSource = autoList;
                lbSuggestions.Visibility = Visibility.Visible;
            }
            else
            {
                lbSuggestions.ItemsSource = null;
                lbSuggestions.Visibility = Visibility.Collapsed;
            }
        }

        private void NotificationMessageRecieved(NotificationMessage msg)
        {
            if (msg.Notification == "ToNewChild" && !isNotClosed)
                Show();
            else if (msg.Notification == "ChildSave")
            {
                BaseWindow.cancel = true;
                isNotClosed = true;
                new NewChildWindow();
                Close();
            }
            else
                Hide();
        }

        private void txtAuto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Tab)
            {
                e.Handled = true;
                lbSuggestions.SelectedIndex = 0;
                text_Append();
            }
            if (e.Key == System.Windows.Input.Key.Down)
            {
                lbSuggestions.Focus();
            }
        }

        private void text_Append()
        {
            string text;
            text = lbSuggestions.SelectedItem.ToString();
            if (!txtAuto.Text.EndsWith(" "))
            {
                txtAuto.Text = txtAuto.Text.Remove(txtAuto.Text.LastIndexOf(' ') + 1);
            }
            txtAuto.AppendText(text + " ");
            txtAuto.SelectionStart = txtAuto.Text.Length;
        }

        private void lbSuggestions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string text;
            if (lbSuggestions.ItemsSource != null)
            {
                lbSuggestions.Visibility = Visibility.Collapsed;
                if (lbSuggestions.SelectedIndex != -1)
                {
                    text = lbSuggestions.SelectedItem.ToString();
                    if (!txtAuto.Text.EndsWith(" "))
                    {
                        txtAuto.Text = txtAuto.Text.Remove(txtAuto.Text.LastIndexOf(' ') + 1);
                    }
                    txtAuto.AppendText(text + " ");
                    txtAuto.Focus();
                    txtAuto.SelectionStart = txtAuto.Text.Length;
                }
            }
        }

        private void lbSuggestions_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Tab)
            {
                e.Handled = true;
                text_Append();
                txtAuto.Focus();
            }
        }
    }
}
