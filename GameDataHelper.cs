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
        public static GameState LoadGameState(string filePath)
        {
            if (File.Exists(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GameState));
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return serializer.Deserialize(reader) as GameState;
                }
            }
            return null;
        }
    }
}
