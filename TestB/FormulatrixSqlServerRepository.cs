using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Xml.Serialization;
using TestB.Entities;
using TestB.Exception;

namespace TestB
{
    public class FormulatrixSqlServerRepository : IFormulatrixRepository
    {
        private readonly FormulaTrixSqlServerDbContext _dbContext;

        public FormulatrixSqlServerRepository(string connectionString)
        {
            _dbContext = new FormulaTrixSqlServerDbContext();
            _dbContext.Database.SetConnectionString(connectionString);
        }
        public void Deregister(string itemName)
        {
            var itemFound = _dbContext.Items.First(i => i.ItemName == itemName);
            if (itemFound == null)
            {
                throw new NotFoundException($"Item with name {itemName} not found.");
            }
            _dbContext.Items.Remove(itemFound);
            _dbContext.SaveChanges();
        }

        public int GetType(string itemName)
        {
            var itemFound = _dbContext.Items.First(i => i.ItemName == itemName);
            if (itemFound == null)
            {
                throw new NotFoundException($"Item with name {itemName} not found.");
            }
            return (int)itemFound.ItemType;
        }

        public void Initialize()
        {
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
            var checkDuplicate = _dbContext.Items.FirstOrDefault(i => i.ItemContent == itemContentString && i.ItemType == itemType);
            if(checkDuplicate != null)
            {
                throw new DuplicateException("Duplicate item content and type found.");
            }
            var dto = new Item
            {
                ItemName = itemName,
                ItemContent = itemContentString,
                ItemType = (byte)itemType
            };
            _dbContext.Add(dto);
            _dbContext.SaveChanges();
        }

        public string Retrieve(string itemName)
        {
            var itemFound = _dbContext.Items.First(i => i.ItemName == itemName);
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
