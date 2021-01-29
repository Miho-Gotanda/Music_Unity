using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class GameManager : MonoBehaviour
{
    [SerializeField] string m_FilePath;

    [SerializeField] Button m_Play;
    [SerializeField] Button m_SetChart;

    [SerializeField] GameObject m_Don;
    [SerializeField] GameObject m_Ka;

    [SerializeField] Transform m_SpawnPoint;
    [SerializeField] Transform m_BeatPoint;

    private string m_Title;
    private int m_Bpm;
    private List<GameObject> m_Notes;

    private void OnEnable()
    {
        m_Play.onClick.
            AsObservable().Subscribe(_ => Play());
        m_SetChart.onClick.
            AsObservable().Subscribe(_ => LoadChart());
    }

    private void LoadChart()
    {
        m_Notes = new List<GameObject>();
        var jsonText = Resources.Load(m_FilePath).ToString();

        JsonNode json = JsonNode.Parse(jsonText);
        m_Title = json["title"].Get<string>();
        m_Bpm = int.Parse(json["bpm"].Get<string>());

        foreach(var note in json["notes"]){
            string type = note["type"].Get<string>();
            float timing = float.Parse(note["timing"].Get<string>());

            GameObject Note;
            if (type == "don")
                Note = Instantiate(m_Don, m_SpawnPoint.position, Quaternion.identity);
            else if (type == "ka")
                Note = Instantiate(m_Ka, m_SpawnPoint.position, Quaternion.identity);
            else
                Note = Instantiate(m_Don, m_SpawnPoint.position, Quaternion.identity);

            m_Notes.Add(Note);
        }
    }

    private void Play()
    {
        Debug.Log("Game Start");
    }
}