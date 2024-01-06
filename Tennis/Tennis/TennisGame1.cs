using System;

namespace Tennis
{
    public class TennisGame1 : ITennisGame
    {
        private int _score1 = 0;
        private int _score2 = 0;
        private readonly string _player1Name;
        private readonly string _player2Name;

        public TennisGame1(string player1Name, string player2Name)
        {
            _player1Name = player1Name;
            _player2Name = player2Name;
        }

        public void WonPoint(string playerName)
        {
            if (playerName == _player1Name)
            {
                _score1 += 1;
            }
            else if (playerName == _player2Name)
            {
                _score2 += 1;
            }
            else
            {
                throw new ArgumentOutOfRangeException($"There is no '{playerName}' player.");
            }
        }

        public string GetScore()
        {
            if (_score1 == _score2)
            {
                return GetTieScore();
            }

            if (_score1 >= 4 || _score2 >= 4)
            {
                return GetLateGameScore();
            }

            return $"{GetIndividualScoreName(_score1)}-{GetIndividualScoreName(_score2)}";
        }

        private static string GetIndividualScoreName(int score)
        {
            switch (score)
            {
                case 0:
                    return "Love";
                case 1:
                    return "Fifteen";
                case 2:
                    return "Thirty";
                case 3:
                    return "Forty";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string GetLateGameScore()
        {
            var scoreDifference = _score1 - _score2;
            if (scoreDifference == 1)
            {
                return $"Advantage {_player1Name}";
            }
            if (scoreDifference == -1)
            {
                return $"Advantage {_player2Name}";
            }
            if (scoreDifference >= 2)
            {
                return $"Win for {_player1Name}";
            }
            return $"Win for {_player2Name}";
        }

        private string GetTieScore()
        {
            switch (_score1)
            {
                case 0:
                    return "Love-All";
                case 1:
                    return "Fifteen-All";
                case 2:
                    return "Thirty-All";
                default:
                    return "Deuce";
            }
        }
    }
}

