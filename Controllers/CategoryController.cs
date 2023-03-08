using AutoMapper;
using OlxApi.Data;
using OlxApi.Dtos;
using OlxApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace OlxApi.Controllers;

[ApiController]
[Route("[controller]")]

public class CategoryController : ControllerBase {

    private OlxContext _context;
    private IMapper _mapper;

    public CategoryController(OlxContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    ///<summary>
    ///Adiciona uma categoria ao banco de dados
    ///</summary>
    ///<param name="categoryDto">Objeto com os campos necessários para criação de uma categoria</param>
    ///<returns>IActionResult</returns>
    ///<response code="201">Caso inserção seja feita com sucesso</response>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddCategory([FromBody] CreateCategoryDto categoryDto){

                Category category = _mapper.Map<Category>(categoryDto);

                _context.Categories.Add(category);
                _context.SaveChanges();

                return CreatedAtAction(nameof(VisualizaCategory),new {id = category._id},category);
            }       

    ///<summary>
    ///Exibe uma categoria salva no banco de dados
    ///</summary>
    ///<returns>IEnumerable</returns>
    ///<response code="200">Caso operação seja feita com sucesso</response>

            [HttpGet]
            public IEnumerable<ReadCategoryDto> VisualizaCategory([FromQuery] int skip = 0,[FromQuery] int take = 50){

               return  _mapper.Map<List<ReadCategoryDto>>(_context.Categories.Skip(skip).Take(take));
            }  

    ///<summary>
    ///Exibe uma categoria específica salva no banco de dados através do id
    ///</summary>
    ///<returns>IActionResult</returns>
    ///<response code="200">Caso operação seja feita com sucesso</response>

            [HttpGet("{id}")]
            public IActionResult VisualizaCategoryId(int id){
               var category =  _context.Categories.FirstOrDefault(category => category._id == id);
               if(category == null) return NotFound();
               var categoryDto = _mapper.Map<ReadCategoryDto>(category);
                return Ok(categoryDto);
            }      

    ///<summary>
    ///Altera todos os elementos de uma categoria no banco de dados
    ///</summary>
    ///<param name="categoryDto">Objeto com os campos necessários para alteração de uma categoria</param>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso alteração seja feita com sucesso</response>

            [HttpPut("{id}")]
            public IActionResult AtualizaCategory(int id, [FromBody] UptadeCategoryDto categoryDto){
                var category = _context.Categories.FirstOrDefault(category => category._id == id);

                if(category == null) return NotFound();
                _mapper.Map(categoryDto,category);
                _context.SaveChanges();
                return NoContent();
            }

    ///<summary>
    ///Altera um elemento em específico de uma categoria no banco de dados
    ///</summary>
    ///<param name="patch">Objeto com os campos necessários para alteração de uma categoria</param>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso alteração seja feita com sucesso</response>

            [HttpPatch("{id}")]

            public IActionResult AtualizaCategoryId(int id, JsonPatchDocument<UptadeCategoryDto>patch){
                var category = _context.Categories.FirstOrDefault(category => category._id == id);

                if(category == null) return NotFound();

                var CategoryParaAtualizar = _mapper.Map<UptadeCategoryDto>(category);
                patch.ApplyTo(CategoryParaAtualizar,ModelState);

                if(!TryValidateModel(CategoryParaAtualizar)){
                    return ValidationProblem(ModelState);
                }

                _mapper.Map(CategoryParaAtualizar,category);
                _context.SaveChanges();
                return NoContent();
            }

    ///<summary>
    ///Deleta uma categoria no banco de dados
    ///</summary>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso a exclusão seja feita com sucesso</response>

            [HttpDelete("{id}")]
            public IActionResult DeletaCategory(int id){
                var category = _context.Categories.FirstOrDefault(category => category._id == id);

                if(category == null) return NotFound();
                _context.Remove(category);
                _context.SaveChanges();
                return NoContent();
            }
}
