using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
	[SerializeField] AudioMixer audioMixer;

    public void SetFullscreen(bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}

	public void SetMotionBlur(bool isMotionBlur)
	{

	}

	public void SetBloom(bool isBloom)
	{

	}

	public void SetQuality(int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}

	public void SetMasterVolume(float volume)
	{
		//audioMixer.SetFloat("MasterVolume", volume);
		audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
	}
	
	public void SetBGMVolume(float volume)
	{
		//audioMixer.SetFloat("BGMVolume", volume);
		audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
	}
	
	public void SetGameSFXVolume(float volume)
	{
		//audioMixer.SetFloat("GameSFXVolume", volume);
		audioMixer.SetFloat("GameSFXVolume", Mathf.Log10(volume) * 20);
	}
	
	public void SetUISFXVolume(float volume)
	{
		//audioMixer.SetFloat("UISFXVolume", volume);
		audioMixer.SetFloat("UISFXVolume", Mathf.Log10(volume) * 20);
	}
}
