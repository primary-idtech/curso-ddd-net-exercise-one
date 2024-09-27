using ROFE.Domain.Abstractions;
using System;

namespace ROFE.Domain.Models.Operation;

public class TypeOperation : Entity
{
    public string Name { get; private set; }

    public static readonly TypeOperation Monetary = new(1, nameof(Monetary));
    public static readonly TypeOperation Stock = new(2, nameof(Stock));

    private TypeOperation(int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }

    public static TypeOperation From(int id)
    {
        return id switch
        {
            1 => Monetary,
            2 => Stock,
            _ => throw new ArgumentOutOfRangeException(nameof(id))
        };
    }
}
