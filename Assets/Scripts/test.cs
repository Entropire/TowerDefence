using UnityEngine;

namespace Assets.Scripts
{
    internal class test : MonoBehaviour
    {
        public GameObject testObj;

        private void Start()
        {
            CustomObject obj = new CustomObject("hello", "hello world", "object");

            FileHandler fileHandler = new FileHandler();
            fileHandler.SaveDataToFile(obj, System.IO.Path.Combine(Application.dataPath, "SaveImages"), "test");

            fileHandler.lo
        }
    }

    internal class CustomObject
    { 
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }

        public CustomObject(string name, string description, string type)
        {
            this.name = name;   
            this.description = description;
            this.type = type;
        }
    }

}
