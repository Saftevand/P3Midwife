using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;


namespace P3_Midwife.Views
{
    public partial class RecordWindow : Window
    {
        private bool _isNotClosed;
        private int _thisID;
        private Patient _currentPatient;
        private Record _currentRecord;
        private Employee _currentEmployee;
        public Record CurrentRecord { get { return _currentRecord; } }
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

            _isNotClosed = false;
            _thisID = RecordShow.ThisRecordID;
            Closing += Filemanagement.ClosingHandler;
        }

        private void show()
        {
            if (_currentEmployee is SOSU)
            {
                NewChildBtn.Visibility = Visibility.Hidden;
            }
            this.Show();
        }

        private void validateUser(Employee emp)
        {
            _currentEmployee = emp;
        }

        private void recordValidation(Record currentRecord)
        {
            _currentRecord = currentRecord;
        }

        private void patientValidation(Patient currentPatient)
        {
            _currentPatient = currentPatient;
        }

        private void NotificationMessageRecieved(NotificationMessage msg)
        {

                if (msg.Notification == "ToRecord" && !_isNotClosed && _currentRecord.IsActive)
                {
                    if (this._thisID == _currentRecord.ThisRecordID)
                    {
                        if (_currentRecord.ChildCPR != null)
                        {
                            NewChildBtn.Visibility = Visibility.Hidden;
                        }
                        show();
                    }     
                    
                }
                else if (msg.Notification == "RecordSave")
                {
                    //BaseWindow.cancel = true;
                    this._isNotClosed = true;
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


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender!=null)
            {
                ComboBox tempComboBox = sender as ComboBox;
                string input = tempComboBox.SelectedItem.ToString();
              //  string input = temp.ToString();
                if (input == "Afvigende" || input == "Patologisk")
                {
                    BirthComplicationsCheckBox.IsChecked = true;
                }
            }
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                Patient child = item as Patient;
                if (child.Mother.RecordList.Find(x => x.ChildCPR == child.CPR) != null)
                {
                    Messenger.Default.Send(child.Mother.RecordList.Find(x => x.ChildCPR == child.CPR), "ChildRecordToNewChildView");
                    Messenger.Default.Send(child.Mother, "PatientToNewChildView");
                    Messenger.Default.Send<Employee>((Employee)ChildrenListBox.Tag, "EmployeetoNewChildView");
                    Messenger.Default.Send(child, "ChildToNewChildView");
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToNewChild"));
                }            
            }
        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Note.Text = DateTime.Now.ToString() + " - " + NewNote.Text + " - Jdm: " + _currentEmployee.Name + "\n";
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
