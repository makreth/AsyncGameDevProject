using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance; // singleton, preserve on scene change
	public Sound[] sounds;

	private void Awake()
	{
		// singleton
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}

		DontDestroyOnLoad(gameObject);

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	private void Start()
	{
		Play("Theme");

		// Immediately start and stop movement sound so pausing is done more easily later
		Play("Drone Move");
		Pause("Drone Move", true);
	}

	public void Play(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);

		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found");
			return;
		}

		s.source.Play();
	}

	public void Stop(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);

		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found");
			return;
		}

		s.source.Stop();
	}

	public void Pause(string name, bool pause)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);

		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found");
			return;
		}

		if (pause)
			s.source.Pause();
		else
			s.source.UnPause();
	}

	public bool IsPlaying(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);

		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found");
			throw new Exception("Sound: " + name + " not found");
		}

		return s.source.isPlaying;
	}
}
