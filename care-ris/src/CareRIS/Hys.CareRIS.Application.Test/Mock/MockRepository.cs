using Hys.Platform.Data.EntityFramework;
using Hys.Platform.Domain;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Interface;
using Hys.CareRIS.EntityFramework;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Hys.Consultation.Application.Test.Utils;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Services;
using Hys.Platform.CrossCutting.LogContract;

namespace Hys.CareRIS.Applicatiton.Test.Mock
{
    public class MockRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        public MockRisProContext DbContext { get; private set; }
        public DbSet<TEntity> DbSet { get; private set; }

        public MockRepository(IDbContext context)
        {
            Guard.ArgumentNotNull(context, "context");
            DbContext = (MockRisProContext)context;
            DbSet = DbContext.Set<TEntity>() as DbSet<TEntity>;
        }

        public IEnumerable<TEntity> Get()
        {
            return DbSet.AsQueryable();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.Where(filter).AsQueryable();
        }

        public IEnumerable<TEntity> Get<TOderKey>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize,
            Expression<Func<TEntity, TOderKey>> sortKeySelector, bool isAsc = true)
        {
            Guard.ArgumentNotNull(filter, "predicate");
            Guard.ArgumentNotNull(sortKeySelector, "sortKeySelector");
            if (isAsc)
            {
                return DbSet
                    .Where(filter)
                    .OrderBy(sortKeySelector)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize).AsQueryable();
            }
            return DbSet
                .Where(filter)
                .OrderByDescending(sortKeySelector)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).Count();
        }

        public void Update(TEntity instance)
        {
            DbSet.Remove(DbSet.FirstOrDefault(i => i.UniqueId.Equals(instance.UniqueId)));
            DbSet.Add(instance);
        }

        public void Add(TEntity instance)
        {
            DbSet.Add(instance);
        }

        public void Delete(TEntity instance)
        {
            DbSet.Remove(instance);
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public void Dispose()
        {

        }


        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }


        public Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class MockPatientRepository : MockRepository<Patient>, IPatientRepository
    {
        public MockPatientRepository(IRisProContext context)
            : base(context)
        {

        }
    }

    public class MockOrderRepository : MockRepository<Order>, IOrderRepository
    {
        public MockOrderRepository(IRisProContext context)
            : base(context)
        {

        }
    }

    public class MockProcedureRepository : MockRepository<Procedure>, IProcedureRepository
    {
        public MockProcedureRepository(IRisProContext context)
            : base(context)
        {

        }
    }

    public class MockReportRepository : MockRepository<Report>, IReportRepository
    {
        public MockReportRepository(IRisProContext context)
            : base(context)
        {

        }
    }

    public class MockReportFileRepository : MockRepository<ReportFile>, IReportFileRepository
    {
        public MockReportFileRepository(IRisProContext context)
            : base(context)
        {

        }
    }

    public class MockPrintTemplateRepository : MockRepository<PrintTemplate>, IPrintTemplateRepository
    {
        public MockPrintTemplateRepository(IRisProContext context)
            : base(context)
        {

        }
    }

    public class MockPrintTemplateFieldsRepository : MockRepository<PrintTemplateFields>, IPrintTemplateFieldsRepository
    {
        public MockPrintTemplateFieldsRepository(IRisProContext context)
            : base(context)
        {

        }
    }

    public class MockReportTemplateDirecRepository : MockRepository<ReportTemplateDirec>, IReportTemplateDirecRepository
    {
        public MockReportTemplateDirecRepository(IRisProContext context)
            : base(context)
        {

        }
    }

    public class MockReportTemplateRepository : MockRepository<ReportTemplate>, IReportTemplateRepository
    {
        public MockReportTemplateRepository(IRisProContext context)
            : base(context)
        {

        }
    }

    public class MockAccesssionNumerListRepository : MockRepository<AccessionNumberList>, IAccessionNumberListRepository
    {
        public MockAccesssionNumerListRepository(IRisProContext context)
            : base(context)
        {

        }
    }

    public class MockBodySystemMapRepository : MockRepository<BodySystemMap>, IBodySystemMapRepository
    {
        public MockBodySystemMapRepository(IRisProContext context)
            : base(context)
        {

        }
    }

    public class MockProcedureCodeRepository : MockRepository<Procedurecode>, IProcedureCodeRepository
    {
        public MockProcedureCodeRepository(IRisProContext context)
            : base(context)
        {

        }
    }

    public class MockModalityRepository : MockRepository<Modality>, IModalityRepository
    {
        public MockModalityRepository(IRisProContext context)
            : base(context)
        {

        }
    }

    public class MockModalityTypeRepository : MockRepository<ModalityType>, IModalityTypeRepository
    {
        public MockModalityTypeRepository(IRisProContext context)
            : base(context)
        {

        }
    }

    public class MockShortcutRepository : MockRepository<Shortcut>, IShortcutRepository
    {
        public MockShortcutRepository(IRisProContext context)
            : base(context)
        {

        }
    }

    public class MockReportPrintLogRepository : MockRepository<ReportPrintLog>, IReportPrintLogRepository
    {
        public MockReportPrintLogRepository(IRisProContext context)
            : base(context)
        {

        }
    }


    public class MockRoleRepository : MockRepository<Role>, IRoleRepository
    {
        public MockRoleRepository(IRisProContext context)
            : base(context)
        {

        }

        public Role GetRoles(string roleName)
        {
            return new Role();
        }
    }
    public class MockOnlineClientRepository : MockRepository<OnlineClient>, IOnlineClientRepository
    {
        public MockOnlineClientRepository(IRisProContext context)
            : base(context)
        {

        }
    }

    public class MockRequestListRepository : MockRepository<RequestList>, IRequestListRepository
    {
        public MockRequestListRepository(IRisProContext context)
            : base(context)
        {

        }
    }

    public class MockRequisitionRepository : MockRepository<Requisition>, IRequisitionRepository
    {
        public MockRequisitionRepository(IRisProContext context)
            : base(context)
        {

        }
    }

    public class MockUserRepository : MockRepository<User>, IUserRepository
    {
        public MockUserRepository(IRisProContext context)
            : base(context)
        {

        }

        public void UpdatePassword(string userId, string password)
        {

        }
        public User2Domain GetUserExpiration(string userId)
        {
            return new User2Domain();
        }
    }

    public class MockLicenseService : ILicenseService
    {
        public MockLicenseService(ICommonLog commonLog)
        {
        }

        public void Dispose()
        {

        }

        public LicenseDataDto GetLicenseData()
        {
            return new LicenseDataDto()
            {
                IsSuccessed = true,
                RisEnabled = true,
                ConsultationEnabled = true,
                MaxOnlineUserCount = int.MaxValue,
            };
        }
    }

}
