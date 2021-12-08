using System;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class ShowType : ISelectableEntityVisitor
    {
        public void Visit(BobberStaticData bobber)
        {
            Debug.Log($"boober coef- " + bobber.CoefficientOfLuck);
        }

        public void Visit(FishingLineStaticData fishingLine)
        {
         
        }

        public void Visit(FishingRodStaticData fishingRod)
        {
            
        }

        public void Visit(HookStaticData hook)
        {
        
        }

        public void Visit(LakeStaticData lake)
        {
           
        }

        public void Visit(LureStaticData lure)
        {
            
        }
    }
}
