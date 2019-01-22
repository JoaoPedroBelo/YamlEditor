namespace Data_Model
{
        public static class MyYamlFileFactory
        {
            public static MyYamlFile CreateMyYamlFile(string path)
            {
                return new MyYamlFile(path);
            }           
        }
    }
