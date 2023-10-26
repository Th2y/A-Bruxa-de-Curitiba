using UnityEngine;

public class LevelGenerator : Singleton<LevelGenerator>
{
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;

    [SerializeField] private Track dayTrack;
    [SerializeField] private Track nightTrack;

    [SerializeField] private Transform posTrack1;
    [SerializeField] private Transform posTrack2;

    [SerializeField] private Material daySky;
    [SerializeField] private Material nightSky;

    private bool isDaySky;

    protected override void Awake()
    {
        base.Awake();

        if (ActualMode.Instance.IsInfinityMode)
        {
            if (playerSO.ActualSkin == "Young")
            {
                meshRenderer.material = playerSO.AneYoung.Material;
                isDaySky = true;
                RenderSettings.skybox = daySky;
                Instantiate(nightTrack, posTrack1);
                Instantiate(nightTrack, posTrack2);
            }
            else
            {
                meshRenderer.material = playerSO.AneOld.Material;
                isDaySky = false;
                RenderSettings.skybox = nightSky;
                Instantiate(dayTrack, posTrack1);
                Instantiate(dayTrack, posTrack2);
            }
        }
        else
        {
            meshRenderer.material = playerSO.AneYoung.Material;
            isDaySky = true;
            RenderSettings.skybox = daySky;
            Instantiate(nightTrack, posTrack1);
            Instantiate(dayTrack, posTrack2);
        }
    }

    public void ChangeTheme()
    {
        if (isDaySky)
        {
            RenderSettings.skybox = nightSky;
            meshRenderer.material = playerSO.AneOld.Material;
        }
        else
        {
            RenderSettings.skybox = daySky;
            meshRenderer.material = playerSO.AneYoung.Material;
        }

        isDaySky = !isDaySky;
    }

    public bool IsLevelFinish()
    {
        return !isDaySky;
    }
}
