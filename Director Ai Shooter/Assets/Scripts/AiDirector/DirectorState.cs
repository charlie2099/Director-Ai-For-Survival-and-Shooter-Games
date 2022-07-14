using System;

namespace AiDirector
{
    public class DirectorState
    {
        public Action OnTempoChange;
        
        private Tempo _currentTempo;
        private float _buildUpDuration;
        private float _peakDuration;
        private float _respiteDuration;

        public Tempo CurrentTempo
        {
            get => _currentTempo;
            set { _currentTempo = value; OnTempoChange.Invoke(); }
        }

        public enum Tempo
        {
            BuildUp,
            Peak,
            PeakFade,
            Respite
        }

        public float GetBuildUpDuration()
        {
            return _buildUpDuration;
        }
        
        public float GetPeakDuration()
        {
            return _peakDuration;
        }
        
        public float GetRespiteDuration()
        {
            return _respiteDuration;
        }
    }
}