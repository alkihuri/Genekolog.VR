namespace TVP.Scene07
{
    public class Scene07_01_01_SurgeryIsStart : SimDomenState
    {
        public Scene07_01_01_SurgeryIsStart(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.SurgeryIsStart_07_01_01;
            FullDesription = "Инструкция:\r\n1. Контролируя изображение на УЗИ мониторе сделать прокол иглой стенки влагалища и капсулы яичника с 1 фолликулом\r\n2. Нажать и не отпускать педаль управления помпой для начала аспирации\r\n3. Пунктировать фолликул за фолликулом иглой, по ходу иглы прямо - контролируя постоянно кончик иглы на УЗИ изображении";
            CurrentStep = 1;
            TotalSteps = 3;
            LevelTittle = "07.01 Процедура";
            Description = "Контролируя изображение на УЗИ мониторе сделать прокол иглой стенки влагалища и капсулы яичника с 1 фолликулом";
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
            stateMachine.ResetToScene07_01();
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }
    }
}