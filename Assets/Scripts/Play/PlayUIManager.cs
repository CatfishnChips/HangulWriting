using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayUIManager : MonoBehaviour
{
    [SerializeField] private List<ParallaxBackground> _parallaxEffects;
    [SerializeField] private GameObject _pausePanel, _winPanel;
    [SerializeField] private TextMeshProUGUI _reactionText, _pointsText;
    [SerializeField] private List<TextMeshProUGUI> _scoreTexts = new List<TextMeshProUGUI>();
    [SerializeField] [TextArea(1, 2)] private List<string> _positiveReactions = new List<string>();
    [SerializeField] [TextArea(1, 2)] private List<string> _negativeReactions = new List<string>();
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Scrollbar _healthBar;
    [SerializeField] private Image _handle;
    [SerializeField] private Color _high, _mid, _low, _last;
    [SerializeField] private Image _mascotImage;

    private void Start(){
        UpdateScene();
    }

    private void UpdateScene(){
        _pointsText.text = SceneManager.Instance.Points.ToString();
        UpdateMascot(SceneManager.Instance.MascotSprite);
    }

    public void Pause(){
        foreach(ParallaxBackground parallax in _parallaxEffects){
            parallax.Paused = true;
        }
        _pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume(){
        foreach(ParallaxBackground parallax in _parallaxEffects){
            parallax.Paused = false;
        }
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Finish(){
        foreach(ParallaxBackground parallax in _parallaxEffects){
            parallax.Paused = true;
        }
        _winPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void UpdateHealthBar(int value, int maxValue){
        float index = (float)value / (float)maxValue;
        _healthBar.size = index;

        Color color;
        if (index < 0.75f && index >= 0.50f){
            color = _mid;
        }
        else if (index < 0.50f && index >= 0.25f){
            color = _low;
        }
        else if (index < 0.35f && index >= 0f){
            color = _last;
        }
        else
        {
            color = _high;
        }
        _handle.color = color;

        _healthText.text = value.ToString();
    }

    public void UpdateReaction(bool value){
        int random = 0;
        string text = "";
        switch (value){
            // Positive Reaction
            case true:
                random = Random.Range(0, _positiveReactions.Count);
                text = _positiveReactions[random];
            break;

            // Negative Reaction
            case false:
                random = Random.Range(0, _negativeReactions.Count);
                text = _negativeReactions[random];
            break;
        }
        _reactionText.text = text;
    }

    public void UpdateScore(int value){
        foreach(TextMeshProUGUI text in _scoreTexts){
            text.text = value.ToString();
        }
        _pointsText.text = SceneManager.Instance.Points.ToString();
    }

    public void ReturnToMenu(){
        Time.timeScale = 1;
        SceneManager.Instance.LoadScene(0);
    }

    public void UpdateMascot(Sprite sprite){
        _mascotImage.sprite = sprite;
    }
}
