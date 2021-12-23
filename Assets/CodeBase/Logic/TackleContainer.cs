using UnityEngine;
using DG.Tweening;
using CodeBase.BobberObject;
using System;
using System.Collections;

namespace CodeBase.GameLogic
{
    public class TackleContainer : MonoBehaviour
    {
        public Transform BobberContainer;
        public Transform HookContainer;
        public Transform OnHookContainer;


        [HideInInspector] public GameObject Bobber;
        [HideInInspector] public GameObject Fish;
        [HideInInspector] public GameObject Lure;
        [HideInInspector] public GameObject Hook;

        private BobberAnimator _bobberAnimator;

        public void MoveToBasicPosition()
        {
            transform.DOMoveY(20, 1f).OnComplete(DestroyBobberAndFish);
            HookContainer.DOLocalMoveY(0, 1f);
         //   SetBasicContainerPosition();
        }

        public void MoveToPlayer()
        {
            transform.DOMoveY(5, 1);
        }

        public void MoveToWater()
        {
           transform.DOMoveY(-6, 1).OnComplete(EnableBobberAnimation);
        }

    
  
        public void MoveFromWater()
        {
            transform.DOMoveY(8, 1);

            Sequence run = DOTween.Sequence();

            Tween rot = HookContainer.transform.DORotate(new Vector3(0, 360, 0), 10, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
            run.Append(rot).SetLoops(-1);
        }

        public void MoveFromWaterAndBreak()
        {
            transform.DOMoveY(8, 1);

            Sequence run = DOTween.Sequence();
            run.Append(transform.DOMoveY(8, 1));
            run.Append(HookContainer.DOLocalMoveY(-10, 1)).OnComplete(DestroyFishAndHook);

        }

        public void SetBobberAnimator()
        {
            _bobberAnimator = Bobber.GetComponent<BobberAnimator>();
        }

     
        public void DestroyLure()
        {
            Destroy(Lure);
            Lure = null;
        }

        public void DestroyBobber()
        {
            if (Bobber != null)
            {
                Destroy(Bobber);
                Bobber = null;
            }
        }

        public void DestroyFish()
        {
            if (Fish != null)
            {
                Destroy(Fish);
                Fish = null;
            }
        }

        public void DestroyHook()
        {
            if (Hook != null)
            {
                Destroy(Hook);
                Hook = null;
            }
        }

        public BobberAnimator BobberAnimator
        {
            set { }
            get { return _bobberAnimator; }
        }
        public void DisableBobberAnimation()
        {
            Animator animator = Bobber.GetComponent<Animator>();
            animator.enabled = false;
        }

        private void EnableBobberAnimation()
        {
            Animator animator = Bobber.GetComponent<Animator>();
            animator.enabled = true;
        }

        private void DestroyBobberAndFish()
        {
            DestroyBobber();
            DestroyFish();
        }


        private void DestroyFishAndHook()
        {
            DestroyFish();
            DestroyHook();
        }

    }
}
