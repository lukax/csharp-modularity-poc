using LOB.Domain;

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    public class AlterNaturalPersonViewModel : AlterPersonViewModel
    {
        protected NaturalPerson Entity { get; set; }

        public int Rg
        {
            get { return Entity.Rg; }
            set
            {
                if (Rg == value) return;
                Entity.Rg = value;
                OnPropertyChanged();
            }
        }
        public int Cpf
        {
            get { return Entity.Cpf; }
            set
            {
                if (Cpf == value) return;
                Entity.Cpf = value;
                OnPropertyChanged();
            }
        }

        public AlterNaturalPersonViewModel(NaturalPerson entity)
            : base(entity)
        {
            Entity = entity;
        }
    }
}