﻿namespace Domain.Common;

public abstract class BaseAuditableEntity<T> : BaseEntity<T>
{
    public DateTimeOffset CreatedOn { get; set; }

    public string CreatedBy { get; set; }

    public DateTimeOffset LastModifiedOn { get; set; }

    public string LastModifiedBy { get; set; }
}