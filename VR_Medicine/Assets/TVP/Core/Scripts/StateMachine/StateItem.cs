using UnityEngine.Events;

namespace TVP
{

    [System.Serializable]
    public class StateItem
    {
        public StateTypeEnum StateType;

        public StateItem(StateTypeEnum state)
        { 
            StateType = state;
        }
    }
}