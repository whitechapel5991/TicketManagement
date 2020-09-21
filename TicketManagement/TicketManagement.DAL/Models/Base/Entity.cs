// ****************************************************************************
// <copyright file="Entity.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System.ComponentModel.DataAnnotations;

namespace TicketManagement.DAL.Models.Base
{
    public abstract class Entity : IEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
    }
}
