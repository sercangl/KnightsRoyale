using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com
{
    public class MeleeWeapon : MonoBehaviour
{
    public int weapondmg = 100;
    public AudioClip VibrationAudioClip;
    public List<AudioClip> SwordCollisionAudioClips = new List<AudioClip>();
    public AudioSource CollisionAudioSourceSwo;
    public AudioSource CollisionAudioSourceHum;
    public ParticleSystem BloodParticle;

        void OnTriggerEnter(Collider other)
    {
            int randomClipNumber = Random.Range(0, SwordCollisionAudioClips.Count);
            CollisionAudioSourceSwo.clip = SwordCollisionAudioClips[randomClipNumber];

            if (other.gameObject.CompareTag("Sword"))
            {
                CollisionAudioSourceSwo.Stop();
                CollisionAudioSourceSwo.Play();
                BloodParticle.Stop();
                BloodParticle.Play();

                //Play Vibration
                //VibrateRightController();

            }
            if (other.gameObject.CompareTag("Player"))
            {
                CollisionAudioSourceHum.Stop();
                CollisionAudioSourceHum.Play();
                BloodParticle.Stop();
                BloodParticle.Play();

                //other.gameObject.GetComponent<PlayerBeh>().TakeDamage(weapondmg);

                //Play Vibration
                //VibrateRightController();
        }

    }
    public void VibrateRightController()
    {
        OVRHapticsClip hapticsClip = new OVRHapticsClip(VibrationAudioClip);
        OVRHaptics.RightChannel.Preempt(hapticsClip);
        OVRHaptics.LeftChannel.Preempt(hapticsClip);


    }
    }

}