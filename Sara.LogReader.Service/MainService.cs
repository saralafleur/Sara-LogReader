using Sara.LogReader.Model;
using Sara.LogReader.Model.LogReaderNS;

namespace Sara.LogReader.Service
{
    public static class MainService
    {
        public static void LoadDataFile()
        {
            XmlDal.Load();
        }
        public static OptionsCacheData GetOptions()
        {
            return XmlDal.DataModel.Options;
        }
    }
}
