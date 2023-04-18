using TVP.UI;

namespace TVP.Scene08
{
    public class Scene08_03_01_lastProccesVagina : SimDomenState
    {
        public Scene08_03_01_lastProccesVagina(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.lastProccesVagina_08_03_01;
            CurrentStep = 1;
            TotalSteps = 3;
            LevelTittle = "08.03 Завершение";
            FullDesription = "Инструкция:\r\n1. Отмыть влагалище от слизи и крови салфеткой, смоченной физраствором стерильным.\r\n2. Провести осмотр влагалища гинекологическим одноразовым стерильным зеркалом с фиксацией мест вколов иглы и оценкой кровотечения из этих мест.\r\n3. Извлечь тампоны и салфетки из влагалища, извлечь зеркало из влагалища.";
            Description = "Провести осмотр влагалища гинекологическим одноразовым стерильным зеркалом с фиксацией мест вколов иглы и оценкой кровотечения из этих мест.";
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