using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SliderPuzzleGameExtension
{
    public class GameDataManager
    {
        //public void SaveGameState(GameState gameState)
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(GameState));
        //    using (StreamWriter writer = new StreamWriter($"gameSave_{gameState.Difficulty}.xml"))
        //    {
        //        serializer.Serialize(writer, gameState);
        //    }
        //}

        //public GameState LoadGameState(string difficulty)
        //{
        //    string fileName = $"gameSave_{difficulty}.xml";
        //    if (File.Exists(fileName))
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(GameState));
        //        using (StreamReader reader = new StreamReader(fileName))
        //        {
        //            return (GameState)serializer.Deserialize(reader);
        //        }
        //    }
        //    return null; // 如果没有保存文件，返回 null
        //}
    }

}
