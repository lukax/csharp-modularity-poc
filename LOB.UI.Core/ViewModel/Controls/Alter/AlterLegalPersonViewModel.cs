using LOB.Domain;

namespace LOB.UI.Core.ViewModel.Controls
{
    public class AlterLegalPersonViewModel : AlterPersonViewModel
    {
        protected LegalPerson Entity { get; set; }

        public int Ie
        {
            get { return Entity.Ie; }
            set
            {
                if (Ie == value) return;
                Entity.Ie = value;
                OnPropertyChanged();
            }
        }
        public int Cnpj
        {
            get { return Entity.Cnpj; }
            set
            {
                if (Cnpj == value) return;
                Entity.Cnpj = value;
                OnPropertyChanged();
            }
        }

        public AlterLegalPersonViewModel(LegalPerson entity)
            : base(entity)
        {
            Entity = entity;
        }
    }
}