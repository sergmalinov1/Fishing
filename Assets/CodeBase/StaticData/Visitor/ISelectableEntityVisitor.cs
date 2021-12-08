using System;

namespace CodeBase.StaticData
{
    public interface ISelectableEntityVisitor
    {
        void Visit(BobberStaticData bobber);
        void Visit(FishingLineStaticData fishingLine);
        void Visit(FishingRodStaticData fishingRod);
        void Visit(HookStaticData hook);
        void Visit(LakeStaticData lake);
        void Visit(LureStaticData lure);

    }
}
