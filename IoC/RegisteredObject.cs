﻿using System;

namespace IoC
{
    class RegisteredObject
    {
        public Type TypeToResolve { get; set; }
        public Type TargetType { get; set; }
        public bool IsSingleton { get; set; }
        public object SingletonInstance { get; set; }
    }
}
