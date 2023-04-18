using UnityEngine;

namespace TVP
{
    public class StartSimState : SimDomenState
    {
        public StartSimState(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.SimIsStart;
            Description = "Инструкция:\r\nВключить УЗИ аппарат с монитором\r\nНастроить изображение на УЗИ мониторе\r\nВключить помпу\r\nПроверить уровень разрежения.\r\nПроверить работу ножной педали\r\nПроверить работу помпы";

        }


        public override void Enter(SimDomenStateMachine stateMachine)
        {
            PrintInfo();
        }

        public override void Exit(SimDomenStateMachine stateMachine)
        { 

        }

        public override void Reset(SimDomenStateMachine stateMachine)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {
             
        }
    }
}