using UnityEngine;
using System.Collections;

public class NormalMonster : Monster {
    protected override void Start() {
        base.Start();
        this.PerformMove();
    }
}
