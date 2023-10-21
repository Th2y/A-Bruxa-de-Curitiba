using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundClip;
    [SerializeField] private AudioSource[] soundEffectsClip;

    private float backgroundSoundValue;
    private float soundEffectValue;

    private bool isPlaying;

    private void Awake()
    {
        ContinueSettings();
    }

    public void PauseMusic()
    {
        if(isPlaying)
        {
            backgroundClip.Play();
            isPlaying = false;
        }else{
            backgroundClip.Pause();
            isPlaying = true;
        }

    }

    private void ContinueSettings()
    {
        backgroundSoundValue = PlayerPrefs.GetFloat(Constants.MusicPref);
        soundEffectValue = PlayerPrefs.GetFloat(Constants.EffectsPref);

        backgroundClip.volume = backgroundSoundValue;

        if(soundEffectsClip != null)
        {
            for(int i = 0; i < soundEffectsClip.Length; i++)
            {
                soundEffectsClip[i].volume = soundEffectValue;
            }
        }
    }
}
