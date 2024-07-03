using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource emptyMagazineSound1911;
    
    public AudioSource shootingChannel;
    // public AudioSource shootingSound1911;
    // public AudioSource shootingSoundM16;
    public AudioSource reloadingSoundM16;
    public AudioSource reloadingSound1911;

    public AudioClip P1911Shot;
    public AudioClip M16Shot;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayShootingSound(Weapon.WeaponModel weapon)
    {
        switch (weapon)
        {
            // Play() だと射撃音が連射の場合に次の射撃音にかき消されるため、PlayOneShot() を使い異なるチャンネルで再生できるようにする
            // それに応じて、shootingChannel に再生する音を設定し、内容は各 Clip から取得する
            case Weapon.WeaponModel.Pistol1911:
                shootingChannel.PlayOneShot(P1911Shot);
                break;
            case Weapon.WeaponModel.M16:
                shootingChannel.PlayOneShot(M16Shot);
                break;
        }
    }

    public void PlayReloadSound(Weapon.WeaponModel weapon)
    {
        switch (weapon)
        {
            case Weapon.WeaponModel.Pistol1911:
                reloadingSound1911.Play();
                break;
            case Weapon.WeaponModel.M16:
                reloadingSoundM16.Play();
                break;
        }
    }
}
