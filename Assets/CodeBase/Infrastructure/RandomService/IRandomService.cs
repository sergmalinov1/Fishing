using CodeBase.Infrastructure.Services;
using CodeBase.StaticData.Fish;

namespace CodeBase.Infrastructure.RandomService
{
    public interface IRandomService : IService
    {
        public float TimeToBite();

        public FishStaticData RandomFish();
        public bool IsCatchedFish();
        void GenerateNewQueue();
    }
}