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
        UITool.GetOrAddComponentInChildren<Button>("BtnMore").onClick.AddListener(() =>
        {
            // Click events can be written in here
            Debug.Log("The More button was clicked");
            PanelManager.Push(new OptionsPanel());
        });
        UITool.GetOrAddComponentInChildren<Button>("BtnNewGame").onClick.AddListener(() =>
        {
            // Click events can be written in here
            GameRoot.Instance.SceneSystem.SetScene(new MainScene());
        });
    }
}
