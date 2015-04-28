using System;

namespace Zuma.GameEngine
{
    public class Game
    {
        #region                        - Fields

        private GameConfig _config = null;
        // Об'єкт даного класу не є створений за допомогою оператора new.
        private Frog _frog;
        private IField _field;
        private PointF[] _bonusLocations;
        private IPath _path;

        private GameStatus _status;
        private GameResult _gameResult;
        // Дане поле ініціалізоване, проте ніколи не використовується.
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
                if (this._status != GameStatus.Over)
                {
                    return GameResult.Undetermined;
                }
                return this._gameResult;
            }
        }

        #endregion


        #region                        - Constructors

        public Game()
        {
            this._status = GameStatus.Created;
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
            // Пропущено фігурні дужки.
            // Варто використовувати ключове слово this.
            if (this._status == GameStatus.Playing || this._status == GameStatus.Paused)
            {
                throw new InvalidOperationException("Game is now playing or paused.");
            }

            if (_config == null)
            {
                _config = new GameConfig();
            }

            _config.Load(configPath);

            _frog.Location = _config.FrogLocation;
            _field = _config.Field;
            _bonusLocations = _config.BonusLocations;
            _path = _config.Path;

            _status = GameStatus.Initialized;
        }

        public void Play()
        {
            if (this._status != GameStatus.Initialized)
            {
                throw new InvalidOperationException("Game is not initialized or already playing.");
            }

            this._initSequenceStarted = false;
            // Зайві пропуски.

        }

        public void Pause()
        {
            // Пропущена реалізація метода.
        }

        #endregion


        #region                        - Helper Methods
        // Непотрібні методи.
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
