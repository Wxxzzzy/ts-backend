namespace TSCore.Application.Common.Models;

public class KeyValuesBase
{
    public int Id { get; set; }
    public string Value { get; set; }

    public KeyValuesBase()
    {
    }

    public KeyValuesBase(int id)
    {
        Id = id;
    }

    public KeyValuesBase(int id, string value)
    {
        Id = id;
        Value = value;
    }
}