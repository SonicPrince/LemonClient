using Lemon;

public class UITemplateTable : DataTable<Tables.UITemplate>
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
