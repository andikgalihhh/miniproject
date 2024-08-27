using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs;

public class TodoDetailDto
{
    public Guid TodoDetailId { get; set; }

    public Guid TodoId { get; set; }
    public string Activity { get; set; }
    public string Category { get; set; }
    public string DetailNote { get; set; }
}
