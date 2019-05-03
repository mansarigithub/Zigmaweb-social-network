using ZigmaWeb.DAL;

namespace ZigmaWeb.Bussines
{
    public abstract class BaseManager
    {
        public BaseManager()
        {

        }

        public static Repository Repository
        {
            get
            {
                return Repository.Instance;
            }
        }

        public static void SaveChanges()
        {
            Repository.Instance.SaveChanges();
        }
    }
}
