public class DefaultKey : Key
{
    private void Start()
    {
        OnKeyPressed += DestroyKey;
    }

    private void OnDisable()
    {
        OnKeyPressed -= DestroyKey;
    }
}
