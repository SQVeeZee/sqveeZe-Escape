using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : BaseCharacterMove
{
    public override void MoveAfterPathCompleted(Transform finalTransform, Action callback)
    {
        base.MoveAfterPathCompleted(finalTransform, callback);

        _characterTransform.DOJump(finalTransform.position, 1, 1, 1).OnComplete(delegate { callback?.Invoke(); });
    }
}
