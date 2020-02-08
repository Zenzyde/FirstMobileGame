using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioOneshotPlayer : MonoBehaviour
{
	[SerializeField] private AudioClip sfxClip;
	[SerializeField] private LayerMask collisionLayer;
	[SerializeField] private EventTrigger eventTrigger;
	private AudioSource source;

	void Start()
	{
		source = GetComponent<AudioSource>();
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.layer == collisionLayer)
		{
			if (eventTrigger == EventTrigger.collisionEnter)
			{
				source.PlayOneShot(sfxClip);
			}
		}
	}

	void OnCollisionExit(Collision other)
	{
		if (other.gameObject.layer == collisionLayer)
		{
			if (eventTrigger == EventTrigger.collisionExit)
			{
				source.PlayOneShot(sfxClip);
			}
		}
	}

	public void SetVolume(float volume)
	{
		source.volume = volume;
	}

	public void SetMuted(bool muted)
	{
		source.mute = muted;
	}

	public bool GetIsMuted()
	{
		return source.mute;
	}
}

public enum EventTrigger
{
	collisionEnter, collisionExit
}