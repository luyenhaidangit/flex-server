﻿using Flex.Core.Shared.Abstracts;

namespace Flex.Core.Domain.System
{
    public class Department : EntityBase
    {
        public string Name { get; set; }

        public int Priority { get; set; }

        public int Status { get; set; }

        public string? Description { get; set; }
    }
}