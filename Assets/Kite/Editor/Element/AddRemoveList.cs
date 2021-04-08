using Kite;
using System;
using System.Collections;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace KiteEditor
{
  public class AddRemoveList : VisualElement
  {
    private static readonly int itemHeight = 21;

    public string label;
    public string addItemLabel;
    public SerializedProperty serializedProperty;
    public Type itemType;

    public VisualElement content;
    public ListView listView;
    private ObjectField addItemField;

    public event Action<int> RemoveItemAction;
    public event Func<UnityEngine.Object, bool> CanBeAdded;
    public event Action<UnityEngine.Object> AddItemAction;

    public AddRemoveList(string label, SerializedProperty serializedProperty, ObjectField addItemField)
    {
      this.label = label;
      this.serializedProperty = serializedProperty;
      this.addItemField = addItemField;

      Add(GenerateTitle());
      Add(GenerateContent());
      RegisterCallback<ExecuteCommandEvent>(OnExecuteCommand);
    }

    private void OnExecuteCommand(ExecuteCommandEvent evt)
    {
      if (evt.commandName == "ObjectSelectorClosed" && addItemField != null)
      {
        UnityEngine.Object addItem = addItemField.value;
        if (addItem != null)
        {
          if (CanBeAdded?.Invoke(addItem) ?? true)
          {
            if (AddItemAction != null)
            {
              AddItemAction(addItem);
              serializedProperty.serializedObject.Update();
              UpdateListViewSize();
            }
            else
            {
              Undo.RecordObject(serializedProperty.serializedObject.targetObject, "Remove item");
              int newItemIndex = serializedProperty.arraySize;
              serializedProperty.InsertArrayElementAtIndex(newItemIndex);
              SerializedProperty itemProperty = serializedProperty.GetArrayElementAtIndex(newItemIndex);
              itemProperty.objectReferenceValue = addItemField.value;
              serializedProperty.serializedObject.Update();
              UpdateListViewSize();
            }
          }
          addItemField.value = null;
        }
      }
    }

    private VisualElement GenerateContent()
    {
      content = new VisualElement();
      content.style.marginLeft = 15;
      content.Add(GenerateListView());
      if (addItemField != null)
        content.Add(addItemField);
      return content;
    }

    private VisualElement GenerateListView()
    {
      listView = new ListView()
      {
        viewDataKey = label
      };
      listView.makeItem = () =>
      {
        VisualElement item = new VisualElement();
        item.style.flexDirection = FlexDirection.Row;
        PropertyField propertyField = new PropertyField();
        propertyField.style.flexGrow = 1;
        Button button = new Button
        {
          text = "x"
        };
        item.Add(propertyField);
        item.Add(button);
        return item;
      };
      listView.bindItem = (VisualElement el, int index) =>
      {
        if (!(el is IBindable field))
          field = el.Query().Where(x => x is IBindable).First() as IBindable;

        if (field == null)
          throw new InvalidOperationException("Can't find BindableElement: please provide BindableVisualElements or provide your own Listview.bindItem callback");

        //SerializedProperty itemProp = listView.itemsSource[index] as SerializedProperty;
        SerializedProperty itemProp = serializedProperty.GetArrayElementAtIndex(index);
        field.bindingPath = itemProp.propertyPath;
        field.BindProperty(itemProp);
        el.Q<Button>().clickable.clicked += () => RemoveItem(index);
      };
      listView.itemHeight = itemHeight;
      listView.showBoundCollectionSize = false;
      listView.selectionType = SelectionType.None;
      listView.focusable = false;
      UpdateListViewSize();
      listView.BindProperty(serializedProperty);
      return listView;
    }

    private void RemoveItem(int index)
    {
      if (RemoveItemAction != null)
      {
        RemoveItemAction(index);
        serializedProperty.serializedObject.Update();
        UpdateListViewSize();
      } else
      {
        Undo.RecordObject(serializedProperty.serializedObject.targetObject, "Remove item");
        serializedProperty.DeleteArrayElementAtIndex(index);
        serializedProperty.serializedObject.Update();
        UpdateListViewSize();
      }
    }

    public void UpdateListViewSize()
    {
      int itemsCount = serializedProperty.arraySize;
      listView.style.height = itemHeight * (itemsCount + (listView.showBoundCollectionSize ? 1 : 0));
    }

    private VisualElement GenerateTitle()
    {
      TextElement title = new TextElement()
      {
        text = label
      };
      title.style.unityFontStyleAndWeight = FontStyle.Bold;
      title.style.marginLeft = 3;
      title.style.marginRight = 3;
      return title;
    }
  }
}