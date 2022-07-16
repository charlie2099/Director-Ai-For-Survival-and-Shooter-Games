using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AiDirector.Scripts
{
    public class DirectorDebug : MonoBehaviour
    {
        [SerializeField] private Text enemyPopCountText;
        [SerializeField] private Text enemySpawnTimeText;
        [SerializeField] private Text directorStateText;
        [SerializeField] private Text timeSpentInPeakText;
        [SerializeField] private Text timeSpentInRespiteText;
        [SerializeField] private Text perceivedIntensityText;
        [SerializeField] private Text elapsedTimeText;

        [Space]
        [SerializeField] private UnityEvent unityEvent;

        private void Update()
        {
            enemyPopCountText.text      = "Enemy Population: "    + Director.Instance.GetEnemyPopulationCount();
            enemySpawnTimeText.text     = "Enemy Spawn Timer: "   + "";
            directorStateText.text      = "Director State: "      + Director.Instance.GetDirectorState().CurrentTempo;
            timeSpentInPeakText.text    = "Peak Duration: "       + Director.Instance.GetPeakDuration().ToString("F2");
            timeSpentInRespiteText.text = "Respite Duration: "    + Director.Instance.GetRespiteDuration().ToString("F2");
            perceivedIntensityText.text = "Perceived Intensity: " + Director.Instance.GetPerceivedIntensity().ToString("F2");
            elapsedTimeText.text        = "Elapsed Time: "        + Time.time.ToString("F2");
        }
    }
}

