namespace TVP.Scene08
{
    public class Scene08_01_01_DontReleasePedal : SimDomenState
    {
        public Scene08_01_01_DontReleasePedal(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.DontReleasePedal_08_01_01;
            CurrentStep = 1;
            TotalSteps = 7;
            LevelTittle = "08.01 Извлечение";
            FullDesription = "Инструкция:\r\n1. Не отпускать педаль\r\n2. Извлечь иглу из биопсийной насадки \r\n3. Поместить иглу с промывающим раствором \r\n4. Промыть иглу и тольĸо после этого отпустить педаль.\r\n5. Передать иглу помощнику\r\n6. Снять с датчика адаптер для биопсии\r\n7. Передать помощнику";
            Description = "Извлечь иглу из биопсийной насадки";
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
            stateMachine.ResetToScene08_01();
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }
    }

}