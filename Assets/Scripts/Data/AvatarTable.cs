using Lemon;

public class AvatarTable : LookupDataTable<Tables.Avatar, int>
{
    public static AvatarTable Instance()
    {
        if (_instance == null)
        {
            _instance = new AvatarTable();
        }
        return _instance;
    }

    private static AvatarTable _instance;
}
