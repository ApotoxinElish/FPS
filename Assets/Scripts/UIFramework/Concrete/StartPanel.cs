using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Start main panel
/// </summary>
public class StartPanel : BasePanel
{
    static readonly string path = "Prefabs/UI/Panel/StartPanel";

    public StartPanel() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("BtnSetting").onClick.AddListener(() =>
        {
            // Click events can be written in here
            Debug.Log("The More button was clicked");
            PanelManager.Push(new SettingPanel());
        });

        UITool.GetOrAddComponentInChildren<Button>("BtnNewGame").onClick.AddListener(() =>
        {
            SaveLoadManager.generateDefultJson();
            // Click events can be written in here
            GameRoot.Instance.SceneSystem.SetScene(new LevelScene("Level1"));
        });

        UITool.GetOrAddComponentInChildren<Button>("BtnContinue").onClick.AddListener(() =>
        {
            string levelName = SaveLoadManager.getLevelName();
            // Click events can be written in here
            GameRoot.Instance.SceneSystem.SetScene(new LevelScene(levelName));
        });
    }
}
