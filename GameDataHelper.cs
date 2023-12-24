using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace SliderPuzzleGameExtension
{
    public static class GameDataHelper
    {
        public static GameState LoadGameState(string difficultyLevel, string imagePath, string filePath)
        {
            GameStates gameStates = LoadGameStates(filePath);
            return gameStates?.States.FirstOrDefault(s => s.DifficultyLevel == difficultyLevel && s.ImagePath == imagePath);
        }

        private static GameStates LoadGameStates(string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GameStates));
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    return serializer.Deserialize(fileStream) as GameStates;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载游戏状态时发生错误: {ex.Message}");
                return new GameStates();
            }
        }

        public static void SaveGameState(GameState gameState, string filePath)
        {
            try {
                GameStates gameStates = File.Exists(filePath) ? LoadGameStates(filePath) : new GameStates();

                var existingState = gameStates.States.FirstOrDefault(s => s.DifficultyLevel == gameState.DifficultyLevel && s.ImagePath == gameState.ImagePath);
                if (existingState != null)
                {
                    // 如果已存在相同难度的状态，则更新该状态
                    gameStates.States.Remove(existingState);
                }

                // 添加新状态
                gameStates.States.Add(gameState);

                // 保存更新后的状态列表
                XmlSerializer serializer = new XmlSerializer(typeof(GameStates));
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    serializer.Serialize(fileStream, gameStates);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存游戏状态时发生错误: {ex.Message}");
            }
        }
    }
}
