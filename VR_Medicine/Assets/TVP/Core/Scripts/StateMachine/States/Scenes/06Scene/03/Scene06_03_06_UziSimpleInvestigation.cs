using TVP.UI;

namespace TVP.Scene06
{
    public class Scene06_03_06_UziSimpleInvestigation : SimDomenState
    {
        public Scene06_03_06_UziSimpleInvestigation(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.SimpleUziInvestigation_06_03_06;
            CurrentStep = 6;
            TotalSteps = 6;
            LevelTittle = "06.03 Подготовка";
            Description = "Этап завершен!";
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