﻿namespace tickets.api.Models
{
    public class FiltroGlobal
    {
        public Guid? Id { get; set; }
        public bool IncluirInactivos { get; set; } = false;
        
    }
}
