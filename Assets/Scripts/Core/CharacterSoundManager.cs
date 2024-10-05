using RPG.Resources;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RPG.Core
{
    public class CharacterSoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip[] damageTakenSoundArray;
        [SerializeField] private AudioClip[] dieSoundArray;
        [SerializeField] private AudioClip[] footSoundArray;

        private Health health;
        private AudioSource audioSource;

        private void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            health.OnDamageTaken += Health_OnDamageTaken;
            health.OnDead += Health_OnDead;
        }

        private void Health_OnDead(object sender, EventArgs e)
        {
            EmitClip(dieSoundArray[GetRandomNumber(damageTakenSoundArray.Length)], 1f);
        }

        private void Health_OnDamageTaken(object sender, Health.OnDamageTakenEventArgs e)
        {
            EmitClip(damageTakenSoundArray[GetRandomNumber(damageTakenSoundArray.Length)], 1f);
        }

        private void EmitClip(AudioClip clip, float volume)
        {
            audioSource.PlayOneShot(clip, volume);
        }
        private void EmitFootSound()
        {
            EmitClip(footSoundArray[GetRandomNumber(footSoundArray.Length)], .1f);

        }

        private int GetRandomNumber(int length)
        {
            return UnityEngine.Random.Range(0, length);
        }

    }

}