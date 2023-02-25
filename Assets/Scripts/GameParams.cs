using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameParams : Singleton<GameParams>
    {
        AudioSource audioData;

        public const int CLICKS_TO_DISAPPEAR_DOOR = 12;
        public const int CLICKS_AVAILABLE = 100;
        public enum EXPERIMENT { EXP1 = 0, EXP2 = 1, EXP3 = 2 }

        public EXPERIMENT ExperimentMode;

        public bool MustReduceDoors = false;
        public bool CanReviveDoors = false;

        protected override void Awake()
        {
            base.Awake();
            audioData = GetComponent<AudioSource>();
        }

        public void SetExperiment(EXPERIMENT e)
        {
            ExperimentMode = e;
            if (e == EXPERIMENT.EXP1)
            {
                MustReduceDoors = false;
                CanReviveDoors = false;
            } else if (e == EXPERIMENT.EXP2)
            {
                MustReduceDoors = true;
                CanReviveDoors = false;
            } else
            {
                MustReduceDoors = true;
                CanReviveDoors = true;
            }
        }

        public void PlayChangeSceneSound()
        {
            audioData.Play(0);
        }
    }
}