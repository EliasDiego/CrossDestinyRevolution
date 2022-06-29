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
		audioMixer.SetFloat("MasterVolume", volume);
	}
	
	public void SetBGMVolume(float volume)
	{
		audioMixer.SetFloat("BGMVolume", volume);
	}
	
	public void SetGameSFXVolume(float volume)
	{
		audioMixer.SetFloat("GameSFXVolume", volume);
	}
	
	public void SetUISFXVolume(float volume)
	{
		audioMixer.SetFloat("UISFXVolume", volume);
	}
}
