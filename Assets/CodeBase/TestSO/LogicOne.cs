using UnityEngine;

namespace CodeBase.TestSO
{

    [CreateAssetMenu(fileName = "LogicOne", menuName = "StaticData/LogicOne", order = 0)]
    public class LogicOne : ScriptableObject
    {
        public int TypeID = 1;

        public void Print()
        {
            Debug.Log("LogicOne");
        }
    }
}
