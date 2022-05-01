using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    class NoteModel: INotifyPropertyChanged
    {
        private string _title;
        private string _text;
        private DateTime _lastChangedDate = DateTime.Now;

        public DateTime LastChangedDate
        {
            get { return _lastChangedDate; }
            set {
                if (_lastChangedDate == value)
                    return;
                _lastChangedDate = value;
                OnPropertyChanged("LastChangedDate");
            }
        }

        public string Title
        {
            get { return _title; }
            set {
                if (_title == value)
                    return;
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public string Text
        {
            get { return _text; }
            set {
                if (_text == value)
                    return;
                _text = value;
                OnPropertyChanged("Text");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
