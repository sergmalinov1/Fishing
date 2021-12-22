using CodeBase.Infrastructure.Services;
using CodeBase.StaticData.Fish;

namespace CodeBase.Infrastructure.RandomService
{
    public interface IRandomService : IService
    {
        float TimeToBite();

        FishStaticData RandomFish();

        int RandomFishSize();
        bool IsCatchedFish();
        void GenerateNewLureStack();
        
    }
}