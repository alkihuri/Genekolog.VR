namespace TVP.Scene08
{
    public class Scene08_03_02_GenMirrorProccess : SimDomenState
    {
        public Scene08_03_02_GenMirrorProccess(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.GenMirrorProccess_08_03_02;
            CurrentStep = 2;
            TotalSteps = 3;
            LevelTittle = "08.03 Завершение";
            FullDesription = "Инструкция:\r\n1. Отмыть влагалище от слизи и крови салфеткой, смоченной физраствором стерильным.\r\n2. Провести осмотр влагалища гинекологическим одноразовым стерильным зеркалом с фиксацией мест вколов иглы и оценкой кровотечения из этих мест.\r\n3. Извлечь тампоны и салфетки из влагалища, извлечь зеркало из влагалища.";
            Description = "Извлечь тампоны и салфетки из влагалища, извлечь зеркало из влагалища.";
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
            stateMachine.ResetToScene08_03();
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }
    }
}