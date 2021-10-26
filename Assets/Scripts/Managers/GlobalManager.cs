using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class GlobalManager : MonoBehaviour
    {
        // 该类管理全局变量
        public GameObject mainPlayer;  // 主玩家
        public int mainPlayerInstanceID;  // 主玩家ID
        
        public bool isGamePaused = false;  // 游戏是否暂停

        public static GlobalManager Instance { get; private set; }

        // 键盘键位绑定
        public Dictionary<string, KeyCode> KeyBinding = new Dictionary<string, KeyCode>
        {
            {"player move forward", KeyCode.W},
            {"player move backward", KeyCode.S},
            {"player move left", KeyCode.A},
            {"player move right", KeyCode.D},
            {"player jump", KeyCode.Space},
            {"player dodge", KeyCode.LeftShift},
        };

        public void ResetKeyBinding(string keyName, KeyCode keyCode)
        {
            // 重置按键
            KeyBinding[keyName] = keyCode;
        }

        private void Awake()
        {
            // 需放在Awake()里，其他脚本Start()会调用该Instance
            Instance = this;
            mainPlayer = GameObject.Find("Player");  // 绑定主玩家
            mainPlayerInstanceID = mainPlayer.GetInstanceID();
        }

        public void GamePause()
        {
            // 暂停游戏
            isGamePaused = true;
        }
    
        public void GameResume()
        {
            // 恢复游戏
            isGamePaused = false;
        }

        // private void Update()
        // {
        //     // 检测游戏暂停
        //     if (!Input.GetKeyDown(KeyCode.Escape)) return;
        //     if (isGamePaused)
        //     {
        //         GameResume();
        //     }
        //     else
        //     {
        //         GamePause();
        //     }
        // }
    }
}