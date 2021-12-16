﻿using UnityEngine;
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
        [HideInInspector] public GameObject OnHook;
        [HideInInspector] public GameObject Fish;
        [HideInInspector] public GameObject Lure;

        private BobberAnimator _bobberAnimator;

        public void MoveToPlayer()
        {
            transform.DOMoveY(5, 1);
        }

        public void MoveToWater()
        {
           transform.DOMoveY(-6, 1).OnComplete(EnableBobberAnimation);
        }

        public void MoveToBasicPosition()
        { 
            transform.DOMoveY(20, 1).OnComplete(DestroyBobberAndFish);
        }
  
        public void MoveFromWater()
        {
            transform.DOMoveY(8, 1);

            Sequence run = DOTween.Sequence();
            Tween rot = HookContainer.transform.DORotate(new Vector3(0, 360, 0), 10, RotateMode.FastBeyond360).SetEase(Ease.Linear);
            run.Append(rot).SetLoops(-1);
        }

        public void SetBobberAnimator()
        {
            _bobberAnimator = Bobber.GetComponent<BobberAnimator>();
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

        public void DestroyLure()
        {
            Destroy(Lure);
        }

        public void DestroyBobber()
        {
            Destroy(Bobber);
        }

        public void DestroyFish()
        {
            Destroy(Fish);
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

    }
}