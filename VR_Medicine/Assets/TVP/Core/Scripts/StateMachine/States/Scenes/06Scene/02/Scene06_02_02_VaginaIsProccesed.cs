namespace TVP.Scene06
{
    public class Scene06_02_02_VaginaIsProccesed : SimDomenState
    {
        public Scene06_02_02_VaginaIsProccesed(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.VaginaIsProccesed_06_02_02;
            Description = "Этап завершен";
            LevelTittle = "06.02 Обработка";
            TotalSteps = 2;
            CurrentStep = 2; 
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
            stateMachine.ResetToScene06_02();
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }
    }
}