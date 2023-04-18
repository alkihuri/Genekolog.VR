using Common.DataManager;
using Common.UI;

namespace TVP.Scene06
{
    public class Scene06_01_Done : SimDomenState
    {


        public Scene06_01_Done(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.SceneIsDone_06_01;
            Description = "Подготовка след. сцены";
            LevelTittle = "06.01 Подготовка УЗИ аппарата";
            TotalSteps = 6;
            CurrentStep = 6;
        }

        public void NextScenLevel()
        {
            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SceneIsStart_06_02);
        }

        public override void Enter(SimDomenStateMachine stateMachine)
        {

            NextScenLevel();
            stateMachine.TimeMachine.OnStateFinihsh?.Invoke();
            IsDoneAtTime = stateMachine.TimeMachine.IsDoneAtTime; 
            PrintInfo();


            

        }

        public override void Exit(SimDomenStateMachine stateMachine)
        {
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }

        public override void Reset(SimDomenStateMachine stateMachine)
        {

            stateMachine.ResetToScene06_01();
        }
    }
}