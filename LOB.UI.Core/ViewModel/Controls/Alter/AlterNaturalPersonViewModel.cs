#region Usings

using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    public class AlterNaturalPersonViewModel : AlterPersonViewModel
    {
        #region Props

        public int Rg
        {
            get { return ((NaturalPerson) Entity).Rg; }
            set
            {
                if (Rg == value) return;
                ((NaturalPerson) Entity).Rg = value;
                OnPropertyChanged();
            }
        }

        public int Cpf
        {
            get { return ((NaturalPerson) Entity).Cpf; }
            set
            {
                if (Cpf == value) return;
                ((NaturalPerson) Entity).Cpf = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public AlterNaturalPersonViewModel(NaturalPerson entity, IRepository repository, 
            AlterAddressViewModel alterAddressViewModel,
            AlterContactInfoViewModel alterContactInfoViewModel)
            : base(entity, repository, alterAddressViewModel, alterContactInfoViewModel)
        {
        }
    }
}