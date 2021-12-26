﻿using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text _scoreTxt;
    private int _score;

    public int Score {
        get => _score;
        set {
            _score = value;
            _scoreTxt.text = _score.ToString("D4");
        }
    }
    
}
