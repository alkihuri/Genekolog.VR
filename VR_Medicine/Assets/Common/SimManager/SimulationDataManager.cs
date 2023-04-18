using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using System;
using NaughtyAttributes;

namespace Common.DataManager
{

    public class SimulationDataManager : MonoBehaviour
    {
        [SerializeField] TMPro.TMP_InputField _inputField;
        [SerializeField] Button _regButton;
        [SerializeField] TextMeshProUGUI _text;

        public static string NICKNAME_KEY = "NICKNAME";
        public static string TOTALSTEPS_KEY = "TOTAL_STEPS";
        public static string COMPLETESTEPS_KEY = "COMPLETESTEPS_KEY";
        public static string ERRORS_KEY = "ERRORS_KEY";
        public static string LAST_ID = "LAST_ID";
        public static string DESCRIPTION_KEY = "DESCRIPTION_KEY";

        public static int THIS_ID;
        [SerializeField] private List<StudentExaminationTestInfo> _listOfStudents = new List<StudentExaminationTestInfo>();


        // Start is called before the first frame update
        void Start()
        {
            ReadDataBase();

            if (_regButton != null)
            {
                NextStudent();
                _regButton.onClick.AddListener(SaveNickName);
            }
            if(_text != null)
            {
                _text.text = GetLast()?.GetText();
            }

        }
        public void SaveNickName()
        {
            var id = PlayerPrefs.GetInt(LAST_ID);
            THIS_ID = id;
            var nickName = _inputField.text;
            nickName = nickName.Replace("-", "");
            nickName = nickName == "" ? "NO-NAME" + "#" + id : nickName;
            PlayerPrefs.SetString(NICKNAME_KEY, nickName);
            Debug.Log(PlayerPrefs.GetString(NICKNAME_KEY) + " registated");
            PlayerPrefs.SetInt(ERRORS_KEY, 0);
            PlayerPrefs.SetInt(COMPLETESTEPS_KEY, 0);
            SaveResult();
        }
        public void SetTVPMode()
        {
            PlayerPrefs.SetString(DESCRIPTION_KEY, "TVP");
            UpdateTotalSteps(8);
        }
        public void SetICSIMode()
        {
            PlayerPrefs.SetString(DESCRIPTION_KEY, "ICSI");
            UpdateTotalSteps(8); // сколько у тебя шагов всего? 
        }
        public static void  NextStudent()
        {
            var ID = PlayerPrefs.GetInt(LAST_ID);
            ID++;
            PlayerPrefs.SetInt(LAST_ID, ID);

        }
        public static void UpdateTotalSteps(int steps)
        {
            PlayerPrefs.SetInt(TOTALSTEPS_KEY, steps);
        }
        public static void IncreaseError()
        {
            var error = PlayerPrefs.GetInt(ERRORS_KEY);
            error++;
            error = Mathf.Clamp(error, 0, PlayerPrefs.GetInt(TOTALSTEPS_KEY));
            PlayerPrefs.SetInt(ERRORS_KEY, error);
            SaveResult();
        }

        public static void IncreaseCompleteSteps()
        {
            int completeSteps = PlayerPrefs.GetInt(COMPLETESTEPS_KEY);
            completeSteps++;
            completeSteps = Mathf.Clamp(completeSteps, 0, PlayerPrefs.GetInt(TOTALSTEPS_KEY)); 
            PlayerPrefs.SetInt(COMPLETESTEPS_KEY, completeSteps);
            SaveResult();
        }
        public static void UpdateCompletedSteps()
        {
            var completedSteps = PlayerPrefs.GetInt(TOTALSTEPS_KEY) - PlayerPrefs.GetInt(ERRORS_KEY);
            completedSteps = Mathf.Clamp(completedSteps, 0, PlayerPrefs.GetInt(TOTALSTEPS_KEY));
        }
        public static void SaveResult()
        {



            string nick = PlayerPrefs.GetString(NICKNAME_KEY);
            string des = PlayerPrefs.GetString(DESCRIPTION_KEY);
            int errors = PlayerPrefs.GetInt(ERRORS_KEY);
            int totalsteps = PlayerPrefs.GetInt(TOTALSTEPS_KEY);
            int completesteps = PlayerPrefs.GetInt(COMPLETESTEPS_KEY);
            var record = nick
                + "_" + des
                + "_" + errors
                + "_" + totalsteps
                + "_" + completesteps;

            record = record.Replace("__", "_");

            PlayerPrefs.SetString(PlayerPrefs.GetInt(LAST_ID).ToString(), record);  
        }




        [ContextMenu("Clear data")]
        public void ClearDataBase() => PlayerPrefs.DeleteAll();



        public static StudentExaminationTestInfo GetLast()
        {
            var record = PlayerPrefs.GetString((PlayerPrefs.GetInt(LAST_ID)).ToString());
            print(record);
            if (record.Split('_').Length < 5)
                return null;

            var nick = record.Split('_')[0];
            var description = record.Split('_')[1];

            int errors = 0;
            Int32.TryParse(record.Split('_')[2], out errors);

            int total = 0;
            Int32.TryParse(record.Split('_')[3], out total);

            int complete = 0;
            Int32.TryParse(record.Split('_')[4], out complete);

            var newItem = new StudentExaminationTestInfo(nick, description, total, errors, complete);

            return newItem;
        }

        [ContextMenu("Read data")]
        public void ReadDataBase()
        {
            string record = "NO_DATA";
            for (int x = 0; x < 100; x++)
            {
                record = PlayerPrefs.GetString(x.ToString());
                if (record.Split('_').Length < 5)
                    continue;
                 
                var nick = record.Split('_')[0];
                var description = record.Split('_')[1];

                int errors = 0;
                Int32.TryParse(record.Split('_')[2], out errors);

                int total = 0;
                Int32.TryParse(record.Split('_')[3], out total);

                int complete = 0;
                Int32.TryParse(record.Split('_')[4], out complete);

                var newItem = new StudentExaminationTestInfo(nick, description, total, errors, complete);

                _listOfStudents.Add(newItem);

            }
        }
        
    }
}