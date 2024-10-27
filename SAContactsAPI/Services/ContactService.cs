using Newtonsoft.Json;
using SAContactsAPI.Models;
using System.Xml;

namespace SAContactsAPI.Services
{
    public class ContactService
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "contacts.json");

        public ContactService()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]"); // Initialize empty JSON array if file doesn't exist
            }
        }

        private List<Contact> LoadContacts()
        {
            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Contact>>(jsonData) ?? new List<Contact>();
        }

        private void SaveContacts(List<Contact> contacts)
        {
            var jsonData = JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }

        public List<Contact> GetAll() => LoadContacts();

        public Contact? Get(int id) => LoadContacts().FirstOrDefault(c => c.Id == id);

        public void Add(Contact contact)
        {
            var contacts = LoadContacts();
            contact.Id = contacts.Count > 0 ? contacts.Max(c => c.Id) + 1 : 1;
            contacts.Add(contact);
            SaveContacts(contacts);
        }

        public void Update(int id, Contact contact)
        {
            var contacts = LoadContacts();
            var index = contacts.FindIndex(c => c.Id == id);
            if (index != -1)
            {
                contacts[index] = contact;
                SaveContacts(contacts);
            }
        }

        public void Delete(int id)
        {
            var contacts = LoadContacts();
            contacts.RemoveAll(c => c.Id == id);
            SaveContacts(contacts);
        }
    }
}
