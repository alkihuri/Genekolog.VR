using TVP.UI;

namespace TVP.Scene06
{
    public class Scene06_03_01_Gel : SimDomenState
    {
        public Scene06_03_01_Gel(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.GelIsApplied_06_03_01;
            CurrentStep = 1;
            TotalSteps = 6; 
            LevelTittle = "06.03 Подготовка";
            Description = "Взять презерватив надеть на узи датчик"; 
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