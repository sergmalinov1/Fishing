using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
  public class PersistentProgress : IPersistentProgress
  {
    public PlayerProgress Progress { get; set; }
  }
}