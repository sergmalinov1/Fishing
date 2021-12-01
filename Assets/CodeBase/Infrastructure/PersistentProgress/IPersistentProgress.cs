using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
  public interface IPersistentProgress : IService
  {
    PlayerProgress Progress { get; set; }
  }
}