using Common.UI;

namespace TVP.Scene06
{
    public class Scene06_01_01_UziDeviceIsOn : SimDomenState
    {


        public Scene06_01_01_UziDeviceIsOn(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.UziOsOn_06_01_01;
            TotalSteps = 6;
            CurrentStep = 1;
            Description = "Подготовить аппарат УЗИ";
            LevelTittle = "06.01 Подготовка УЗИ аппарата";   
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
            stateMachine.ResetToScene06_01();
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }
    }



}