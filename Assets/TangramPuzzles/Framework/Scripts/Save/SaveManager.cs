using MirraGames.SDK;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using YG;

namespace BizzyBeeGames
{
    public class SaveManager : SingletonComponent<SaveManager>
    {
        #region Member Variables

        private const int key = 546;

        private List<ISaveable> saveables;
        private JSONNode loadedSave;

        #endregion

        #region Properties

        public string SaveFilePath { get { return Application.persistentDataPath + "/save.txt"; } }

        private List<ISaveable> Saveables
        {
            get
            {
                if (saveables == null)
                {
                    saveables = new List<ISaveable>();
                }

                return saveables;
            }
        }

        #endregion

        #region Unity Methods

        private void Start()
        {
            Debug.Log("Save file path: " + SaveFilePath);
        }

        private void OnDestroy()
        {
            Save();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                Save();
            }
        }

        #endregion

        #region Public Methods

        public void Register(ISaveable saveable)
        {
            Saveables.Add(saveable);
        }

        public JSONNode LoadSave(ISaveable saveable)
        {
            return LoadSave(saveable.SaveId);
        }

        public JSONNode LoadSave(string saveId)
        {
            if (loadedSave == null && !LoadSave(out loadedSave))
            {
                return null;
            }

            if (!loadedSave.AsObject.HasKey(saveId))
            {
                return null;
            }

            return loadedSave[saveId];
        }

#if UNITY_EDITOR

        public static void PrintSaveDataToConsole()
        {
            if (!System.IO.File.Exists(Instance.SaveFilePath))
            {
                UnityEditor.EditorUtility.DisplayDialog("Delete Save File", "There is no save file.", "Ok");
                return;
            }

            string contents = Utilities.EncryptDecrypt(System.IO.File.ReadAllText(Instance.SaveFilePath), key);

            Debug.Log(contents);
        }

#endif

        #endregion

        #region Private Methods

        public void Save()
        {
            Dictionary<string, object> saveJson = new Dictionary<string, object>();

            for (int i = 0; i < saveables.Count; i++)
            {
                saveJson.Add(saveables[i].SaveId, saveables[i].Save());
            }

            string jsonStr = Utilities.ConvertToJsonString(saveJson);
            string encryptedJsonStr = Utilities.EncryptDecrypt(jsonStr, key);

            MirraSDK.Data.SetString("save", jsonStr);
            MirraSDK.Data.Save();
            // Save to local file
           /* System.IO.File.WriteAllText(SaveFilePath, encryptedJsonStr);
            
            // Save to PlayerPrefs (acting as cloud storage)
            YandexGame.savesData.SaveData = jsonStr;
            YandexGame.SaveProgress();*/
        }

        private bool LoadSave(out JSONNode json)
        {
            json = null;

            // First attempt to load from PlayerPrefs
            if (MirraSDK.Data.HasKey("save"))
            {
                string jsonStr = MirraSDK.Data.GetString("save");
                json = JSON.Parse(jsonStr);

                if (json != null)
                {
                    return true;
                }
            }
            return false;           
        }

        #endregion
    }
}