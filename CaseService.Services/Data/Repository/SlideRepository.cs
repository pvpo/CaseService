using CaseService.Services.Abstract.Data;
using CaseService.Services.Domain;

namespace CaseService.Services.Data {
    public class SlideRepository : BaseDomainRepository<Slide, SlideRepository>
    {
        protected override string GetCollectionName() {
            return Slide.collectionName;
        }
    }
}