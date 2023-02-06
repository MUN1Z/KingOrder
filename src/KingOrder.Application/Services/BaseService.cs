using AutoMapper;
using KingOrder.Domain.Interfaces;

namespace KingOrder.Application.Services
{
    public class BaseService
    {
        #region private members

        private readonly IUnitOfWork _uow;

        protected readonly IMapper _mapper;

        #endregion private members

        #region constructors

        public BaseService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        #endregion constructors

        #region public methods implementations

        public void BeginTransaction()
        {
            _uow.BeginTransaction();
        }

        public void Commit()
        {
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

        #endregion public methods implementations
    }
}
