using TVP.UI;

namespace TVP.Scene06
{
    public class Scene06_03_03_Adapter : SimDomenState
    {
        public Scene06_03_03_Adapter(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.BiopsyAdapterIsMounted_06_03_03;
            CurrentStep = 3;
            TotalSteps = 6;
            LevelTittle = "06.03 Подготовка";
            Description = "Введение во влагалище";
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