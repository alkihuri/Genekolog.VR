namespace TVP.Scene06
{
    public class Scene06_03_Done : SimDomenState
    {
        public Scene06_03_Done(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.SceneIsDone_06_03;
            Description = "Подготовка след. сцены";
            LevelTittle = "06.03 Подготовка";
            TotalSteps = 6;
            CurrentStep = 6;
        }

        public override void Enter(SimDomenStateMachine stateMachine)
        {
            stateMachine.TimeMachine.OnStateFinihsh?.Invoke();
            IsDoneAtTime = stateMachine.TimeMachine.IsDoneAtTime;
            PrintInfo();
            NextScenLevel();
        }
        public void NextScenLevel()
        {
            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SceneIsStart_06_04);
        }

        public override void Exit(SimDomenStateMachine stateMachine)
        {
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }

        public override void Reset(SimDomenStateMachine stateMachine)
        {
            stateMachine.ResetToScene06_03();
        }
    }
}