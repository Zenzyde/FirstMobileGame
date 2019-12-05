using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioLoopPlayer : MonoBehaviour
{
	[SerializeField] AudioClip clip;
	private AudioSource source;

	void Start()
	{
		source = GetComponent<AudioSource>();
		source.clip = clip;
		StartCoroutine(PlayMusic());
	}

	IEnumerator PlayMusic()
	{
		source.Play();
		while (true)
		{
			if (!source.isPlaying)
			{
				source.Play();
				yield return null;
			}
		}
	}
}