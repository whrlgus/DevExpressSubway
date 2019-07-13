namespace Subway.Data
{
    public class DataRepository
    {
        static DataRepository()
        {
            Daejeon = new DaejeonData();
        }
        public static DaejeonData Daejeon { get; }

    }

}
