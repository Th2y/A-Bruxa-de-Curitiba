using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider sliderEffects;
    [SerializeField] private Slider sliderMusics;

    private float volumeEffects;
    private float volumeMusics;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(Constants.EffectsPref))
            sliderEffects.value = 1;
        else
            sliderEffects.value = PlayerPrefs.GetFloat(Constants.EffectsPref);

        if (!PlayerPrefs.HasKey(Constants.MusicPref))
            sliderMusics.value = 1;
        else
            sliderMusics.value = PlayerPrefs.GetFloat(Constants.MusicPref);
    }

    public void VolumeEffects(float volume)
    {
        volumeEffects = volume;
        GameObject[] efeito = GameObject.FindGameObjectsWithTag(Constants.EffectsTag);
        if (efeito.Length > 0)
        {
            for (int i = 0; i < efeito.Length; i++)
            {
                efeito[i].GetComponent<AudioSource>().volume = volumeEffects;
            }
        }

        PlayerPrefs.SetFloat(Constants.EffectsPref, volumeEffects);
    }

    public void VolumeMusics(float volume)
    {
        volumeMusics = volume;
        GameObject[] musica = GameObject.FindGameObjectsWithTag(Constants.MusicTag);
        if (musica.Length > 0)
        {
            for (int i = 0; i < musica.Length; i++)
            {
                musica[i].GetComponent<AudioSource>().volume = volumeMusics;
            }
        }

        PlayerPrefs.SetFloat(Constants.MusicPref, volumeMusics);
    }
}