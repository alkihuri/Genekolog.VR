namespace TVP.Scene06
{
    public class Scene06_04_01_NiddleSelect : SimDomenState
    {
        public Scene06_04_01_NiddleSelect(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.NiddleISSelected_06_04_01;
            FullDesription = "Инструкция:\r\n1. Выбрать иглу для пункции\r\n2. Проверить иглы на проходимость: аспирировать жидкость из тестовой пробирки\r\n3. Ввести иглу для пункции в адаптер";
            CurrentStep = 1;
            TotalSteps = 3;
            LevelTittle = "06.04 Исследование";
            Description = "Проверить иглы на проходимость: аспирировать жидкость из тестовой пробирки";

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