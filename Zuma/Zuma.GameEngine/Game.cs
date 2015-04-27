using System;

namespace Zuma.GameEngine
{
    public class Game
    {
        #region                        - Fields

        private GameConfig _config = null;

        private Frog _frog;
        private IField _field;
        private PointF[] _bonusLocations;
        private IPath _path;

        private GameStatus _status;
        private GameResult _gameResult;

        private bool _initSequenceStarted;

        #endregion


        #region                        - Properties

        public GameStatus Status
        {
            get { return _status; }
        }

        public GameResult GameResult
        {
            get
            {
                if (_status != GameStatus.Over)
                    return GameResult.Undetermined;
                return _gameResult;
            }
        }

        #endregion


        #region                        - Constructors

        public Game()
        {
            _status = GameStatus.Created;
        }

        public Game(string configPath)
            : this()
        {
            LoadConfig(configPath);
        }

        #endregion


        #region                        - Public Methods

        public void LoadConfig(string configPath)
        {
            if (_status == GameStatus.Playing || _status == GameStatus.Paused)
                throw new InvalidOperationException("Game is now playing or paused.");

            if (_config == null)
                _config = new GameConfig();
            
            _config.Load(configPath);

            _frog.Location = _config.FrogLocation;
            _field = _config.Field;
            _bonusLocations = _config.BonusLocations;
            _path = _config.Path;

            _status = GameStatus.Initialized;
        }

        public void Play()
        {
            if (_status != GameStatus.Initialized)
                throw new InvalidOperationException("Game is not initialized or already playing.");

            _initSequenceStarted = false;


        }

        public void Pause()
        {

        }

        #endregion


        #region                        - Helper Methods
        
        private void GameLyfeCycle()
        {

        }

        private void StartLifeCycleTimer()
        {

        }

        #endregion
    }

    public enum GameStatus
    {
        Created,
        Initialized,
        Playing,
        Paused,
        Over
    }

    public enum GameResult
    {
        Undetermined,
        Win,
        Defeat
    }
}
