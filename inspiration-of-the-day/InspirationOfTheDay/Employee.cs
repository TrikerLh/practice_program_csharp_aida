namespace InspirationOfTheDay;

public class Employee
{
    private readonly string _name;
    private readonly ContactData _contactData;

    public Employee(string name, ContactData contactData)
    {
        _name = name;
        _contactData = contactData;
    }

    public string Name => _name;
}