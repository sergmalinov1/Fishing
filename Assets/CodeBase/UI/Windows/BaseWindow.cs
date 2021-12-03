﻿using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
  public abstract class BaseWindow : MonoBehaviour
  {
    
    public Button CloseButton;
    

    public void CloseWindow() => Destroy(gameObject);

    private void Awake() => 
      OnAwake();

    private void Start()
    {
      Initialize();
      SubscribeUpdate();
    }

    private void OnDestroy() => 
      Cleanup();

    protected virtual void OnAwake() => 
      CloseButton.onClick.AddListener(() => Destroy(gameObject));

    protected virtual void Initialize() {}

    protected virtual void SubscribeUpdate() {}
    protected virtual void Cleanup() {}
  }
}