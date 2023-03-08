using AutoMapper;
using OlxApi.Data;
using OlxApi.Dtos;
using OlxApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace OlxApi.Controllers;

[ApiController]
[Route("[controller]")]

public class AdController : ControllerBase {

    private OlxContext _context;
    private IMapper _mapper;

    public AdController(OlxContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    ///<summary>
    ///Adiciona um anúncio ao banco de dados
    ///</summary>
    ///<param name="addAdDto">Objeto com os campos necessários para criação de um anúncio</param>
    ///<returns>IActionResult</returns>
    ///<response code="201">Caso inserção seja feita com sucesso</response>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddAd([FromBody] CreateAdDto addAdDto){

                Ad ad = _mapper.Map<Ad>(addAdDto);

                _context.Ads.Add(ad);
                _context.SaveChanges();

                return CreatedAtAction(nameof(VisualizaAd),new {id = ad._id},ad);
            }       

    ///<summary>
    ///Exibe um anúncio salvo no banco de dados
    ///</summary>
    ///<returns>IEnumerable</returns>
    ///<response code="200">Caso operação seja feita com sucesso</response>

            [HttpGet]
            public IEnumerable<ReadAdDto> VisualizaAd([FromQuery] int skip = 0,[FromQuery] int take = 50){

               return  _mapper.Map<List<ReadAdDto>>(_context.Ads.Skip(skip).Take(take));
            }  

    ///<summary>
    ///Exibe um anúncio específico salvo no banco de dados através do id
    ///</summary>
    ///<returns>IActionResult</returns>
    ///<response code="200">Caso operação seja feita com sucesso</response>

            [HttpGet("{id}")]
            public IActionResult VisualizaAdId(int id){
               var ad =  _context.Ads.FirstOrDefault(ad => ad._id == id);
               if(ad == null) return NotFound();
               var adDto = _mapper.Map<ReadAdDto>(ad);
                return Ok(adDto);
            }      

    ///<summary>
    ///Altera todos os elementos de um anúncio no banco de dados
    ///</summary>
    ///<param name="adDto">Objeto com os campos necessários para alteração de um anúncio</param>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso alteração seja feita com sucesso</response>

            [HttpPut("{id}")]
            public IActionResult AtualizaAd(int id, [FromBody] UptadeAdDto adDto){
                var ad = _context.Ads.FirstOrDefault(ad => ad._id == id);

                if(ad == null) return NotFound();
                _mapper.Map(adDto,ad);
                _context.SaveChanges();
                return NoContent();
            }

    ///<summary>
    ///Altera um elemento em específico de um anúncio no banco de dados através do id
    ///</summary>
    ///<param name="patch">Objeto com os campos necessários para alteração específica de um anúncio</param>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso alteração seja feita com sucesso</response>

            [HttpPatch("{id}")]

            public IActionResult AtualizaAdId(int id, JsonPatchDocument<UptadeAdDto>patch){
                var ad = _context.Ads.FirstOrDefault(ad => ad._id == id);

                if(ad == null) return NotFound();

                var AdParaAtualizar = _mapper.Map<UptadeAdDto>(ad);
                patch.ApplyTo(AdParaAtualizar,ModelState);

                if(!TryValidateModel(AdParaAtualizar)){
                    return ValidationProblem(ModelState);
                }

                _mapper.Map(AdParaAtualizar,ad);
                _context.SaveChanges();
                return NoContent();
            }

    ///<summary>
    ///Deleta um anúncio no banco de dados
    ///</summary>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso a exclusão seja feita com sucesso</response>

            [HttpDelete("{id}")]
            public IActionResult DeletaAd(int id){
                var ad = _context.Ads.FirstOrDefault(ad => ad._id == id);

                if(ad == null) return NotFound();
                _context.Remove(ad);
                _context.SaveChanges();
                return NoContent();
            }
}
