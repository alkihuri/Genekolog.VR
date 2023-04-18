using UnityEngine;

namespace Common.DataManager
{
    [System.Serializable]
    public class StudentExaminationTestInfo
    {

        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _totalSteps;
        [SerializeField] private int _errors;
        [SerializeField] private int _passedSteps;

        public StudentExaminationTestInfo(string name, string description, int totalSteps, int errors, int passedSteps)
        {
            _name = name;
            _description = description;
            _totalSteps = totalSteps;
            _errors = errors;
            _passedSteps = passedSteps;
        }

        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public int TotalSteps { get => _totalSteps; set => _totalSteps = value; }
        public int Errors { get => _errors; set => _errors = value; }
        public int PassedSteps { get => _passedSteps; set => _passedSteps = value; }

        internal string GetText()
        {
            return Name
                + "\n" + Description
                + "\n" + $"Пройденые этапы {PassedSteps} / {TotalSteps}"
                + "\n" + $"ошибки {Errors} / {TotalSteps}";
        }
    }
}