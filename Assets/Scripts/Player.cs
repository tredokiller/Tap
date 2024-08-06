using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    
    private bool _isMoving;

    private Transform _transform;
    
    public float CurrentSpeed
    {
        get => PlayerPrefs.GetFloat(SaveLabels.PlayerCurrentSpeedPrefName, MinSpeed);
        set => PlayerPrefs.SetFloat(SaveLabels.PlayerCurrentSpeedPrefName, value);
    }
    
    
    
    public readonly float MinSpeed = 10;
    public readonly float MaxSpeed = 20;
    
    private readonly Queue<Vector3> _targetPositions = new Queue<Vector3>();

    private void Awake()
    {
        Instance = this;
        
        _transform = transform;
    }

    private void OnEnable()
    {
        ControlsManager.OnTap += OnTap;
    }


    private void OnTap(Vector2 tapPosition)
    {
        if (!ControlsManager.IsPointerOverUI(tapPosition))
        {
            AddTapToQueue(tapPosition);
        }
    }
    
    private void AddTapToQueue(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        
        _targetPositions.Enqueue(ray.origin); 

        if (!_isMoving)
        {
            StartCoroutine(MoveToNextPosition());
        }
    }
    
    private IEnumerator MoveToNextPosition()
    {
        while (_targetPositions.Count > 0)
        {
            _isMoving = true;
            Vector3 targetPosition = _targetPositions.Dequeue();
            targetPosition.z = 0;

            while (Vector3.Distance(_transform.position, targetPosition) > 0.1f)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, targetPosition, CurrentSpeed * Time.deltaTime);
                yield return null;
            }
        }

        _isMoving = false;
    }

    private void OnDisable()
    {
        ControlsManager.OnTap -= OnTap;
    }
}
