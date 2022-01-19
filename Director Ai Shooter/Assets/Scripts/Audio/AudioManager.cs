using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance; 
	public AudioMixerGroup mixerGroup;
	public Sound[] sounds;

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

	private void OnEnable()
    {
	    EventManager.StartListening("GunFired", Play);
	    EventManager.StartListening("GeneratorActivating", Play);
	    EventManager.StartListening("GeneratorOnline", Play);
	    EventManager.StartListening("GeneratorStopped", Stop);
    }

    private void OnDisable()
    {
	    EventManager.StopListening("GunFired", Play);
	    EventManager.StopListening("GeneratorActivating", Play);
	    EventManager.StopListening("GeneratorOnline", Play);
	    EventManager.StopListening("GeneratorStopped", Stop);
	}

    private void OnApplicationQuit()
    {
		Destroy(this);
		EventManager.StopListening("GunFired", Play);
		EventManager.StopListening("GeneratorActivating", Play);
		EventManager.StopListening("GeneratorOnline", Play);
		EventManager.StopListening("GeneratorStopped", Stop);
	}

	public void Play(EventParam eventParam)
	{
		Sound s = Array.Find(sounds, item => item.name == eventParam.soundstr_);
		if (s == null) 
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public void Stop(EventParam eventParam)
	{
		Sound s = Array.Find(sounds, item => item.name == eventParam.soundstr_);
		if (s == null) 
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.Stop();
	}
}
