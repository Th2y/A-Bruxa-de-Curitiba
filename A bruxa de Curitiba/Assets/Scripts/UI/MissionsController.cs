using UnityEngine;

public class MissionsController : MonoBehaviour
{
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private Mission missionPrefab;
    [SerializeField] private Transform localToInstantiate;

    private void Start()
    {
        foreach(MissionSO missionSO in playerSO.Missions)
        {
            Mission mission = Instantiate(missionPrefab, localToInstantiate);
            mission.SetValues(missionSO.Completed, missionSO.Description);
        }
    }
}
