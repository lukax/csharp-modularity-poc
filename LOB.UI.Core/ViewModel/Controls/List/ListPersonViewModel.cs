#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List
{
    public class ListPersonViewModel : ListBaseEntityViewModel<Person>
    {
        #region Props

        private Lazy<IQueryable<Person>> _persons;

        public IList<Person> Persons {
            get { return _persons.Value.ToList(); }
        }

        public Person Person {
            get { return Entity; }
            set {
                if (Entity == value) return;
                Entity = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public ListPersonViewModel(Person entity, IRepository repository)
            : base(entity, repository) {
            _persons = new Lazy<IQueryable<Person>>(Repository.GetList<Person>);
        }

        public override bool CanUpdate(object arg) {
            //TODO: Business logic
            return true;
        }

        public override bool CanDelete(object arg) {
            //TODO: Business logic
            return true;
        }
    }
}