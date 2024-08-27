using Core.Features.Queries.AddTodo;
using Core.Features.Queries.AddTodoDetail;
using Core.Features.Queries.DeleteTodo;
using Core.Features.Queries.GetAllTodo;
using Core.Features.Queries.UpdateTodo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence.Models;


namespace Application.Controllers;

public class TodoController : BaseController
{
    private readonly IMediator _mediator;

    public TodoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[HttpGet("v1/table/specification/{id}")]
    //public async Task<GetTableSpecificationsResponse> GetTableSpecifications(Guid id)
    //{
    //    var request = new GetTableSpecificationsQuery()
    //    {
    //        TableSpecificationId = id
    //    };
    //    var response = await _mediator.Send(request);
    //    return response;
    //}

    [HttpGet("v1/todo/all")]
    public async Task<ActionResult<List<GetAllTodoResponse>>> GetAllTodo()
    {
        var request = new GetAllTodoQuery();
        var response = await _mediator.Send(request);
        if (response == null)
        {
            return NotFound(); // Kembalikan 404 jika tidak ada data
        }
        return Ok(response);
    }

    [HttpPost("v1/todo/add")]
    public async Task<ActionResult<AddTodoResponse>> AddTodo([FromBody] AddTodoQuery command)
    {
        var response = await _mediator.Send(command);

        if (response == null)
        {
            return BadRequest("Failed to add table specification.");
        }

        return CreatedAtAction(nameof(AddTodo), new { id = response.TodoId }, response);
    }

    [HttpPost("v1/todo/add/detail/add-bulk")]
    public async Task<ActionResult<AddTodoDetailResponse>> AddTodoDetail([FromBody] AddTodoDetailQuery command)
    {
        var response = await _mediator.Send(command);

        if (response == null || !response.TodoDetails.Any())
        {
            return BadRequest("Failed to add table specification.");
        }

        return CreatedAtAction(nameof(AddTodoDetail), new { id = response.TodoDetails.First() }, response);
    }

    [HttpPost("v1/todo/all/update")]
    public async Task<ActionResult<UpdateTodoResponse>> UpdateTodo([FromBody] UpdateTodoQuery command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("v1/table/specification/delete")]
    public async Task<ActionResult<DeleteTodoResponse>> DeleteTableSpecification([FromBody] DeleteTodoQuery command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }



}