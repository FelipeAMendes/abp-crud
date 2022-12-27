using Volo.Abp.Auditing;

namespace Abp.Crud.Entities;

public interface ICrudEntity : IHasCreationTime, IHasModificationTime, IHasDeletionTime { }
