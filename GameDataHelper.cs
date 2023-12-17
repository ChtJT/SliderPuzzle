using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SliderPuzzleGameExtension
{
    public static class GameDataHelper
    {
        public static GameState LoadGameState(string difficultyLevel, string filePath)
        {
            GameStates gameStates = LoadGameStates(filePath);
            return gameStates?.States.FirstOrDefault(s => s.DifficultyLevel == difficultyLevel);
        }

        private static GameStates LoadGameStates(string filePath)
        {
            if (!File.Exists(filePath)) return null;

            XmlSerializer serializer = new XmlSerializer(typeof(GameStates));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return serializer.Deserialize(fileStream) as GameStates;
            }
        }

        public static void SaveGameState(GameState gameState, string filePath)
        {
            GameStates gameStates = File.Exists(filePath) ? LoadGameStates(filePath) : new GameStates();

            // 添加新状态
            gameStates.States.Add(gameState);

            // 保存更新后的状态列表
            XmlSerializer serializer = new XmlSerializer(typeof(GameStates));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fileStream, gameStates);
            }
        }

    }
}
