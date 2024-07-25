using Gameplay;
using System;
using UnityEngine.UIElements;

public class HeroIconElement : VisualElement
{
    private TemplateContainer _heroIconTemplate;
    private Action<HeroIconElement> _callback;
    public ActorData actorData;

    public HeroIconElement(VisualElement rootElement, ActorData actorDataLink, Action<HeroIconElement> callback)
    {
        _callback = callback;
        actorData = actorDataLink;
        _heroIconTemplate = UserInterfaceShare.Instance.HeroIconTemplete.Instantiate();

        Add(_heroIconTemplate);
        rootElement.Add(this);

        RegisterCallback<MouseDownEvent>(OnMouseDown);
    }

    ~HeroIconElement()
    {
        UnregisterCallback<MouseDownEvent>(OnMouseDown);
    }

    private void OnMouseDown(MouseDownEvent mouseEvent)
    {
        if (mouseEvent.button == 0)
        {
            _callback?.Invoke(this);
        }
    }
}