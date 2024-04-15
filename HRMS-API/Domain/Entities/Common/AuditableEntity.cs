namespace Domain.Entities.Common;
public abstract class AuditableEntity<TId>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public AuditableEntity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        LastModified = DateTime.UtcNow;
        Created = DateTime.UtcNow;
    }
    public virtual TId Id { get; protected set; }
    public DateTime? Created { get; set; } = DateTime.UtcNow;
    public DateTime? LastModified { get; set; }
    public DateTime? Deleted { get; set; }
    public string? CreatedBy { get; set; } = string.Empty;
    public string? LastModifiedBy { get; set; }
    public string? DeletedBy { get; set; }

    int? _requestedHashCode;

    public bool IsTransient()
    {
        return Id!.Equals(default(TId));
    }

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public override bool Equals(object obj)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    {
        if (obj == null || obj is not AuditableEntity<TId>)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        var item = (AuditableEntity<TId>)obj;

        if (item.IsTransient() || IsTransient())
            return false;
        else
            return item == this;
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = Id!.GetHashCode() ^ 31;

            return _requestedHashCode.Value;
        }
        else
            return base.GetHashCode();
    }
}