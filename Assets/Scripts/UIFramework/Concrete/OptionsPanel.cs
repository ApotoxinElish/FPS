using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : BasePanel
{
    static readonly string path = "Prefabs/UI/Panel/OptionsPanel";

    public OptionsPanel() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(() =>
        {
            PanelManager.Pop();
        });
    }
}
