using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
//public class StrengthSelectorConfigurator : MonoBehaviour {
//    public Entity target;
//    public List<Strength> possibleStrengths = new List<Strength>();
//    public GameObject buttonPrefab;
//
//    public void Startup() {
//        if (buttonPrefab.GetComponent<Button>() == null) {
//            Debug.LogError("button prefab in StrengthSelectorConfigurator must have a BUTTON!  Come on people!");
//        }
//    }
//
//    public List<Strength> strengths { get {
//        List<Strength> availableStrengths = new List<Strength>();
//        foreach(Strength strength in possibleStrengths) {
//            if (strength.MeetsRequirements(target))
//                availableStrengths.Add(strength);
//        }
//        return availableStrengths;
//    } }
//
//    public void Setup() {
//        List<Strength> availableStrengths = strengths;
//        int strengthNum = 0;
//        int numStrengths = availableStrengths.length;
//        float spacing = 1.0f / (float)numStrengths;
//        foreach(Strength strength in availableStrengths) {
//            GameObject newButton = Instantiate(buttonPrefab, transform);
//            Button buttonComponent = newButton.GetComponent<Button>();
//        }
//    }
//}
