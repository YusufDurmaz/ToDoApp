using System.ComponentModel;
using ToDoApp.Model;

namespace ToDoApp.ViewModel
{
    public class TaskItemViewModel : INotifyPropertyChanged
    {
        private int id;
        private string title;
        private bool isCompleted;

        public int Id
        {
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public string Title
        {
            get => title;
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public bool IsCompleted
        {
            get => isCompleted;
            set
            {
                if (isCompleted != value)
                {
                    isCompleted = value;
                    OnPropertyChanged(nameof(IsCompleted));
                }
            }
        }

        public TaskItemViewModel(TaskItem taskItem)
        {
            Id = taskItem.Id;
            Title = taskItem.Title;
            IsCompleted = taskItem.IsCompleted;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
