using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    private     static readonly string      BackgroundPref = "BackgroundPref";
    private     static readonly string      SoundEffectPref = "SoundEffectPref";

    private     float                       backgroundSoundValue, soundEffectValue;

    public      AudioSource                 backgroundClip;
    public      AudioSource[]               soundEffectsClip;

    private MenuController menucontroller;
    private bool isPlaying;

    // Start is called before the first frame update
    void Awake()
    {
        ContinueSettings();
        menucontroller = FindObjectOfType(typeof(MenuController)) as MenuController;
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
        backgroundSoundValue = PlayerPrefs.GetFloat(BackgroundPref);
        soundEffectValue = PlayerPrefs.GetFloat(SoundEffectPref);

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
