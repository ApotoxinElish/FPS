using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class GlobalManager : MonoBehaviour
    {
        // global variable management
        public GameObject mainPlayer;  // the main player
        public int mainPlayerInstanceID;  // the ID of the main player
        
        public bool isGamePaused = false;  // is the game paused

        public static GlobalManager Instance { get; private set; }

        // keyboard key binding
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
            // reset the keyboard key binding
            KeyBinding[keyName] = keyCode;
        }

        private void Awake()
        {
            Instance = this;
            mainPlayer = GameObject.Find("Player");  // get the main player game object (must be in the hierarchy)
            mainPlayerInstanceID = mainPlayer.GetInstanceID();
        }

        public void GamePause()
        {
            // pause the game
            isGamePaused = true;
        }
    
        public void GameResume()
        {
            // resume the game
            isGamePaused = false;
        }

        // private void Update()
        // {
        //     // press "Esc" button to pause the game
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