using AutoMapper;
using OlxApi.Data;
using OlxApi.Dtos;
using OlxApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace OlxApi.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase {

    private OlxContext _context;
    private IMapper _mapper;

    public UserController(OlxContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    ///<summary>
    ///Adiciona um usuário ao banco de dados
    ///</summary>
    ///<param name="userDto">Objeto com os campos necessários para criação de um usuário</param>
    ///<returns>IActionResult</returns>
    ///<response code="201">Caso inserção seja feita com sucesso</response>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddUser([FromBody] CreateUserDto userDto){

                User user = _mapper.Map<User>(userDto);

                _context.Users.Add(user);
                _context.SaveChanges();

                return CreatedAtAction(nameof(VisualizaUser),new {id = user._id},user);
            }       

    ///<summary>
    ///Exibe um usuário salvo no banco de dados
    ///</summary>
    ///<returns>IEnumerable</returns>
    ///<response code="200">Caso operação seja feita com sucesso</response>

            [HttpGet]
            public IEnumerable<ReadUserDto> VisualizaUser([FromQuery] int skip = 0,[FromQuery] int take = 50){

               return  _mapper.Map<List<ReadUserDto>>(_context.Users.Skip(skip).Take(take));
            }  

    ///<summary>
    ///Exibe um usuário específico salvo no banco de dados através do id
    ///</summary>
    ///<returns>IActionResult</returns>
    ///<response code="200">Caso operação seja feita com sucesso</response>

            [HttpGet("{id}")]
            public IActionResult VisualizaUserId(int id){
               var user =  _context.Users.FirstOrDefault(user => user._id == id);
               if(user == null) return NotFound();
               var userDto = _mapper.Map<ReadUserDto>(user);
                return Ok(userDto);
            }      

    ///<summary>
    ///Altera todos os elementos de um usuário no banco de dados
    ///</summary>
    ///<param name="userDto">Objeto com os campos necessários para alteração de um usuário</param>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso alteração seja feita com sucesso</response>

            [HttpPut("{id}")]
            public IActionResult AtualizaUser(int id, [FromBody] UptadeUserDto userDto){
                var user = _context.Users.FirstOrDefault(user => user._id == id);

                if(user == null) return NotFound();
                _mapper.Map(userDto,user);
                _context.SaveChanges();
                return NoContent();
            }

    ///<summary>
    ///Altera um elemento em específico de um usuário no banco de dados
    ///</summary>
    ///<param name="patch">Objeto com os campos necessários para alteração de um usuário</param>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso alteração seja feita com sucesso</response>

            [HttpPatch("{id}")]

            public IActionResult AtualizaUserId(int id, JsonPatchDocument<UptadeUserDto>patch){
                var user = _context.Users.FirstOrDefault(user => user._id == id);

                if(user == null) return NotFound();

                var UserParaAtualizar = _mapper.Map<UptadeUserDto>(user);
                patch.ApplyTo(UserParaAtualizar,ModelState);

                if(!TryValidateModel(UserParaAtualizar)){
                    return ValidationProblem(ModelState);
                }

                _mapper.Map(UserParaAtualizar,user);
                _context.SaveChanges();
                return NoContent();
            }

    ///<summary>
    ///Deleta um usuário no banco de dados
    ///</summary>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso a exclusão seja feita com sucesso</response>

            [HttpDelete("{id}")]
            public IActionResult DeletaUser(int id){
                var user = _context.Users.FirstOrDefault(user => user._id == id);

                if(user == null) return NotFound();
                _context.Remove(user);
                _context.SaveChanges();
                return NoContent();
            }
}
