public class ResourceLogger
{
    public string Source { get; set; }
    public float Amount { get; set; }

    public ResourceLogger(string source, float amount)
    {
        Source = source;
        Amount = amount;
    }
}
