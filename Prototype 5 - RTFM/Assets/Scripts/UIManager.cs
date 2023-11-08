
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI energyLevel;
        public TextMeshProUGUI safetyLevel;
        public TextMeshProUGUI trainIdentifier;
        public TextMeshProUGUI issues;
        public Text trainIdentifierSign;

        public void UpdateEnergyLevel(int energyLevelNum)
        {
            energyLevel.text = energyLevelNum.ToString() + "%";
        }

        public void UpdateSafetyLevel(int sln)
        {
            safetyLevel.text = sln + "%";
        }

        public void SetTrainIdentifier(string tid)
        {
            trainIdentifier.text = tid;
            trainIdentifierSign.text = tid;
        }

        public void UpdateIssues(int num)
        {
            issues.text = num.ToString();
        }
    }
