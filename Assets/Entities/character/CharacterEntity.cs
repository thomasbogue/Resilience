using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity : Entity {
    public TrackableFloat trackableWounds;

    void Start() {
        trackableWounds.val = wounds;
    }

    public override void Wound(float amount) {
        wounds += amount;
        trackableWounds.val = wounds;
    }
}
