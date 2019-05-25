using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com
{

    public class kılıcustuses : MonoBehaviour
    {
        public AudioSource CollisionAudioSourceSwo;
        public AudioSource CollisionAudioSourceHum;
        public List<AudioClip> SwordCollisionAudioClips = new List<AudioClip>();
        public AudioClip VibrationAudioClip;
        public ParticleSystem BloodParticle;
        public ParticleSystem SwordParticle;



        void Start()
        {
            BloodParticle.Stop();
            SwordParticle.Stop();

        }

        void OnTriggerEnter(Collider other)
        {

            int randomClipNumber = Random.Range(0, SwordCollisionAudioClips.Count);
            CollisionAudioSourceSwo.clip = SwordCollisionAudioClips[randomClipNumber];

            if (other.gameObject.CompareTag("Sharp"))
            {
                CollisionAudioSourceSwo.Stop();
                CollisionAudioSourceSwo.Play();

                SwordParticle.Stop();
                SwordParticle.Play();

                VibrateRightController();


            }

            if (other.gameObject.CompareTag("Player"))
            {
                CollisionAudioSourceHum.Stop();
                CollisionAudioSourceHum.Play();

                BloodParticle.Stop();
                BloodParticle.Play();

                VibrateRightController();

            }
            /*
            if (other.gameObject.CompareTag("Particle"))
            {
                Partical_System.Play();

            }*/

        }

        public void VibrateRightController()
        {
            OVRHapticsClip hapticsClip = new OVRHapticsClip(VibrationAudioClip);
            OVRHaptics.RightChannel.Preempt(hapticsClip);
            OVRHaptics.LeftChannel.Preempt(hapticsClip);


        }

    }
}
