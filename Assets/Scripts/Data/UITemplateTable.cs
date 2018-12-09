using Lemon;

public class UITemplateTable : LookupDataTable<Tables.UITemplate, int>
{
    public static UITemplateTable Instance()
    {
        if (_instance == null)
        {
            _instance = new UITemplateTable();
        }
        return _instance;
    }

    private static UITemplateTable _instance;
}
