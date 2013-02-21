using System;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.Base;

namespace LOB.UI.Core.ViewModel.Controls
{
    public class AlterPersonViewModel : AlterEntityViewModel<Person>
    {

        public string FirstName
        {
            get { return Entity.FirstName; }
            set
            {
                if (FirstName == value) return;
                Entity.FirstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get { return Entity.LastName; }
            set
            {
                if (LastName == value) return;
                Entity.LastName = value;
                OnPropertyChanged();
            }
        }
        public string NickName
        {
            get { return Entity.NickName; }
            set
            {
                if (NickName == value) return;
                Entity.NickName = value;
                OnPropertyChanged();
            }
        }
        public DateTime BirthDate
        {
            get { return Entity.BirthDate; }
            set
            {
                if (BirthDate == value) return;
                Entity.BirthDate = value;
                OnPropertyChanged();
            }
        }
        public string Notes
        {
            get { return Entity.Notes; }
            set
            {
                if (Notes == value) return;
                Entity.Notes = value;
                OnPropertyChanged();
            }
        }

        public AlterPersonViewModel(Person entity)
            : base(entity)
        {

        }
    }
}