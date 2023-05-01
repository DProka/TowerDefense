using UnityEngine;
using TMPro;

public class UIWaveInfo : MonoBehaviour
{

    public TextMeshProUGUI text;

    private void Start()
    {
        GameController.gameController.OnWaveChange += OnWaveChanged;
    }

    void OnWaveChanged(int waveNumber)
    {
        if (GameController.gameController.waveNumber < 10)
        {
            waveNumber += 1;
            text.text = waveNumber.ToString() + " Wave";
        }
        else
        {
            text.text = waveNumber.ToString() + " Wave";
        }
    }
}
