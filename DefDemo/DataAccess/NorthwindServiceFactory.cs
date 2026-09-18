using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SvcLayer
{
    public static class NorthwindServiceFactory
    {
        public static INorthwindService Create() {
            return new NorthwindContext();
        }
    }
}
