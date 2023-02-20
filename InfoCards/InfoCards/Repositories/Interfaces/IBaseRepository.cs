using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InfoCards.Repositories.Interfaces
{
    public interface IBaseRepository<InformationCard>
    {
        public IList<InformationCard> GetAll();
        public InformationCard Create(ObservableCollection<InformationCard> informationCards);
        public InformationCard Update(InformationCard informationCard);
        public ObservableCollection<InformationCard> Delete(int id);
    }
}
