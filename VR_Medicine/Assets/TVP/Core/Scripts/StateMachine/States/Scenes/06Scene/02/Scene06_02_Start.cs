using TVP.UI;

namespace TVP.Scene06
{
    public class Scene06_02_Start : SimDomenState
    {
        public Scene06_02_Start(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            TotalSteps = 2;
            CurrentStep = 0;
            StateType = StateTypeEnum.SceneIsStart_06_02;
            FullDesription = "Инструкция:\r\n1. Обработать (вымыть + антисептиĸ) руĸи - стандарт EN-1500, надеть перчатĸи стерильные неопудренные.\r\n2. Обработка промежности и влагалища"; 
            LevelTittle = "06.02 Обработка";
            Description = "Обработать (вымыть + антисептиĸ) руĸи - стандарт EN-1500"; 
        }

        public override void Enter(SimDomenStateMachine stateMachine)
        {
            PrintInfo();
            SimStateCanvas.CurrentSimulation.NewSceneConfig(); 
            stateMachine.TimeMachine.StartTimer(60);
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