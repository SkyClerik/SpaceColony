using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using Gameplay.Data;

namespace Gameplay.Inventory
{
    [RequireComponent(typeof(UIDocument))]
    public sealed class PlayerInventory : MonoBehaviour
    {
        private static StoredItem _currentDraggedItem = null;

        [SerializeField]
        private List<StoredItem> _storedItems = new List<StoredItem>();

        private bool _isInventoryReady;
        private bool _isLoadReady;
        private UIDocument _document;
        private VisualElement _root;
        private VisualElement _screen;
        private VisualElement _inventoryGrid;
        private VisualElement _telegraph;
        private InventoryDimensions _inventoryDimensions;
        private InventoryDimensions _slotDimension;
        private Rect _gridRect;
        private Vector2 _mousePositionNormal;
        private const string _telegraphName = "Telegraph";
        private const string _iconSlotHighlighterName = "slot-icon-highlighted";
        private const string _gridName = "Grid";
        private const string _screenName = "Screen";

        public static StoredItem CurrentDraggedItem { get => _currentDraggedItem; set => _currentDraggedItem = value; }
        public VisualElement Root => _root;
        public InventoryDimensions SlotDimension => _slotDimension;
        public UIDocument Document => _document;
        public List<StoredItem> StoredItems => _storedItems;

        private void Awake()
        {
            Configure();
        }

        private void Start()
        {
            LoadInventory();
            StartingHide();
        }

        private async void Configure()
        {
            _document = GetComponent<UIDocument>();
            _root = _document.rootVisualElement;
            _inventoryGrid = _root.Q<VisualElement>(_gridName);
            _screen = _root.Q<VisualElement>(_screenName);

            PlayerInventoriesContainer.Instance.Inventories.Add(this);

            ConfigureInventoryTelegraph();

            await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);

            ConfigureSlotDimensions();
            ConfigureInventorySize();
            CalculateGridRect();
            VisibleScreen(false);
            _isInventoryReady = true;
        }

        private async void StartingHide()
        {
            await UniTask.WaitUntil(() => _isLoadReady);

            Hide();
            VisibleItems(true);
            VisibleScreen(true);
        }

        private void Hide() => _screen.style.display = DisplayStyle.None;

        private void Show() => _screen.style.display = DisplayStyle.Flex;

        private void ConfigureInventoryTelegraph()
        {
            _telegraph = new VisualElement
            {
                name = _telegraphName,
            };
            _telegraph.AddToClassList(_iconSlotHighlighterName);
            AddItemToInventoryGrid(_telegraph);
        }

        private void ConfigureSlotDimensions()
        {
            VisualElement firstSlot = _inventoryGrid.Children().First();

            _slotDimension = new InventoryDimensions
            {
                Width = Mathf.RoundToInt(firstSlot.worldBound.width),
                Height = Mathf.RoundToInt(firstSlot.worldBound.height)
            };
        }

        private void ConfigureInventorySize()
        {
            var childrens = _inventoryGrid.Children().ToList();

            _inventoryDimensions.Width = 0;
            _inventoryDimensions.Height = 1;

            if (childrens.Count > 0)
            {
                float tempY = childrens[0].worldBound.y;
                bool row = false;
                foreach (VisualElement box in childrens)
                {
                    if (box.worldBound.y > tempY)
                    {
                        row = true;
                        _inventoryDimensions.Height++;
                        tempY = box.worldBound.y;
                    }

                    if (!row)
                        _inventoryDimensions.Width++;
                }
            }
        }

        public void AddItemToInventoryGrid(VisualElement item) => _inventoryGrid.Add(item);
        private void RemoveItemFromInventoryGrid(VisualElement item) => _inventoryGrid.Remove(item);

        private async void LoadInventory()
        {
            await UniTask.WaitUntil(() => _isInventoryReady);

            foreach (StoredItem loadedItem in _storedItems)
            {
                ItemVisual inventoryItemVisual = new ItemVisual(ownerInventory: this, ownerStored: loadedItem);

                AddItemToInventoryGrid(inventoryItemVisual);

                bool inventoryHasSpace = await GetPositionForItem(inventoryItemVisual);

                if (!inventoryHasSpace)
                {
                    Debug.Log("No space - Cannot pick up the item");
                    RemoveItemFromInventoryGrid(inventoryItemVisual);
                    continue;
                }

                ConfigureInventoryItem(loadedItem, inventoryItemVisual);
            }

            _isLoadReady = true;
        }

        private static void ConfigureInventoryItem(StoredItem item, ItemVisual visual) => item.RootVisual = visual;

        private void VisibleItems(bool visible)
        {
            foreach (StoredItem item in _storedItems)
            {
                item.RootVisual.style.visibility = visible ? Visibility.Visible : Visibility.Hidden;
            }
        }

        private void VisibleScreen(bool visible)
        {
            _screen.style.visibility = visible ? Visibility.Visible : Visibility.Hidden;
        }

        private static void SetItemPosition(VisualElement element, Vector2 vector)
        {
            element.style.left = vector.x;
            element.style.top = vector.y;
        }

        private async Task<bool> GetPositionForItem(VisualElement newItem)
        {
            for (int y = 0; y < _inventoryDimensions.Height; y++)
            {
                for (int x = 0; x < _inventoryDimensions.Width; x++)
                {
                    SetItemPosition(newItem, new Vector2(_slotDimension.Width * x, _slotDimension.Height * y));

                    await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);

                    bool parentOverlaping = GridRectOverlap(newItem);
                    StoredItem overlappingItem = _storedItems.FirstOrDefault(s => s.RootVisual != null && s.RootVisual.worldBound.Overlaps(newItem.worldBound));

                    if (overlappingItem == null && parentOverlaping)
                        return true;
                }
            }

            return false;
        }

        public (bool canPlace, Vector2 position) ShowPlacementTarget(ItemVisual draggedItem, out StoredItem overlapItem)
        {
            overlapItem = null;
            bool overlap = GridRectOverlap(draggedItem);
            if (!overlap)
            {
                _telegraph.style.visibility = Visibility.Hidden;
                return (canPlace: false, position: Vector2.zero);
            }

            VisualElement targetSlot = _inventoryGrid.Children()
                .Where(x => x.worldBound.Overlaps(draggedItem.worldBound) && x != draggedItem)
                .OrderBy(x => Vector2.Distance(x.worldBound.position, draggedItem.worldBound.position))
                .First();

            _telegraph.style.width = draggedItem.style.width;
            _telegraph.style.height = draggedItem.style.height;

            SetItemPosition(_telegraph, new Vector2(targetSlot.layout.position.x, targetSlot.layout.position.y));

            _telegraph.style.visibility = Visibility.Visible;

            var overlappingItems = _storedItems
                .Where(x => x.RootVisual != null && x.RootVisual.worldBound
                .Overlaps(_telegraph.worldBound))
                .ToArray();

            if (overlappingItems.Length == 1)
            {
                overlapItem = overlappingItems[0];
            }
            else if (overlappingItems.Length > 1)
            {
                _telegraph.style.visibility = Visibility.Hidden;
                return (canPlace: false, position: Vector2.zero);
            }

            return (canPlace: true, targetSlot.worldBound.position);
        }

        private bool GridRectOverlap(VisualElement item)
        {
            if (item.worldBound.xMin >= _gridRect.xMin && item.worldBound.yMin >= _gridRect.yMin && item.worldBound.xMax <= _gridRect.xMax && item.worldBound.yMax <= _gridRect.yMax)
                return true;

            return false;
        }

        private void CalculateGridRect()
        {
            _gridRect = _inventoryGrid.worldBound;
            _gridRect.width = _slotDimension.Width * _inventoryDimensions.Width;
            _gridRect.height = _slotDimension.Height * _inventoryDimensions.Height;
            _gridRect.x -= (_slotDimension.Width / 2);
            _gridRect.y -= (_slotDimension.Height / 2);
            _gridRect.width += _slotDimension.Width;
            _gridRect.height += _slotDimension.Height;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (_isLoadReady)
                {
                    if (_screen.style.display == DisplayStyle.None)
                    {
                        Show();
                    }
                    else
                    {
                        if (CurrentDraggedItem != null)
                            return;

                        Hide();
                    }
                }
            }

            if (!_document.isActiveAndEnabled)
                return;

            if (CurrentDraggedItem == null || CurrentDraggedItem.RootVisual == null)
                return;

            if (CurrentDraggedItem.OwnerInventory != this)
                return;

            _mousePositionNormal = Input.mousePosition;
            _mousePositionNormal.x = _mousePositionNormal.x - (CurrentDraggedItem.RootVisual.layout.width / 2);
            _mousePositionNormal.y = (Screen.height - _mousePositionNormal.y) - (CurrentDraggedItem.RootVisual.layout.height / 2);
            CurrentDraggedItem.RootVisual.SetPosition(_mousePositionNormal);

            if (Input.GetMouseButtonDown(1))
            {
                RotateItem();
            }
        }

        private void RotateItem() => CurrentDraggedItem.RootVisual.Rotate();
    }
}