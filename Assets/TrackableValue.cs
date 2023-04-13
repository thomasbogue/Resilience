using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrackableValue<T> : ScriptableObject {
    [SerializeField]
    private T _val;
    //public delegate void OnChange(T newValue);
    public UnityEvent onChange;
    public T val {
        get { return _val; } 
        set {
            _val = value;
            onChange.Invoke();
        } 
    }
}
