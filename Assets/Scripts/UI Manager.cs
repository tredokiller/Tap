using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider speedSlider;
    
    private Player _player;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        speedSlider.onValueChanged.AddListener(UpdateSpeed);
    }
    
    private void Initialize() 
    {
        _player = Player.Instance;

        speedSlider.minValue = _player.MinSpeed;
        speedSlider.maxValue = _player.MaxSpeed;

        speedSlider.value = _player.CurrentSpeed;
    }
    
    private void UpdateSpeed(float speed)
    {
        _player.CurrentSpeed = speed;
    }

    private void OnDisable()
    {
        speedSlider.onValueChanged.RemoveListener(UpdateSpeed); 
    }
}
