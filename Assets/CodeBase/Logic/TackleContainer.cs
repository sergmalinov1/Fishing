using UnityEngine;
using DG.Tweening;

namespace CodeBase.GameLogic
{
    public class TackleContainer : MonoBehaviour
    {
        public Transform BobberContainer;
        public Transform OnHookContainer;

        public GameObject Bobber;
        public GameObject OnHook;

        public void MoveToPlayer()
        {
            transform.DOMoveY(5, 1);
        }

        public void MoveToWater()
        {
            transform.DOMoveY(-6, 1);
        }

        public void EnableBobberAnimation()
        {
            Animator animator = Bobber.GetComponent<Animator>();
            animator.enabled = true;
        }

    }
}
