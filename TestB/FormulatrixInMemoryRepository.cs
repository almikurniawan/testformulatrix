using System.Text.Json;
using System.Xml.Serialization;
using TestB.Entities;
using TestB.Exception;

namespace TestB
{
    public class FormulatrixInMemoryRepository : IFormulatrixRepository
    {
        private ICollection<Item> _items;
        public void Deregister(string itemName)
        {
            Item? itemToRemove = null;
            foreach (var item in _items)
            {
                if (item.ItemName == itemName)
                {
                    itemToRemove = item;
                }
            }
            if (itemToRemove == null)
            {
                throw new NotFoundException($"Item with name {itemName} not found.");
            }
            _items.Remove(itemToRemove);
        }

        public int GetType(string itemName)
        {
            Item? itemFound = null;
            foreach (var item in _items)
            {
                if (item.ItemName == itemName)
                {
                    itemFound = item;
                }
            }
            if (itemFound == null)
            {
                throw new NotFoundException($"Item with name {itemName} not found.");
            }
            return (int)itemFound.ItemType;
        }

        public void Initialize()
        {
            _items = new List<Item>();
        }

        public void Register<N>(string itemName, N itemContent, int itemType)
        {
            string itemContentString;
            if (itemType == 1)
            {
                itemContentString = JsonSerializer.Serialize(itemContent);
            }
            else
            {
                var stringwriter = new System.IO.StringWriter();
                var serializer = new XmlSerializer(typeof(N));
                serializer.Serialize(stringwriter, itemContent);
                itemContentString = stringwriter.ToString();
            }
            var dto = new Item
            {
                ItemName = itemName,
                ItemContent = itemContentString,
                ItemType = (byte)itemType
            };
            foreach (var item in _items)
            {
                if (item.ItemContent == itemContentString && item.ItemType == itemType)
                {
                    throw new DuplicateException("Duplicate item content and type found.");
                }
            }
            _items.Add(dto);
        }

        public string Retrieve(string itemName)
        {
            Item? itemFound = null;
            foreach (var item in _items)
            {
                if (item.ItemName == itemName)
                {
                    itemFound = item;
                }
            }
            if (itemFound == null)
            {
                throw new NotFoundException($"Item with name {itemName} not found.");
            }
            if (itemFound?.ItemType == 1)
            {
                return JsonSerializer.Serialize(itemFound);
            }
            else
            {
                var stringwriter = new System.IO.StringWriter();
                var serializer = new XmlSerializer(typeof(Item));
                serializer.Serialize(stringwriter, itemFound);
                return stringwriter.ToString();
            }
        }
    }
}
