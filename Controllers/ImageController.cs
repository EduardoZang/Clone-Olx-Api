using AutoMapper;
using OlxApi.Data;
using OlxApi.Dtos;
using OlxApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace OlxApi.Controllers;

[ApiController]
[Route("[controller]")]

public class ImageController : ControllerBase {

    private OlxContext _context;
    private IMapper _mapper;

    public ImageController(OlxContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    ///<summary>
    ///Adiciona uma imagem ao banco de dados
    ///</summary>
    ///<param name="addImageDto">Objeto com os campos necessários para adição de uma imagem</param>
    ///<returns>IActionResult</returns>
    ///<response code="201">Caso inserção seja feita com sucesso</response>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddImg([FromBody] CreateImageDto addImageDto){

                Image image = _mapper.Map<Image>(addImageDto);

                _context.Images.Add(image);
                _context.SaveChanges();

                return CreatedAtAction(nameof(VisualizaImage),new {id = image._id},image);
            }       

    ///<summary>
    ///Exibe uma imagem salva no banco de dados
    ///</summary>
    ///<returns>IEnumerable</returns>
    ///<response code="200">Caso operação seja feita com sucesso</response>

            [HttpGet]
            public IEnumerable<ReadImageDto> VisualizaImage([FromQuery] int skip = 0,[FromQuery] int take = 50){

               return  _mapper.Map<List<ReadImageDto>>(_context.Images.Skip(skip).Take(take));
            }  

    ///<summary>
    ///Exibe uma imagem específica salvo no banco de dados através do id
    ///</summary>
    ///<returns>IActionResult</returns>
    ///<response code="200">Caso operação seja feita com sucesso</response>

            [HttpGet("{id}")]
            public IActionResult VisualizaImageId(int id){
               var image =  _context.Images.FirstOrDefault(image => image._id == id);
               if(image == null) return NotFound();
               var imageDto = _mapper.Map<ReadImageDto>(image);
                return Ok(imageDto);
            }      

    ///<summary>
    ///Altera todos os elementos de uma imagem no banco de dados
    ///</summary>
    ///<param name="imageDto">Objeto com os campos necessários para alteração de uma imagem</param>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso alteração seja feita com sucesso</response>

            [HttpPut("{id}")]
            public IActionResult AtualizaImage(int id, [FromBody] UptadeImageDto imageDto){
                var image = _context.Images.FirstOrDefault(image => image._id == id);

                if(image == null) return NotFound();
                _mapper.Map(imageDto,image);
                _context.SaveChanges();
                return NoContent();
            }

    ///<summary>
    ///Altera um elemento em específico de uma imagem no banco de dados através do id
    ///</summary>
    ///<param name="patch">Objeto com os campos necessários para alteração específica de uma imagem</param>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso alteração seja feita com sucesso</response>

            [HttpPatch("{id}")]

            public IActionResult AtualizaImageId(int id, JsonPatchDocument<UptadeImageDto>patch){
                var image = _context.Images.FirstOrDefault(images => images._id == id);

                if(image == null) return NotFound();

                var ImageParaAtualizar = _mapper.Map<UptadeImageDto>(image);
                patch.ApplyTo(ImageParaAtualizar,ModelState);

                if(!TryValidateModel(ImageParaAtualizar)){
                    return ValidationProblem(ModelState);
                }

                _mapper.Map(ImageParaAtualizar,image);
                _context.SaveChanges();
                return NoContent();
            }

    ///<summary>
    ///Deleta uma imagem no banco de dados
    ///</summary>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso a exclusão seja feita com sucesso</response>

            [HttpDelete("{id}")]
            public IActionResult DeletaImage(int id){
                var image = _context.Images.FirstOrDefault(image => image._id == id);

                if(image == null) return NotFound();
                _context.Remove(image);
                _context.SaveChanges();
                return NoContent();
            }
}
