using UnityEngine;
using System.IO;
using Data;
using Cysharp.Threading.Tasks;
using System;

namespace UnityUtils.Core.DataManagment
{
    public class DataWriterAndReader<T>
    {
        public string SaveDataDirectoryPath = "";
        public string SaveFileName = "";

        public DataWriterAndReader(string DataDirectoryPath, string DataFileName)
        {
            SaveDataDirectoryPath = DataDirectoryPath;
            SaveFileName = DataFileName;
        }

        public async UniTask<T> InitializeDataFile(Func<object> CreateNewDataObject)
        {
            T dataToProcess = default;
            string loadPath = Path.Combine(SaveDataDirectoryPath, SaveFileName);
            Debug.Log("load path : " + loadPath);

            if (File.Exists(loadPath))
            {
                try
                {
                    string dataToLoad = "";

                    using (FileStream stream = new FileStream(loadPath, FileMode.Open))
                    {
                        using (StreamReader fileReader = new StreamReader(stream))
                        {
                            dataToLoad = await fileReader.ReadToEndAsync();
                        }
                    }

                    dataToProcess = JsonUtility.FromJson<T>(dataToLoad);
                    return dataToProcess;
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            else
            {
                object data = CreateNewDataObject?.Invoke();

                if (data is T convertedData)
                {
                    dataToProcess = convertedData;
                }

                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(loadPath));
                    Debug.Log("Creating Save File At: " + loadPath);

                    string dataToStore = JsonUtility.ToJson(dataToProcess, true);

                    using (FileStream stream = new FileStream(loadPath, FileMode.Create))
                    {
                        using (StreamWriter fileWriter = new StreamWriter(stream))
                        {
                            await fileWriter.WriteAsync(dataToStore);
                        }
                    }
                }
                catch (System.Exception ex)
                {

                    Debug.LogError("GAME NOT SAVED" + loadPath + "" + ex.Message);
                }
                return dataToProcess;
            }

        }

        public void UpdateDataFile(T data)
        {
            string savePath = Path.Combine(SaveDataDirectoryPath, SaveFileName);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                Debug.Log("Updating Save File At: " + savePath);

                string dataToStore = JsonUtility.ToJson(data, true);

                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    using (StreamWriter fileWriter = new StreamWriter(stream))
                    {
                        fileWriter.Write(dataToStore);
                    }
                }
            }
            catch (System.Exception ex)
            {

                Debug.LogError("GAME NOT SAVED" + savePath + "" + ex.Message);
            }

        }
    }
}
