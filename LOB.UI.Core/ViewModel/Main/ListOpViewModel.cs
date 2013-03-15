using System;
using System.Collections.Generic;
using System.Linq;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.Names;
using LOB.UI.Interface.ViewModel.Controls.List;
using LOB.UI.Interface.ViewModel.Controls.List.Base;

namespace LOB.UI.Core.ViewModel.Main
{
    public class ListOpViewModel : BaseViewModel, IListOpViewModel
    {
        private IList<string> _entitys;
        public IList<string> Entitys
        {
            get { return _entitys; }
            set
            {
                if (_entitys == value) return;
                _entitys = value;
                OnPropertyChanged();
            }
        }

        private string _entity;
        public string Entity
        {
            get { return _entity; }
            set
            {
                if (_entity == value) return;
                _entity = value;
                OnPropertyChanged();
            }
        }

        public ListOpViewModel(Category entity, IRepository repository)
        {
            //Entitys = Enum.GetValues(typeof (OperationName))..ToList();
        }

        public override void InitializeServices()
        {
            throw new NotImplementedException();
        }

        public override void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
