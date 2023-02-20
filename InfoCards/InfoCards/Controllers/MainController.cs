using InfoCards.Models;
using InfoCards.Repositories.Implimentations;
using InfoCards.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InfoCards.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        BaseRepository baseRepository = new BaseRepository();

        [HttpGet]
        [Route("GetAll")]
        public IList<InformationCard> GetAll()
        {
            IList<InformationCard> allCards = baseRepository.GetAll();
            return allCards;
        }

        [HttpPost]
        [Route("file/create")]
        public InformationCard Post(ObservableCollection<InformationCard> informationCards)
        {
            InformationCard card = baseRepository.Create(informationCards);
            return card;
        }

        [HttpPut]
        [Route("file/update")]
        public InformationCard Put(InformationCard informationCard)
        {
            baseRepository.Update(informationCard);
            return informationCard;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            baseRepository.Delete(id);
            return NoContent();
        }
    }
}
