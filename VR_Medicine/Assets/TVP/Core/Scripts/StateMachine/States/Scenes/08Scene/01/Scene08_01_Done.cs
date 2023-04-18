namespace TVP.Scene08
{
    public class Scene08_01_Done : SimDomenState
    {
        public Scene08_01_Done(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.SceneIsDone_08_01;
            CurrentStep = 7;
            TotalSteps = 7;
            LevelTittle = "08.01 Извлечение";
            FullDesription = "Инструкция:\r\n1. Не отпускать педаль\r\n2. Извлечь иглу из биопсийной насадки \r\n3. Поместить иглу с промывающим раствором \r\n4. Промыть иглу и тольĸо после этого отпустить педаль.\r\n5. Передать иглу помощнику\r\n6. Снять с датчика адаптер для биопсии\r\n7. Передать помощнику";
            Description = "Подготовка след. сцены";
        }

        public override void Enter(SimDomenStateMachine stateMachine)
        {
            IsDoneAtTime = stateMachine.TimeMachine.IsDoneAtTime;
            stateMachine.TimeMachine.OnStateFinihsh?.Invoke();
            PrintInfo();
            NextScenLevel();
        }
        public void NextScenLevel()
        {
            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SceneIsStart_08_02); 
        }

        public override void Exit(SimDomenStateMachine stateMachine)
        {

        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }

        public override void Reset(SimDomenStateMachine stateMachine)
        {
            stateMachine.ResetToScene08_01();
        }
    }
}