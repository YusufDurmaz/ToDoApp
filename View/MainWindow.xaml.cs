using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using ToDoApp.Model;
using ToDoApp.ViewModel;
using Newtonsoft.Json;
using System.Windows.Controls;

namespace ToDoApp
{
    public partial class MainWindow : Window
    {
        private string _filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "TodoApp",
            "tasks.json"
        );

        public ObservableCollection<TaskItemViewModel> TaskItems { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            TaskItems = new ObservableCollection<TaskItemViewModel>();
            LoadTasks();
            // ObservableCollection, UI'deki değişiklikleri otomatik olarak yansıtır

            // DataContext'i ayarlama (binding)
            DataContext = this;
        }

        private void LoadTasks()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                var taskItems = JsonConvert.DeserializeObject<ObservableCollection<TaskItem>>(json);
                if (taskItems != null)
                {
                    TaskItems = new ObservableCollection<TaskItemViewModel>(
                        taskItems.Select(taskItem => new TaskItemViewModel(taskItem))
                    );
                    taskList.ItemsSource = TaskItems;
                }
            }
        }

        private void SaveTasks()
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (TaskItems != null && TaskItems.Any())
            {
                var json = JsonConvert.SerializeObject(TaskItems);
                File.WriteAllText(_filePath, json);
            }
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTask.Text))
            {

                var newTask = new TaskItem
                {
                    Id = TaskItems.Any() ? TaskItems.Last().Id + 1 : 1, // Id'yi her yeni görevde arttırıyoruz
                    Title = txtTask.Text,
                    IsCompleted = false
                };

                TaskItems.Add(new TaskItemViewModel(newTask));
                txtTask.Clear();
            }
        }

        // Uygulama kapanırken kaydet
        protected override void OnClosed(EventArgs e)
        {
            SaveTasks();
            base.OnClosed(e);
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is TaskItemViewModel selectedTask)
            {
                TaskItems.Remove(selectedTask);
            }
        }
    }
}
