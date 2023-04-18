using TVP.UI;

namespace TVP.Scene06
{
    public class Scene06_03_05_UziEmulation : SimDomenState
    {
        public Scene06_03_05_UziEmulation(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.UziEmulation_06_03_05;
            CurrentStep = 5;
            TotalSteps = 6;
            LevelTittle = "06.03 Подготовка";
            Description = "Исследование";
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
            stateMachine.ResetToScene06_03();
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }
    }
}