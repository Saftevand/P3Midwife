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
using GalaSoft.MvvmLight.Messaging;


namespace P3_Midwife.Views
{
    public partial class RecordWindow : BaseWindow
    {
        bool isNotClosed;
        int thisID;
        Patient CurrentPatient;
        Record CurrentRecord;
        Employee CurrentEmployee;
        public RecordWindow(Record RecordShow)
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }
            Messenger.Default.Register<Patient>(this, "PatientToRecordView", patientValidation);
            Messenger.Default.Register<Record>(this, "NewRecordToRecordView", recordValidation);
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);
            Messenger.Default.Register<Employee>(this, "EmployeetoRecordView", validateUser);

            isNotClosed = false;
            this.thisID = RecordShow.ThisRecordID;
        }

        private void show()
        {
            if (CurrentEmployee is SOSU)
            {
                NewChildBtn.Visibility = Visibility.Hidden;
            }
            Show();
        }

        private void validateUser(Employee emp)
        {
            CurrentEmployee = emp;
        }

        private void recordValidation(Record _currentRecord)
        {
            CurrentRecord = _currentRecord;
        }

        private void patientValidation(Patient _currentPatient)
        {
            CurrentPatient = _currentPatient;
        }

        private void NotificationMessageRecieved(NotificationMessage msg)
        {
            if (thisID == CurrentRecord.ThisRecordID)
            {
                if (msg.Notification == "ToRecord" && !isNotClosed && CurrentRecord.IsActive)
                {
                    if (CurrentRecord.ChildCPR != null)
                    {
                        NewChildBtn.Visibility = Visibility.Hidden;
                    }
                    show();
                }
                else if (msg.Notification == "RecordSave")
                {
                    BaseWindow.cancel = true;
                    this.isNotClosed = true;
                    this.Close();
                }
                else if (msg.Notification == "AccessDenied")
                {
                    MessageBox.Show("Adgang nægtet!");
                }
                else if (msg.Notification != "NewChildDialog")
                {
                    this.Hide();
                }
            }
            else if (msg.Notification != "NewChildDialog")
            {
                this.Hide();
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox send = sender as TextBox;
            if (send.Text == "0")
            {
                send.Clear();
            }
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                Patient child = item as Patient;
                if (child.Mother.RecordList.Find(x => x.IsActive == true) != null)
                {
                    Messenger.Default.Send(child.Mother.RecordList.Find(x => x.IsActive == true), "ChildRecordToNewChildView");
                    Messenger.Default.Send(child.Mother, "PatientToNewChildView");
                    Messenger.Default.Send<Employee>((Employee)ChildrenListBox.Tag, "EmployeetoNewChildView");
                    Messenger.Default.Send(child, "ChildToNewChildView");
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToNewChild"));
                }
              
            }
        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Note.Text = DateTime.Now.ToString() + " - " + NewNote.Text + " - Jdm: " + CurrentEmployee.Name + "\n";
            NewNote.Clear();
        }

        List<string> autoList = new List<string>();
        WordSuggesetionProvider provider = new WordSuggesetionProvider();

        private void NewNote_TextChanged(object sender, TextChangedEventArgs e)
        {
            autoList.Clear();

            if (NewNote.Text.EndsWith(" "))
            {
                NewNote.Text = TextEditor.WordReplacement(NewNote.Text.ToString());
                NewNote.SelectionStart = NewNote.Text.Length;
            }

            autoList = provider.GetSuggestions(NewNote.Text.ToLower());

            if (autoList.Count > 0)
            {
                txtSuggestions.ItemsSource = autoList;
                txtSuggestions.Visibility = Visibility.Visible;
            }
            else
            {
                txtSuggestions.ItemsSource = null;
                txtSuggestions.Visibility = Visibility.Collapsed;
            }
        }

        private void NewNote_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                txtSuggestions.SelectedIndex = 0;
                textAppend(sender as TextBox);
            }
            if (e.Key == Key.Down)
            {
                txtSuggestions.Focus();
            }
        }

        private void textAppend(TextBox sender)
        {
            string text;
            text = txtSuggestions.SelectedItem.ToString();
            if (!sender.Text.EndsWith(" "))
            {
                sender.Text = sender.Text.Remove(sender.Text.LastIndexOf(' ') + 1);
            }
            sender.AppendText(text + " ");
            sender.SelectionStart = sender.Text.Length;
        }

        private void txtSuggestions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox senderListBox = sender as ListBox;

            string text;
            if (txtSuggestions.ItemsSource != null)
            {
                txtSuggestions.Visibility = Visibility.Collapsed;
                if (txtSuggestions.SelectedIndex != -1)
                {
                    text = txtSuggestions.SelectedItem.ToString();
                    if (!NewNote.Text.EndsWith(" "))
                    {
                        NewNote.Text = NewNote.Text.Remove(NewNote.Text.LastIndexOf(' ') + 1);
                    }
                    NewNote.AppendText(text + " ");
                    NewNote.Focus();
                    NewNote.SelectionStart = NewNote.Text.Length;
                }
            }
        }

        private void txtSuggestions_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox senderBox = sender as TextBox;
            if (e.Key == System.Windows.Input.Key.Tab)
            {
                e.Handled = true;
                textAppend(senderBox);
                senderBox.Focus();
            }
        }
    }
}
