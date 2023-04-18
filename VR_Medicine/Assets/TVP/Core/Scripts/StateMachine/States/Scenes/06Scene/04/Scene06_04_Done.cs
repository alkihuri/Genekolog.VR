namespace TVP.Scene06
{
    public class Scene06_04_Done : SimDomenState
    {
        public Scene06_04_Done(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.SceneIsDone_06_04; 
            FullDesription = "Инструкция:\r\n1. Выбрать иглу для пункции\r\n2. Проверить иглы на проходимость: аспирировать жидкость из тестовой пробирки\r\n3. Ввести иглу для пункции в адаптер";
            CurrentStep = 3;
            TotalSteps = 3;
            LevelTittle = "06.04 Исследование";
            Description = "Подготовка след. сцены"; 
        }
        public void NextScenLevel()
        {
            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SceneIsStart_07_01);
        }

        public override void Exit(SimDomenStateMachine stateMachine)
        {

        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }

        public override void Enter(SimDomenStateMachine stateMachine)
        {
            stateMachine.TimeMachine.OnStateFinihsh?.Invoke();
            PrintInfo();
            NextScenLevel();
            IsDoneAtTime = stateMachine.TimeMachine.IsDoneAtTime;

        }

        public override void Reset(SimDomenStateMachine stateMachine)
        {
            stateMachine.ResetToScene06_04();
        }
    }
}