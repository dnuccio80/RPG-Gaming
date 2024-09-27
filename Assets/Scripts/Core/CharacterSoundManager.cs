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
            EmitClip(dieSoundArray[GetRandomNumber(damageTakenSoundArray.Length)]);
        }

        private void Health_OnDamageTaken(object sender, Health.OnDamageTakenEventArgs e)
        {
            EmitClip(damageTakenSoundArray[GetRandomNumber(damageTakenSoundArray.Length)]);
        }

        private void EmitClip(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }

        private int GetRandomNumber(int length)
        {
            return UnityEngine.Random.Range(0, length);
        }
    }

}