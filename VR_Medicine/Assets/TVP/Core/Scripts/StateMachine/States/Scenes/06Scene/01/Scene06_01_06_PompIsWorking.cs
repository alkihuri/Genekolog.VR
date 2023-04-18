using Common.UI;

namespace TVP.Scene06
{
    public class Scene06_01_06_PompIsWorking : SimDomenState
    {


        public Scene06_01_06_PompIsWorking(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.PompIsWorking_06_01_06;
            TotalSteps = 6;
            CurrentStep = 6;
            Description = "Этап завершен!";
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