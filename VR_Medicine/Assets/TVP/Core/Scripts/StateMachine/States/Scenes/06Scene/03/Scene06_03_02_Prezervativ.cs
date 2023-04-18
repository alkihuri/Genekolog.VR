using TVP.UI;

namespace TVP.Scene06
{
    public class Scene06_03_02_Prezervativ : SimDomenState
    {
        public Scene06_03_02_Prezervativ(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.PrezervativIsApplied_06_03_02;
            CurrentStep = 2;
            TotalSteps = 6; 
            LevelTittle = "06.03 Подготовка";
            Description = "Взять адаптер прикрепить к узи датчику"; 
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