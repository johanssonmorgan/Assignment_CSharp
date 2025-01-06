namespace Business.Helpers;

public static class IdGenerator
{
    public static string Generate()
    {
        // Generates a new Guid and in this case I only use the first section of it.
        return Guid.NewGuid().ToString().Split('-')[0];
    }
}
