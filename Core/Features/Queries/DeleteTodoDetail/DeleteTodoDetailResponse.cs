using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.Queries.DeleteTodoDetail;

public class DeleteTodoDetailResponse
{
    public Guid TodoDetailId { get; set; }
    public Todo TodoId { get; set; }
    public string Activity { get; set; }
    public string Category { get; set; }
    public string DetailCount { get; set; }
}
