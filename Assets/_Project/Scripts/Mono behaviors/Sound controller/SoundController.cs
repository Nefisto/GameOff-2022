using System;
using NTools;
using Sirenix.OdinInspector;
using UnityEngine;
using EventHandler = NTools.EventHandler;

public class SoundController : MonoBehaviour
{
    [Title("Sounds")]
    [SerializeField]
    private AudioClip success;
        
    [SerializeField]
    private AudioClip fail;

    [SerializeField]
    private AudioEvent successfullyDig;

    [SerializeField]
    private AudioClip enteringHouse;

    [SerializeField]
    private AudioClip exitingHouse;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        EventHandler.RegisterEvent<string>(GameEventsNames.SUCCESSFULLY_BREW, OnSuccessfullyBrew);
        EventHandler.RegisterEvent(GameEventsNames.FAILED_TO_BREW, () => audioSource.PlayOneShot(fail));
        EventHandler.RegisterEvent(GameEventsNames.SUCCESSFULLY_COLLECT_FLOWER, () => successfullyDig.Play(audioSource));
        
        EventHandler.RegisterEvent(GameEventsNames.OPEN_CRAFT_HUD, () => audioSource.PlayOneShot(enteringHouse));
        EventHandler.RegisterEvent(GameEventsNames.CLOSE_CRAFT_HUD, () => audioSource.PlayOneShot(exitingHouse));
    }

    private void OnSuccessfullyBrew(string potionName)
        => audioSource.PlayOneShot(success);
}