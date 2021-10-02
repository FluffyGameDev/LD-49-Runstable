using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class LevelSaveData
{
	private static readonly string K_FILENAME = "/LevelSave.dat";

	private int m_UnlockedLevelCount = 1;
	public int UnlockedLevelCount
    {
        get { return m_UnlockedLevelCount; }
        set { m_UnlockedLevelCount = value; }
    }

	public static void SaveData(LevelSaveData data)
	{
		using (FileStream file = File.Create(Application.persistentDataPath + K_FILENAME))
		{
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(file, data);
		}
	}

	public static LevelSaveData LoadData()
	{
		LevelSaveData data = null;
		if (File.Exists(Application.persistentDataPath + K_FILENAME))
		{
			using (FileStream file = File.OpenRead(Application.persistentDataPath + K_FILENAME))
			{
				BinaryFormatter bf = new BinaryFormatter();
				data = (LevelSaveData)bf.Deserialize(file);
			}
		}

		if (data == null) { data = new LevelSaveData(); }

		return data;
	}
}