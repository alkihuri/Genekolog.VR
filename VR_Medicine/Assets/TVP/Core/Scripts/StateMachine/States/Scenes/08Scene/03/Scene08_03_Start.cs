using TVP.UI;

namespace TVP.Scene08
{
    public class Scene08_03_Start : SimDomenState
    {
        public Scene08_03_Start(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.SceneIsStart_08_03;
            CurrentStep = 0;
            TotalSteps = 3;
            LevelTittle = "08.03 Завершение";
            FullDesription = "Инструкция:\r\n1. Отмыть влагалище от слизи и крови салфеткой, смоченной физраствором стерильным.\r\n2. Провести осмотр влагалища гинекологическим одноразовым стерильным зеркалом с фиксацией мест вколов иглы и оценкой кровотечения из этих мест.\r\n3. Извлечь тампоны и салфетки из влагалища, извлечь зеркало из влагалища.";
            Description = "Отмыть влагалище от слизи и крови салфеткой, смоченной физраствором стерильным.";
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
            stateMachine.ResetToScene08_03();
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }
    }
}