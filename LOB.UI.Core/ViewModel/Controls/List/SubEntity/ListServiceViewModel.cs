﻿using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.List.Base;

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity
{
    public class ListServiceViewModel :ListBaseEntityViewModel<Service>
    {
        public ListServiceViewModel(Service entity, IRepository repository) : base(entity, repository)
        {
        }
    }
}