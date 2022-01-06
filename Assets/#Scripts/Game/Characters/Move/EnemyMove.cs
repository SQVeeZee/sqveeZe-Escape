using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : BaseCharacterMove
{
    public override void MoveAfterPathCompleted(Transform finalTransform, Action callback)
    {
        base.MoveAfterPathCompleted(finalTransform, callback);

        _characterTransform.DOJump(finalTransform.position, 5, 1, 3).OnComplete(delegate { callback?.Invoke(); });
    }
}
