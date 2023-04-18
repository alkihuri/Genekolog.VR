namespace TVP.Scene07
{
    public class Scene07_01_Done: SimDomenState
    {
        public Scene07_01_Done(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.SceneIsDone_07_01;
            FullDesription = "Инструкция:\r\n1. Контролируя изображение на УЗИ мониторе сделать прокол иглой стенки влагалища и капсулы яичника с 1 фолликулом\r\n2. Нажать и не отпускать педаль управления помпой для начала аспирации\r\n3. Пунктировать фолликул за фолликулом иглой, по ходу иглы прямо - контролируя постоянно кончик иглы на УЗИ изображении";
            CurrentStep = 3;
            TotalSteps = 3;
            LevelTittle = "07.01 Процедура";
            Description = "Подготовка след. сцены";
        }

        public override void Enter(SimDomenStateMachine stateMachine)
        {
            PrintInfo(); 
            stateMachine.TimeMachine.OnStateFinihsh?.Invoke();  
            NextScenLevel();
            IsDoneAtTime = stateMachine.TimeMachine.IsDoneAtTime;
        }
        public void NextScenLevel()
        {
            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SceneIsStart_08_01);
            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.DontReleasePedal_08_01_01);
        } 

        public override void Exit(SimDomenStateMachine stateMachine)
        {

        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }

        public override void Reset(SimDomenStateMachine stateMachine)
        {
            stateMachine.ResetToScene07_01();
        }
    }
}