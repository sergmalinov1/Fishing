using UnityEngine;

namespace CodeBase.TestSO
{
    [CreateAssetMenu(fileName = "MainSO", menuName = "StaticData/MainSO", order = 0)]
    public class MainSO : ScriptableObject
    {
        public GameObject gameObject;
        public ScriptableObject so;


        public void Test()
        {
            LogicOne logic = so as LogicOne;
            logic.Print();
        }
    }
}
