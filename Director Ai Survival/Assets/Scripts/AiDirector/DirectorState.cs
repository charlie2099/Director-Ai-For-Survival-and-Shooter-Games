namespace AiDirector
{
    public class DirectorState
    {
        private Tempo _currentTempo;
        private float _buildUpDuration;
        private float _peakDuration;
        private float _respiteDuration;
        
        public enum Tempo
        {
            BuildUp,
            Peal,
            PeakFade,
            Respite
        }

        public Tempo GetTempo()
        {
            return _currentTempo;
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