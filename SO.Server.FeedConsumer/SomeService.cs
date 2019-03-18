using SO.Server.Data.Entities;

namespace SO.Server.Data
{
    public class SomeService
    {
        private readonly IUnitOfWork _uow;

        public SomeService(IUnitOfWork unit)
        {
            _uow = unit;
        }

        public void SomeMethod(Event entity)
        {
            _uow.GetRepository<Event>().Add(entity);
            _uow.Commit();
        }
    }
}
