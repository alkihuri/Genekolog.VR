namespace TVP.Scene06
{
    public class Scene06_04_02_AspiratingCheck : SimDomenState
    {
        public Scene06_04_02_AspiratingCheck(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.AspiratingTestIsDone_06_04_02;
            FullDesription = "Инструкция:\r\n1. Выбрать иглу для пункции\r\n2. Проверить иглы на проходимость: аспирировать жидкость из тестовой пробирки\r\n3. Ввести иглу для пункции в адаптер";
            CurrentStep = 2;
            TotalSteps = 3;
            LevelTittle = "06.04 Исследование";
            Description = "Ввести иглу для пункции в адаптер";

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
            stateMachine.ResetToScene06_04();
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }
    }

}