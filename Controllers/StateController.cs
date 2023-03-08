using AutoMapper;
using OlxApi.Data;
using OlxApi.Dtos;
using OlxApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace OlxApi.Controllers;

[ApiController]
[Route("[controller]")]

public class StateController : ControllerBase {

    private OlxContext _context;
    private IMapper _mapper;

    public StateController(OlxContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    ///<summary>
    ///Adiciona um estado ao banco de dados
    ///</summary>
    ///<param name="stateDto">Objeto com os campos necessários para criação de um estado</param>
    ///<returns>IActionResult</returns>
    ///<response code="201">Caso inserção seja feita com sucesso</response>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddState([FromBody] CreateStateDto stateDto){

                State state = _mapper.Map<State>(stateDto);

                _context.States.Add(state);
                _context.SaveChanges();

                return CreatedAtAction(nameof(VisualizaState),new {id = state._id},state);
            }       

    ///<summary>
    ///Exibe um estado salvo no banco de dados
    ///</summary>
    ///<returns>IEnumerable</returns>
    ///<response code="200">Caso operação seja feita com sucesso</response>

            [HttpGet]
            public IEnumerable<ReadStateDto> VisualizaState([FromQuery] int skip = 0,[FromQuery] int take = 50){

               return  _mapper.Map<List<ReadStateDto>>(_context.States.Skip(skip).Take(take));
            }  

    ///<summary>
    ///Exibe um estado específico salvo no banco de dados através do id
    ///</summary>
    ///<returns>IActionResult</returns>
    ///<response code="200">Caso operação seja feita com sucesso</response>

            [HttpGet("{id}")]
            public IActionResult VisualizaStateId(int id){
               var state =  _context.States.FirstOrDefault(state => state._id == id);
               if(state == null) return NotFound();
               var stateDto = _mapper.Map<ReadStateDto>(state);
                return Ok(stateDto);
            }      

    ///<summary>
    ///Altera todos os elementos de um estado no banco de dados
    ///</summary>
    ///<param name="stateDto">Objeto com os campos necessários para alteração de um estado</param>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso alteração seja feita com sucesso</response>

            [HttpPut("{id}")]
            public IActionResult AtualizaState(int id, [FromBody] UptadeStateDto stateDto){
                var state = _context.States.FirstOrDefault(state => state._id == id);

                if(state == null) return NotFound();
                _mapper.Map(stateDto,state);
                _context.SaveChanges();
                return NoContent();
            }

    ///<summary>
    ///Altera um elemento em específico de um estado no banco de dados
    ///</summary>
    ///<param name="patch">Objeto com os campos necessários para alteração de um estado</param>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso alteração seja feita com sucesso</response>

            [HttpPatch("{id}")]

            public IActionResult AtualizaStateId(int id, JsonPatchDocument<UptadeStateDto>patch){
                var state = _context.States.FirstOrDefault(state => state._id == id);

                if(state == null) return NotFound();

                var StateParaAtualizar = _mapper.Map<UptadeStateDto>(state);
                patch.ApplyTo(StateParaAtualizar,ModelState);

                if(!TryValidateModel(StateParaAtualizar)){
                    return ValidationProblem(ModelState);
                }

                _mapper.Map(StateParaAtualizar,state);
                _context.SaveChanges();
                return NoContent();
            }

    ///<summary>
    ///Deleta um estado no banco de dados
    ///</summary>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso a exclusão seja feita com sucesso</response>

            [HttpDelete("{id}")]
            public IActionResult DeletaState(int id){
                var state = _context.States.FirstOrDefault(state => state._id == id);

                if(state == null) return NotFound();
                _context.Remove(state);
                _context.SaveChanges();
                return NoContent();
            }
}
