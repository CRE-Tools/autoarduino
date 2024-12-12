using System.IO;
using UnityEditor;
using UnityEngine;

namespace hrs.autoarduino.runtime
{
    [InitializeOnLoad]
    public class AutoConfig
    {
        const string _path = "Assets/AutoArduino/";
        const string _file = _path + "ArduinoInput.cs";
        const string _baseFile = "Packages/com.hrs.autoarduino/Scripts/ArduinoInputBase.txt";

        static ApiCompatibilityLevel _compatibilityLevel;
        static BuildTargetGroup _buildTargetGroup;


        static AutoConfig()
        {
            //return;

            _buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            _compatibilityLevel = PlayerSettings.GetApiCompatibilityLevel(_buildTargetGroup);

            bool newApi = SetApiCompatibilityLevel();
            bool newDirectory = CreateDirectory();
            bool newFile = CreateFile();

            if (newFile)
                AssetDatabase.Refresh();
        }

        static bool SetApiCompatibilityLevel()
        {
            if (_compatibilityLevel != ApiCompatibilityLevel.NET_Unity_4_8)
            {
                PlayerSettings.SetApiCompatibilityLevel(_buildTargetGroup, ApiCompatibilityLevel.NET_Unity_4_8);
                return true;
            }
            return false;
        }

        static bool CreateDirectory()
        {
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
                return true;
            }
            return false;
        }

        static bool CreateFile()
        {
            if (!File.Exists(_file))
            {
                File.WriteAllText(_file,
                    ((TextAsset)AssetDatabase.LoadAssetAtPath(_baseFile, typeof(TextAsset))).text
                    );
                return true;
            }
            return false;
        }
    }
}
