using System;
using UnityEngine;

namespace Managers
{
    public class CursorManager : MonoBehaviour
    {
        private void Start()
        {
            // 隐藏鼠标
            Cursor.visible = false;
        }
    }
}