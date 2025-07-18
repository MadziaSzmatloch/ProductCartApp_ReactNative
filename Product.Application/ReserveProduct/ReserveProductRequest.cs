﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Product.Application.ReserveProduct
{
    public record ReserveProductRequest(Guid ProductId, int Quantity) : IRequest;
}
