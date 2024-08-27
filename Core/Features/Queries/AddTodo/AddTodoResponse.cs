using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.Queries.AddTodo;

public class AddTodoResponse
{
    public Guid TodoId { get; set; }
    public string Day { get; set; }
    public DateTime TodayDate { get; set; }
    public string Note { get; set; }
    public int DetailCount { get; set; }
}
