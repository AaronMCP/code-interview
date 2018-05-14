using Hys.Consultation.Domain.Entities;
using Hys.Consultation.Domain.Interface;
using Hys.Consultation.EntityFramework;
using Hys.Platform.Data.EntityFramework;

namespace Hys.Consultation.EntityFramework.Repositories
{
    public class ExamModuleRepository : Repository<ExamModule>, IExamModuleRepository
    {
        public ExamModuleRepository(IConsultationContext context)
            :base(context)
        {

        }
    }
}
