using LOB.Dao.Interface;
using LOB.Domain;

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    public class AlterLegalPersonViewModel : AlterPersonViewModel
    {
        #region Props
        public int Ie
        {
            get { return ((LegalPerson)Entity).Ie; }
            set
            {
                if (Ie == value) return;
                ((LegalPerson)Entity).Ie = value;
                OnPropertyChanged();
            }
        }
        public int Cnpj
        {
            get { return ((LegalPerson)Entity).Cnpj; }
            set
            {
                if (Cnpj == value) return;
                ((LegalPerson)Entity).Cnpj = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public AlterLegalPersonViewModel(LegalPerson entity, IRepository repository)
            : base(entity, repository)
        {
        }
    }
}