using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI missionDescription;
    [SerializeField] private Toggle missionTogle;

    public void SetValues(bool completed, string description)
    {
        missionTogle.isOn = completed;
        missionDescription.text = description;
    }
}
